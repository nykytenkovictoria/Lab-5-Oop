using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class SmartWatch
    {
        // components
        private Battery battery;
        private FitnessTracker fitness;
        private GPSModule gps;
        private Person owner;
        private WatchEventManager eventManager;
        private string logFilePath = "watch_log.txt";

        public SmartWatch(Person watchOwner, WatchEventManager events, int batteryStart = 100)
        {
            this.eventManager = events;
            this.owner = watchOwner;

            // Create components
            this.battery = new Battery(eventManager.BatteryEvents, batteryStart);
            this.fitness = new FitnessTracker(new HealthData(75, 120, 80, 0, 36.6), eventManager.HealthEvents);
            this.gps = new GPSModule();

            // Set initial location
            gps.UpdateLocation(owner.LastKnownLatitude, owner.LastKnownLongitude);

            LogEvent($"SmartWatch ready for {owner.Name}");
            Console.WriteLine($"SmartWatch initialized for {owner.Name}\n");
        }

        private void LogEvent(string msg)
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss} - {msg}";
            try
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log error: {ex.Message}");
            }
        }

        // workout simulation
        public void DoWorkout()
        {
            Console.WriteLine("\nStarting workout...");

            try
            {
                battery.UsePower(10);
                fitness.SimulateStep();
                fitness.RandomHealthFluctuation();

                Console.WriteLine($"Workout complete!");
                Console.WriteLine($"Health: {fitness.CurrentHealth}");
                Console.WriteLine($"Battery: {battery.ChargeLevel}%\n");
            }
            catch (BatteryEmptyException ex)
            {
                Console.WriteLine($"{ex.Message}");
                LogEvent($"WORKOUT FAILED: {ex.Message}");
            }
        }

        // voice command processing
        public void ProcessVoiceCommand(string command)
        {
            Console.WriteLine($"\nYou said: '{command}'");

            try
            {
                battery.UsePower(2);

                string response = "";

                if (command.ToLower().Contains("hello"))
                    response = "Hello! Nice to see you!";
                else if (command.ToLower().Contains("health"))
                    response = $"Your health: {fitness.CurrentHealth}";
                else if (command.ToLower().Contains("steps"))
                    response = $"You've taken {fitness.CurrentHealth.Steps} steps today";
                else if (command.ToLower().Contains("battery"))
                    response = $"Battery level: {battery.ChargeLevel}%";
                else if (command.ToLower().Contains("location"))
                    response = $"Your location: {gps.GetCurrentLocation()}";
                else if (command.ToLower().Contains("help"))
                    response = "Commands: hello, health, steps, battery, location, emergency";
                else if (command.ToLower().Contains("emergency"))
                {
                    response = "EMERGENCY! Help is on the way!";
                    eventManager.HealthEvents.TriggerHealthEmergency("User requested emergency help!");
                }
                else
                    response = "I don't understand. Say 'help' for commands.";

                Console.WriteLine($"Watch response: {response}\n");

                eventManager.VoiceEvents.TriggerVoiceCommandExecuted($"Command: {command}");
            }
            catch (BatteryEmptyException ex)
            {
                Console.WriteLine($"Cannot process command: {ex.Message}\n");
            }
        }

        // health status
        public void CheckHealth()
        {
            Console.WriteLine("\n🩺 HEALTH CHECK:");
            Console.WriteLine($"   Heart Rate: {fitness.CurrentHealth.HeartRate} bpm");
            Console.WriteLine($"   Blood Pressure: {fitness.CurrentHealth.SystolicPressure}/{fitness.CurrentHealth.DiastolicPressure}");
            Console.WriteLine($"   Steps: {fitness.CurrentHealth.Steps}");
            Console.WriteLine($"   Temperature: {fitness.CurrentHealth.Temperature}°C");

            if (fitness.CurrentHealth.HeartRate > 120)
                Console.WriteLine("Warning: High heart rate!");
            else if (fitness.CurrentHealth.HeartRate < 60)
                Console.WriteLine("Warning: Low heart rate!");
            else
                Console.WriteLine("Health status: NORMAL\n");
        }

        // Show watch status
        public void ShowStatus()
        {
            Console.WriteLine($"SMART WATCH STATUS");
            Console.WriteLine($"Owner: {owner.Name} {owner.Family}");
            Console.WriteLine($"Battery: {battery.ChargeLevel}%");
            Console.WriteLine($"Location: {gps.GetCurrentLocation()}");
            Console.WriteLine($"Heart Rate: {fitness.CurrentHealth.HeartRate} bpm");
            Console.WriteLine($"Steps Today: {fitness.CurrentHealth.Steps}");
            Console.WriteLine("══════════════════════════════════\n");
        }

        // Recharge watch
        public void Recharge(int amount)
        {
            battery.Recharge(amount);
        }

        // Getters
        public int GetBatteryLevel() => battery.ChargeLevel;
        public string GetHealthStatus() => fitness.CurrentHealth.ToString();
    }
}
