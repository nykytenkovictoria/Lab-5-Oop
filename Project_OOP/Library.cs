// Library.cs
// Електронний бібліотечний фонд університету

using System.Collections.Generic;

namespace DigitalUniversity
{
    public class Library
    {
        private string _location;
        private int _capacity;
        private List<Classroom> _classrooms = new();
        private List<string> _catalog = new();

        public string Location
        {
            get => _location;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _location = value; 
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

        public int ClassroomCount => _classrooms.Count;
        public int BookCount => _catalog.Count;

        public bool IsOpen { get; set; }

        public string FullInfo { get; private set; }

        static Library()
        {
            Console.WriteLine("[Library] Статичний конструктор: клас ініціалізовано.");
        }

        public Library()
        {
            _location = "—";
            _capacity = 0;
            IsOpen = false;
            FullInfo = BuildInfo();
            Console.WriteLine("[Library] Конструктор без параметрів: бібліотеку створено.");
        }

        public Library(string location, int capacity)
        {
            _location = location;
            _capacity = capacity; 
            IsOpen = true;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Library] Конструктор з параметрами: '{_location}', місць: {_capacity}");
        }

        public Library(string location, int capacity, bool isOpen)
            : this(location, capacity)
        {
            IsOpen = isOpen;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Library] Конструктор виклику іншого: відкрита={IsOpen}");
        }

        public Library(Library other)
        {
            _location = other._location + " (копія)";
            _capacity = other._capacity;
            IsOpen = other.IsOpen;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Library] Конструктор копії: скопійовано '{_location}'");
        }

        private Library(string location)
        {
            _location = location;
            _capacity = 0;
            IsOpen = false;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Library] Закритий конструктор: резервна бібліотека '{_location}'");
        }

        public static Library CreateReserve(string location) => new Library(location);

        private string BuildInfo() =>
            $"Бібліотека: {_location} | Місць: {_capacity} | Відкрита: {IsOpen}";

        // Adds a classroom to the campus fund (composition).
        public void AddClassroom(Classroom room) => _classrooms.Add(room);

        // Adds a book title to the catalog.
        public void AddBook(string title) => _catalog.Add(title);

        // Searches the electronic catalog for a keyword.
        public void SearchBook(string keyword)
        {
            Console.WriteLine($"[Library] Searching catalog for '{keyword}'...");
            var found = _catalog.FindAll(b => b.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            if (found.Count == 0)
                Console.WriteLine("  No results found.");
            else
                found.ForEach(b => Console.WriteLine($"  Found: {b}"));
        }

        // Records a book borrowing event.
        public void BorrowBook(Student student, string title)
        {
            Console.WriteLine($"[Library] {student.Name} borrows '{title}' from {_location} library.");
        }

        // Prints a summary of the library catalog and rooms.
        public void GetCatalog()
        {
            Console.WriteLine($"[Library] Location: {_location}, capacity: {_capacity}");
            Console.WriteLine($"  Books in catalog: {_catalog.Count}");
            Console.WriteLine($"  Classrooms managed: {_classrooms.Count}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  Розташування : {_location}");
            Console.WriteLine($"  Місць        : {_capacity}");
            Console.WriteLine($"  Відкрита     : {IsOpen}");
            Console.WriteLine($"  Книг         : {_catalog.Count}");
            Console.WriteLine($"  Аудиторій    : {_classrooms.Count}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
        }

        public override string ToString() => $"Library at '{_location}' ({_capacity} seats)";
    }
}