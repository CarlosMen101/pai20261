namespace NorthwindApp.Models
{
    /// <summary>
    /// Clase que representa la entidad Producto según la tabla 'Products' de la base de datos Northwind.
    /// </summary>
    public class Producto
    {
        // Propiedades auto-implementadas que encapsulan el estado del producto
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}