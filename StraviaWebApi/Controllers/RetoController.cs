


using Microsoft.AspNetCore.Mvc;
using StraviaWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StraviaTEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetoController : ControllerBase
    {
        
        private readonly string connectionString;

        public RetoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public IActionResult GetRetos()
        {
            List<Reto> retos = new List<Reto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reto reto = new Reto
                        {
                            NombreReto = reader["NombreReto"].ToString(),
                            Privacidad = reader["Privacidad"].ToString(),
                            Periodo = Convert.ToInt32(reader["Periodo"]),
                            TipoActividad = reader["TipoActividad"].ToString(),
                            Altitud = reader["Altitud"].ToString(),
                            Fondo = reader["Fondo"].ToString()
                        };

                        retos.Add(reto);
                    }
                }
            }

            return Ok(retos);
        }


        [HttpGet("{nombreReto}")]
        public IActionResult GetReto(string nombreReto)
        {
            Reto reto = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreReto", nombreReto);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reto = new Reto
                        {
                            NombreReto = reader["NombreReto"].ToString(),
                            Privacidad = reader["Privacidad"].ToString(),
                            Periodo = Convert.ToInt32(reader["Periodo"]),
                            TipoActividad = reader["TipoActividad"].ToString(),
                            Altitud = reader["Altitud"].ToString(),
                            Fondo = reader["Fondo"].ToString()
                        };
                    }
                }
            }

            if (reto == null)
            {
                return NotFound();
            }

            return Ok(reto);
        }


        [HttpPost]
        public IActionResult PostReto([FromBody] Reto nuevoReto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@NombreReto", nuevoReto.NombreReto);
                command.Parameters.AddWithValue("@Privacidad", nuevoReto.Privacidad);
                command.Parameters.AddWithValue("@Periodo", nuevoReto.Periodo);
                command.Parameters.AddWithValue("@TipoActividad", nuevoReto.TipoActividad);
                command.Parameters.AddWithValue("@Altitud", nuevoReto.Altitud);
                command.Parameters.AddWithValue("@Fondo", nuevoReto.Fondo);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetReto), new { nombreReto = nuevoReto.NombreReto }, nuevoReto);
        }
        [HttpPut("{nombreReto}")]
        public IActionResult PutReto(string nombreReto, [FromBody] Reto retoActualizado)
        {
            if (nombreReto != retoActualizado.NombreReto)
            {
                return BadRequest();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "UPDATE");
                command.Parameters.AddWithValue("@NombreReto", retoActualizado.NombreReto);
                command.Parameters.AddWithValue("@Privacidad", retoActualizado.Privacidad);
                command.Parameters.AddWithValue("@Periodo", retoActualizado.Periodo);
                command.Parameters.AddWithValue("@TipoActividad", retoActualizado.TipoActividad);
                command.Parameters.AddWithValue("@Altitud", retoActualizado.Altitud);
                command.Parameters.AddWithValue("@Fondo", retoActualizado.Fondo);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }


        [HttpDelete("{nombreReto}")]
        public IActionResult DeleteReto(string nombreReto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = $"EXEC CrudReto 'DELETE', {nombreReto}";
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}

