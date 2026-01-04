using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Interfaces.Services;

namespace ProjectDigitization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Public variables
        private readonly IProductServices _productServices;
        private readonly IGenericLogger<ProductsController> _logger;
        #endregion

        public ProductsController(IProductServices productServices, IGenericLogger<ProductsController> genericLogger)
        {
            _productServices = productServices;
            _logger = genericLogger;
        }

        #region public methods
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            object? response = null;
            _logger.LogInformation("GetAllProducts API Controller call started");
            try
            {
                response = await _productServices.GetAllProductsAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred in GetAllProducts API Controller");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            _logger.LogInformation("GetAllProducts API Controller call ended");
            return Ok(response);
        }
        #endregion
    }
}
