using System.Windows.Media;

namespace Conecta4App
{
    // Enumeración para el tipo de participante
    public enum TipoJugador
    {
        Humano,
        Computadora
    }

    // Enumeración para el color de la ficha
    public enum ColorFicha
    {
        Rojo,
        Azul
    }

    public class Jugador
    {
        // Documentación de variables / propiedades:
        public string Nombre { get; set; }           // Nombre descriptivo del jugador
        public TipoJugador Tipo { get; set; }        // Indica si es Humano o Computadora
        public ColorFicha Color { get; set; }        // Asignación de color: Rojo o Azul
        public int ValorMatriz { get; set; }         // Número identificador interno (1 o 2)

        public Jugador(string nombre, TipoJugador tipo, ColorFicha color, int valorMatriz)
        {
            Nombre = nombre;
            Tipo = tipo;
            Color = color;
            ValorMatriz = valorMatriz;
        }

        // Obtiene el Brush correspondiente para la interfaz gráfica
        public SolidColorBrush ObtenerBrushColor()
        {
            return Color == ColorFicha.Rojo
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Colors.DodgerBlue);
        }
    }
}