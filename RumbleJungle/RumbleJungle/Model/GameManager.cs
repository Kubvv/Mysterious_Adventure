using CommonServiceLocator;
using System.Windows;

namespace RumbleJungle.Model
{
    public class GameManager
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();
        private readonly WeaponManager weaponManager = ServiceLocator.Current.GetInstance<WeaponManager>();

        public Rambler Rambler { get; private set; } = new Rambler();
        public Treasure Treasure { get; private set; } = new Treasure();

        public void StartGame()
        {
            jungleManager.GenerateJungle();
            Rambler.Reset();
            Treasure.Reset();
            weaponManager.CollectWeapon();
            jungleManager.ReleaseRambler(Rambler);
        }

        public void MoveRambler(Point point)
        {
            if (point.X >= Rambler.Coordinates.X - 1 && point.X <= Rambler.Coordinates.X + 1 && point.Y >= Rambler.Coordinates.Y - 1 && point.Y <= Rambler.Coordinates.Y + 1)
            {
                Rambler.SetCoordinates(point);
            }
        }
    }
}
