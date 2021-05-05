using RambleJungle.ViewModel;
using System.Windows;

namespace RambleJungle.View
{
    /// <summary>
    /// Logika interakcji dla klasy JungleView.xaml
    /// </summary>
    public partial class JungleView : Window
    {
        public JungleView()
        {
            InitializeComponent();
        }

        private void Jungle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            (DataContext as JungleViewModel).CanvasHeight = jungle.ActualHeight;
            (DataContext as JungleViewModel).CanvasWidth = jungle.ActualWidth;
        }
    }
}
