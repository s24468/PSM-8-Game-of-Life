using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Shapes;

namespace PSM_8
{
    public partial class MainWindow : Window
    {
        private const int Size = 20;
        private readonly Rectangle[,] _cells = new Rectangle[Size, Size];
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private bool[,] _currentGeneration = new bool[Size, Size];
        private bool[,] _nextGeneration = new bool[Size, Size];
        private Algorithms _algorithms = new Algorithms(Size);

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = 20,
                        Height = 20,
                        Fill = System.Windows.Media.Brushes.White,
                        Stroke = System.Windows.Media.Brushes.Black,
                        StrokeThickness = 1
                    };
                    Canvas.SetLeft(rect, i * 20);
                    Canvas.SetTop(rect, j * 20);
                    canvas.Children.Add(rect);
                    _cells[i, j] = rect;
                }
            }

            _currentGeneration[2, 1] = true;
            _currentGeneration[2, 2] = true;
            _currentGeneration[2, 3] = true;
            _currentGeneration[1, 2] = true;
            _currentGeneration[0, 1] = true;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_currentGeneration[i, j])
                    {
                        _cells[i, j].Fill = System.Windows.Media.Brushes.Black;
                    }
                }
            }

            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = inputTextBox.Text;
            Console.WriteLine(
                inputText); // Wyświetla wprowadzony tekst w konsoli. Można go wykorzystać w dowolny sposób.
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _currentGeneration[2, 2] = true;
            _currentGeneration[2, 3] = true;
            _currentGeneration[1, 2] = true;
            _currentGeneration[0, 1] = true;

            _timer.Stop();
            _currentGeneration = new bool[Size, Size];
            _nextGeneration = new bool[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _cells[i, j].Fill = System.Windows.Media.Brushes.White;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Calculate the next generation based on the current generation
            
            _nextGeneration = _algorithms.CalculateNextGeneration(_currentGeneration,inputTextBox);

            // Update the cells on the canvas
            _algorithms.UpdateCells(_nextGeneration, _cells);
            // Update the current generation with the next generation
            _currentGeneration = (bool[,])_nextGeneration.Clone();
        }
        
    }
}