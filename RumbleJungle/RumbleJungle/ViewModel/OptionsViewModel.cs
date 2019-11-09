using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        private int jungleWidth = Config.JungleWidth;
        public int JungleWidth
        {
            get => jungleWidth;
            set => Set(ref jungleWidth, value);
        }

        private int jungleHeight = Config.JungleHeight;
        public int JungleHeight
        {
            get => jungleHeight;
            set => Set(ref jungleHeight, value);
        }

        private RelayCommand saveOptions;
        public RelayCommand SaveOptions => saveOptions ?? (saveOptions = new RelayCommand(() => ExecuteSaveOptions()));

        private void ExecuteSaveOptions()
        {
            Config.SetJungleSize(JungleWidth, JungleHeight);
        }
    }
}
