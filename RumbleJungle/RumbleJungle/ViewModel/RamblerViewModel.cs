using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        private readonly GameModel gameModel;

        public int Health => gameModel.Rambler.Health;

        public RamblerViewModel(GameModel gameModel) : base(gameModel.Rambler)
        {
            this.gameModel = gameModel;
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
