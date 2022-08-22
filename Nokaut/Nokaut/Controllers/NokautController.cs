using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nokaut.Models;
using Nokaut.Service;

namespace Nokaut.Controllers
{
    [Route("api")]
    [ApiController]
    public class NokautController : ControllerBase
    {
        private readonly IProductListScrapper _productListScrapper;
        private readonly IOptions<NokautConfig> _nokautConfig;

        public NokautController(IProductListScrapper productListScrapper, IOptions<NokautConfig> nokautConfig)
        {

            _productListScrapper = productListScrapper;
            _nokautConfig = nokautConfig;
        }

        [HttpGet]
        public async Task <ActionResult> Get([FromQuery] string input)
        {
          
            var result = await _productListScrapper.GetProducts(input);
            return Ok(result);
    }
    }

    

}