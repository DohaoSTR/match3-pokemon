using Match3.Model;
using System.Windows;

namespace Match3.View
{
    public partial class MenuWindow : Window
    {
        private Settings _settings;

        public MenuWindow(Settings settings)
        {
            InitializeComponent();

            _settings = settings;
        }

        private void GoPlay(object sender, RoutedEventArgs routedEventArgs)
        {
            var game = new GameWindow(_settings)
            {
                Top = Top,
                Left = Left
            };
            game.Show();
            Close();
        }

        private void GoSettings(object sender, RoutedEventArgs routedEventArgs)
        {
            SettingsWindow settingsWindow = new SettingsWindow(_settings);
            settingsWindow.Show();
            Close();
        }

        private void Exit(object sender, RoutedEventArgs routedEventArgs)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
