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
            set { 
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

        static Decanat()
        {
            Console.WriteLine("[Decanat] Статичний конструктор: клас ініціалізовано.");
        }

        public Decanat()
        {
            _faculty = "—";
            _headName = "—"; 
            IsActive = false;
            FullInfo = BuildInfo();
            Console.WriteLine("[Decanat] Конструктор без параметрів: деканат створено.");
        }

        public Decanat(string faculty, string headName)
        {
            _faculty = faculty;
            _headName = headName;
            IsActive = true;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Decanat] Конструктор з параметрами: '{_faculty}', керівник: {_headName}");
        }

        public Decanat(string faculty, string headName, bool isActive)
            : this(faculty, headName)
        {
            IsActive = isActive;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Decanat] Конструктор виклику іншого: активний={IsActive}");
        }

        public Decanat(Decanat other)
        {
            _faculty = other._faculty + " (копія)"; 
            _headName = other._headName;
            IsActive = other.IsActive;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Decanat] Конструктор копії: скопійовано '{_faculty}'");
        }

        private Decanat(string faculty)
        {
            _faculty = faculty;
            _headName = "System";
            IsActive = false;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Decanat] Закритий конструктор: системний деканат '{_faculty}'");
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
            $"Деканат: {_faculty} | Керівник: {_headName} | Активний: {IsActive}";

        public void AddStudent(Student student) => _students.Add(student);

        // Processes a student request and creates a document (composition).
        public void ProcessRequest(Student student, string requestType)
        {
            Console.WriteLine($"[Decanat] Processing request '{requestType}' for {student.Name}");
            var doc = new Document($"DOC-{_documents.Count + 1:000}", requestType);
            doc.Create();
            _documents.Add(doc);
        }

        // Lists all managed students.
        public void ManageStudents()
        {
            Console.WriteLine($"[Decanat] Faculty '{_faculty}' manages {_students.Count} student(s):");
            foreach (var s in _students)
                Console.WriteLine($"  {s.Name} | GPA={s.GPA} | " +
                    $"{(s.IsEligibleForScholarship() ? "Стипендія" : "")}");
        }

        // Generates a summary report.
        public void GenerateReport()
        {
            Console.WriteLine($"[Decanat] Звіт факультету '{_faculty}'. Керівник: {_headName}");
            Console.WriteLine($"  Студентів: {_students.Count} | Документів: {_documents.Count}");
            Console.WriteLine($"  Операційний: {IsOperational()} | Перевантажений: {IsOverloaded()}");
            int eligible = _students.FindAll(s => s.IsEligibleForScholarship()).Count;
            int atRisk = _students.FindAll(s => s.IsAtRisk()).Count;
            Console.WriteLine($"  Право на стипендію: {eligible} | Під загрозою: {atRisk}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  Факультет    : {_faculty}");
            Console.WriteLine($"  Керівник     : {_headName}");
            Console.WriteLine($"  Активний     : {IsActive}");
            Console.WriteLine($"  Студентів    : {_students.Count}");
            Console.WriteLine($"  Документів   : {_documents.Count}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
        }

        public override string ToString() => $"Decanat of faculty '{_faculty}', Head: {_headName}";
    }
}