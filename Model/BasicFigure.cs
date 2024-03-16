using System.Windows.Media.Imaging;

namespace Match3.Model
{
    public class BasicFigure : Figure
    {
        private const int PointsForDestroying = 100;

        public BasicFigure(FigureType type, Vector2 position)
        {
            Type = type;
            Position = position;
        }

        public override void Destroy(Figure[,] list)
        {
            if (IsNullObject)
            {
                return;
            }

            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
        }

        public override BitmapImage GetBitmapImage()
        {
            switch (Type)
            {
                case FigureType.Red:
                    return GetBitmapImage("Red");
                case FigureType.Blue:
                    return GetBitmapImage("Blue");
                case FigureType.Green:
                    return GetBitmapImage("Green");
                case FigureType.Yellow:
                    return GetBitmapImage("Yellow");
                case FigureType.Pink:
                    return GetBitmapImage("Pink");
                default:
                    return GetBitmapImage("Yellow");
            }
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}