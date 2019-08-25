using CommonServiceLocator;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        public double Strength => gameModel.Rambler.Strength;
            public RamblerViewModel(GameModel gameModel) : base(gameModel.Rambler)
        {
            gameModel.Rambler.Moved += RamblerMoved;
            gameModel.Rambler.HealthChanged += RamblerHealthChanged;
            gameModel.Rambler.StrengthChanged += RamblerStrengthChanged;
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            Update();
        }
        private void RamblerHealthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Health");
        }

        private void RamblerStrengthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Strength");
        }
    }
}
