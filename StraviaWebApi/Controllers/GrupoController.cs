using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using StraviaWebApi.Models;

namespace StraviaTEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly string connectionString;

        public GrupoController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetGrupos()
        {
            List<Grupo> grupos = new List<Grupo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudGrupo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Grupo grupo = new Grupo
                                {
                                    NombreGrupo = reader["NombreGrupo"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Administrador = reader["Administrador"].ToString(),
                                    Creacion = Convert.ToDateTime(reader["Creacion"]),
                                    GrupoId = reader["GrupoID"].ToString()
                                };

                                grupos.Add(grupo);
                            }
                        }
                    }
                }

                return Ok(grupos);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{grupoId}")]
        public IActionResult GetGrupo(string grupoId)
        {
            Grupo grupo = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudGrupo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                        command.Parameters.AddWithValue("@GrupoID", grupoId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                grupo = new Grupo
                                {
                                    NombreGrupo = reader["NombreGrupo"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Administrador = reader["Administrador"].ToString(),
                                    Creacion = Convert.ToDateTime(reader["Creacion"]),
                                    GrupoId = reader["GrupoID"].ToString()
                                };
                            }
                        }
                    }
                }

                if (grupo == null)
                {
                    return NotFound();
                }

                return Ok(grupo);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostGrupo([FromBody] Grupo newGrupo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudGrupo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@NombreGrupo", newGrupo.NombreGrupo);
                        command.Parameters.AddWithValue("@Descripcion", newGrupo.Descripcion);
                        command.Parameters.AddWithValue("@Administrador", newGrupo.Administrador);
                        command.Parameters.AddWithValue("@Creacion", newGrupo.Creacion);
                        command.Parameters.AddWithValue("@GrupoID", newGrupo.GrupoId);

                        command.ExecuteNonQuery();
                    }
                }

                return CreatedAtAction(nameof(GetGrupo), new { grupoId = newGrupo.GrupoId}, newGrupo);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{grupoId}")]
        public IActionResult PutGrupo(string grupoId, [FromBody] Grupo updatedGrupo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudGrupo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "UPDATE");
                        command.Parameters.AddWithValue("@NombreGrupo", updatedGrupo.NombreGrupo);
                        command.Parameters.AddWithValue("@Descripcion", updatedGrupo.Descripcion);
                        command.Parameters.AddWithValue("@Administrador", updatedGrupo.Administrador);
                        command.Parameters.AddWithValue("@Creacion", updatedGrupo.Creacion);
                        command.Parameters.AddWithValue("@GrupoID", grupoId);

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

        [HttpDelete("{grupoId}")]
        public IActionResult DeleteGrupo(string grupoId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudGrupo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "DELETE");
                        command.Parameters.AddWithValue("@GrupoID", grupoId);

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