using ProjectDigitization.DataAccess.Context;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Interfaces.Repository;
using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.DataAccess.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        #region Public variables
        private readonly DatabaseContext _dbContext;
        private readonly IGenericLogger<ProductsRepository> _logger;
        #endregion
        public ProductsRepository(DatabaseContext dbContext, IGenericLogger<ProductsRepository> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            _logger.LogInformation("GetAllProductsAsync repository call started");
            IEnumerable<Products> products = new List<Products>();
            try
            {
                products = _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllProductsAsync repository");
                Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
            }
            _logger.LogInformation("GetAllProductsAsync repository call ended");
            return products;
        }
    }
}
