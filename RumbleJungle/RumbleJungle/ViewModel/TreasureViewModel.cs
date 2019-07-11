using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private int foundQuantity;
        public int FoundQuantity
        {
            get => foundQuantity;
            set => Set(ref foundQuantity, value);
        }

        public int TotalQuantity => Configuration.TreasureCount;

        public TreasureViewModel(JungleObjectTypes treasureType)
        {
            Name = "Treasure";
            FoundQuantity = 0;
        }
    }
}
