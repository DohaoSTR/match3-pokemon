using Match3.Model;
using System.Windows;

namespace Match3.View
{
    public partial class ResultsWindow : Window
    {
        private Settings _settings { get; set; }

        public ResultsWindow(int finalScore, Settings settings)
        {
            InitializeComponent();

            _settings = settings;
            FinalScore.Text = "Счёт: " + finalScore;
        }

        private void BackInMenu(object sender, RoutedEventArgs e)
        {
            var menu = new MenuWindow(_settings)
            {
                Top = Top,
                Left = Left
            };

            menu.Show();
            Close();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}