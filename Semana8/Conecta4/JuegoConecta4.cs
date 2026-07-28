using System;
using System.Collections.Generic;

namespace Conecta4App
{
    public class JuegoConecta4
    {
        public Tablero TableroJuego { get; private set; }
        public Jugador Jugador1 { get; private set; }
        public Jugador Jugador2 { get; private set; }
        public Jugador JugadorActual { get; private set; }
        public bool JuegoTerminado { get; private set; }

        private Random _random = new Random();

        public JuegoConecta4()
        {
            TableroJuego = new Tablero();
        }

        public void IniciarJuego(bool contraPC, bool iniciaHumanoPrimero)
        {
            TableroJuego.ReiniciarTablero();
            JuegoTerminado = false;

            Jugador1 = new Jugador("Jugador 1", TipoJugador.Humano, ColorFicha.Rojo, 1);
            Jugador2 = contraPC
                ? new Jugador("Computadora", TipoJugador.Computadora, ColorFicha.Azul, 2)
                : new Jugador("Jugador 2", TipoJugador.Humano, ColorFicha.Azul, 2);

            JugadorActual = iniciaHumanoPrimero ? Jugador1 : Jugador2;
        }

        public void CambiarTurno()
        {
            JugadorActual = (JugadorActual == Jugador1) ? Jugador2 : Jugador1;
        }

        public void FinalizarJuego() => JuegoTerminado = true;

        public int ObtenerColumnaAleatoriaPC()
        {
            List<int> validas = new List<int>();
            for (int c = 0; c < Tablero.TOTAL_COLUMNAS; c++)
                if (TableroJuego.EsColumnaValida(c)) validas.Add(c);

            return validas.Count > 0 ? validas[_random.Next(0, validas.Count)] : -1;
        }
    }
}