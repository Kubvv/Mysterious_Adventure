using GalaSoft.MvvmLight;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string Shape => $"/RumbleJungle;component/Images/{Configuration.ShapeOf(JungleObjectTypes.Treasure)}.svg";

        private int foundQuantity;
        public int FoundQuantity
        {
            get => foundQuantity;
            set => Set(ref foundQuantity, value);
        }

        public int TotalQuantity => Configuration.TreasureCount;

        public TreasureViewModel()
        {
            Name = "Treasure";
            FoundQuantity = 0;
        }
    }
}
