using System.Collections.Generic;
using System.Windows.Controls;
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
        
        
        public bool[,] CalculateNextGeneration(bool[,] _currentGeneration,TextBox inputTextBox)//inputTextBox.Text.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
        {
            bool[,] _nextGeneration = new bool[Size, Size];
            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {


                string[] box_Text = inputTextBox.Text.Split('/');
                string dividendStr = box_Text[0];
                string divisorStr = box_Text[1];

                List<int> liveList = new List<int>();
                List<int> birthList = new List<int>();

                foreach (char digit in dividendStr)
                {
                    int num = int.Parse(digit.ToString());
                    if (!liveList.Contains(num))
                    {
                        liveList.Add(num);
                    }
                }

                foreach (char digit in divisorStr)
                {
                    int num = int.Parse(digit.ToString());
                    birthList.Add(num);
                }

                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        int aliveNeighbors = GetNumberOfAliveNeighbors(_currentGeneration, i, j);
                        if (_currentGeneration[i, j])
                        {
                            if (liveList.Contains(aliveNeighbors))
                            {
                                _nextGeneration[i, j] = true;
                            }
                            else
                            {
                                _nextGeneration[i, j] = false;
                            }
                        }
                        else
                        {
                            if (birthList.Contains(aliveNeighbors))
                            {
                                _nextGeneration[i, j] = true;
                            }
                        }
                    }
                }
            }

            return _nextGeneration;
        }
    }
}