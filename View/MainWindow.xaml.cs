using Match3.Model;
using System.Windows;

namespace Match3.View
{
    public partial class MainWindow : Window
    {
        private readonly Settings _settings;

        public MainWindow()
        {
            InitializeComponent();

            _settings = new Settings(8, 60);

            MenuWindow menuWindow = new MenuWindow(_settings);
            menuWindow.Show();
            Close();
        }
    }
}