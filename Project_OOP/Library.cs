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
        private List<string> _borrowedBooks = new();

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

        public bool IsLibraryOpen() => IsOpen;

        /// Чи є книга в каталозі
        public bool HasBook(string title) =>
            _catalog.Exists(b => b.Contains(title, StringComparison.OrdinalIgnoreCase));

        /// Чи бібліотека переповнена (більше 80% аудиторій зайнято)
        public bool IsNearCapacity() => _classrooms.Count > 0 && _classrooms.Count >= _capacity * 0.8;

        /// Чи є вільні аудиторії
        public bool HasFreeClassrooms() => _classrooms.Exists(r => !r.IsBooked);

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
            if (found.Count == 0) Console.WriteLine("  Нічого не знайдено.");
            else found.ForEach(b => Console.WriteLine($"  Знайдено: {b}"));
        }

        // Records a book borrowing event.
        public void BorrowBook(Student student, string title)
        {
            if (!HasBook(title))
            { Console.WriteLine($"[Library] Книга '{title}' відсутня в каталозі."); return; }
            _borrowedBooks.Add($"{student.Name}:{title}");
            Console.WriteLine($"[Library] {student.Name} отримує '{title}'");
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
            Console.WriteLine($"  Видано книг  : {_borrowedBooks.Count}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
        }

        public override string ToString() => $"Library at '{_location}' ({_capacity} seats)";
    }
}