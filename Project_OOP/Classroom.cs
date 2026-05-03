// Classroom.cs
// Аудиторний фонд університету

namespace DigitalUniversity
{
    public class Classroom
    {
        private string _roomNumber;
        private int _capacity;
        private bool _isBooked;

        public string RoomNumber => _roomNumber;

        public Classroom(string roomNumber, int capacity)
        {
            _roomNumber = roomNumber;
            _capacity = capacity;
            _isBooked = false;
        }

        /// Reserves the classroom for a session.
        public void Book(Teacher teacher, Course course)
        {
            if (_isBooked)
            {
                Console.WriteLine($"[Classroom] Room {_roomNumber} is already booked.");
                return;
            }
            _isBooked = true;
            Console.WriteLine($"[Classroom] Room {_roomNumber} booked by {teacher.Name} for '{course.Title}'");
        }

        /// Returns the room's current schedule (stub).
        public void GetSchedule()
        {
            Console.WriteLine($"[Classroom] Schedule for room {_roomNumber}: capacity={_capacity}, booked={_isBooked}");
        }

        /// Displays room information.
        public void GetInfo()
        {
            Console.WriteLine($"[Classroom] Room {_roomNumber}, capacity: {_capacity} seats.");
        }

        public override string ToString() => $"Classroom {_roomNumber} (cap. {_capacity})";
    }
}