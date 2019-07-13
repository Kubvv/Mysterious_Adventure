using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class BeastViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private JungleObjectTypes beastType;

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string Shape => $"/RumbleJungle;component/Images/{Configuration.ShapeOf(beastType)}.svg";

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public BeastViewModel(JungleObjectTypes beastType)
        {
            this.beastType = beastType;
            Name = Enum.GetName(typeof(JungleObjectTypes), beastType);
            Quantity = jungleViewModel.QuantityOf(beastType);
        }
    }
}
