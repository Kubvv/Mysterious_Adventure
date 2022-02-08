using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using RambleJungle.Model;
using System;

namespace RambleJungle.ViewModel
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
            Ioc.Default.ConfigureServices(new ServiceCollection()
                .AddSingleton<GameModel>()
                .AddSingleton<JungleModel>()
                .AddSingleton<WeaponModel>()
                .AddSingleton<MainViewModel>()
                .AddSingleton<OptionsViewModel>()
                .AddSingleton<JungleViewModel>()
                .AddSingleton<JungleObjectViewModel>()
                .AddSingleton<ActionViewModel>()
                .AddSingleton<StatusBarViewModel>()
                .AddSingleton<WeaponViewModel>()
                .AddSingleton<RamblerViewModel>()
                .AddSingleton<TreasureViewModel>()
                .BuildServiceProvider());
        }

        public MainViewModel MainInstance => Ioc.Default.GetService<MainViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(MainViewModel)));
        public OptionsViewModel OptionsInstance => Ioc.Default.GetService<OptionsViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(OptionsViewModel)));
        public JungleViewModel JungleInstance => Ioc.Default.GetService<JungleViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(JungleViewModel)));
        public ActionViewModel ActionInstance => Ioc.Default.GetService<ActionViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(ActionViewModel)));
        public JungleObjectViewModel JungleObjectInstance => Ioc.Default.GetService<JungleObjectViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(JungleObjectViewModel)));
        public StatusBarViewModel StatusBarInstance => Ioc.Default.GetService<StatusBarViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(StatusBarViewModel)));
        public WeaponViewModel WeaponInstance => Ioc.Default.GetService<WeaponViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(WeaponViewModel)));
        public RamblerViewModel RamblerInstance => Ioc.Default.GetService<RamblerViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(RamblerViewModel)));
        public TreasureViewModel TreasureInstance => Ioc.Default.GetService<TreasureViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(TreasureViewModel)));

        public static void Cleanup()
        {
        }
    }
}