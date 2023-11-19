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

        [HttpGet("{nombreCarrera}/{nombreCategoria}")]
        public IActionResult GetCategoria(string nombreCarrera, string nombreCategoria)
        {
            Categoria categoria = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GestionarCategoriasPorCarrera", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@NombreCategoria", nombreCategoria);
                    command.Parameters.AddWithValue("@Operacion", "SELECT ONE");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoria = new Categoria
                            {
                                NombreCategoria = reader["NombreCategoria"].ToString(),
                                DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                NombreCarrera = reader["NombreCarrera"].ToString()
                            };
                        }
                    }
                }
            }
            return Ok(categoria);
        }

        [HttpGet("{nombreCarrera}")]
        public IActionResult GetAllCategorias(string nombreCarrera)
        {
            var categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GestionarCategoriasPorCarrera", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@Operacion", "SELECT");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var categoria = new Categoria
                            {
                                NombreCategoria = reader["NombreCategoria"].ToString(),
                                DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                NombreCarrera = reader["NombreCarrera"].ToString()
                            };
                            categorias.Add(categoria);
                        }
                    }
                }
            }
            return Ok(categorias);
        }

        [HttpPost("{nombreCarrera}")]
        public IActionResult InsertCategoria(string nombreCarrera)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GestionarCategoriasPorCarrera", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@Operacion", "INSERT");
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }


    }
}