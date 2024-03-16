using Match3.View;
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
            {
                return;
            }

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
                    return GetBitmapImage("Bomb");
            }
        }
    }
}