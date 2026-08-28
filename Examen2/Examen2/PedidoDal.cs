using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using NorthwindApp.Models;

namespace NorthwindApp.Data
{
    public class PedidoDAL
    {
        private readonly string _connectionString = "Server=localhost;Database=Northwind;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Obtiene todos los pedidos ordenados de forma ascendente (del más antiguo al más nuevo).
        /// </summary>
        public List<Pedido> ObtenerPedidos()
        {
            var lista = new List<Pedido>();

            string query = @"SELECT o.OrderID, 
                                    ISNULL(c.CompanyName, 'Cliente No Asignado') AS CustomerName, 
                                    o.OrderDate, 
                                    ISNULL(o.Freight, 0) AS Freight
                             FROM Orders o
                             LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                             ORDER BY o.OrderDate ASC, o.OrderID ASC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pedido
                            {
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                CustomerName = reader["CustomerName"].ToString() ?? string.Empty,
                                OrderDate = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : null,
                                Freight = Convert.ToDecimal(reader["Freight"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Filtra pedidos buscando por Nombre del Cliente ordenados del más antiguo al más nuevo.
        /// </summary>
        public List<Pedido> BuscarPedidosPorCliente(string cliente)
        {
            var lista = new List<Pedido>();

            string query = @"SELECT o.OrderID, 
                                    ISNULL(c.CompanyName, 'Cliente No Asignado') AS CustomerName, 
                                    o.OrderDate, 
                                    ISNULL(o.Freight, 0) AS Freight
                             FROM Orders o
                             LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                             WHERE c.CompanyName LIKE @Filtro
                             ORDER BY o.OrderDate ASC, o.OrderID ASC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filtro", "%" + cliente + "%");

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pedido
                            {
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                CustomerName = reader["CustomerName"].ToString() ?? string.Empty,
                                OrderDate = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : null,
                                Freight = Convert.ToDecimal(reader["Freight"])
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}