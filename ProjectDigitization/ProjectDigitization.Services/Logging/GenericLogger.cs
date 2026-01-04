using Microsoft.Extensions.Logging;
using ProjectDigitization.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Services.Logging
{
    public class GenericLogger<T>: IGenericLogger<T>
    {
        private readonly ILogger<T> _logger;
        public GenericLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogTrace(string message, params object[] args) => _logger.LogTrace(message, args);
        public void LogDebug(string message, params object[] args) => _logger.LogDebug(message, args);
        public void LogInformation(string message, params object[] args) => _logger.LogInformation(message, args);
        public void LogWarning(string message, params object[] args) => _logger.LogWarning(message, args);
        public void LogError(Exception? exception, string message, params object[] args)
        {
            if (exception is null)
                _logger.LogError(message, args);
            else
                _logger.LogError(exception, message, args);
        }
    }
}
