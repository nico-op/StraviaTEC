using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using StraviaWebApi.Models;

namespace StraviaWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly string connectionString;

        public UsuarioController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CrudUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Nombre = reader.GetString(0),
                            Apellido1 = reader.GetString(1),
                            Apellido2 = reader.GetString(2),
                            FechaNacimiento = reader.GetDateTime(3),
                            FechaActual = reader.GetDateTime(4),
                            Nacionalidad = reader.GetString(5),
                            Foto = reader.GetString(6),
                            NombreUsuario = reader.GetString(7),
                            Contrasena = reader.GetString(8),
                            Edad = reader.GetInt32(9)
                        };

                        usuarios.Add(usuario);
                    }
                }
            }

            return Ok(usuarios);
        }

        [HttpGet("{nombreUsuario}")]
        public IActionResult GetUsuario(string nombreUsuario)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CrudUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Nombre = reader.GetString(0),
                            Apellido1 = reader.GetString(1),
                            Apellido2 = reader.GetString(2),
                            FechaNacimiento = reader.GetDateTime(3),
                            FechaActual = reader.GetDateTime(4),
                            Nacionalidad = reader.GetString(5),
                            Foto = reader.GetString(6),
                            NombreUsuario = reader.GetString(7),
                            Contrasena = reader.GetString(8),
                            Edad = reader.GetInt32(9)
                        };
                    }
                }
            }

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult PostUsuario([FromBody] Usuario newUsuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@Nombre", newUsuario.Nombre);
                        command.Parameters.AddWithValue("@Apellido1", newUsuario.Apellido1);
                        command.Parameters.AddWithValue("@Apellido2", newUsuario.Apellido2);
                        command.Parameters.AddWithValue("@Fecha_nacimiento", newUsuario.FechaNacimiento);
                        command.Parameters.AddWithValue("@Fecha_actual", newUsuario.FechaActual);
                        command.Parameters.AddWithValue("@Nacionalidad", newUsuario.Nacionalidad);
                        command.Parameters.AddWithValue("@Foto", newUsuario.Foto);
                        command.Parameters.AddWithValue("@NombreUsuario", newUsuario.NombreUsuario);
                        command.Parameters.AddWithValue("@Contrasena", newUsuario.Contrasena);

                        // Remove the following line, as it is not needed
                        // command.Parameters.AddWithValue("@Edad", newUsuario.Edad);

                        command.ExecuteNonQuery();
                    }
                }

                return CreatedAtAction(nameof(GetUsuario), new { nombreUsuario = newUsuario.NombreUsuario }, newUsuario);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{nombreUsuario}")]
        public IActionResult PutUsuario(string nombreUsuario, [FromBody] Usuario updatedUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CrudUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Operacion", "UPDATE");
                command.Parameters.AddWithValue("@Nombre", updatedUsuario.Nombre);
                command.Parameters.AddWithValue("@Apellido1", updatedUsuario.Apellido1);
                command.Parameters.AddWithValue("@Apellido2", updatedUsuario.Apellido2);
                command.Parameters.AddWithValue("@Fecha_nacimiento", updatedUsuario.FechaNacimiento);
                command.Parameters.AddWithValue("@Fecha_actual", updatedUsuario.FechaActual);
                command.Parameters.AddWithValue("@Nacionalidad", updatedUsuario.Nacionalidad);
                command.Parameters.AddWithValue("@Foto", updatedUsuario.Foto);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@Contrasena", updatedUsuario.Contrasena);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        [HttpDelete("{nombreUsuario}")]
        public IActionResult DeleteUsuario(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CrudUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }




    }
}
