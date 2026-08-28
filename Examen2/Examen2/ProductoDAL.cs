using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using NorthwindApp.Models;

namespace NorthwindApp.Data
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones CRUD y consultas SQL para la entidad Producto.
    /// Aplica el patrón DAO (Data Access Object) y encapsula el acceso a la base de datos.
    /// </summary>
    public class ProductoDAL
    {
        // Cadena de conexión a la base de datos Northwind (Ajustar según la instancia local de SQL Server)
        private readonly string _connectionString = "Server=localhost;Database=Northwind;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Obtiene la lista completa de productos incluyendo el nombre de la categoría.
        /// </summary>
        /// <returns>Lista de objetos de tipo Producto.</returns>
        public List<Producto> ObtenerProductos()
        {
            var lista = new List<Producto>();

            // Sentencia SQL con INNER JOIN para recuperar el nombre de la categoría
            string query = @"SELECT p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, 
                                    ISNULL(p.UnitPrice, 0) AS UnitPrice, 
                                    ISNULL(p.UnitsInStock, 0) AS UnitsInStock, 
                                    p.Discontinued
                             FROM Products p
                             LEFT JOIN Categories c ON p.CategoryID = c.CategoryID";

            // Uso de la sintaxis using para garantizar el cierre y liberación de recursos de conexión
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Mapeo Objeto-Relacional (ORM) manual
                            Producto p = new Producto
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                CategoryID = reader["CategoryID"] != DBNull.Value ? Convert.ToInt32(reader["CategoryID"]) : null,
                                CategoryName = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : "Sin Categoría",
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                UnitsInStock = Convert.ToInt16(reader["UnitsInStock"]),
                                Discontinued = Convert.ToBoolean(reader["Discontinued"])
                            };
                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Filtra productos por nombre utilizando un parámetro SQL para evitar inyección SQL.
        /// </summary>
        /// <param name="filtro">Texto a buscar dentro del nombre del producto.</param>
        /// <returns>Lista filtrada de objetos Producto.</returns>
        public List<Producto> BuscarProductosPorNombre(string filtro)
        {
            var lista = new List<Producto>();

            string query = @"SELECT p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, 
                                    ISNULL(p.UnitPrice, 0) AS UnitPrice, 
                                    ISNULL(p.UnitsInStock, 0) AS UnitsInStock, 
                                    p.Discontinued
                             FROM Products p
                             LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                             WHERE p.ProductName LIKE @Filtro";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Uso de parámetros para seguridad
                    command.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto p = new Producto
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                CategoryID = reader["CategoryID"] != DBNull.Value ? Convert.ToInt32(reader["CategoryID"]) : null,
                                CategoryName = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : "Sin Categoría",
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                UnitsInStock = Convert.ToInt16(reader["UnitsInStock"]),
                                Discontinued = Convert.ToBoolean(reader["Discontinued"])
                            };
                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }
    }
}