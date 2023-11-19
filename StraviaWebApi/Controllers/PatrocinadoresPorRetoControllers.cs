using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraviaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StraviaTEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatrocinadoresPorRetoController : ControllerBase
    {
        private readonly string connectionString;

        public PatrocinadoresPorRetoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetPatrocinadoresPorRetos()
        {
            List<PatrocinadoresPorReto> patrocinadoresPorRetos = new List<PatrocinadoresPorReto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PatrocinadoresPorReto patrocinadorPorReto = new PatrocinadoresPorReto
                        {
                            NombreReto = reader["NombreReto"].ToString(),
                            NombreComercial = reader["NombreComercial"].ToString(),
                        };

                        patrocinadoresPorRetos.Add(patrocinadorPorReto);
                    }
                }
            }

            return Ok(patrocinadoresPorRetos);
        }

        [HttpGet("{nombreReto}/{nombreComercial}")]
        public IActionResult GetPatrocinadorPorReto(string nombreReto, string nombreComercial)
        {
            PatrocinadoresPorReto patrocinadorPorReto = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreReto", nombreReto);
                command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patrocinadorPorReto = new PatrocinadoresPorReto
                        {
                            NombreReto = reader["NombreReto"].ToString(),
                            NombreComercial = reader["NombreComercial"].ToString(),
                        };
                    }
                }
            }

            if (patrocinadorPorReto == null)
            {
                return NotFound();
            }

            return Ok(patrocinadorPorReto);
        }

        [HttpPost]
        public IActionResult PostPatrocinadorPorReto([FromBody] PatrocinadoresPorReto nuevoPatrocinadorPorReto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@NombreReto", nuevoPatrocinadorPorReto.NombreReto);
                command.Parameters.AddWithValue("@NombreComercial", nuevoPatrocinadorPorReto.NombreComercial);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetPatrocinadorPorReto), new { nombreReto = nuevoPatrocinadorPorReto.NombreReto, nombreComercial = nuevoPatrocinadorPorReto.NombreComercial }, nuevoPatrocinadorPorReto);
        }

        [HttpDelete("{nombreReto}/{nombreComercial}")]
        public IActionResult DeletePatrocinadorPorReto(string nombreReto, string nombreComercial)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudPatrocinadoresPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@NombreReto", nombreReto);
                command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
