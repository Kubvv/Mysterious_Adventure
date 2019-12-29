using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectStatusViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly JungleObject jungleObject;

        public string Name => jungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{jungleObject.Name}.svg";
        public int Count => jungleModel.CountOf(jungleObject.JungleObjectType);

        public JungleObjectStatusViewModel(JungleObject firstJungleObject)
        {
            jungleObject = firstJungleObject;
            if (firstJungleObject != null)
            {
                foreach (JungleObject jungleObject in jungleModel.GetJungleObjects(firstJungleObject.JungleObjectType))
                {
                    jungleObject.StatusChanged += StatusChanged;
                }
            }
        }

        // TODO: odpiąć zdarzenia

        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Count));
        }
    }
}