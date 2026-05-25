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

        public string Faculty
        {
            get => _faculty;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _faculty = value;
            }
        }

        public string HeadName
        {
            get => _headName;
            set => _headName = value;
        }


        public int DocumentCount => _documents.Count;
        public int StudentCount => _students.Count;
        public bool IsActive { get; set; }

        public string FullInfo { get; private set; }


        public Decanat()
        {
            _faculty = Messages.Get("decanat", "default_faculty");
            _headName = Messages.Get("decanat", "default_head");
            IsActive = false;
            FullInfo = BuildInfo();
        }

        public Decanat(string faculty, string headName)
        {
            _faculty = faculty;
            _headName = headName;
            IsActive = true;
            FullInfo = BuildInfo();
        }

        public Decanat(string faculty, string headName, bool isActive)
            : this(faculty, headName)
        {
            IsActive = isActive;
            FullInfo = BuildInfo();
        }

        public Decanat(Decanat other)
        {
            _faculty = other._faculty + Messages.Get("decanat", "copy_suffix");
            _headName = other._headName;
            IsActive = other.IsActive;
            FullInfo = BuildInfo();
        }

        private Decanat(string faculty)
        {
            _faculty = faculty;
            _headName = Messages.Get("decanat", "system_head");
            IsActive = false;
            FullInfo = BuildInfo();
        }

        public static Decanat CreateSystem(string faculty) => new Decanat(faculty);


        /// Чи є необроблені запити (документів менше, ніж студентів × 2)
        public bool HasPendingRequests() => _documents.Count < _students.Count * 2;

        /// Чи деканат завантажений (більше 100 студентів)
        public bool IsOverloaded() => _students.Count > 100;
        public bool IsOperational() => IsActive && _students.Count > 0;

        /// Чи є студент у деканаті
        public bool HasStudent(string studentId) =>
            _students.Exists(s => s.Id.Equals(studentId, StringComparison.OrdinalIgnoreCase));

        private string BuildInfo() =>
            Messages.Get("decanat", "build_info", _faculty, _headName, IsActive);

        public void AddStudent(Student student) => _students.Add(student);

        // Processes a student request and creates a document (composition).
        public void ProcessRequest(Student student, string requestType)
        {
            Messages.Print("decanat", "process_request", requestType, student.Name);
            var doc = new Document($"DOC-{_documents.Count + 1:000}", requestType);
            doc.Create();
            _documents.Add(doc);
        }

        // Lists all managed students.
        public void ManageStudents()
        {
            Messages.Print("decanat", "manage_students_header", _faculty, _students.Count);
            foreach (var s in _students)
            {
                Console.Write($"  {s.Name} | GPA={s.GPA}");
                if (s.IsEligibleForScholarship())
                    Console.Write($" | {Messages.Get("decanat", "scholarship_indicator")}");
                Console.WriteLine();
            }
        }

        // Generates a summary report.
        public void GenerateReport()
        {
            Messages.Print("decanat", "report_header", _faculty, _headName);
            Messages.Print("decanat", "report_stats", _students.Count, _documents.Count);
            Messages.Print("decanat", "report_operational", IsOperational(), IsOverloaded());
            int eligible = _students.FindAll(s => s.IsEligibleForScholarship()).Count;
            int atRisk = _students.FindAll(s => s.IsAtRisk()).Count;
            Messages.Print("decanat", "report_eligible_risk", eligible, atRisk);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("decanat", "faculty_label")}    : {_faculty}");
            Console.WriteLine($"  {Messages.Get("decanat", "head_label")}     : {_headName}");
            Console.WriteLine($"  {Messages.Get("decanat", "active_label")}     : {IsActive}");
            Console.WriteLine($"  {Messages.Get("decanat", "students_label")}    : {_students.Count}");
            Console.WriteLine($"  {Messages.Get("decanat", "documents_label")}   : {_documents.Count}");
            Console.WriteLine($"  {Messages.Get("decanat", "fullinfo_label")}     : {FullInfo}");
        }

        public override string ToString() => Messages.Get("decanat", "to_string", _faculty, _headName);
    }
}