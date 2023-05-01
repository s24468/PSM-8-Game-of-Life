using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using static System.Int32;

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
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    cells[i, j].Fill = nextGeneration[i, j]
                        ? System.Windows.Media.Brushes.Black
                        : System.Windows.Media.Brushes.White;
                }
            }
        }

        private int GetNumberOfAliveNeighbors(bool[,] currentGeneration, int xCoordinate, int yCoordinate)
        {
            var aliveNeighbors = 0;
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    var ii = RoundIfNegativeNumbers(xCoordinate + x) % Size;
                    var jj = RoundIfNegativeNumbers(yCoordinate + y) % Size;
                    if (currentGeneration[ii, jj])
                    {
                        aliveNeighbors++;
                    }
                }
            }

            return aliveNeighbors;
        }

        private int RoundIfNegativeNumbers(int x)
        {
            if (x < 0)
            {
                return Size - 1;
            }

            return x;
        }

        public bool[,] InitializeGeneration(TextBox inputTextBox)
        {
            var generation = new bool[Size, Size];

            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                var coordinates = new List<(int, int)>();

                var pairs = inputTextBox.Text.Split(' ');
                foreach (var pair in pairs)
                {
                    var nums = pair.Split(',');
                    coordinates.Add((Parse(nums[0]), Parse(nums[1])));
                }

                foreach (var (x, y) in coordinates)
                {
                    generation[x, y] = true;
                }
            }

            return generation;
        }

        public bool[,] CalculateNextGeneration(bool[,] currentGeneration, TextBox inputTextBox)
        {
            var nextGeneration = new bool[Size, Size];
            if (nextGeneration == null) throw new Exception(nameof(nextGeneration));
            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                var (liveList, birthList) = CreateLiveBirthList(inputTextBox);

                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        var aliveNeighbors = GetNumberOfAliveNeighbors(currentGeneration, i, j);
                        if (currentGeneration[i, j])
                        {
                            if (liveList.Contains(aliveNeighbors))
                            {
                                nextGeneration[i, j] = true;
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

        private static (List<int> liveList, List<int> birthList) CreateLiveBirthList(TextBox inputTextBox)
        {
            var boxText = inputTextBox.Text.Split('/') ?? throw new Exception("inputTextBox.Text.Split(\'/\')");
            var liveList = new List<int>();
            var birthList = new List<int>();

            foreach (var digit in boxText[0])
            {
                var num = Parse(digit.ToString());
                if (!liveList.Contains(num))
                {
                    liveList.Add(num);
                }
            }

            foreach (var digit in boxText[1])
            {
                birthList.Add(Parse(digit.ToString()));
            }

            return (liveList, birthList);
        }
    }
}