using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab_2
{
    public partial class Game2048View : Window
    {
        private GameLogic _game;
        private Dictionary<int, Color> _tileColors;

        public Game2048View()
        {
            InitializeComponent();
            InitializeTileColors();

            _game = new GameLogic();
            NewGame_Click(null, null);
        }

        private void InitializeTileColors()
        {
            _tileColors = new Dictionary<int, Color>
            {
                { 2, (Color)ColorConverter.ConvertFromString("#EEE4DA") },
                { 4, (Color)ColorConverter.ConvertFromString("#EDE0C8") },
                { 8, (Color)ColorConverter.ConvertFromString("#F2B179") },
                { 16, (Color)ColorConverter.ConvertFromString("#F59563") },
                { 32, (Color)ColorConverter.ConvertFromString("#F67C5F") },
                { 64, (Color)ColorConverter.ConvertFromString("#F65E3B") },
                { 128, (Color)ColorConverter.ConvertFromString("#EDCF72") },
                { 256, (Color)ColorConverter.ConvertFromString("#EDCC61") },
                { 512, (Color)ColorConverter.ConvertFromString("#EDC850") },
                { 1024, (Color)ColorConverter.ConvertFromString("#EDC53F") },
                { 2048, (Color)ColorConverter.ConvertFromString("#EDC22E") }
            };
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            GameOverOverlay.Visibility = Visibility.Collapsed;
            _game.StartNewGame();
            UpdateBoardUI();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Direction? direction = null;
            switch (e.Key)
            {
                case Key.Up: direction = Direction.Up; break;
                case Key.Down: direction = Direction.Down; break;
                case Key.Left: direction = Direction.Left; break;
                case Key.Right: direction = Direction.Right; break;
            }

            if (direction.HasValue)
            {
                bool boardChanged = _game.Move(direction.Value);
                if (boardChanged)
                {
                    UpdateBoardUI();
                }
            }
        }

        private void UpdateBoardUI()
        {
            GameCanvas.Children.Clear();
            DrawBackgroundGrid();

            double canvasSize = GameCanvas.Width;

            double cellSize = canvasSize / _game.BoardSize;
            double padding = cellSize * 0.05;
            double tileSize = cellSize - 2 * padding;

            for (int r = 0; r < _game.BoardSize; r++)
            {
                for (int c = 0; c < _game.BoardSize; c++)
                {
                    int value = _game.Board[r, c];
                    if (value > 0)
                    {
                        Border tile = CreateTile(value, tileSize);
                        Canvas.SetLeft(tile, c * cellSize + padding);
                        Canvas.SetTop(tile, r * cellSize + padding);
                        GameCanvas.Children.Add(tile);
                    }
                }
            }

            ScoreText.Text = _game.Score.ToString();

            if (_game.IsGameOver)
            {
                GameOverOverlay.Visibility = Visibility.Visible;
            }
        }

        private void DrawBackgroundGrid()
        {
            double canvasSize = GameCanvas.Width;

            double cellSize = canvasSize / _game.BoardSize;
            double padding = cellSize * 0.05;
            double tileSize = cellSize - 2 * padding;

            for (int r = 0; r < _game.BoardSize; r++)
            {
                for (int c = 0; c < _game.BoardSize; c++)
                {
                    var backgroundTile = new Border
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CDC1B4")),
                        CornerRadius = new CornerRadius(3)
                    };
                    Canvas.SetLeft(backgroundTile, c * cellSize + padding);
                    Canvas.SetTop(backgroundTile, r * cellSize + padding);
                    GameCanvas.Children.Add(backgroundTile);
                }
            }
        }
        private Border CreateTile(int value, double size)
        {
            Color backgroundColor = _tileColors.ContainsKey(value) ? _tileColors[value] : _tileColors[2048];
            Color foregroundColor = (value < 8) ? (Color)ColorConverter.ConvertFromString("#776E65") : Colors.White;
            int fontSize = (value < 100) ? 48 : (value < 1000) ? 36 : 24;

            var textBlock = new TextBlock
            {
                Text = value.ToString(),
                Foreground = new SolidColorBrush(foregroundColor),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var tile = new Border
            {
                Width = size,
                Height = size,
                Background = new SolidColorBrush(backgroundColor),
                CornerRadius = new CornerRadius(3),
                Child = textBlock
            };

            return tile;
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            UpdateBoardUI();
        }
    }
}
