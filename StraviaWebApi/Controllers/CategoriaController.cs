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
    public class CategoriaController : ControllerBase
    {
        private readonly string connectionString;

        public CategoriaController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult CreateCategoria([FromBody] Categoria categoria)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCategoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "INSERT");
                        command.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                        command.Parameters.AddWithValue("@DescripcionCategoria", categoria.DescripcionCategoria);
                        

                        command.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nombreCarrera}")]
        public IActionResult GetCategoria(string nombreCarrera)
        {
            Categoria categoria = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CrudCategoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Operacion", "SELECT ONE");
                        command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                categoria = new Categoria
                                {
                                    NombreCategoria = reader["NombreCategoria"].ToString(),
                                    DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                };
                            }
                        }
                    }
                }

                if (categoria == null)
                {
                    return NotFound();
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                // Log the exception message or return it in a BadRequest
                return BadRequest(ex.Message);
            }
        }


    }
}