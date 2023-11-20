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
    public class UsuariosPorGrupoController : ControllerBase
    {
        private readonly string connectionString;

        public UsuariosPorGrupoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetUsuariosPorGrupos()
        {
            try
            {
                List<UsuariosPorGrupo> usuariosPorGrupos = new List<UsuariosPorGrupo>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("CrudUsuariosPorGrupo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", "SELECT");

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsuariosPorGrupo usuarioPorGrupo = new UsuariosPorGrupo
                            {
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                GrupoID = reader["GrupoID"].ToString(),
                            };

                            usuariosPorGrupos.Add(usuarioPorGrupo);
                        }
                    }
                }

                return Ok(usuariosPorGrupos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{nombreUsuario}/{grupoID}")]
        public IActionResult GetUsuarioPorGrupo(string nombreUsuario, string grupoID)
        {
            try
            {
                UsuariosPorGrupo usuarioPorGrupo = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("CrudUsuariosPorGrupo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@GrupoID", grupoID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioPorGrupo = new UsuariosPorGrupo
                            {
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                GrupoID = reader["GrupoID"].ToString(),
                            };
                        }
                    }
                }

                if (usuarioPorGrupo == null)
                {
                    return NotFound();
                }

                return Ok(usuarioPorGrupo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult PostUsuarioPorGrupo([FromBody] UsuariosPorGrupo nuevoUsuarioPorGrupo)
        {
            try
            {
                // Verifica si el usuario existe en la tabla Usuario antes de la inserción
                if (!UsuarioExists(nuevoUsuarioPorGrupo.NombreUsuario))
                {
                    return StatusCode(400, "El usuario no existe en la tabla Usuario.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("CrudUsuariosPorGrupo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", "INSERT");
                    command.Parameters.AddWithValue("@NombreUsuario", nuevoUsuarioPorGrupo.NombreUsuario);
                    command.Parameters.AddWithValue("@GrupoID", nuevoUsuarioPorGrupo.GrupoID);

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                return CreatedAtAction(nameof(GetUsuarioPorGrupo), new { nombreUsuario = nuevoUsuarioPorGrupo.NombreUsuario, grupoID = nuevoUsuarioPorGrupo.GrupoID }, nuevoUsuarioPorGrupo);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{nombreUsuario}/{grupoID}")]
        public IActionResult PutUsuarioPorGrupo(string nombreUsuario, string grupoID, [FromBody] UsuariosPorGrupo usuarioPorGrupoActualizado)
        {
            try
            {
                if (nombreUsuario != usuarioPorGrupoActualizado.NombreUsuario || grupoID != usuarioPorGrupoActualizado.GrupoID)
                {
                    return BadRequest();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("CrudUsuariosPorGrupo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", "UPDATE");
                    command.Parameters.AddWithValue("@NombreUsuario", usuarioPorGrupoActualizado.NombreUsuario);
                    command.Parameters.AddWithValue("@GrupoID", usuarioPorGrupoActualizado.GrupoID);

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{nombreUsuario}/{grupoID}")]
        public IActionResult DeleteUsuarioPorGrupo(string nombreUsuario, string grupoID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("CrudUsuariosPorGrupo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", "DELETE");
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@GrupoID", grupoID);

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Método para verificar si el usuario existe en la tabla Usuario
        private bool UsuarioExists(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Usuario WHERE NombreUsuario = @NombreUsuario", connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
