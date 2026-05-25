// Classroom.cs
// Аудиторний фонд університету

namespace DigitalUniversity
{
    public class Classroom : UniversityResource, IBookable
    {
        private string _roomNumber;
        private bool _isBooked;
        private string _bookedBy;
        private static int _totalRooms;

        public string RoomNumber
        {
            get => _roomNumber;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _roomNumber = value;
            }
        }


        public bool IsBooked => _isBooked;

        public static int TotalRooms => _totalRooms;

        public string FullInfo { get; private set; }

        static Classroom()
        {
            _totalRooms = 0;
        }

        public Classroom() : base(Messages.Get("classroom", "default_location"), 0)
        {
            _roomNumber = Messages.Get("classroom", "default_room_number");
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
        }

        public Classroom(string roomNumber, int capacity) : base(Messages.Get("classroom", "name_prefix") + roomNumber, capacity)
        {
            _roomNumber = roomNumber;
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
        }

        public Classroom(string roomNumber, int capacity, string location)
            : this(roomNumber, capacity)
        {
            _location = Messages.Get("classroom", "name_prefix") + roomNumber + ", " + location;
            FullInfo = BuildInfo();

        }

        public Classroom(Classroom other) : base(other._location, other._capacity)
        {
            _roomNumber = other._roomNumber + "-copy";
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
        }

        private Classroom(string roomNumber) : base(Messages.Get("classroom", "reserve_name"), 0)
        {
            _roomNumber = roomNumber;
            FullInfo = BuildInfo();
        }

        public static Classroom CreateReserve(string room) => new Classroom(room);

        public bool IsFree() => !_isBooked;

        public bool IsLarge() => _capacity > 50;

        /// Чи підходить для групи певного розміру
        public bool CanFitGroup(int groupSize) => _capacity >= groupSize && !_isBooked;

        private string BuildInfo() =>
            Messages.Get("classroom", "build_info", _roomNumber, _capacity, _location, _isBooked);

        /// Reserves the classroom for a session.
        public void Book(Teacher teacher, Course course)
        {
            if (_isBooked)
            {
                Messages.Print("classroom", "book_already_taken", _roomNumber, _bookedBy);
                return;
            }
            _isBooked = true;
            _bookedBy = $"{teacher.Name} / {course.Title}";
            FullInfo = BuildInfo();
            Messages.Print("classroom", "book_success", _roomNumber, _bookedBy);
        }

        public void Release()
        {
            _isBooked = false;
            _bookedBy = "";
            FullInfo = BuildInfo();
            Messages.Print("classroom", "release", _roomNumber);
        }

        /// Returns the room's current schedule (stub).
        public void GetSchedule()
        {
            Messages.Print("classroom", "get_schedule", _roomNumber, _capacity, _isBooked);
            if (_isBooked)
                Messages.Print("classroom", "booked_by", _bookedBy);
        }

        /// Displays room information.
        public override void GetInfo()
        {
            Messages.Print("classroom", "get_info", _roomNumber, _capacity);
        }

        public override bool IsAvailable() => !_isBooked && _capacity > 0;

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("classroom", "room_label")}    : {_roomNumber}");
            Console.WriteLine($"  {Messages.Get("classroom", "capacity_label")}        : {_capacity}");
            Console.WriteLine($"  {Messages.Get("classroom", "location_label")}       : {_location}");
            Console.WriteLine($"  {Messages.Get("classroom", "booked_label")}      : {_isBooked}");
            Console.WriteLine($"  {Messages.Get("classroom", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("classroom", "total_rooms_label")}  : {TotalRooms}");
        }

        public override string ToString() => Messages.Get("classroom", "to_string", _roomNumber, _capacity);

        public static Classroom operator +(Classroom a, Classroom b)
        {
            var result = new Classroom(
                a._roomNumber + "+" + b._roomNumber,
                a._capacity + b._capacity,
                a._location
            );
            Messages.Print("classroom", "op_plus", result.RoomNumber, result.Capacity);
            return result;
        }

        // Оператори порівняння: по кількості місць
        public static bool operator ==(Classroom a, Classroom b) => a._capacity == b._capacity;
        public static bool operator !=(Classroom a, Classroom b) => a._capacity != b._capacity;
        public static bool operator >(Classroom a, Classroom b) => a._capacity > b._capacity;
        public static bool operator <(Classroom a, Classroom b) => a._capacity < b._capacity;
        public static bool operator >=(Classroom a, Classroom b) => a._capacity >= b._capacity;
        public static bool operator <=(Classroom a, Classroom b) => a._capacity <= b._capacity;

        public override bool Equals(object? obj) => obj is Classroom c && _capacity == c._capacity;

        public override int GetHashCode() => _capacity.GetHashCode();
    }
}