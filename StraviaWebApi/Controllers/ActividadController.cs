
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
    public class ActividadController : ControllerBase
    {
        private readonly string connectionString;

        public ActividadController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public IActionResult GetActividades()
        {
            List<Actividad> actividades = new List<Actividad>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "SELECT");

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Actividad actividad = new Actividad
                        {
                            ActividadId = Convert.ToInt32(reader["ActividadId"]),
                            TipoActividad = reader["TipoActividad"].ToString(),
                            Kilometraje = Convert.ToInt32(reader["Kilometraje"]),
                            Altitud = Convert.ToInt32(reader["Altitud"]),
                            Ruta = reader["Ruta"].ToString(),
                            FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                            Duracion = Convert.ToInt32(reader["Duracion"]),
                            NombreUsuario = reader["NombreUsuario"].ToString()
                        };

                        actividades.Add(actividad);
                    }
                }
            }
            return Ok(actividades);
        }


        [HttpPost]
        public IActionResult CreateActividad([FromBody] Actividad newActividad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "INSERT");
                command.Parameters.AddWithValue("@TipoActividad", newActividad.TipoActividad);
                command.Parameters.AddWithValue("@Kilometraje", newActividad.Kilometraje);
                command.Parameters.AddWithValue("@Altitud", newActividad.Altitud);
                command.Parameters.AddWithValue("@Ruta", newActividad.Ruta);
                command.Parameters.AddWithValue("@FechaHora", newActividad.FechaHora);
                command.Parameters.AddWithValue("@Duracion", newActividad.Duracion);
                command.Parameters.AddWithValue("@NombreUsuario", newActividad.NombreUsuario);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return Ok(newActividad);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActividad(int id, [FromBody] Actividad updatedActividad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "UPDATE");
                command.Parameters.AddWithValue("@ActividadID", id);
                command.Parameters.AddWithValue("@TipoActividad", updatedActividad.TipoActividad);
                command.Parameters.AddWithValue("@Kilometraje", updatedActividad.Kilometraje);
                command.Parameters.AddWithValue("@Altitud", updatedActividad.Altitud);
                command.Parameters.AddWithValue("@Ruta", updatedActividad.Ruta);
                command.Parameters.AddWithValue("@FechaHora", updatedActividad.FechaHora);
                command.Parameters.AddWithValue("@Duracion", updatedActividad.Duracion);
                command.Parameters.AddWithValue("@NombreUsuario", updatedActividad.NombreUsuario);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return Ok(updatedActividad);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActividad(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CrudActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Operacion", "DELETE");
                command.Parameters.AddWithValue("@ActividadID", id);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return Ok();
        }

    }
}
