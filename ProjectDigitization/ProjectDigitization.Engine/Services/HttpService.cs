using Newtonsoft.Json;
using ProjectDigitization.Engine.Interfaces;
using ProjectDigitization.Engine.Models;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ProjectDigitization.Engine.Services
{
    public class HttpService : IHttpService
    {
        #region Private Variables
        private readonly AppsettingsForEngine _configuration;
        private readonly IGenericLogger<HttpService> _logger;
        private readonly string _accessToken = string.Empty;
        #endregion
        public HttpService(AppsettingsForEngine configuration, IGenericLogger<HttpService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<APIResponseModel<T>> DoAPICall<T>(HttpMethod method, string apiURLPath, string APIUrl, object Request = null)
        {
            APIResponseModel<T> responseModel = new APIResponseModel<T>();
            try
            {
                if (!string.IsNullOrEmpty(apiURLPath))
                {
                    using(var client = new HttpClient()) 
                    {
                        client.BaseAddress = new Uri(string.Join("", apiURLPath, APIUrl));
                        _logger.LogInformation("ProjectDigitizationEngineLogs DoAPICall() ", client.BaseAddress.ToString());
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //client.Timeout = Timeout.InfiniteTimeSpan;
                        HttpResponseMessage response = new HttpResponseMessage();
                        if (method.Equals(HttpMethod.Get))
                        {
                            response = await client.GetAsync(client.BaseAddress);
                        }
                        else if (method.Equals(HttpMethod.Post))
                        {
                            var jsonRequest = JsonConvert.SerializeObject(Request);
                            response = await client.PostAsJsonAsync(client.BaseAddress, jsonRequest);
                        }
                        responseModel.IsSuccess = response.IsSuccessStatusCode;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(responseString))
                            {
                                responseModel.Reponse = JsonConvert.DeserializeObject<object>(responseString);
                            }
                        }
                        else
                        {
                            responseModel.ErrorMessage = "StatusCode: " + response.StatusCode.ToString();
                            _logger.LogInformation("ProjectDigitizationEngineLogs DoAPICall() Errormessage: ", responseModel.ErrorMessage);
                            _logger.LogInformation("ProjectDigitizationEngineLogs DoAPICall() URI Path:", string.Join("", apiURLPath, APIUrl));
                            _logger.LogInformation("ProjectDigitizationEngineLogs DoAPICall() Request:", Request?.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing DoAPI Call.");
                responseModel.IsSuccess = false;
                responseModel.ErrorMessage = ex.Message;
            }
            return responseModel;
        }
    }
}
