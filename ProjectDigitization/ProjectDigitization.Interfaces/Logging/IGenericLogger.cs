using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Interfaces.Logging
{
    public interface IGenericLogger<T>
    {
        void LogTrace(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception? exception, string message, params object[] args);
    }
}
