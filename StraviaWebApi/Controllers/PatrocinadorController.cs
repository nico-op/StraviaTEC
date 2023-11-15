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
    public class PatrocinadorController : ControllerBase
    {
        private readonly string connectionString;

        public PatrocinadorController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        
        [HttpGet("{nombreComercial}")]
        public IActionResult GetPatrocinador(string nombreComercial)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudPatrocinador", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                        command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Patrocinador patrocinador = new Patrocinador
                                {
                                    NombreLegal = reader["NombreLegal"].ToString(),
                                    Logo = reader["Logo"].ToString(),
                                    Telefono = Convert.ToInt32(reader["Telefono"]),
                                    NombreComercial = reader["NombreComercial"].ToString(),
                                };

                                return Ok(patrocinador);
                            }
                        }
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllPatrocinadores()
        {
            List<Patrocinador> patrocinadores = new List<Patrocinador>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudPatrocinador", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patrocinador patrocinador = new Patrocinador
                                {
                                    NombreLegal = reader["NombreLegal"].ToString(),
                                    Logo = reader["Logo"].ToString(),
                                    Telefono = Convert.ToInt32(reader["Telefono"]),
                                    NombreComercial = reader["NombreComercial"].ToString(),
                                };

                                patrocinadores.Add(patrocinador);
                            }
                        }
                    }
                }

                return Ok(patrocinadores);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePatrocinador([FromBody] Patrocinador newPatrocinador)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudPatrocinador", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@NombreLegal", newPatrocinador.NombreLegal);
                        command.Parameters.AddWithValue("@Logo", newPatrocinador.Logo);
                        command.Parameters.AddWithValue("@Telefono", newPatrocinador.Telefono);
                        command.Parameters.AddWithValue("@NombreComercial", newPatrocinador.NombreComercial);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok(newPatrocinador);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{nombreComercial}")]
        public IActionResult UpdatePatrocinador(string nombreComercial, [FromBody] Patrocinador updatedPatrocinador)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudPatrocinador", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "UPDATE");
                        command.Parameters.AddWithValue("@NombreLegal", updatedPatrocinador.NombreLegal);
                        command.Parameters.AddWithValue("@Logo", updatedPatrocinador.Logo);
                        command.Parameters.AddWithValue("@Telefono", updatedPatrocinador.Telefono);
                        command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok(updatedPatrocinador);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{nombreComercial}")]
        public IActionResult DeletePatrocinador(string nombreComercial)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudPatrocinador", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "DELETE");
                        command.Parameters.AddWithValue("@NombreComercial", nombreComercial);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }
    }
}
