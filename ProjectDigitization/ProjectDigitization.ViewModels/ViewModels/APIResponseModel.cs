using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.ViewModels.ViewModels
{
    public class APIResponseModel<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object Reponse { get; set; } = new object();
        public string ErrorMessage { get; set; }
    }
    public class  APIResponseViewModel
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public object Reponse { get; set; } = new object();
        public int StatusCode { get; set; }
    }
}
