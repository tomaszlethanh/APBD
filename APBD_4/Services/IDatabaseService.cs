using APBD_4.Models;
using System.Collections.Generic;


namespace APBD_4.Services
{
    public interface IDatabaseService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        string CreateAnimal(Animal newAnimal);
        string UpdateAnimal(int idAnimal, Animal newAnimal);
        string DeleteAnimal(int idAnimal);
    }
}
