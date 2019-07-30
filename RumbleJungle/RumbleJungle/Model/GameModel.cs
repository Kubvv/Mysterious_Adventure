using CommonServiceLocator;
using System.Windows;

namespace RumbleJungle.Model
{
    public class GameModel
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly WeaponModel weaponModel = ServiceLocator.Current.GetInstance<WeaponModel>();

        public Rambler Rambler { get; private set; } = new Rambler();

        public void StartGame()
        {
            jungleModel.GenerateJungle();
            Rambler.Reset();
            weaponModel.CollectWeapon();
            jungleModel.ReleaseRambler(Rambler);
        }

        public void MoveRambler(Point point)
        {
            JungleObject jungleObject = jungleModel.GetJungleObjectAt(point);
            if (point.X >= Rambler.Coordinates.X - 1 && point.X <= Rambler.Coordinates.X + 1 && point.Y >= Rambler.Coordinates.Y - 1 && point.Y <= Rambler.Coordinates.Y + 1 
                && jungleObject.JungleObjectType != JungleObjectTypes.DenseJungle)
            {
                jungleObject.SetStatus(Statuses.Visited);
                //Tu wywołamy akcje;
                Rambler.SetCoordinates(point);

            }
        }
    }
}
