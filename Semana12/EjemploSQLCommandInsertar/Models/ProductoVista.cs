using System;
using System.Collections.Generic;
using System.Text;

namespace EjemploSQLCommandInsertar.Models
{
    public class ProductoVista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
}
