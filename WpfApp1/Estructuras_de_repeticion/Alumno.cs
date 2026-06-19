using System;
using System.Collections.Generic;
using System.Text;

namespace Estructuras_de_repeticion
{
    internal class Alumno
    {

        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public int Edad { get; set; }

        public Alumno(string nombre, string apellidos, int edad  )
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
        }
    }
}
