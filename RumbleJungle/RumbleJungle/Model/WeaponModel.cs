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
            Weapons.ForEach(weapon => weapon.Reset());
        }

        public void ChangeRandomWeaponCount(int quantity)
        {
            int randomWeapon = Configuration.Random.Next(Weapons.Count - 1) + 1;
            Weapons[randomWeapon].ChangeCount(quantity);
        }

        public void ChangeAllWeaponsCount(int quantity)
        {
            Weapons.ForEach(weapon => weapon.ChangeCount(quantity));
        }

        public void SetDoubleAttack()
        {
            int randomWeapon = Configuration.Random.Next(Weapons.Count - 1) + 1;
            Weapons[randomWeapon].SetDoubleAttack(true);
        }
    }
}
