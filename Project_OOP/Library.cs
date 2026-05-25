// Library.cs
// Електронний бібліотечний фонд університету

using System.Collections.Generic;

namespace DigitalUniversity
{
    public class Library : UniversityResource
    {
        private List<Classroom> _classrooms = new();
        private List<string> _catalog = new();
        private List<string> _borrowedBooks = new();


        public int ClassroomCount => _classrooms.Count;
        public int BookCount => _catalog.Count;

        public bool IsOpen { get; set; }

        public string FullInfo { get; private set; }

        public Library() : base(Messages.Get("library", "default_location"), 0)
        {
            IsOpen = false;
            FullInfo = BuildInfo();
        }

        public Library(string location, int capacity) : base(location, capacity)
        {
            IsOpen = true;
            FullInfo = BuildInfo();
        }

        public Library(string location, int capacity, bool isOpen)
            : this(location, capacity)
        {
            IsOpen = isOpen;
            FullInfo = BuildInfo();
        }

        public Library(Library other) : base(other._location + "-copy", other._capacity)
        {
            IsOpen = other.IsOpen;
            FullInfo = BuildInfo();
        }

        private Library(string location) : base(location, 0)
        {
            IsOpen = false;
            FullInfo = BuildInfo();
        }

        public static Library CreateReserve(string location) => new Library(location);

        public override void GetInfo()
            => Messages.Print("library", "get_info", FullInfo, _catalog.Count, _classrooms.Count);

        public override bool IsAvailable() => IsOpen && _catalog.Count > 0;

        public bool IsLibraryOpen() => IsOpen;

        /// Чи є книга в каталозі
        public bool HasBook(string title) =>
            _catalog.Exists(b => b.Contains(title, StringComparison.OrdinalIgnoreCase));

        /// Чи бібліотека переповнена (більше 80% аудиторій зайнято)
        public bool IsNearCapacity() => _classrooms.Count > 0 && _classrooms.Count >= _capacity * 0.8;

        /// Чи є вільні аудиторії
        public bool HasFreeClassrooms() => _classrooms.Exists(r => !r.IsBooked);

        private string BuildInfo() =>
            Messages.Get("library", "build_info", _location, _capacity, IsOpen);

        // Adds a classroom to the campus fund (composition).
        public void AddClassroom(Classroom room) => _classrooms.Add(room);

        // Adds a book title to the catalog.
        public void AddBook(string title) => _catalog.Add(title);

        // Searches the electronic catalog for a keyword.
        public void SearchBook(string keyword)
        {
            Messages.Print("library", "search_header", keyword);
            var found = _catalog.FindAll(b => b.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            if (found.Count == 0)
                Messages.Print("library", "search_not_found");
            else
                found.ForEach(b => Messages.Print("library", "search_found", b));
        }

        // Records a book borrowing event.
        public void BorrowBook(Student student, string title)
        {
            if (!HasBook(title))
            {
                Messages.Print("library", "borrow_not_found", title);
                return;
            }
            _borrowedBooks.Add($"{student.Name}:{title}");
            Messages.Print("library", "borrow_success", student.Name, title);
        }

        // Prints a summary of the library catalog and rooms.
        public void GetCatalog()
        {
            Messages.Print("library", "catalog_info", _location, _capacity);
            Messages.Print("library", "catalog_books", _catalog.Count);
            Messages.Print("library", "catalog_classrooms", _classrooms.Count);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("library", "location_label")} : {_location}");
            Console.WriteLine($"  {Messages.Get("library", "capacity_label")}        : {_capacity}");
            Console.WriteLine($"  {Messages.Get("library", "isopen_label")}     : {IsOpen}");
            Console.WriteLine($"  {Messages.Get("library", "books_label")}         : {_catalog.Count}");
            Console.WriteLine($"  {Messages.Get("library", "classrooms_label")}    : {_classrooms.Count}");
            Console.WriteLine($"  {Messages.Get("library", "borrowed_label")}  : {_borrowedBooks.Count}");
            Console.WriteLine($"  {Messages.Get("library", "fullinfo_label")}     : {FullInfo}");
        }

        public override string ToString() => Messages.Get("library", "to_string", _location, _capacity);
    }
}