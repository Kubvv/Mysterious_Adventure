using System;
using System.Collections.Generic;

namespace RumbleJungle.Model
{
    public class WeaponModel
    {
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public WeaponModel()
        {
            foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                Weapons.Add(new Weapon(weaponType));
            }
        }
        public void CollectWeapon()
        {
            Weapons.ForEach(weapon => weapon.Reset());
        }

        public Weapon RandomWeapon()
        {
            int randomWeapon = Config.Random.Next(Weapons.Count - 1) + 1;
            return Weapons[randomWeapon];
        }

        public void ChangeRandomWeaponCount(int quantity)
        {
            RandomWeapon().ChangeCount(quantity);
        }

        public void ChangeAllWeaponsCount(int quantity)
        {
            Weapons.ForEach(weapon => weapon.ChangeCount(quantity));
        }

        public void SetDoubleAttack()
        {
            RandomWeapon().SetDoubleAttack(true);
        }
    }
}
