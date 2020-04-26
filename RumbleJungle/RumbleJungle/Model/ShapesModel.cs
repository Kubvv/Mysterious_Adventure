using System;
using System.Collections.Generic;
using System.Windows;

namespace RumbleJungle.Model
{
    public class ShapesModel
    {
        private readonly Dictionary<JungleObjectType, FrameworkElement> JungleShapes;
        private readonly Dictionary<WeaponType, FrameworkElement> WeaponShapes;

        public ShapesModel()
        {
            JungleShapes = new Dictionary<JungleObjectType, FrameworkElement>();
            foreach (JungleObjectType jungleObjectType in Enum.GetValues(typeof(JungleObjectType)))
            {
                switch (jungleObjectType)
                {
                    case JungleObjectType.Rambler:
                        JungleShapes.Add(jungleObjectType, new Images.Rambler());
                        break;
                    case JungleObjectType.DragonflySwarm:
                        JungleShapes.Add(jungleObjectType, new Images.DragonflySwarm());
                        break;
                    case JungleObjectType.WildPig:
                        JungleShapes.Add(jungleObjectType, new Images.WildPig());
                        break;
                    case JungleObjectType.Snakes:
                        JungleShapes.Add(jungleObjectType, new Images.Snakes());
                        break;
                    case JungleObjectType.CarnivorousPlant:
                        JungleShapes.Add(jungleObjectType, new Images.CarnivorousPlant());
                        break;
                    case JungleObjectType.Minotaur:
                        JungleShapes.Add(jungleObjectType, new Images.Minotaur());
                        break;
                    case JungleObjectType.Hydra:
                        JungleShapes.Add(jungleObjectType, new Images.Hydra());
                        break;
                    case JungleObjectType.LostWeapon:
                        JungleShapes.Add(jungleObjectType, new Images.LostWeapon());
                        break;
                    case JungleObjectType.Elixir:
                        JungleShapes.Add(jungleObjectType, new Images.Elixir());
                        break;
                    case JungleObjectType.Map:
                        JungleShapes.Add(jungleObjectType, new Images.Map());
                        break;
                    case JungleObjectType.Radar:
                        JungleShapes.Add(jungleObjectType, new Images.Radar());
                        break;
                    case JungleObjectType.MagnifyingGlass:
                        JungleShapes.Add(jungleObjectType, new Images.Rambler());
                        break;
                    case JungleObjectType.Talisman:
                        JungleShapes.Add(jungleObjectType, new Images.Talisman());
                        break;
                    case JungleObjectType.Natives:
                        JungleShapes.Add(jungleObjectType, new Images.Natives());
                        break;
                    case JungleObjectType.Quicksand:
                        JungleShapes.Add(jungleObjectType, new Images.QuickSand());
                        break;
                    case JungleObjectType.Trap:
                        JungleShapes.Add(jungleObjectType, new Images.Trap());
                        break;
                    case JungleObjectType.Treasure:
                        JungleShapes.Add(jungleObjectType, new Images.Treasure());
                        break;
                    case JungleObjectType.EmptyField:
                        JungleShapes.Add(jungleObjectType, null);
                        break;
                    case JungleObjectType.Camp:
                        JungleShapes.Add(jungleObjectType, new Images.Camp());
                        break;
                    case JungleObjectType.Tent:
                        JungleShapes.Add(jungleObjectType, new Images.Tent());
                        break;
                    case JungleObjectType.ForgottenCity:
                        JungleShapes.Add(jungleObjectType, new Images.ForgottenCity());
                        break;
                    case JungleObjectType.DenseJungle:
                        JungleShapes.Add(jungleObjectType, new Images.DenseJungle());
                        break;
                    default:
                        break;
                }

                WeaponShapes = new Dictionary<WeaponType, FrameworkElement>();
                foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
                {
                    switch (weaponType)
                    {
                        case WeaponType.Dagger:
                            WeaponShapes.Add(weaponType, new Images.Dagger());
                            break;
                        case WeaponType.Torch:
                            WeaponShapes.Add(weaponType, new Images.Torch());
                            break;
                        case WeaponType.Spear:
                            WeaponShapes.Add(weaponType, new Images.Spear());
                            break;
                        case WeaponType.Machete:
                            WeaponShapes.Add(weaponType, new Images.Machete());
                            break;
                        case WeaponType.Bow:
                            WeaponShapes.Add(weaponType, new Images.Bow());
                            break;
                        case WeaponType.Battleaxe:
                            WeaponShapes.Add(weaponType, new Images.BattleAxe());
                            break;
                        case WeaponType.Sword:
                            WeaponShapes.Add(weaponType, new Images.Sword());
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public FrameworkElement GetJungleShape(JungleObjectType jungleObjectType) => JungleShapes[jungleObjectType];

        public FrameworkElement GetWeaponShape(WeaponType weaponType) => WeaponShapes[weaponType];
    }
}
