using Match3.View;
using System;
using System.Windows.Media.Imaging;

namespace Match3.Model
{
    public class Bomb : Figure
    {
        private const int PointsForDestroying = 100;

        public Bomb(Figure figure)
        {
            Position = figure.Position;
            Type = figure.Type;
        }

        public override void Destroy(Figure[,] list)
        {
            if (IsNullObject)
                return;
            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
            ActivateBonus(list);
        }

        private void ActivateBonus(Figure[,] list)
        {
            if (Position.Y > 0)
            {
                list[Position.X, Position.Y - 1].Destroy(list);
                if (Position.X > 0)
                {
                    list[Position.X - 1, Position.Y - 1].Destroy(list);
                }
                if (Position.X < GameWindow.GridSize - 1)
                {
                    list[Position.X + 1, Position.Y - 1].Destroy(list);
                }
            }
            if (Position.Y < GameWindow.GridSize - 1)
            {
                list[Position.X, Position.Y + 1].Destroy(list);
                if (Position.X > 0)
                {
                    list[Position.X - 1, Position.Y + 1].Destroy(list);
                }
                if (Position.X < GameWindow.GridSize - 1)
                {
                    list[Position.X + 1, Position.Y + 1].Destroy(list);
                }
            }
            if (Position.X > 0)
            {
                list[Position.X - 1, Position.Y].Destroy(list);
            }
            if (Position.X < GameWindow.GridSize - 1)
            {
                list[Position.X + 1, Position.Y].Destroy(list);
            }
        }

        public override BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/BombRed.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/BombBlue.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/BombGreen.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/BombYellow.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/BombPink.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Bomb.png");
                    return new BitmapImage(uriSource);
            }
        }
    }
}
