using System;
using System.Collections.Generic;

namespace RumbleJungle.Model
{
    public class WeaponModel
    {
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public WeaponModel()
        {
            foreach (WeaponTypes weaponType in Enum.GetValues(typeof(WeaponTypes)))
            {
                Weapons.Add(new Weapon(weaponType));
            }
        }
        public void CollectWeapon()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Reset();
            }
        }

        public void ChangeRandomWeaponCount(int quantity)
        {
            Random random = new Random();
            int randomWeapon = random.Next(Weapons.Count);
            Weapons[randomWeapon].ChangeCount(quantity);
        }

        public void ChangeAllWeaponsCount(int quantity)
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.ChangeCount(quantity);
            }
        }
    }
}
