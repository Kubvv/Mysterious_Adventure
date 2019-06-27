using GalaSoft.MvvmLight;

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

        public JungleObjectViewModel(int row, int col)
        {
            name = $"{row}-{col}";
            jungleRow = row;
            jungleCol = col;
        }
    }
}