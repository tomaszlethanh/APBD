using APBD_4.Models;
using APBD_4.Services;
using Microsoft.AspNetCore.Mvc;


namespace APBD_4.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {

        private IDatabaseService _dbService;

        public AnimalsController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            if (orderBy == null)
                orderBy = "name";
            return Ok(_dbService.GetAnimals(orderBy));
        }

        [HttpPost]
        public IActionResult CreateAnimal(Animal newAnimal)
        {
            return Ok(_dbService.CreateAnimal(newAnimal));
        }



        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, Animal newAnimal)
        {
            return Ok(_dbService.UpdateAnimal(idAnimal, newAnimal));
        }

        
        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            return Ok(_dbService.DeleteAnimal(idAnimal));
        }
    }
}
