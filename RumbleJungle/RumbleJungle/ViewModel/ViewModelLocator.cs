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
using GalaSoft.MvvmLight.Ioc;
using RumbleJungle.Model;

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

            SimpleIoc.Default.Register<GameModel>();
            SimpleIoc.Default.Register<JungleModel>();
            SimpleIoc.Default.Register<WeaponModel>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IngameMenuViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();

            SimpleIoc.Default.Register<JungleViewModel>();
            SimpleIoc.Default.Register<JungleObjectViewModel>();
            SimpleIoc.Default.Register<ActionViewModel>();
            SimpleIoc.Default.Register<StatusBarViewModel>();
            SimpleIoc.Default.Register<WeaponViewModel>();
            SimpleIoc.Default.Register<RamblerViewModel>();
            SimpleIoc.Default.Register<TreasureStatusViewModel>();
        }

        public MainViewModel MainInstance => ServiceLocator.Current.GetInstance<MainViewModel>();
        public IngameMenuViewModel IngameMenuInstance => ServiceLocator.Current.GetInstance<IngameMenuViewModel>();
        public OptionsViewModel OptionsInstance => ServiceLocator.Current.GetInstance<OptionsViewModel>();
        public JungleViewModel JungleInstance => ServiceLocator.Current.GetInstance<JungleViewModel>();
        public ActionViewModel ActionInstance => ServiceLocator.Current.GetInstance<ActionViewModel>();
        public JungleObjectViewModel JungleObjectInstance => ServiceLocator.Current.GetInstance<JungleObjectViewModel>();
        public StatusBarViewModel StatusBarInstance => ServiceLocator.Current.GetInstance<StatusBarViewModel>();
        public WeaponViewModel WeaponInstance => ServiceLocator.Current.GetInstance<WeaponViewModel>();
        public RamblerViewModel RamblerInstance => ServiceLocator.Current.GetInstance<RamblerViewModel>();
        public TreasureStatusViewModel TreasureInstance => ServiceLocator.Current.GetInstance<TreasureStatusViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}