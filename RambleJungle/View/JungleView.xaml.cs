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
            if (DataContext is JungleViewModel jungleViewModel)
            {
                jungleViewModel.CanvasHeight = jungle.ActualHeight;
                jungleViewModel.CanvasWidth = jungle.ActualWidth;
            }
        }
    }
}
