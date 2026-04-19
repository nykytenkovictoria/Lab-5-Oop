using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class SmartVoiceAssistant : SmartWatch
    {
        public string AssistantName { get; private set; }
        private List<string> reminders;

        public SmartVoiceAssistant(Person watchOwner, WatchEventManager events, string assistantName, int batteryStart = 100)
            : base(watchOwner, events, batteryStart)
        {
            AssistantName = assistantName;
            reminders = new List<string>();
            Console.WriteLine($"Voice Assistant '{AssistantName}' activated!\n");
        }

        // New feature: Tell jokes
        public void TellJoke()
        {
            Console.WriteLine($"{AssistantName}: Why don't smart watches get lost?");
            Console.WriteLine("Because they always follow their GPS! 😄\n");
        }

        // New feature: Set reminders
        public void SetReminder(string reminder)
        {
            reminders.Add(reminder);
            Console.WriteLine($"Reminder saved: {reminder}");
            Console.WriteLine($"Total reminders: {reminders.Count}\n");
        }

        // New feature: Show reminders
        public void ShowReminders()
        {
            if (reminders.Count == 0)
            {
                Console.WriteLine("No reminders yet.\n");
                return;
            }

            Console.WriteLine("Your reminders:");
            for (int i = 0; i < reminders.Count; i++)
                Console.WriteLine($"   {i + 1}. {reminders[i]}");
            Console.WriteLine();
        }

        // New feature: Weather
        public void GetWeather()
        {
            Random rnd = new Random();
            string[] conditions = { "Sunny", "Cloudy", "Rainy" };
            int temp = rnd.Next(15, 30);

            Console.WriteLine($"Weather: {conditions[rnd.Next(conditions.Length)]}, {temp}°C\n");
        }

        // Override voice command to add assistant features
        public new void ProcessVoiceCommand(string command)
        {
            string lower = command.ToLower();

            if (lower.Contains("joke"))
                TellJoke();
            else if (lower.Contains("remind"))
            {
                string reminder = command.Replace("remind", "", StringComparison.OrdinalIgnoreCase).Trim();
                if (!string.IsNullOrEmpty(reminder))
                    SetReminder(reminder);
                else
                    Console.WriteLine("What should I remind you about?\n");
            }
            else if (lower.Contains("reminders"))
                ShowReminders();
            else if (lower.Contains("weather"))
                GetWeather();
            else
                base.ProcessVoiceCommand(command); // Use parent class for basic commands
        }

        // Override status to show assistant info
        public new void ShowStatus()
        {
            base.ShowStatus();
            Console.WriteLine($"Assistant: {AssistantName}");
            Console.WriteLine($"Reminders: {reminders.Count}");
        }
    }
}
