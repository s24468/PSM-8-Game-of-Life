using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PSM_8
{
    public partial class MainWindow : Window
    {
        private const int Size = 20;
        private readonly Rectangle[,] _cells = new Rectangle[Size, Size];
        private readonly DispatcherTimer _timer = new();
        private bool[,] _currentGeneration = new bool[Size, Size];
        private bool[,] _nextGeneration = new bool[Size, Size];
        private readonly Algorithms _algorithms = new(Size);

        public MainWindow()
        {
            InitializeComponent();

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    var rect = new Rectangle
                    {
                        Width = 20,
                        Height = 20,
                        Fill = System.Windows.Media.Brushes.White,
                        Stroke = System.Windows.Media.Brushes.Black,
                        StrokeThickness = 1,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };
                    Canvas.SetLeft(rect, i * 20);
                    Canvas.SetTop(rect, j * 20);
                    canvas.Children.Add(rect);
                    _cells[i, j] = rect;
                }
            }


            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _currentGeneration = _algorithms.InitializeGeneration(InitializeTextBox);
            _timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            // Calculate the next generation based on the current generation
            _nextGeneration = _algorithms.CalculateNextGeneration(_currentGeneration, RulesTextBox);
            // Update the cells on the canvas
            _algorithms.UpdateCells(_nextGeneration, _cells);
            // Update the current generation with the next generation
            _currentGeneration = (bool[,])_nextGeneration.Clone();
        }
    }
}