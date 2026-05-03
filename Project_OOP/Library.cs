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

        public Library(string location, int capacity)
        {
            _location = location;
            _capacity = capacity;
        }

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

        public override string ToString() => $"Library at '{_location}' ({_capacity} seats)";
    }
}