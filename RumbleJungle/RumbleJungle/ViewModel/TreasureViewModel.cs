using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel;

        public string Name { get; private set; }
        public string Shape => $"/RumbleJungle;component/Images/{Name}.svg";
        public int Count => jungleModel.CountOf(JungleObjectType.Treasure);
        public static int Total => Config.JungleObjectsCount[JungleObjectType.Treasure];
        public int Found => Total - Count;

        public TreasureViewModel(JungleModel jungleModel)
        {
            this.jungleModel = jungleModel;

            if (jungleModel != null)
            {
                List<JungleObject> treasures = jungleModel.GetJungleObjects(JungleObjectType.Treasure);
                Name = treasures.First().Name;
                foreach (JungleObject treasure in treasures)
                {
                    treasure.StatusChanged += StatusChanged;
                }
                foreach (JungleObject jungleObject in jungleModel.Jungle)
                {
                    jungleObject.TypeChanged += TypeChanged;
                }
            }
        }

        private void TypeChanged(object sender, EventArgs e)
        {
            JungleObject jungleObject = (JungleObject)sender;
            if (jungleObject.JungleObjectType == JungleObjectType.Treasure)
            {
                jungleObject.StatusChanged += StatusChanged;
                RaisePropertyChanged(nameof(Found));
            }
        }

        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Found));
        }
    }
}