using System.Windows;

namespace RumbleJungle.Model
{
    public class ShapesModel
    {
        //private readonly Dictionary<JungleObjectType, FrameworkElement> jungleShapes;
        //private readonly Dictionary<WeaponType, FrameworkElement> weaponShapes;

        //public ShapesModel()
        //{
        //    jungleShapes = new Dictionary<JungleObjectType, FrameworkElement>();
        //    foreach (JungleObjectType jungleObjectType in Enum.GetValues(typeof(JungleObjectType)))
        //    {
        //        jungleShapes.Add(jungleObjectType, NewJungleShape(jungleObjectType));
        //    }

        //    weaponShapes = new Dictionary<WeaponType, FrameworkElement>();
        //    foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        //    {
        //        weaponShapes.Add(weaponType, NewWeaponShape(weaponType));
        //    }
        //}

        //public FrameworkElement GetJungleShape(JungleObjectType jungleObjectType) => jungleShapes[jungleObjectType];

        //public FrameworkElement GetWeaponShape(WeaponType weaponType) => weaponShapes[weaponType];

        public FrameworkElement GetJungleShape(JungleObjectType jungleObjectType) => NewJungleShape(jungleObjectType);

        public FrameworkElement GetWeaponShape(WeaponType weaponType) => NewWeaponShape(weaponType);

        private FrameworkElement NewJungleShape(JungleObjectType jungleObjectType)
        {
            FrameworkElement result = null;
            switch (jungleObjectType)
            {
                case JungleObjectType.Rambler:
                    result = new Images.Rambler();
                    break;
                case JungleObjectType.DragonflySwarm:
                    result = new Images.DragonflySwarm();
                    break;
                case JungleObjectType.WildPig:
                    result = new Images.WildPig();
                    break;
                case JungleObjectType.Snakes:
                    result = new Images.Snakes();
                    break;
                case JungleObjectType.CarnivorousPlant:
                    result = new Images.CarnivorousPlant();
                    break;
                case JungleObjectType.Minotaur:
                    result = new Images.Minotaur();
                    break;
                case JungleObjectType.Hydra:
                    result = new Images.Hydra();
                    break;
                case JungleObjectType.LostWeapon:
                    result = new Images.LostWeapon();
                    break;
                case JungleObjectType.Elixir:
                    result = new Images.Elixir();
                    break;
                case JungleObjectType.Map:
                    result = new Images.Map();
                    break;
                case JungleObjectType.Radar:
                    result = new Images.Radar();
                    break;
                case JungleObjectType.MagnifyingGlass:
                    result = new Images.MagnifyingGlass();
                    break;
                case JungleObjectType.Talisman:
                    result = new Images.Talisman();
                    break;
                case JungleObjectType.Natives:
                    result = new Images.Natives();
                    break;
                case JungleObjectType.Quicksand:
                    result = new Images.QuickSand();
                    break;
                case JungleObjectType.Trap:
                    result = new Images.Trap();
                    break;
                case JungleObjectType.Treasure:
                    result = new Images.Treasure();
                    break;
                case JungleObjectType.EmptyField:
                    result = null;
                    break;
                case JungleObjectType.Camp:
                    result = new Images.Camp();
                    break;
                case JungleObjectType.Tent:
                    result = new Images.Tent();
                    break;
                case JungleObjectType.ForgottenCity:
                    result = new Images.ForgottenCity();
                    break;
                case JungleObjectType.DenseJungle:
                    result = new Images.DenseJungle();
                    break;
                default:
                    break;
            }
            return result;
        }

        private FrameworkElement NewWeaponShape(WeaponType weaponType)
        {
            FrameworkElement result = null;
            switch (weaponType)
            {
                case WeaponType.Dagger:
                    result = new Images.Dagger();
                    break;
                case WeaponType.Torch:
                    result = new Images.Torch();
                    break;
                case WeaponType.Spear:
                    result = new Images.Spear();
                    break;
                case WeaponType.Machete:
                    result = new Images.Machete();
                    break;
                case WeaponType.Bow:
                    result = new Images.Bow();
                    break;
                case WeaponType.Battleaxe:
                    result = new Images.BattleAxe();
                    break;
                case WeaponType.Sword:
                    result = new Images.Sword();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
