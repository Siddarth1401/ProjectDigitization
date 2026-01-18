using Newtonsoft.Json;
using ProjectDigitization.Engine.Interfaces;
using ProjectDigitization.Engine.Models;
using ProjectDigitization.ViewModels.ViewModels;
using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Engine.Services
{
    public class Generator : IGenerator
    {
        #region public variables
        private readonly ILogger<Generator> _logger;
        private readonly AppsettingsForEngine _appsettingsForEngine;
        private readonly IHttpService _httpService;
        #endregion
        public Generator(ILogger<Generator> logger, AppsettingsForEngine appsettingsForEngine, IHttpService httpService)
        {
            _logger = logger;
            _appsettingsForEngine = appsettingsForEngine;
            _httpService = httpService;
        }
        public async Task ProcessAction(string requestDetails)
        {
            // Implementation for processing the action based on requestDetails
            // await Task.CompletedTask;
            bool isSuccess = false;
            isSuccess = await EngineMethods();
        }
        private async Task<bool> EngineMethods()
        {
            // Here I am calling method on my consumer ProjectDigitization
            IEnumerable<Products> products = new List<Products>();
            try
            {
                _logger.LogInformation("EngineMethods started");
                string getEngineRequest = _appsettingsForEngine.apiPaths.ProjectDigitizationEndpoint;
                var engineResponse = await _httpService.DoAPICall<string>(HttpMethod.Get, _appsettingsForEngine.apiPaths.ProjectDigitizationURL, getEngineRequest);
                APIResponseViewModel APIReponse = JsonConvert.DeserializeObject<APIResponseViewModel>(engineResponse.Reponse.ToString());
                if (engineResponse.IsSuccess)
                {
                    List<Products> productsList = JsonConvert.DeserializeObject<List<Products>>(APIReponse.Reponse.ToString());
                    var product = engineResponse.Reponse;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in EngineMethods");
            }
            return true;
        }
    }
}
