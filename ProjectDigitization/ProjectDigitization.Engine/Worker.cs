using System;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using ProjectDigitization.ViewModels.ViewModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectDigitization.Engine.Models;
using ProjectDigitization.Engine.Interfaces;

namespace ProjectDigitization.Engine
{
    public class Worker : BackgroundService
    {
        #region Private variables
        private readonly ILogger<Worker> _logger;
        private readonly AppsettingsForEngine _settings;
        private readonly IGenerator _generator;
        private IConnection _connection;
        private IChannel? _channel;
        #endregion
        public Worker(ILogger<Worker> logger, IOptions<AppsettingsForEngine> options, IConfiguration configuration, IGenerator generator)
        {
            _logger = logger;
            _settings = options.Value;
            _generator = generator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                if(await InitateRMQ())
                {
                    _logger.LogInformation("RMQ Connection initiated successfully");
                    // Engine logic to consume messages from the queue would go here
                    string queueName = _settings.QueueSettingsForEngine.Queue;
                    System.Console.WriteLine($"Listening to queue: {queueName}");
                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    consumer.ReceivedAsync += async(sender, e) =>
                    {
                        // message processing logic
                        string requestDetails = Encoding.UTF8.GetString(e.Body.ToArray());
                        System.Console.WriteLine($"Received message: {requestDetails}");
                        System.Console.WriteLine(string.Concat("Message received from the exchange ", e.Exchange));
                        System.Console.WriteLine(string.Concat("Message received Time ", System.DateTime.Now.ToString()));
                        System.Console.WriteLine(string.Concat("Consumer tag: ", e.ConsumerTag));
                        System.Console.WriteLine(string.Concat("Delivery tag: ", e.DeliveryTag.ToString()));
                        System.Console.WriteLine(string.Concat("Routing tag: ", e.RoutingKey));
                        await _generator.ProcessAction(requestDetails);
                        await _channel.BasicAckAsync(e.DeliveryTag, false);
                        System.Console.WriteLine("Message processed and acknowledged");
                    };
                    await _channel.BasicQosAsync(0, 1, false);
                    await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);
                    System.Console.WriteLine("Consumer registered and listening for messages...");
                }
                else
                {
                    _logger.LogWarning("RMQ Connection initiation failed");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Worker ExecuteAsync");
            }
            finally
            {
                _logger.LogInformation("Worker ended at: {time}", DateTimeOffset.Now);
            }
        }
        private async Task<bool> InitateRMQ()
        {
            bool isInitiated = false;
            try
            {
                string rmqHost = _settings.QueueSettingsForEngine.Host?.Trim() ?? string.Empty;
                string rmqUserName = _settings.QueueSettingsForEngine.UserName?.Trim() ?? string.Empty;
                string rmqPassword = _settings.QueueSettingsForEngine.Password ?? string.Empty;
                int rmqPort = _settings.QueueSettingsForEngine.Port;
                string rmqVirtualHost = _settings.QueueSettingsForEngine.VirtualHost?.Trim() ?? string.Empty;


                // Support common misconfiguration where user provided "user:virtualhost" in UserName
                if (!string.IsNullOrEmpty(rmqUserName) && rmqUserName.Contains(':') && string.IsNullOrEmpty(rmqVirtualHost))
                {
                    var parts = rmqUserName.Split(':', 2);
                    rmqUserName = parts[0];
                    rmqVirtualHost = parts.Length > 1 ? parts[1] : rmqVirtualHost;
                    _logger.LogWarning("Detected ':' in QueueSettings.UserName. Parsed UserName='{UserName}', VirtualHost='{VirtualHost}'", rmqUserName, rmqVirtualHost);
                }

                // Validate essential settings early and fail fast with clear log
                if (string.IsNullOrEmpty(rmqHost) || string.IsNullOrEmpty(rmqUserName) || string.IsNullOrEmpty(rmqPassword) || string.IsNullOrEmpty(rmqVirtualHost))
                {
                    _logger.LogError("Invalid QueueSettings for RMQ. Host:'{Host}', UserName:'{UserName}', VirtualHost:'{VirtualHost}'", rmqHost, rmqUserName, rmqVirtualHost);
                    return false;
                }

                var factory = new ConnectionFactory()
                {
                    HostName = rmqHost,
                    UserName = rmqUserName,
                    Password = rmqPassword,
                    Port = rmqPort,
                    VirtualHost = rmqVirtualHost,
                    Ssl = new SslOption
                    {
                        Enabled = _settings.QueueSettingsForEngine.UseSsl,
                        ServerName = rmqHost
                    },
                };
                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while initiating RMQ connection");
            }
            finally
            {
                isInitiated = _connection != null && _channel != null;
            }
            return isInitiated;
        }
    }
}
