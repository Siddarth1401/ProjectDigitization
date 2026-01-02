using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Interfaces.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();
    }
}
