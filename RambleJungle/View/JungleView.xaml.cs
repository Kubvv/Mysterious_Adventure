using RambleJungle.Base.Tools;
using RambleJungle.ViewModel;
using System;
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
            SourceInitialized += Jungle_SourceInitialized;
        }

        private void Jungle_SourceInitialized(object? sender, EventArgs e)
        {
            Monitor monitor = Monitor.BiggestMonitor();
            Top = monitor.WorkingArea.Top;
            Left = monitor.WorkingArea.Left;
            WindowState = WindowState.Maximized;
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
