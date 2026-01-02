using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDigitization.Interfaces.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();
    }
}
