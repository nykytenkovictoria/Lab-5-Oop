using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class Battery
    {
        private int chargeLevel;
        private BatteryEvents events;

        public int ChargeLevel
        {
            get => chargeLevel;
            private set
            {
                if (value < 0) chargeLevel = 0;
                else if (value > 100) chargeLevel = 100;
                else chargeLevel = value;
                // if battery low smaller than 15% trigger an event
                if (chargeLevel <= 15 && events != null)
                    events.TriggerBatteryLow($"Battery low: {chargeLevel}%");
            }
        }

        public Battery(BatteryEvents events, int initialCharge = 100)
        {
            this.events = events;
            ChargeLevel = initialCharge;
        }

        public void UsePower(int amount)
        {
            if (amount < 0) throw new InvalidInputException("Power usage cannot be negative");
            int newCharge = ChargeLevel - amount;
            if (newCharge < 0)
                throw new BatteryEmptyException("Battery exhausted! Watch shutting down.");
            ChargeLevel = newCharge;
        }

        public void Recharge(int amount)
        {
            if (amount < 0) throw new InvalidInputException("Recharge amount cannot be negative");
            ChargeLevel += amount;
        }
    }
}
