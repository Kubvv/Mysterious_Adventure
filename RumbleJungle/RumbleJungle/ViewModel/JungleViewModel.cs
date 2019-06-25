using GalaSoft.MvvmLight;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private Jungle jungle = new Jungle();

        internal void StartGame()
        {
            jungle.Generate();
        }
    }
}
