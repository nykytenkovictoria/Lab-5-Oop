using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class Communicator
    {
        private List<string> messageHistory = new List<string>();
        private MessageEvents events;

        public Communicator(MessageEvents events)
        {
            this.events = events;
        }

        public void SendMessage(Person recipient, string message, Person sender)
        {
            string fullMsg = $"{sender.Name} -> {recipient.Name}: {message}";
            recipient.ReceiveMessage(fullMsg);
            messageHistory.Add(fullMsg);
            if (events != null)
                events.TriggerMessageReceived($"Message sent: {fullMsg}");
        }

        public void Broadcast(string message, Person[] allPeople, Person sender)
        {
            foreach (var p in allPeople)
            {
                if (p != sender)
                    SendMessage(p, message, sender);
            }
        }

        public string GetHistory()
        {
            return string.Join("\n", messageHistory);
        }
    }
}
