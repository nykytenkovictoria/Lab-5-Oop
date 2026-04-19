using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    // delegates for smart watch
    public delegate void WatchEventHandler(string message);

    // low battery event
    public class BatteryEvents
    {
        public event WatchEventHandler OnBatteryLow;

        public void TriggerBatteryLow(string msg)
        {
            if (OnBatteryLow != null)
                OnBatteryLow(msg);
        }
    }


    // smth with health of user
    public class HealthEvents
    {
        public event WatchEventHandler OnHealthEmergency;

        public void TriggerHealthEmergency(string msg)
        {
            if (OnHealthEmergency != null)
                OnHealthEmergency(msg);
        }
    }


    // user received a message
    public class MessageEvents
    {
        public event WatchEventHandler OnMessageReceived;

        public void TriggerMessageReceived(string msg)
        {
            if (OnMessageReceived != null)
                OnMessageReceived(msg);
        }
    }

    // voice event

    public class VoiceEvents
    {
        public event WatchEventHandler OnVoiceCommandExecuted;

        public void TriggerVoiceCommandExecuted(string msg)
        {
            if (OnVoiceCommandExecuted != null)
                OnVoiceCommandExecuted(msg);
        }
    }


    // define all events in manager

    public class WatchEventManager
    {
        public BatteryEvents BatteryEvents = new BatteryEvents();
        public HealthEvents HealthEvents = new HealthEvents();
        public MessageEvents MessageEvents = new MessageEvents();
        public VoiceEvents VoiceEvents = new VoiceEvents();
    }
}
