// Student.cs
// Електронний кабінет студента

using System.Dynamic;

namespace DigitalUniversity
{
   
    public class Student
    {
        private string _id;
        private string _name;
        private string _group;
        private string _course;
        private double _gpa;
        private static int _totalStudents;

        public string Id
        {
            get => _id;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _name = value; 
            }
        }

        public string Group
        {
            get => _group;
            set => _group = value;
        }

        public double GPA
        {
            get => _gpa;
            set { 
                if (value >= 0.0 && value <= 100.0)
                    _gpa = value; 
            
            }
        }

        public static int TotalStudents => _totalStudents;

        public bool IsActive { get; set; }

        // Закритий сетер
        public string FullInfo { get; private set; }


        // 1. Статичний конструктор
        static Student()
        {
            _totalStudents = 0;
            Console.WriteLine("[Student] Статичний конструктор: клас ініціалізовано.");
        }

        // 2. Конструктор без параметрів
        public Student()
        {
            _id = "S-000"; 
            _name = "Невідомий"; 
            _group = "—"; 
            _course = "—"; 
            _gpa = 0.0;
            IsActive = false;
            FullInfo = BuildInfo();
            _totalStudents++;
            Console.WriteLine($"[Student] Конструктор без параметрів: створено '{_name}'");
        }

        public Student(string id, string name, string group, string course)
        {
            _id = id;
            _name = name; 
            _group = group; 
            _course = course; 
            _gpa = 0.0;
            IsActive = true;
            FullInfo = BuildInfo();
            _totalStudents++;
            Console.WriteLine($"[Student] Конструктор з параметрами: створено '{_name}', група {_group}");
        }

        // 4. Конструктор що викликає інший конструктор
        public Student(string id, string name, string group, string course, double gpa)
            : this(id, name, group, course)
        {
            _gpa = gpa;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Student] Конструктор виклику іншого: GPA = {_gpa}");
        }

        // 5. Конструктор копії
        public Student(Student other)
        {
            _id = other._id + "-copy";
            _name = other._name;
            _group = other._group;
            _course = other._course;
            _gpa = other._gpa;
            IsActive = other.IsActive;
            FullInfo = BuildInfo();
            _totalStudents++;
            Console.WriteLine($"[Student] Конструктор копії: скопійовано '{_name}'");
        }


        // 6. Закритий конструктор
        private Student(string id)
        {
            _id = id;
            _name = "System";
            _group = "—";
            _course = "—";
            IsActive = false;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Student] Закритий конструктор: службовий [{_id}]");
        }

        public static Student CreateSystem(string id) => new Student(id);


        private string BuildInfo() =>
            $"[{_id}] {_name} | Група: {_group} | Курс: {_course} | GPA: {_gpa}";

        // Opens the student's personal electronic cabinet.
        public void ViewCabinet()
        {
            Console.WriteLine($"[Student] {_name} opens electronic cabinet. Group: {_group}, Course: {_course}");
        }

        // Submits a report or assignment.
        public void SubmitReport(OnlineReport report)
        {
            Console.WriteLine($"[Student] {_name} submits report: {report.Type}");
        }

        // Views the current class schedule
        public void ViewSchedule()
        {
            Console.WriteLine($"[Student] {_name} views schedule for group {_group}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  ID           : {_id}");
            Console.WriteLine($"  Ім'я         : {_name}");
            Console.WriteLine($"  Група        : {_group}");
            Console.WriteLine($"  Курс         : {_course}");
            Console.WriteLine($"  GPA          : {_gpa}");
            Console.WriteLine($"  Активний     : {IsActive}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього студ. : {TotalStudents}");
        }

        public override string ToString() => $"Student [{_id}] {_name}, group {_group}";
    }
}
