using Match3.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Match3.View
{
    public partial class GameWindow : Window
    {
        private Settings _settings;

        public static int GridSize;
        public const int CellSizePx = 70;
        public const int CanvasTop = 0;
        public const int CanvasLeft = 0;
        private readonly int TimeForGame;

        private Dictionary<Vector2, Image> _images;
        private Dictionary<Vector2, Button> _buttons;
        private Game _game;
        private GameAnimator _animator;

        private DispatcherTimer _timer;
        private bool _isWindowInitialized = false;
        private int _timeSeconds;

        public void Init()
        {
            switch (GridSize)
            {
                case 4:
                    Height = 395;
                    Width = 295;
                    break;
                case 5:
                    Height = 465;
                    Width = 365;
                    break;
                case 6:
                    Height = 535;
                    Width = 435;
                    break;
                case 7:
                    Height = 605;
                    Width = 505;
                    break;
                case 8:
                    Height = 675;
                    Width = 575;
                    break;
                case 9:
                    Height = 745;
                    Width = 645;
                    break;
                case 10:
                    Height = 815;
                    Width = 715;
                    break;
            }

            _game = new Game(this, GridSize);
            _animator = new GameAnimator();
            _buttons = new Dictionary<Vector2, Button>();
            _images = new Dictionary<Vector2, Image>();

            CreateGridLayout();
            _game.Initialize();
            SetVisuals();

            InitializeCounters();
        }

        public GameWindow(Settings settings)
        {
            InitializeComponent();

            _settings = settings;

            GridSize = settings.GridSize;
            TimeForGame = settings.TimeForGame;

            Init();
        }

        private void ReturnToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Tick -= UpdateTime;
            _timer = new DispatcherTimer();
            CanvasLayout.Children.RemoveRange(1, CanvasLayout.Children.Count - 1);
            _isWindowInitialized = false;

            MenuWindow window = new MenuWindow(_settings);
            window.Show();
            Close();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Tick -= UpdateTime;
            _timer = new DispatcherTimer();

            TimeText.Text = "1";

            CanvasLayout.Children.RemoveRange(1, CanvasLayout.Children.Count - 1);
            _isWindowInitialized = false;

            Init();
        }

        public void MarkSelected(Vector2 buttonIndex)
        {
            _buttons[buttonIndex].Background = new SolidColorBrush(Colors.LightCoral);
        }

        public void MarkDeselected(Vector2 buttonIndex)
        {
            Color color = ((buttonIndex.X + buttonIndex.Y) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
            _buttons[buttonIndex].Background = new SolidColorBrush(color);
        }

        public void DestroyAnimation()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var position = new Vector2(i, j);
                    if (_game.GetFigure(position).IsNullObject)
                    {
                        _animator.DestroyAnimation(_images[position]);
                    }
                }
            }
        }

        public void SwapAnimation(Vector2 firstPosition, Vector2 secondPosition)
        {
            var firstFigure = _images[firstPosition];
            var secondFigure = _images[secondPosition];
            _animator.MoveAnimation(firstFigure, firstPosition, secondPosition);
            _animator.MoveAnimation(secondFigure, secondPosition, firstPosition);
        }

        public void PushDownAnimation(List<Vector2> dropsFrom, List<Vector2> dropsTo)
        {
            for (int i = 0; i < dropsFrom.Count; i++)
            {
                var figure = _images[dropsFrom[i]];
                _animator.MoveAnimation(figure, dropsFrom[i], dropsTo[i]);
            }
        }

        public void SetVisuals()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var position = new Vector2(i, j);
                    if (Game.IsInitialized && _isWindowInitialized)
                        CanvasLayout.Children.Remove(_images[position]);

                    var figure = _game.GetFigure(position);
                    if (figure.IsNullObject)
                        continue;

                    var image = new Image
                    {
                        Source = _game.GetFigure(position).GetBitmapImage(),
                        Width = CellSizePx
                    };
                    Canvas.SetTop(image, CanvasTop + CellSizePx * j);
                    Canvas.SetLeft(image, CanvasLeft + CellSizePx * i);
                    image.IsHitTestVisible = false;
                    CanvasLayout.Children.Add(image);
                    _images[position] = image;
                }
            }
            UpdateScore();
            _isWindowInitialized = true;
        }

        private void UpdateScore()
        {
            var score = _game.GetScore();
            ScoreText.Text = score.ToString();
        }

        private void InitializeCounters()
        {
            Game.NullifyScore();
            UpdateScore();
            _timeSeconds = 0;
            _timer = new DispatcherTimer();
            _timer.Tick += UpdateTime;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            _timeSeconds++;
            if (_timeSeconds >= TimeForGame)
            {
                _timer.Tick -= UpdateTime;
                _timer = new DispatcherTimer();
                var results = new ResultsWindow(_game.GetScore(), _settings)
                {
                    Top = Top,
                    Left = Left
                };
                results.Show();
                Close();
            }
            TimeText.Text = _timeSeconds.ToString();
        }

        private void GridClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = (Vector2)button.DataContext;
            _game.SelectFigure(id);
        }

        private void CreateGridLayout()
        {
            for (int i = 0; i < GridSize; i++)
            {
                var column = new ColumnDefinition
                {
                    Width = new GridLength(CellSizePx)
                };
                var row = new RowDefinition()
                {
                    Height = new GridLength(CellSizePx)
                };
                GridLayout.ColumnDefinitions.Add(column);
                GridLayout.RowDefinitions.Add(row);
            }

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Color color = ((i + j) % 2 == 0) ? Colors.LightGray : Colors.LightGoldenrodYellow;
                    var button = new Button()
                    {
                        Background = new SolidColorBrush(color),
                        BorderThickness = new Thickness(0),
                        DataContext = new Vector2(i, j)
                    };
                    button.Click += GridClick;
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    GridLayout.Children.Add(button);

                    _buttons[new Vector2(i, j)] = button;
                }
            }
        }
    }
}
