using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        public RamblerViewModel(GameModel gameModel) : base(gameModel.Rambler)
        {
            gameModel.Rambler.Moved += RamblerMoved;
            gameModel.Rambler.HealthChanged += RamblerHealthChanged;
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            Update();
        }
        private void RamblerHealthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Health");
        }
    }
}
