using System;

namespace RumbleJungle.Model
{
    public class LivingJungleObject : JungleObject
    {
        public int Health { get; private set; }

        public LivingJungleObject(JungleObjectTypes livingJungleObjectType) : base(livingJungleObjectType)
        {
            Health = 0;
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
    }
}
