using ProjectDigitization.DataAccess.Context;
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
        #endregion
        public ProductsRepository(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            IEnumerable<Products> products = new List<Products>();
            try
            {
                products = _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
            }
            return products;
        }
    }
}
