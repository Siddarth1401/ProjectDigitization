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
        #endregion
        public ProductServices(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            IEnumerable<Products> products = new List<Products>();
            try
            {
                products = await _productsRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
            }
            return products;
        }
    }
}
