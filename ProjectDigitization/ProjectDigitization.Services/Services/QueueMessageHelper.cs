using Microsoft.Extensions.Options;
using ProjectDigitization.Interfaces.Services;
using ProjectDigitization.ViewModels.ViewModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Services.Services
{
    public class QueueMessageHelper:IQueueMessageHelper
    {
        public static string UName = "guest";
        public static string PWD = "guest";
        public static string HName = "localhost";
        public readonly Appsettings _appsettings;

        public QueueMessageHelper(Appsettings appsettings)
        {
            _appsettings = appsettings;
        }
        public async Task<bool> SendQueueMessage(string message, string exchange)
        {
            bool IsTriggered = false;
            try
            {
                var qs = _appsettings.QueueSettings;
                if (qs == null)
                    throw new InvalidOperationException("QueueSettings not configured. Bind Appsettings in Program.cs (Configuration.GetSection(\"Appsettings\")).");

                var factory = new ConnectionFactory()
                {
                    HostName = _appsettings.QueueSettings.Host,
                    UserName = _appsettings.QueueSettings.UserName,
                    Password = _appsettings.QueueSettings.Password,
                    VirtualHost = _appsettings.QueueSettings.VirtualHost,
                    Port = _appsettings.QueueSettings.Port,
                    Ssl = new SslOption()
                    {
                        Enabled = true,
                        ServerName = _appsettings.QueueSettings.Host
                    }
                };
                using (var connection = await factory.CreateConnectionAsync())
                {
                    using (var model = await connection.CreateChannelAsync())
                    {
                        var properties = new BasicProperties();
                        properties.Persistent = true;
                        byte[] messagebuffer = Encoding.Default.GetBytes(message);
                        await model.BasicPublishAsync(exchange, _appsettings.QueueSettings.Key, true, properties, messagebuffer);
                        IsTriggered = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return IsTriggered;
        }
    }
}
