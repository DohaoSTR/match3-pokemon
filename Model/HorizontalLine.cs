using Match3.View;
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
                list[i, Position.Y].Destroy(list);
            }
        }

        public override BitmapImage GetBitmapImage()
        {
            switch (Type)
            {
                case FigureType.Red:
                    return GetBitmapImage("LineRedH");
                case FigureType.Blue:
                    return GetBitmapImage("LineBlueH");
                case FigureType.Green:
                    return GetBitmapImage("LineGreenH");
                case FigureType.Yellow:
                    return GetBitmapImage("LineYellowH");
                case FigureType.Pink:
                    return GetBitmapImage("LinePinkH");
                default:
                    return GetBitmapImage("Line");
            }
        }
    }
}