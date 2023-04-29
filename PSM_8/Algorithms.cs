using System;
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

        public int GetNumberOfAliveNeighbors(bool[,] _currentGeneration, int xCoordinate, int yCoordinate)
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
                    if (ii >= 0 && ii < Size && jj >= 0 && jj < Size && _currentGeneration[ii, jj]) // ZWYKŁA TABLICA
                    {
                        aliveNeighbors++;
                    }
                }
            }

            return aliveNeighbors;
        }

        public bool[,] innitializeGeneration(TextBox inputTextBox)
        {
            var generation = new bool[Size, Size];
            var coordinatesX = new List<int>();
            var coordinatesY = new List<int>();

            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                var pairs = inputTextBox.Text.Split(' ');
                foreach (var pair in pairs)
                {
                    var nums = pair.Split(',');
                    coordinatesX.Add(int.Parse(nums[0]));
                    coordinatesY.Add(int.Parse(nums[1]));
                }

                for (int i = 0; i < coordinatesX.Count; i++)
                {
                    var x = coordinatesX[i];
                    var y = coordinatesY[i];
                    generation[x,y] = true;
                }
            }

            return generation;
        }

        public bool[,] CalculateNextGeneration(bool[,] _currentGeneration, TextBox inputTextBox)
        {
            var nextGeneration = new bool[Size, Size];
            if (nextGeneration == null) throw new ArgumentNullException(nameof(nextGeneration));
            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                var (liveList, birthList) = CreateLiveBirthList(inputTextBox);

                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        var aliveNeighbors = GetNumberOfAliveNeighbors(_currentGeneration, i, j);
                        if (_currentGeneration[i, j])
                        {
                            if (liveList.Contains(aliveNeighbors))
                            {
                                nextGeneration[i, j] = true;
                            }
                            else
                            {
                                nextGeneration[i, j] = false;
                            }
                        }
                        else
                        {
                            if (birthList.Contains(aliveNeighbors))
                            {
                                nextGeneration[i, j] = true;
                            }
                        }
                    }
                }
            }

            return nextGeneration;
        }

        public (List<int> liveList, List<int> birthList) CreateLiveBirthList(TextBox inputTextBox)
        {
            var box_Text = inputTextBox.Text.Split('/');
            var liveList = new List<int>();
            var birthList = new List<int>();

            foreach (char digit in box_Text[0])
            {
                var num = int.Parse(digit.ToString());
                if (!liveList.Contains(num))
                {
                    liveList.Add(num);
                }
            }

            foreach (var digit in box_Text[1])
            {
                birthList.Add(int.Parse(digit.ToString()));
            }

            return (liveList, birthList);
        }
    }
}


// var generation = new bool[Size, Size];
// var coordinates = new List<(int, int)>();
//
// var pairs = inputTextBox.Text.Split(' ');
// foreach (var pair in pairs)
// {
//     var nums = pair.Split(',');
//     coordinates.Add((int.Parse(nums[0]), int.Parse(nums[1])));
// }
//
// foreach (var (x, y) in coordinates)
// {
//     generation[x][y] = true;
// }