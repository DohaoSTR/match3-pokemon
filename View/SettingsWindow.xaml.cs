using Match3.Model;
using System;
using System.Windows;

namespace Match3.View
{
    public partial class SettingsWindow : Window
    {
        private Settings _settings;

        public SettingsWindow(Settings settings)
        {
            InitializeComponent();

            _settings = settings;

            SizeChooseBox.Items.Add("4x4");
            SizeChooseBox.Items.Add("5x5");
            SizeChooseBox.Items.Add("6x6");
            SizeChooseBox.Items.Add("7x7");
            SizeChooseBox.Items.Add("8x8");
            SizeChooseBox.Items.Add("9x9");
            SizeChooseBox.Items.Add("10x10");

            switch (settings.GridSize)
            {
                case 4:
                    SizeChooseBox.SelectedIndex = 0;
                    break;
                case 5:
                    SizeChooseBox.SelectedIndex = 1;
                    break;
                case 6:
                    SizeChooseBox.SelectedIndex = 2;
                    break;
                case 7:
                    SizeChooseBox.SelectedIndex = 3;
                    break;
                case 8:
                    SizeChooseBox.SelectedIndex = 4;
                    break;
                case 9:
                    SizeChooseBox.SelectedIndex = 5;
                    break;
                case 10:
                    SizeChooseBox.SelectedIndex = 6;
                    break;
            }

            GameTimeTextBox.Text = settings.TimeForGame.ToString();
        }

        public void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int gridSize = 8, gameTime;

            switch (SizeChooseBox.SelectedItem)
            {
                case "4x4":
                    gridSize = 4;
                    break;
                case "5x5":
                    gridSize = 5;
                    break;
                case "6x6":
                    gridSize = 6;
                    break;
                case "7x7":
                    gridSize = 7;
                    break;
                case "8x8":
                    gridSize = 8;
                    break;
                case "9x9":
                    gridSize = 9;
                    break;
                case "10x10":
                    gridSize = 10;
                    break;
            }

            if (string.IsNullOrEmpty(GameTimeTextBox.Text) && string.IsNullOrWhiteSpace(GameTimeTextBox.Text))
            {
                MessageBox.Show("Введите время игры!");
                return;
            }

            try
            {
                gameTime = Convert.ToInt32(GameTimeTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Время игры должно быть целочисленным значением, в секундах!");
                return;
            }

            if (gameTime < 10)
            {
                MessageBox.Show("Время игры не может быть меньше 10 секунд!");
                return;
            }

            Settings settings = new Settings(gridSize, gameTime);
            _settings = settings;

            MenuWindow menuWindow = new MenuWindow(settings);
            menuWindow.Show();
            Close();
        }

        public void ReturnToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow(_settings);
            menuWindow.Show();
            Close();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
