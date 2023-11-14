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
    public class UsuariosPorCarreraController : ControllerBase
    {
        private readonly string connectionString;

        public UsuariosPorCarreraController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetUsuariosPorCarreras()
        {
            List<UsuariosPorCarrera> usuariosPorCarreras = new List<UsuariosPorCarrera>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuariosPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsuariosPorCarrera usuarioPorCarrera = new UsuariosPorCarrera
                        {
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            NombreCarrera = reader["NombreCarrera"].ToString(),
                        };

                        usuariosPorCarreras.Add(usuarioPorCarrera);
                    }
                }
            }

            return Ok(usuariosPorCarreras);
        }

        [HttpGet("{nombreUsuario}/{nombreCarrera}")]
        public IActionResult GetUsuarioPorCarrera(string nombreUsuario, string nombreCarrera)
        {
            UsuariosPorCarrera usuarioPorCarrera = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuariosPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuarioPorCarrera = new UsuariosPorCarrera
                        {
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            NombreCarrera = reader["NombreCarrera"].ToString(),
                        };
                    }
                }
            }

            if (usuarioPorCarrera == null)
            {
                return NotFound();
            }

            return Ok(usuarioPorCarrera);
        }

        [HttpPost]
        public IActionResult PostUsuarioPorCarrera([FromBody] UsuariosPorCarrera nuevoUsuarioPorCarrera)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuariosPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@NombreUsuario", nuevoUsuarioPorCarrera.NombreUsuario);
                command.Parameters.AddWithValue("@NombreCarrera", nuevoUsuarioPorCarrera.NombreCarrera);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetUsuarioPorCarrera), new { nombreUsuario = nuevoUsuarioPorCarrera.NombreUsuario, nombreCarrera = nuevoUsuarioPorCarrera.NombreCarrera }, nuevoUsuarioPorCarrera);
        }

        [HttpPut("{nombreUsuario}/{nombreCarrera}")]
        public IActionResult PutUsuarioPorCarrera(string nombreUsuario, string nombreCarrera, [FromBody] UsuariosPorCarrera usuarioPorCarreraActualizado)
        {
            if (nombreUsuario != usuarioPorCarreraActualizado.NombreUsuario || nombreCarrera != usuarioPorCarreraActualizado.NombreCarrera)
            {
                return BadRequest();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuariosPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "UPDATE");
                command.Parameters.AddWithValue("@NombreUsuario", usuarioPorCarreraActualizado.NombreUsuario);
                command.Parameters.AddWithValue("@NombreCarrera", usuarioPorCarreraActualizado.NombreCarrera);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        [HttpDelete("{nombreUsuario}/{nombreCarrera}")]
        public IActionResult DeleteUsuarioPorCarrera(string nombreUsuario, string nombreCarrera)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudUsuariosPorCarrera", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return NoContent();
        }
    }
}
