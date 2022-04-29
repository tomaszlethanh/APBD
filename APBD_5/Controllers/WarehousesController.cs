using APBD_5.Models;
using APBD_5.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APBD_5.Controllers
{
    [Route("api")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {

        private IDatabaseService _dbService;

        public WarehousesController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("warehouses")]
        public async Task<IActionResult> RegisterProductAsync(ProductWarehouse productWarehouse)
        {
            var ans = await _dbService.RegisterProductAsync(productWarehouse);
            if (ans == -1)
                return NotFound("Product with IdNumber not found");
            else if (ans == -2)
                return NotFound("Warehouse with IdWarehouse not found");
            else if (ans == -3)
                return NotFound("No order to fulfill");
            return Ok(ans);
        }

        [HttpPost("warehouses2")]
        public async Task<IActionResult> RegisterProductAsync2(ProductWarehouse productWarehouse)
        {
            return Ok(await _dbService.RegisterProductByProcedureAsync(productWarehouse));
        }

    }
}
