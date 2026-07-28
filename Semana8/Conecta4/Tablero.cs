namespace Conecta4App
{
    public class Tablero
    {
        public const int TOTAL_FILAS = 6;
        public const int TOTAL_COLUMNAS = 7;
        private int[,] _matrizTablero;

        public Tablero()
        {
            _matrizTablero = new int[TOTAL_FILAS, TOTAL_COLUMNAS];
            ReiniciarTablero();
        }

        public void ReiniciarTablero()
        {
            for (int f = 0; f < TOTAL_FILAS; f++)
                for (int c = 0; c < TOTAL_COLUMNAS; c++)
                    _matrizTablero[f, c] = 0;
        }

        public int ColocarFicha(int columna, int valorJugador)
        {
            if (columna < 0 || columna >= TOTAL_COLUMNAS) return -1;

            for (int fila = TOTAL_FILAS - 1; fila >= 0; fila--)
            {
                if (_matrizTablero[fila, columna] == 0)
                {
                    _matrizTablero[fila, columna] = valorJugador;
                    return fila;
                }
            }
            return -1;
        }

        public bool EsColumnaValida(int columna) => _matrizTablero[0, columna] == 0;

        public bool EstaLleno()
        {
            for (int c = 0; c < TOTAL_COLUMNAS; c++)
                if (EsColumnaValida(c)) return false;
            return true;
        }

        public bool VerificarVictoria(int v)
        {
            // Horizontal
            for (int f = 0; f < TOTAL_FILAS; f++)
                for (int c = 0; c <= TOTAL_COLUMNAS - 4; c++)
                    if (_matrizTablero[f, c] == v && _matrizTablero[f, c + 1] == v &&
                        _matrizTablero[f, c + 2] == v && _matrizTablero[f, c + 3] == v) return true;

            // Vertical
            for (int f = 0; f <= TOTAL_FILAS - 4; f++)
                for (int c = 0; c < TOTAL_COLUMNAS; c++)
                    if (_matrizTablero[f, c] == v && _matrizTablero[f + 1, c] == v &&
                        _matrizTablero[f + 2, c] == v && _matrizTablero[f + 3, c] == v) return true;

            // Diagonal /
            for (int f = 3; f < TOTAL_FILAS; f++)
                for (int c = 0; c <= TOTAL_COLUMNAS - 4; c++)
                    if (_matrizTablero[f, c] == v && _matrizTablero[f - 1, c + 1] == v &&
                        _matrizTablero[f - 2, c + 2] == v && _matrizTablero[f - 3, c + 3] == v) return true;

            // Diagonal \
            for (int f = 0; f <= TOTAL_FILAS - 4; f++)
                for (int c = 0; c <= TOTAL_COLUMNAS - 4; c++)
                    if (_matrizTablero[f, c] == v && _matrizTablero[f + 1, c + 1] == v &&
                        _matrizTablero[f + 2, c + 2] == v && _matrizTablero[f + 3, c + 3] == v) return true;

            return false;
        }
    }
}