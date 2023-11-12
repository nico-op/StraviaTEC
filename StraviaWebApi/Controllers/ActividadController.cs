
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
        

        [HttpGet("{usuario}")]
        public ActionResult<List<Actividad>> GetByUser(string usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarActividadesPorUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@NombreUsuario", usuario));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Actividad> actividades = new List<Actividad>();
                while (reader.Read())
                {
                    Actividad actividad = new Actividad
                    {
                        ActividadId = (int)reader["ActividadID"],
                        TipoActividad = reader["TipoActividad"].ToString(),
                        Kilometraje = (int)reader["Kilometraje"],
                        Altitud = (int)reader["Altitud"],
                        Ruta = reader["Ruta"].ToString(),
                        FechaHora = (DateTime)reader["FechaHora"],
                        Duracion = (int)reader["Duracion"],
                        NombreUsuario = usuario
                    };

                    actividades.Add(actividad);
                }

                reader.Close();

                if (actividades.Count > 0)
                {
                    return actividades;
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult<Actividad> Create(Actividad actividad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("InsertarActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoActividad", actividad.TipoActividad);
                command.Parameters.AddWithValue("@Kilometraje", actividad.Kilometraje);
                command.Parameters.AddWithValue("@Altitud", actividad.Altitud);
                command.Parameters.AddWithValue("@Ruta", actividad.Ruta);
                command.Parameters.AddWithValue("@FechaHora", actividad.FechaHora);
                command.Parameters.AddWithValue("@Duracion", actividad.Duracion);
                command.Parameters.AddWithValue("@NombreUsuario", actividad.NombreUsuario);

                command.ExecuteNonQuery();

                return CreatedAtAction(nameof(GetByUser), new { usuario = actividad.NombreUsuario }, actividad);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Actividad actividad)
        {
            if (id != actividad.ActividadId)
            {
                return BadRequest();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ActualizarActividad", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActividadID", id);
                command.Parameters.AddWithValue("@TipoActividad", actividad.TipoActividad);
                command.Parameters.AddWithValue("@Kilometraje", actividad.Kilometraje);
                command.Parameters.AddWithValue("@Altitud", actividad.Altitud);
                command.Parameters.AddWithValue("@Ruta", actividad.Ruta);
                command.Parameters.AddWithValue("@FechaHora", actividad.FechaHora);
                command.Parameters.AddWithValue("@Duracion", actividad.Duracion);
                command.Parameters.AddWithValue("@NombreUsuario", actividad.NombreUsuario);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("EliminarActividadPorID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActividadID", id);

                command.ExecuteNonQuery();
            }

            return NoContent();
        }

    }
}
