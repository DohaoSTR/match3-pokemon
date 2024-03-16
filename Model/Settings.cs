namespace Match3.Model
{
    public class Settings
    {
        public int GridSize { get; set; }

        public int TimeForGame { get; set; }

        public Settings(int gridSize, int timeForGame)
        {
            GridSize = gridSize;
            TimeForGame = timeForGame;
        }
    }
}
