// Classroom.cs
// Аудиторний фонд університету

namespace DigitalUniversity
{
    public class Classroom
    {
        private string _roomNumber;
        private int _capacity;
        private bool _isBooked;
        private string _bookedBy;
        private static int _totalRooms;

        public string RoomNumber
        {
            get => _roomNumber;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _roomNumber = value;
            }
        }

        public int Capacity
        {
            get => _capacity;
            set { 
                if (value > 0) 
                    _capacity = value;
            }
        }

      
        public bool IsBooked => _isBooked;

        public static int TotalRooms => _totalRooms;

        public string Building { get; set; }

        public string FullInfo { get; private set; }

        static Classroom()
        {
            _totalRooms = 0;
            Console.WriteLine("[Classroom] Статичний конструктор: клас ініціалізовано.");
        }

        public Classroom()
        {
            _roomNumber = "000";
            _capacity = 0;
            _isBooked = false;
            Building = "—";
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine("[Classroom] Конструктор без параметрів: аудиторію створено.");
        }

        public Classroom(string roomNumber, int capacity)
        {
            _roomNumber = roomNumber;
            _capacity = capacity;
            _isBooked = false;
            Building = "—";
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine($"[Classroom] Конструктор з параметрами: ауд.{_roomNumber}, місць={_capacity}");
        }

        public Classroom(string roomNumber, int capacity, string building)
            : this(roomNumber, capacity)
        {
            Building = building;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Конструктор виклику іншого: корпус={Building}");
        }

        public Classroom(Classroom other)
        {
            _roomNumber = other._roomNumber + "-copy";
            _capacity = other._capacity;
            Building = other.Building;
            _isBooked = false;
            FullInfo = BuildInfo();
            _totalRooms++;
            Console.WriteLine($"[Classroom] Конструктор копії: скопійовано ауд.{_roomNumber}");
        }

        private Classroom(string roomNumber)
        {
            _roomNumber = roomNumber;
            _capacity = 0;
            Building = "Резерв";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Classroom] Закритий конструктор: резервна ауд.{_roomNumber}");
        }

        public static Classroom CreateReserve(string room) => new Classroom(room);

        public bool IsFree() => !_isBooked;

        public bool IsLarge() => _capacity > 50;

        /// Чи підходить для групи певного розміру
        public bool CanFitGroup(int groupSize) => _capacity >= groupSize && !_isBooked;
        
        private string BuildInfo() =>
            $"Ауд.{_roomNumber} | Місць: {_capacity} | Корпус: {Building} | Зайнята: {_isBooked}";

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
        public void GetInfo()
        {
            Console.WriteLine($"[Classroom] Room {_roomNumber}, capacity: {_capacity} seats.");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  Аудиторія    : {_roomNumber}");
            Console.WriteLine($"  Місць        : {_capacity}");
            Console.WriteLine($"  Корпус       : {Building}");
            Console.WriteLine($"  Зайнята      : {_isBooked}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього ауд.  : {TotalRooms}");
        }

        public override string ToString() => $"Classroom {_roomNumber} (cap. {_capacity})";
    }
}