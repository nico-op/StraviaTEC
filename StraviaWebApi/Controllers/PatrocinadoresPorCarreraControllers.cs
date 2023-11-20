using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraviaWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StraviaTEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatrocinadoresPorCarreraController : ControllerBase
    {
        private readonly string connectionString;

        public PatrocinadoresPorCarreraController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetPatrocinadoresPorCarreras()
        {
            List<PatrocinadoresPorCarrera> patrocinadoresPorCarreras = new List<PatrocinadoresPorCarrera>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PatrocinadoresPorCarrera patrocinadorPorCarrera = new PatrocinadoresPorCarrera
                        {
                            NombreCarrera = reader["NombreCarrera"].ToString(),
                            NombreComercial = reader["NombreComercial"].ToString(),
                        };

                        patrocinadoresPorCarreras.Add(patrocinadorPorCarrera);
                    }
                }
            }

            return Ok(patrocinadoresPorCarreras);
        }

        [HttpGet("{nombreCarrera}/{nombreComercial}")]
        public IActionResult GetPatrocinadorPorCarrera(string nombreCarrera, string nombreComercial)
        {
            PatrocinadoresPorCarrera patrocinadorPorCarrera = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patrocinadorPorCarrera = new PatrocinadoresPorCarrera
                        {
                            NombreCarrera = reader["NombreCarrera"].ToString(),
                            NombreComercial = reader["NombreComercial"].ToString(),
                        };
                    }
                }
            }

            if (patrocinadorPorCarrera == null)
            {
                return NotFound();
            }

            return Ok(patrocinadorPorCarrera);
        }

        [HttpPost]
        public IActionResult PostPatrocinadorPorCarrera([FromBody] PatrocinadoresPorCarrera nuevoPatrocinadorPorCarrera)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@NombreCarrera", nuevoPatrocinadorPorCarrera.NombreCarrera);
                command.Parameters.AddWithValue("@NombreComercial", nuevoPatrocinadorPorCarrera.NombreComercial);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetPatrocinadorPorCarrera), new { nombreCarrera = nuevoPatrocinadorPorCarrera.NombreCarrera, nombreComercial = nuevoPatrocinadorPorCarrera.NombreComercial }, nuevoPatrocinadorPorCarrera);
        }

        [HttpDelete("{nombreCarrera}/{nombreComercial}")]
        public IActionResult DeletePatrocinadorPorCarrera(string nombreCarrera, string nombreComercial)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
