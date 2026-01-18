using Microsoft.Extensions.Options;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Interfaces.Repository;
using ProjectDigitization.Interfaces.Services;
using ProjectDigitization.ViewModels.ViewModels;
using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ProjectDigitization.Services.Services
{
    public class ProductServices : IProductServices
    {
        #region Public variables
        private readonly IProductsRepository _productsRepository;
        private readonly IGenericLogger<ProductServices> _logger;
        private readonly IOptions<Appsettings> _appsettings;
        #endregion
        public ProductServices(IProductsRepository productsRepository, IGenericLogger<ProductServices> logger, IOptions<Appsettings> appsettings)
        {
            _productsRepository = productsRepository;
            _logger = logger;
            _appsettings = appsettings;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            _logger.LogInformation("GetAllProductsAsync service call started");
            IEnumerable<Products> products = new List<Products>();
            try
            {
                products = await _productsRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllProductsAsync service");
                Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
            }
            _logger.LogInformation("GetAllProductsAsync service call ended");
            return products;
        }
        public async Task<bool> GetAllProductsAsyncForEngine()
        {
            bool isQueueMessageSent = false;
            try
            {
                EngineQueueModel engineQueueModel = new EngineQueueModel()
                {
                    Event = "GetAllProducts"
                };
                QueueMessageHelper messageHelper = new QueueMessageHelper(_appsettings.Value);
                await messageHelper.SendQueueMessage(JsonSerializer.Serialize(messageHelper),"projectdigitization");
                isQueueMessageSent = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllProductsAsyncForEngine service");
            }
            return isQueueMessageSent;
        }
    }
}
