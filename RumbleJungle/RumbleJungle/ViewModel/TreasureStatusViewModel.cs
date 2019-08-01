using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RumbleJungle.ViewModel
{
    public class TreasureStatusViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        private JungleObject treasure;

        public string Name => treasure.Name;
        public string Shape => $"/RumbleJungle;component/Images/{treasure.Name}.svg";
        public int Count => jungleModel.CountOf(treasure.JungleObjectType);
        public int Total => Configuration.JungleObjectsCount[JungleObjectTypes.Treasure];
        public int Found => Total - Count;

        public TreasureStatusViewModel()
        {
            treasure = jungleModel.GetJungleObjects(new List<JungleObjectTypes>() { JungleObjectTypes.Treasure }).FirstOrDefault();
            foreach (JungleObject jungleObject in jungleModel.GetJungleObjects(treasure.JungleObjectType))
            {
                jungleObject.StatusChanged += StatusChanged;
            }
        }

        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Found");
        }
    }
}