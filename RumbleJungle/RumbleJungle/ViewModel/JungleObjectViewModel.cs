using GalaSoft.MvvmLight;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private int jungleRow;
        public int JungleRow
        {
            get => jungleRow;
            set => Set(ref jungleRow, value);
        }

        private int jungleCol;
        public int JungleCol
        {
            get => jungleCol;
            set => Set(ref jungleCol, value);
        }

        public JungleObjectViewModel(Point point)
        {
            Name = $"{point.Y}-{point.X}";
            JungleRow = (int)point.Y;
            JungleCol = (int)point.X;
        }
    }
}