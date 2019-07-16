using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class ItemsViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private JungleObjectTypes itemType;

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string Shape => $"/RumbleJungle;component/Images/{Configuration.ShapeOf(itemType)}.svg";

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public ItemsViewModel(JungleObjectTypes itemType)
        {
            this.itemType = itemType;
            Name = Enum.GetName(typeof(JungleObjectTypes), itemType);
            Quantity = jungleViewModel.QuantityOf(itemType);
        }
    }
}
