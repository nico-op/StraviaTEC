
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
        private readonly string connectionString = "Server=DESKTOP-45ERV0H\\SQLEXPRESS04;Database=StraviaTEC;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

        

        [HttpGet("{usuario}")]
        public ActionResult<List<Actividad>> GetByUser(string usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ConsultarActividadesPorUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Usuario", usuario));

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
                command.Parameters.AddWithValue("@Usuario", actividad.NombreUsuario);

                command.ExecuteNonQuery();

                return CreatedAtAction(nameof(GetByUser), new { id = actividad.ActividadId }, actividad);
            }
        }

        // [HttpPut("{id}")]
        // public IActionResult Update(int id, Actividad actividad)
        // {
        //     using (SqlConnection connection = new SqlConnection(connectionString))
        //     {
        //         connection.Open();

        //         SqlCommand command = new SqlCommand("", connection);
        //         command.Parameters.AddWithValue("@nombre", actividad.Nombre);
        //         command.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
        //         command.Parameters.AddWithValue("@fechaHora", actividad.FechaHora);
        //         command.Parameters.AddWithValue("@duracion", actividad.Duracion);
        //         command.Parameters.AddWithValue("@distancia", actividad.Distancia);
        //         command.Parameters.AddWithValue("@tipo", actividad.Tipo);
        //         command.Parameters.AddWithValue("@idUsuario", actividad.IdUsuario);
        //         command.Parameters.AddWithValue("@id", id);

        //         int rowsAffected = command.ExecuteNonQuery();

        //         if (rowsAffected == 0)
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             return NoContent();
        //         }
        //     }
        // }

        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     using (SqlConnection connection = new SqlConnection(connectionString))
        //     {
        //         connection.Open();

        //         SqlCommand command = new SqlCommand("DELETE FROM Actividad WHERE IdActividad = @id", connection);
        //         command.Parameters.AddWithValue("@id", id);

        //         int rowsAffected = command.ExecuteNonQuery();

        //         if (rowsAffected == 0)
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             return NoContent();
        //         }
        //     }
        // }
    }
}
