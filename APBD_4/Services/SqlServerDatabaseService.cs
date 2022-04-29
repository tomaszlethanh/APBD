using APBD_4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace APBD_4.Services
{
    
    public class SqlServerDatabaseService : IDatabaseService
    {

        private IConfiguration _configuration;

        public SqlServerDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var res = new List<Animal>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Animal ORDER BY CASE @order WHEN 'name' THEN name " +
                    "WHEN 'description' THEN description WHEN 'category' THEN category WHEN 'area' THEN area END;";

                com.Parameters.AddWithValue("@order", orderBy);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    res.Add(new Animal
                    {
                        IdAnimal = dr["IdAnimal"].ToString(),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });
                }
                return res;
            }
        }

        public string CreateAnimal(Animal newAnimal)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES(@name, @description, @category, @area)";

                com.Parameters.AddWithValue("@name", newAnimal.Name);
                com.Parameters.AddWithValue("@description", newAnimal.Description);
                com.Parameters.AddWithValue("@category", newAnimal.Category);
                com.Parameters.AddWithValue("@area", newAnimal.Area);
                


                con.Open();
                res = com.ExecuteNonQuery();
            }
            return "Number of rows affected: " + res;
        }

        public string UpdateAnimal(int idAnimal, Animal newAnimal)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @Area WHERE idAnimal = @idAnimal";

                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                com.Parameters.AddWithValue("@name", newAnimal.Name);
                com.Parameters.AddWithValue("@description", newAnimal.Description);
                com.Parameters.AddWithValue("@category", newAnimal.Category);
                com.Parameters.AddWithValue("@area", newAnimal.Area);

                con.Open();
                res = com.ExecuteNonQuery();
            }
            return "Number of rows affected: " + res;
        }

        public string DeleteAnimal(int idAnimal)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM Animal WHERE idAnimal = @idAnimal";

                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                

                con.Open();
                res = com.ExecuteNonQuery();
            }
            return "Number of rows affected: " + res;
        }
    }

   
}
