using System;

namespace NorthwindApp.Models
{
    public class Pedido
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public decimal Freight { get; set; }
    }
}