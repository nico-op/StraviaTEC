using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StraviaWebApi.Models;


namespace StraviaTEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmigoController : ControllerBase
    {
        private readonly string connectionString;

        public AmigoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetAmigos()
        {
            List<Amigo> amigos = new List<Amigo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Amigo amigo = new Amigo
                                {
                                    UsuarioOrigen = reader["UsuarioOrigen"].ToString(),
                                    UsuarioDestino = reader["UsuarioDestino"].ToString()
                                };

                                amigos.Add(amigo);
                            }
                        }
                    }
                }

                return Ok(amigos);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioOrigen}")]
        public IActionResult GetAmigosByUser(string usuarioOrigen)
        {
            List<Amigo> amigos = new List<Amigo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT BY USER");
                        command.Parameters.AddWithValue("@UsuarioOrigen", usuarioOrigen);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Amigo amigo = new Amigo
                                {
                                    UsuarioOrigen = reader["UsuarioOrigen"].ToString(),
                                    UsuarioDestino = reader["UsuarioDestino"].ToString()
                                };

                                amigos.Add(amigo);
                            }
                        }
                    }
                }

                return Ok(amigos);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioOrigen}/{usuarioDestino}")]
        public IActionResult GetAmigo(string usuarioOrigen, string usuarioDestino)
        {
            Amigo amigo = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                        command.Parameters.AddWithValue("@UsuarioOrigen", usuarioOrigen);
                        command.Parameters.AddWithValue("@UsuarioDestino", usuarioDestino);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                amigo = new Amigo
                                {
                                    UsuarioOrigen = reader["UsuarioOrigen"].ToString(),
                                    UsuarioDestino = reader["UsuarioDestino"].ToString()
                                };
                            }
                        }
                    }
                }

                if (amigo == null)
                {
                    return NotFound();
                }

                return Ok(amigo);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult PostAmigo([FromBody] Amigo newAmigo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@UsuarioOrigen", newAmigo.UsuarioOrigen);
                        command.Parameters.AddWithValue("@UsuarioDestino", newAmigo.UsuarioDestino);

                        command.ExecuteNonQuery();
                    }
                }

                return CreatedAtAction(nameof(GetAmigo), new { usuarioOrigen = newAmigo.UsuarioOrigen, usuarioDestino = newAmigo.UsuarioDestino }, newAmigo);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{usuarioOrigen}/{usuarioDestino}")]
        public IActionResult DeleteAmigo(string usuarioOrigen, string usuarioDestino)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudAmigo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "DELETE");
                        command.Parameters.AddWithValue("@UsuarioOrigen", usuarioOrigen);
                        command.Parameters.AddWithValue("@UsuarioDestino", usuarioDestino);

                        command.ExecuteNonQuery();
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }



    }
}
