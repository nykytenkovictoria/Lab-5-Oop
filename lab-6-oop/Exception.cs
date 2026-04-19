using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class BatteryEmptyException : Exception
    {
        public BatteryEmptyException(string message) : base(message) { }
    }

    public class CriticalHealthException : Exception
    {
        public CriticalHealthException(string message) : base(message) { }
    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
}
