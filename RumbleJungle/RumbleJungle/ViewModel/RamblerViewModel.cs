using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        private readonly GameModel gameModel;

        public double Strength => gameModel.Rambler.Strength;
        public bool Visible => gameModel.Rambler.Visible;
        
        public RamblerViewModel(GameModel gameModel) : base(gameModel.Rambler)
        {
            this.gameModel = gameModel;
            this.gameModel.Rambler.Moved += RamblerMoved;
            this.gameModel.Rambler.HealthChanged += RamblerHealthChanged;
            this.gameModel.Rambler.StrengthChanged += RamblerStrengthChanged;
            this.gameModel.Rambler.VisibleChanged += RamblerVisibleChanged;
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            Update();
        }
        private void RamblerHealthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Health));
        }

        private void RamblerStrengthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Strength));
        }

        private void RamblerVisibleChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Visible));
        }
    }
}
