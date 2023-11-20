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
    public class CarreraController : ControllerBase
    {
        private readonly string connectionString;

        public CarreraController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public IActionResult GetCarreras()
        {
            List<Carrera> carreras = new List<Carrera>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCarrera", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Carrera carrera = new Carrera
                                {
                                    Costo = Convert.ToInt32(reader["Costo"]),
                                    Modalidad = reader["Modalidad"].ToString(),
                                    FechaCarrera = Convert.ToDateTime(reader["FechaCarrera"]),
                                    Recorrido = reader["Recorrido"].ToString(),
                                    NombreCarrera = reader["NombreCarrera"].ToString()
                                };

                                carreras.Add(carrera);
                            }
                        }
                    }
                }

                return Ok(carreras);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nombreCarrera}")]
        public IActionResult GetCarrera(string nombreCarrera)
        {
            Carrera carrera = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCarrera", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                        command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                carrera = new Carrera
                                {
                                    Costo = Convert.ToInt32(reader["Costo"]),
                                    Modalidad = reader["Modalidad"].ToString(),
                                    FechaCarrera = Convert.ToDateTime(reader["FechaCarrera"]),
                                    Recorrido = reader["Recorrido"].ToString(),
                                    NombreCarrera = reader["NombreCarrera"].ToString()
                                };
                            }
                        }
                    }
                }

                if (carrera == null)
                {
                    return NotFound();
                }

                return Ok(carrera);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult PostCarrera([FromBody] Carrera newCarrera)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCarrera", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@Costo", newCarrera.Costo);
                        command.Parameters.AddWithValue("@Modalidad", newCarrera.Modalidad);
                        command.Parameters.AddWithValue("@FechaCarrera", newCarrera.FechaCarrera);
                        command.Parameters.AddWithValue("@Recorrido", newCarrera.Recorrido);
                        command.Parameters.AddWithValue("@NombreCarrera", newCarrera.NombreCarrera);

                        command.ExecuteNonQuery();
                    }
                }

                return CreatedAtAction(nameof(GetCarrera), new { nombreCarrera = newCarrera.NombreCarrera }, newCarrera);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{nombreCarrera}")]
        public IActionResult PutCarrera(string nombreCarrera, [FromBody] Carrera updatedCarrera)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCarrera", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "UPDATE");
                        command.Parameters.AddWithValue("@Costo", updatedCarrera.Costo);
                        command.Parameters.AddWithValue("@Modalidad", updatedCarrera.Modalidad);
                        command.Parameters.AddWithValue("@FechaCarrera", updatedCarrera.FechaCarrera);
                        command.Parameters.AddWithValue("@Recorrido", updatedCarrera.Recorrido);
                        command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

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

        [HttpDelete("{nombreCarrera}")]
        public IActionResult DeleteCarrera(string nombreCarrera)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCarrera", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Operacion", "DELETE");
                        command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

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
