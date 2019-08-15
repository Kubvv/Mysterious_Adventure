using CommonServiceLocator;
using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleObject
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        
        public JungleObjectTypes JungleObjectType { get; private set; }
        public string Name { get; private set; }
        public Point Coordinates { get; private set; }
        public Statuses Status { get; private set; }

        public event EventHandler StatusChanged;

        public JungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectTypes), jungleObjectType);
            Status = Configuration.VisibleItems.Contains(jungleObjectType) || Configuration.DebugMode ? Statuses.Visible : Statuses.Hidden;
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }

        internal void SetStatus(Statuses status)
        {
            Status = status;
            StatusChanged?.Invoke(this, null);
        }

        internal Point Action()
        {
            Point result = Coordinates;

            Random random = new Random();

            if (Configuration.Beasts.Contains(JungleObjectType))
            {
                // battle
            }
            else if (JungleObjectType == JungleObjectTypes.Elixir)
            {
                int healthAdded = random.Next(11) + 25;
                gameModel.Rambler.ChangeHealth(healthAdded);
            }
            else if (JungleObjectType == JungleObjectTypes.Compass)
            {
                Point point = new Point(Coordinates.X - 2, Coordinates.Y - 2);
                for (int column = 0; column < 5; column++)
                {
                    for (int row = 0; row < 5; row++)
                    {
                        point.X = Coordinates.X + column - 2;
                        point.Y = Coordinates.Y + row - 2;
                        jungleModel.SetPointedAt(point, true);
                    }
                }
            }
            else if (JungleObjectType == JungleObjectTypes.Map)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.Talisman)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.MagnifyingGlass)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.LostWeapon)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.Camp)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.Tent)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.ForgottenCity)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.Natives)
            {

            }
            else if (JungleObjectType == JungleObjectTypes.Trap)
            {
                int healthSubtracted = random.Next(11) + 25;
                gameModel.Rambler.ChangeHealth(-healthSubtracted);
            }
            else if (JungleObjectType == JungleObjectTypes.Quicksand)
            {
                // result = random hidden empty field

                int emptyFieldsCount = jungleModel.CountOf(JungleObjectTypes.EmptyField);
                if (emptyFieldsCount > 0)
                {
                    int randomEmptyField = random.Next(emptyFieldsCount) + 1;
                    result = jungleModel.CoordinatesOf(JungleObjectTypes.EmptyField, randomEmptyField);
                }
                gameModel.Rambler.ChangeHealth(-10);
            }
            else if (JungleObjectType == JungleObjectTypes.Treasure)
            {

            }
            return result;
        }
    }
}
