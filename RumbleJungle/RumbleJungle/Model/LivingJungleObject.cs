using System;

namespace RumbleJungle.Model
{
    public class LivingJungleObject : JungleObject
    {
        public int Health { get; private set; }

        public LivingJungleObject(JungleObjectType livingJungleObjectType) : base(livingJungleObjectType)
        {
        }

        public event EventHandler HealthChanged;

        public void ChangeHealth(int healthChange)
        {
            Health += healthChange;
            if (Health > 100)
            {
                Health = 100;
            }
            else if (Health < 0)
            {
                Health = 0;
            }
            HealthChanged?.Invoke(this, null);
        }

        public void SetHealth(int health)
        {
            Health = health;
            HealthChanged?.Invoke(this, null);
        }
    }
}
