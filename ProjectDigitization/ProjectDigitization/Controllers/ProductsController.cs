using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDigitization.Interfaces.Services;

namespace ProjectDigitization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Public variables
        private readonly IProductServices _productServices;
        #endregion

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #region public methods
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            object? response = null;
            try
            {
                response = await _productServices.GetAllProductsAsync();
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return Ok(response);
        }
        #endregion
    }
}
