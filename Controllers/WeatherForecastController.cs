using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace NFTDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        //string devConnString = "Data Source=BEL-G09S-DEV;Initial Catalog=TAG;User ID=sa;Password=NCVMAag+^8ZrgGgt^jpdwB8rQZ5%eF;Integrated Security=False";
        string stagingConnString = ConfigurationManager.ConnectionStrings["AzureSqlConnString"].ConnectionString;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string queryString = "SELECT Full_Name, Email FROM USERS";
            List<String> lastNames = new List<string>();
            using (SqlConnection conn = new SqlConnection(stagingConnString))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            lastNames.Add(reader["Full_Name"].ToString());
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            var rng = new Random();
            return Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = lastNames[index]
            })
            .ToArray();
        }
    }
}
