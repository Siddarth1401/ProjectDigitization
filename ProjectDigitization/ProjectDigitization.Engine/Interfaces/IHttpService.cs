using ProjectDigitization.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Engine.Interfaces
{
    public interface IHttpService
    {
        Task<APIResponseModel<T>> DoAPICall<T>(HttpMethod method, string apiURLPath, string APIURL, object Request = null);
    }
}
