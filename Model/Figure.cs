using System.Windows.Media.Imaging;

namespace Match3.Model
{
    public abstract class Figure
    {
        public FigureType Type { get; set; }

        public Vector2 Position { get; set; }

        public bool IsNullObject { get; set; }

        public abstract void Destroy(Figure[,] list);

        public abstract BitmapImage GetBitmapImage();
    }
}
