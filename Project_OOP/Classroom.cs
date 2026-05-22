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
            Console.WriteLine("[Classroom] Статичний конструктор: клас ініціалізовано.");
        }

        public Classroom() : base("—", 0)
        {
            _roomNumber = "000";
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine("[Classroom] Конструктор без параметрів: аудиторію створено.");
        }

        public Classroom(string roomNumber, int capacity) : base($"Ауд.{roomNumber}", capacity)
        {
            _roomNumber = roomNumber;
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine($"[Classroom] Конструктор з параметрами: ауд.{_roomNumber}, місць={_capacity}");
        }

        public Classroom(string roomNumber, int capacity, string location)
            : this(roomNumber, capacity)
        {
            _location = $"Ауд.{roomNumber}, {location}";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Конструктор виклику іншого: {location}");
        }

        public Classroom(Classroom other) : base(other._location, other._capacity)
        {
            _roomNumber = other._roomNumber + "-copy";
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine($"[Classroom] Конструктор копії: скопійовано ауд.{_roomNumber}");
        }

        private Classroom(string roomNumber) : base("Резерв", 0)
        {
            _roomNumber = roomNumber;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Закритий конструктор: резервна ауд.{_roomNumber}");
        }

        public static Classroom CreateReserve(string room) => new Classroom(room);

        public bool IsFree() => !_isBooked;

        public bool IsLarge() => _capacity > 50;

        /// Чи підходить для групи певного розміру
        public bool CanFitGroup(int groupSize) => _capacity >= groupSize && !_isBooked;

        private string BuildInfo() =>
            $"Ауд.{_roomNumber} | Місць: {_capacity} | Місце: {_location} | Зайнята: {_isBooked}";

        /// Reserves the classroom for a session.
        public void Book(Teacher teacher, Course course)
        {
            if (_isBooked)
            { Console.WriteLine($"[Classroom] Ауд.{_roomNumber} вже зайнята ({_bookedBy})."); return; }
            _isBooked = true;
            _bookedBy = $"{teacher.Name} / {course.Title}";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Ауд.{_roomNumber} заброньована: {_bookedBy}");
        }

        public void Release()
        {
            _isBooked = false; _bookedBy = "";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Ауд.{_roomNumber} звільнена.");
        }

        /// Returns the room's current schedule (stub).
        public void GetSchedule()
        {
            Console.WriteLine($"[Classroom] Ауд.{_roomNumber}: місць={_capacity}, зайнята={_isBooked}");
            if (_isBooked) Console.WriteLine($"  Ким зайнята: {_bookedBy}");
        }

        /// Displays room information.
        public override void GetInfo()
        {
            Console.WriteLine($"[Classroom] Room {_roomNumber}, capacity: {_capacity} seats.");
        }

        public override bool IsAvailable() => !_isBooked && _capacity > 0;

        public void PrintInfo()
        {
            Console.WriteLine($"  Аудиторія    : {_roomNumber}");
            Console.WriteLine($"  Місць        : {_capacity}");
            Console.WriteLine($"  Корпус       : {_location}");
            Console.WriteLine($"  Зайнята      : {_isBooked}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього ауд.  : {TotalRooms}");
        }

        public override string ToString() => $"Classroom {_roomNumber} (cap. {_capacity})";

        public static Classroom operator +(Classroom a, Classroom b)
        {
            var result = new Classroom(
                a._roomNumber + "+" + b._roomNumber,
                a._capacity + b._capacity,
                a._location
            );
            Console.WriteLine($"[Classroom op+] Об'єднано: ауд.{result.RoomNumber}, місць={result.Capacity}");
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