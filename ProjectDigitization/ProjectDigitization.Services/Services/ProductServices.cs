using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Interfaces.Repository;
using ProjectDigitization.Interfaces.Services;
using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Services.Services
{
    public class ProductServices : IProductServices
    {
        #region Public variables
        private readonly IProductsRepository _productsRepository;
        private readonly IGenericLogger<ProductServices> _logger;
        #endregion
        public ProductServices(IProductsRepository productsRepository, IGenericLogger<ProductServices> logger)
        {
            _productsRepository = productsRepository;
            _logger = logger;
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
    }
}
