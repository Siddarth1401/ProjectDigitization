using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Engine.Interfaces
{
    public interface IGenerator
    {
        public Task ProcessAction(string requestDetails);
    }
}
