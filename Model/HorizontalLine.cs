using Match3.View;
using System;
using System.Windows.Media.Imaging;

namespace Match3.Model
{
    public class HorizontalLine : Figure
    {
        private const int PointsForDestroying = 100;

        public HorizontalLine(Figure figure)
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
            for (int i = 0; i < GameWindow.GridSize; i++)
            {
                list[i, Position.Y].Destroy(list);
            }
        }

        public override BitmapImage GetBitmapImage()
        {
            Uri uriSource;
            switch (Type)
            {
                case FigureType.Red:
                    uriSource = new Uri(@"pack://application:,,,/img/LineRedH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Blue:
                    uriSource = new Uri(@"pack://application:,,,/img/LineBlueH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Green:
                    uriSource = new Uri(@"pack://application:,,,/img/LineGreenH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Yellow:
                    uriSource = new Uri(@"pack://application:,,,/img/LineYellowH.png");
                    return new BitmapImage(uriSource);
                case FigureType.Pink:
                    uriSource = new Uri(@"pack://application:,,,/img/LinePinkH.png");
                    return new BitmapImage(uriSource);
                default:
                    uriSource = new Uri(@"pack://application:,,,/img/Line.png");
                    return new BitmapImage(uriSource);
            }
        }
    }
}
