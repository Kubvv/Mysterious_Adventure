using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class ItemsViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public ItemsViewModel(JungleObjectTypes itemsType)
        {
            Name = Enum.GetName(typeof(JungleObjectTypes), itemsType);
            Quantity = jungleViewModel.QuantityOf(itemsType);
        }
    }
}
