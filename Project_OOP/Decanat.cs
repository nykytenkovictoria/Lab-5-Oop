// Decanat.cs
// Електронний деканат

using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DigitalUniversity
{
    public class Decanat
    {
        private string _faculty;
        private string _headName;
        // Aggregation — students exist independently
        private List<Student> _students = new();
        // Composition — documents are owned by this decanat
        private List<Document> _documents = new();

        public Decanat(string faculty, string headName)
        {
            _faculty = faculty;
            _headName = headName;
        }

        public void AddStudent(Student student) => _students.Add(student);

        /// Processes a student request and creates a document (composition).
        public void ProcessRequest(Student student, string requestType)
        {
            Console.WriteLine($"[Decanat] Processing request '{requestType}' for {student.Name}");
            var doc = new Document($"DOC-{_documents.Count + 1:000}", requestType);
            doc.Create();
            _documents.Add(doc);
        }

        /// Lists all managed students.
        public void ManageStudents()
        {
            Console.WriteLine($"[Decanat] Faculty '{_faculty}' manages {_students.Count} student(s):");
            foreach (var s in _students)
                Console.WriteLine($"  - {s}");
        }

        /// Generates a summary report.
        public void GenerateReport()
        {
            Console.WriteLine($"[Decanat] Generating report for faculty '{_faculty}'. Head: {_headName}");
            Console.WriteLine($"  Total students: {_students.Count}, Documents on file: {_documents.Count}");
        }

        public override string ToString() => $"Decanat of faculty '{_faculty}', Head: {_headName}";
    }
}