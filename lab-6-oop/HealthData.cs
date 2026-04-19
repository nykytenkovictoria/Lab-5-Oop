using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class HealthData
    {
        private int heartRate;
        private int systolicPressure;
        private int diastolicPressure;
        private int steps;
        private double temperature;

        public int HeartRate
        {
            get => heartRate;
            set => heartRate = value;
        }

        public int SystolicPressure
        {
            get => systolicPressure;
            set => systolicPressure = value;
        }

        public int DiastolicPressure
        {
            get => diastolicPressure;
            set => diastolicPressure = value;
        }

        public int Steps
        {
            get => steps;
            set => steps = value;
        }

        public double Temperature
        {
            get => temperature;
            set => temperature = value;
        }

        public HealthData(int heartRate, int sys, int dia, int steps, double temp)
        {
            this.heartRate = heartRate;
            this.systolicPressure = sys;
            this.diastolicPressure = dia;
            this.steps = steps;
            this.temperature = temp;
        }

        public bool IsCritical()
        {
            return heartRate > 140 || heartRate < 40 ||
                   systolicPressure > 180 || systolicPressure < 80 ||
                   temperature > 39.0 || temperature < 35.0;
        }

        public override string ToString()
        {
            return $"{heartRate} bpm, BP {systolicPressure}/{diastolicPressure}, {steps} steps, {temperature:F1}°C";
        }
    }
}
