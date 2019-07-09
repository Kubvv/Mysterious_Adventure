/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:RumbleJungle"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace RumbleJungle.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<JungleViewModel>();
            SimpleIoc.Default.Register<StatusBarViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();
            SimpleIoc.Default.Register<IngameMenuViewModel>();
        }

        public MainViewModel MainInstance => ServiceLocator.Current.GetInstance<MainViewModel>();

        public JungleViewModel JungleInstance => ServiceLocator.Current.GetInstance<JungleViewModel>();

        public StatusBarViewModel StatusBarInstance => ServiceLocator.Current.GetInstance<StatusBarViewModel>();

        public OptionsViewModel OptionsInstance => ServiceLocator.Current.GetInstance<OptionsViewModel>();

        public IngameMenuViewModel IngameMenuInstance => ServiceLocator.Current.GetInstance<IngameMenuViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}