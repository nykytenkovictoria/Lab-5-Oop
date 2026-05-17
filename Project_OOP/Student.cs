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
        private int _missedClasses;
        private static int _totalStudents;

        public string Id
        {
            get => _id;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }

        public int MissedClasses
        {
            get => _missedClasses;
            set
            {
                if (value >= 0)
                    _missedClasses = value;
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
            set
            {
                if (value >= 0.0 && value <= 100.0)
                    _gpa = value;

            }
        }

        public static int TotalStudents => _totalStudents;

        public bool IsActive { get; set; }

        // Закритий сетер
        public string FullInfo { get; private set; }


        public bool IsExcellentStudent() => _gpa >= 90.0;

        public bool IsAtRisk() => _gpa < 60.0 && IsActive;

        public bool HasDebt() => _missedClasses > 20;

        public bool IsEligibleForScholarship() => _gpa >= 75.0 && !HasDebt() && IsActive;

        public bool IsEnrolledAndActive() => IsActive && !string.IsNullOrEmpty(_group) && _group != "—";


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
            _missedClasses = 0;
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
            _missedClasses = 0;
            IsActive = true;
            FullInfo = BuildInfo();
            _totalStudents++;
            Console.WriteLine($"[Student] Конструктор з параметрами: створено '{_name}', група {_group}");
        }

        // 4. Конструктор що викликає інший конструктор
        public Student(string id, string name, string group, string course, double gpa, int missedClasses)
            : this(id, name, group, course)
        {
            _gpa = gpa;
            _missedClasses = missedClasses;
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
            _missedClasses = other._missedClasses;
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
            _missedClasses = 0;
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
            Console.WriteLine($"[Student] {_name} " +
                $"opens electronic cabinet. Group: {_group}, Course: {_course}");
            Console.WriteLine($"  Група: {_group} " +
                $"| Курс: {_course} | GPA: {_gpa}");
            Console.WriteLine($"  Статус: {(IsExcellentStudent() ?
                "Відмінник" : IsAtRisk() ?
                "Під загрозою відрахування"
                : "Звичайний студент")}");
        }

        // Submits a report or assignment.
        public void SubmitReport(OnlineReport report)
        {
            Console.WriteLine($"[Student] {_name} submits report: {report.Type}");
            report.Submit();
        }

        // Views the current class schedule
        public void ViewSchedule()
        {
            Console.WriteLine($"[Student] {_name} views schedule for group {_group}");
        }

        public void CheckStatus()
        {
            Console.WriteLine($"[Student] Статус {_name}:");
            Console.WriteLine($"  Відмінник          : {IsExcellentStudent()}");
            Console.WriteLine($"  Під загрозою       : {IsAtRisk()}");
            Console.WriteLine($"  Є заборгованість   : {HasDebt()}");
            Console.WriteLine($"  Право на стипендію : {IsEligibleForScholarship()}");
            Console.WriteLine($"  Активний/зарахований: {IsEnrolledAndActive()}");
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

        // Бінарний -  : різниця GPA між двома студентами 
        public static double operator -(Student a, Student b)
        {
            double diff = Math.Abs(a._gpa - b._gpa);
            Console.WriteLine($"[Student op-] Різниця GPA між {a.Name} та {b.Name}: {diff}");
            return diff;
        }

        // Унарний ++  : підвищити GPA на 1 бал
        public static Student operator ++(Student s)
        {
            s._gpa = Math.Min(100, s._gpa + 1);
            s.FullInfo = s.BuildInfo();
            Console.WriteLine($"[Student op++] {s.Name}: GPA підвищено до {s.GPA}");
            return s;
        }

        // Унарний --  : знизити GPA на 1 бал
        public static Student operator --(Student s)
        {
            s._gpa = Math.Max(0, s._gpa - 1);
            s.FullInfo = s.BuildInfo();
            Console.WriteLine($"[Student op--] {s.Name}: GPA знижено до {s.GPA}");
            return s;
        }


        // Унарний !   : чи НЕ відмінник
        public static bool operator !(Student s) => !s.IsExcellentStudent();


        // true / false : студент "активний"
        public static bool operator true(Student s) => s.IsActive;
        public static bool operator false(Student s) => !s.IsActive;

        // Оператори порівняння
        public static bool operator ==(Student a, Student b) => a._gpa == b._gpa;
        public static bool operator !=(Student a, Student b) => a._gpa != b._gpa;
        public static bool operator >(Student a, Student b) => a._gpa > b._gpa;
        public static bool operator <(Student a, Student b) => a._gpa < b._gpa;
        public static bool operator >=(Student a, Student b) => a._gpa >= b._gpa;
        public static bool operator <=(Student a, Student b) => a._gpa <= b._gpa;
    }
}
