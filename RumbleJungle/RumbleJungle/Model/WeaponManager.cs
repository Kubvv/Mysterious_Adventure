using System;
using System.Collections.Generic;

namespace RumbleJungle.Model
{
    public class WeaponManager
    {
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public WeaponManager()
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
    }
}
