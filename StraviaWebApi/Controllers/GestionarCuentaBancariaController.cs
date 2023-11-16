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
    public class GestionarCuentaBancariaController : ControllerBase
    {
        private readonly string connectionString;

        public GestionarCuentaBancariaController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult GestionarCuentaBancaria([FromBody] GestionarCuentaBancaria cuentaBancaria)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("GestionarCuentaBancaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", cuentaBancaria.NombreCarrera);
                    command.Parameters.AddWithValue("@NombreBanco", cuentaBancaria.NombreBanco);
                    command.Parameters.AddWithValue("@NumeroCuenta", cuentaBancaria.NumeroCuenta);
                    command.Parameters.AddWithValue("@Accion", "Insertar");

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                return Ok("Operación realizada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{nombreCarrera}")]
        public IActionResult SeleccionarCuentasBancarias(string nombreCarrera)
        {
            try
            {
                List<GestionarCuentaBancaria> cuentasBancarias = new List<GestionarCuentaBancaria>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("GestionarCuentaBancaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@Accion", "Seleccionar");

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GestionarCuentaBancaria cuentaBancaria = new GestionarCuentaBancaria
                            {
                                NombreCarrera = reader["NombreCarrera"].ToString(),
                                NombreBanco = reader["NombreBanco"].ToString(),
                                NumeroCuenta = reader["NumeroCuenta"].ToString()
                            };

                            cuentasBancarias.Add(cuentaBancaria);
                        }
                    }
                }

                return Ok(cuentasBancarias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{nombreCarrera}/{numeroCuenta}")]
        public IActionResult SeleccionarCuentaBancaria(string nombreCarrera, string numeroCuenta)
        {
            try
            {
                GestionarCuentaBancaria cuentaBancaria = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("GestionarCuentaBancaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
                    command.Parameters.AddWithValue("@Accion", "SeleccionarUno");

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cuentaBancaria = new GestionarCuentaBancaria
                            {
                                NombreCarrera = reader["NombreCarrera"].ToString(),
                                NombreBanco = reader["NombreBanco"].ToString(),
                                NumeroCuenta = reader["NumeroCuenta"].ToString()
                            };
                        }
                    }
                }

                if (cuentaBancaria == null)
                {
                    return NotFound();
                }

                return Ok(cuentaBancaria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{nombreCarrera}/{numeroCuenta}")]
        public IActionResult EliminarCuentaBancaria(string nombreCarrera, string numeroCuenta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("GestionarCuentaBancaria", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreCarrera", nombreCarrera);
                    command.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
                    command.Parameters.AddWithValue("@Accion", "Eliminar");

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
    }
}
