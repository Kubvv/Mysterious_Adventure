using System.Windows;

namespace RumbleJungle.View
{
    /// <summary>
    /// Logika interakcji dla klasy OptionsView.xaml
    /// </summary>
    public partial class OptionsView : Window
    {
        public OptionsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
