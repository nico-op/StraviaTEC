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
    public class UsuarioPorRetoController : ControllerBase
    {
        private readonly string connectionString;

        public UsuarioPorRetoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetUsuariosPorRetos()
        {
            List<UsuarioPorReto> usuariosPorRetos = new List<UsuarioPorReto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuarioPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsuarioPorReto usuarioPorReto = new UsuarioPorReto
                        {
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            NombreReto = reader["NombreReto"].ToString(),
                        };

                        usuariosPorRetos.Add(usuarioPorReto);
                    }
                }
            }

            return Ok(usuariosPorRetos);
        }

        [HttpGet("{nombreUsuario}/{nombreReto}")]
        public IActionResult GetUsuarioPorReto(string nombreUsuario, string nombreReto)
        {
            UsuarioPorReto usuarioPorReto = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuarioPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@NombreReto", nombreReto);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuarioPorReto = new UsuarioPorReto
                        {
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            NombreReto = reader["NombreReto"].ToString(),
                        };
                    }
                }
            }

            if (usuarioPorReto == null)
            {
                return NotFound();
            }

            return Ok(usuarioPorReto);
        }

        [HttpPost]
        public IActionResult PostUsuarioPorReto([FromBody] UsuarioPorReto nuevoUsuarioPorReto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuarioPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@NombreUsuario", nuevoUsuarioPorReto.NombreUsuario);
                command.Parameters.AddWithValue("@NombreReto", nuevoUsuarioPorReto.NombreReto);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetUsuarioPorReto), new { nombreUsuario = nuevoUsuarioPorReto.NombreUsuario, nombreReto = nuevoUsuarioPorReto.NombreReto }, nuevoUsuarioPorReto);
        }

        [HttpPut("{nombreUsuario}/{nombreReto}")]
        public IActionResult PutUsuarioPorReto(string nombreUsuario, string nombreReto, [FromBody] UsuarioPorReto usuarioPorRetoActualizado)
        {
            if (nombreUsuario != usuarioPorRetoActualizado.NombreUsuario || nombreReto != usuarioPorRetoActualizado.NombreReto)
            {
                return BadRequest();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuarioPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "UPDATE");
                command.Parameters.AddWithValue("@NombreUsuario", usuarioPorRetoActualizado.NombreUsuario);
                command.Parameters.AddWithValue("@NombreReto", usuarioPorRetoActualizado.NombreReto);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        [HttpDelete("{nombreUsuario}/{nombreReto}")]
        public IActionResult DeleteUsuarioPorReto(string nombreUsuario, string nombreReto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuarioPorReto", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@NombreReto", nombreReto);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
