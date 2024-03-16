using Match3.View;
using System.Windows.Media.Imaging;

namespace Match3.Model
{
    public class VerticalLine : Figure
    {
        private const int PointsForDestroying = 100;

        public VerticalLine(Figure figure)
        {
            Position = figure.Position;
            Type = figure.Type;
        }

        public override void Destroy(Figure[,] list)
        {
            if (IsNullObject)
            {
                return;
            }

            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
            ActivateBonus(list);
        }

        private void ActivateBonus(Figure[,] list)
        {
            for (int i = 0; i < GameWindow.GridSize; i++)
            {
                list[Position.X, i].Destroy(list);
            }
        }

        public override BitmapImage GetBitmapImage()
        {
            switch (Type)
            {
                case FigureType.Red:
                    return GetBitmapImage("LineRed");
                case FigureType.Blue:
                    return GetBitmapImage("LineBlue");
                case FigureType.Green:
                    return GetBitmapImage("LineGreen");
                case FigureType.Yellow:
                    return GetBitmapImage("LineYellow");
                case FigureType.Pink:
                    return GetBitmapImage("LinePink");
                default:
                    return GetBitmapImage("Line");
            }
        }
    }
}