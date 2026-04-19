using System;

namespace lab_6_oop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("══════════════════════════════════");
            Console.WriteLine("   SMART WATCH DEMO WITH ASSISTANT   ");
            Console.WriteLine("══════════════════════════════════\n");

            // Create event manager
            WatchEventManager events = new WatchEventManager();

            // Subscribe to events
            events.BatteryEvents.OnBatteryLow += BatteryEventHandler;
            events.HealthEvents.OnHealthEmergency += HealthEmergencyHandler;
            events.VoiceEvents.OnVoiceCommandExecuted += VoiceCommandHandler;

            // Create watch owner
            Person owner = new Person("Alex", "Erohin", 20, 50.45, 30.52);

            // Create REGULAR smart watch
            Console.WriteLine("=== PART 1: REGULAR SMART WATCH ===\n");
            SmartWatch regularWatch = new SmartWatch(owner, events, 80);
            regularWatch.ShowStatus();
            regularWatch.ProcessVoiceCommand("hello");
            regularWatch.ProcessVoiceCommand("health");

            // Create SMART VOICE ASSISTANT (inherited class)
            Console.WriteLine("\n=== PART 2: SMART VOICE ASSISTANT ===\n");
            SmartVoiceAssistant assistant = new SmartVoiceAssistant(owner, events, "Alexa", 100);
            assistant.ShowStatus();

            // Demonstrate assistant features
            Console.WriteLine("--- Assistant Features ---");
            assistant.TellJoke();
            assistant.GetWeather();
            assistant.SetReminder("Buy groceries at 6pm");
            assistant.SetReminder("Call mom tomorrow");
            assistant.ShowReminders();

            // Voice commands with assistant
            Console.WriteLine("--- Voice Commands with Assistant ---");
            assistant.ProcessVoiceCommand("hello");
            assistant.ProcessVoiceCommand("health");
            assistant.ProcessVoiceCommand("tell me a joke");
            assistant.ProcessVoiceCommand("what's the weather");
            assistant.ProcessVoiceCommand("remind walk the dog");
            assistant.ProcessVoiceCommand("show my reminders");

            // Show that assistant still has all watch features
            Console.WriteLine("\n--- Assistant also does workouts ---");
            assistant.DoWorkout();
            assistant.CheckHealth();

            // Demonstrate inheritance - battery affects both
            Console.WriteLine("\n=== PART 3: LOW BATTERY TEST ===\n");
            SmartVoiceAssistant lowBatteryAssistant = new SmartVoiceAssistant(owner, events, "Siri", 15);
            lowBatteryAssistant.ProcessVoiceCommand("hello");
            lowBatteryAssistant.DoWorkout(); // This will trigger battery event

            Console.WriteLine("\nDEMO COMPLETE");
            Console.WriteLine("The SmartVoiceAssistant inherited ALL features from SmartWatch");
            Console.WriteLine("AND added new features: jokes, reminders, weather, custom wake word");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Event handlers
        static void BatteryEventHandler(string message)
        {
            Console.WriteLine($"[EVENT] {message}");
        }

        static void HealthEmergencyHandler(string message)
        {
            Console.WriteLine($"[EVENT] HEALTH EMERGENCY: {message}");
        }

        static void VoiceCommandHandler(string message)
        {
            Console.WriteLine($"[EVENT] {message}");
        }
    }
}