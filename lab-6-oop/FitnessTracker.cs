using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class FitnessTracker
    {
        private HealthData currentHealth;
        private HealthEvents events;

        public HealthData CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value ?? throw new InvalidInputException("Health data cannot be null");
        }

        public FitnessTracker(HealthData initialHealth, HealthEvents events)
        {
            this.currentHealth = initialHealth;
            this.events = events;
        }

        public void SimulateStep()
        {
            currentHealth.Steps += 100; // Add 100 steps at a time
            Console.WriteLine($"Steps increased to {currentHealth.Steps}");
        }

        // just increase heart rate a bit
        public void RandomHealthFluctuation()
        {
            Random rnd = new Random();
            currentHealth.HeartRate += rnd.Next(-5, 10);

            // Keep heart rate in reasonable range
            if (currentHealth.HeartRate < 50) currentHealth.HeartRate = 50;
            if (currentHealth.HeartRate > 180) currentHealth.HeartRate = 180;

            Console.WriteLine($"Heart rate changed to {currentHealth.HeartRate} bpm");

            if (currentHealth.IsCritical() && events != null)
                events.TriggerHealthEmergency($"Critical heart rate: {currentHealth.HeartRate} bpm");
        }
    }
}
