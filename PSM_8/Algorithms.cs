using System.Windows.Shapes;
namespace PSM_8
{
    public class Algorithms
    {
        private int Size { get; }

        public Algorithms(int size)
        {
            Size = size;
        }

        public void UpdateCells(bool[,] nextGeneration, Rectangle[,] cells)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (nextGeneration[i, j])
                    {
                        cells[i, j].Fill = System.Windows.Media.Brushes.Black;
                    }
                    else
                    {
                        cells[i, j].Fill = System.Windows.Media.Brushes.White;
                    }
                }
            }
        }
        public int GetNumberOfAliveNeighbors(bool[,] _currentGeneration,int xCoordinate, int yCoordinate)
        {
            int aliveNeighbors = 0;
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int ii = xCoordinate + x;
                    int jj = yCoordinate + y;
                    if (ii >= 0 && ii < Size && jj >= 0 && jj < Size && _currentGeneration[ii, jj])// ZWYKŁA TABLICA
                    {
                        aliveNeighbors++;
                    }
                }
            }

            return aliveNeighbors;
        }
    }
}