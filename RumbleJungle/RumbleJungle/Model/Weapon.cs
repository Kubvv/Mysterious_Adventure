using System;

namespace RumbleJungle.Model
{
    public class Weapon
    {
        public WeaponTypes WeaponType { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }

        public event EventHandler CountChanged;

        public Weapon(WeaponTypes weaponType)
        {
            WeaponType = weaponType;
            Name = Enum.GetName(typeof(WeaponTypes), weaponType);
            Reset();
        }

        public void Reset()
        {
            Count = WeaponType == WeaponTypes.Dagger ? -1 : 1;
        }

        internal void ChangeCount(int quantity)
        {
            Count += quantity;
            CountChanged?.Invoke(this, null);
        }
    }
}
