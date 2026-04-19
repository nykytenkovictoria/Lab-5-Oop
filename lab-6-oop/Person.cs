using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class Person
    {
        private string name;
        private string family;
        private int age;
        private double lastKnownLat;
        private double lastKnownLon;
        private List<string> messages = new List<string>();

        public string Name
        {
            get => name;
            set => name = string.IsNullOrWhiteSpace(value) ? throw new InvalidInputException("Name required") : value;
        }

        public string Family
        {
            get => family;
            set => family = string.IsNullOrWhiteSpace(value) ? throw new InvalidInputException("Family required") : value;
        }

        public int Age
        {
            get => age;
            set => age = value < 0 ? throw new InvalidInputException("Age cannot be negative") : value;
        }

        public double LastKnownLatitude
        {
            get => lastKnownLat;
            set => lastKnownLat = value;
        }

        public double LastKnownLongitude
        {
            get => lastKnownLon;
            set => lastKnownLon = value;
        }

        public Person(string name, string family, int age, double lat = 50.4501, double lon = 30.5234)
        {
            Name = name;
            Family = family;
            Age = age;
            LastKnownLatitude = lat;
            LastKnownLongitude = lon;
        }

        public void ReceiveMessage(string msg)
        {
            messages.Add(msg);
            Console.WriteLine($"{Name} {Family} received: {msg}");
        }

        public void ShowMessages()
        {
            Console.WriteLine($"\n--- Messages for {Name} {Family} ---");
            foreach (var m in messages)
                Console.WriteLine(m);
        }

        public void UserInfoHandler(string message)
        {
            Console.WriteLine($"User {Name} {Family} (Age: {Age}) - Event triggered: {message}");
        }
    }
}
