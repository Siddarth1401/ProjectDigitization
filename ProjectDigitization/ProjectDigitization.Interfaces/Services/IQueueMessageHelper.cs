using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Interfaces.Services
{
    public interface IQueueMessageHelper
    {
        Task<bool> SendQueueMessage(string message, string exchange);
    }
}
