using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public string Name { get; private set; }
        public string Shape => $"/RumbleJungle;component/Images/{Name}.svg";
        public int Count => jungleModel.CountOf(JungleObjectTypes.Treasure);
        public int Total => Configuration.JungleObjectsCount[JungleObjectTypes.Treasure];
        public int Found => Total - Count;

        public TreasureViewModel()
        {
            List<JungleObject> treasures = jungleModel.GetJungleObjects(JungleObjectTypes.Treasure);
            Name = treasures.First().Name;
            foreach (JungleObject treasure in treasures)
            {
                treasure.StatusChanged += StatusChanged;
                treasure.TypeChanged += TypeChanged;
            }
        }

        private void TypeChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Found");
        }

        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Found");
        }
    }
}