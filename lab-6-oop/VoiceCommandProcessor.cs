using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class VoiceCommandProcessor
    {
        private VoiceEvents events;

        public VoiceCommandProcessor(VoiceEvents events)
        {
            this.events = events;
        }

        public string ProcessCommand(string command)
        {
            string lower = command.ToLower();
            string response;
            return lower;

        }
    }
}
