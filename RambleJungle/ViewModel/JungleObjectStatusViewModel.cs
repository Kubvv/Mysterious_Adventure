using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using RambleJungle.Model;
using System;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class JungleObjectStatusViewModel : ViewModelBase, IDisposable
    {
        private readonly JungleModel jungleModel = SimpleIoc.Default.GetInstance<JungleModel>();
        private readonly JungleObject jungleObject;

        public string Name => jungleObject.Name;
        public FrameworkElement Shape => ShapesModel.GetJungleShape(jungleObject.JungleObjectType, null);
        public int Count => jungleModel.CountOf(jungleObject.JungleObjectType);

        public JungleObjectStatusViewModel(JungleObject firstJungleObject)
        {
            jungleObject = firstJungleObject;
            if (firstJungleObject != null)
            {
                foreach (JungleObject jungleObject in jungleModel.GetJungleObjects(jungleObject.JungleObjectType))
                {
                    jungleObject.StatusChanged += StatusChanged;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (JungleObject jungleObject in jungleModel.GetJungleObjects(jungleObject.JungleObjectType))
                {
                    jungleObject.StatusChanged -= StatusChanged;
                }
            }
        }

        private void StatusChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Count));
        }
    }
}