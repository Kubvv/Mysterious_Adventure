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

        private string quantity;
        public string Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public TreasureViewModel(JungleObjectTypes treasureType)
        {
            Name = "Treasure";
            Quantity = jungleViewModel.QuantityOfTreasure(treasureType);
        }
    }
}
