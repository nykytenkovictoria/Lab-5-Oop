// Department.cs
// Кафедра / підрозділ університету

using System.Collections.Generic;

namespace DigitalUniversity
{
    public class Department
    {
        private string _name;
        private string _dean;
        // Aggregation — courses exist independently of this department
        private List<Course> _courses = new();
        private List<Teacher> _teachers = new();
        private List<Student> _students = new();

        public string Name
        {
            get => _name;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _name = value; 
            }
        }

        public string Dean
        {
            get => _dean;
            set => _dean = value;
        }


        public int CourseCount => _courses.Count;
        public int TeacherCount => _teachers.Count;
        public int StudentCount => _students.Count;

        public string Faculty { get; set; }

        public string FullInfo { get; private set; }

        // 1. Статичний конструктор
        static Department()
        {
            Console.WriteLine("[Department] Статичний конструктор: клас ініціалізовано.");
        }

        // 2. Конструктор без параметрів
        public Department()
        {
            _name = "Невідома кафедра";
            _dean = "—"; 
            Faculty = "—";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Department] Конструктор без параметрів: '{_name}'");
        }

        // 3. Конструктор з параметрами (агрегація — курси передаються ззовні)
        public Department(string name, string dean)
        {
            _name = name;
            _dean = dean;
            Faculty = "—";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Department] Конструктор з параметрами: '{_name}', декан: {_dean}");
        }

        // 4. Конструктор що викликає інший конструктор
        public Department(string name, string dean, string faculty)
            : this(name, dean)
        {
            Faculty = faculty;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Department] Конструктор виклику іншого: факультет={Faculty}");
        }

        // 5. Конструктор копії (агрегація)
        public Department(Department other)
        {
            _name = other._name + " (копія)"; _dean = other._dean;
            Faculty = other.Faculty;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Department] Конструктор копії: скопійовано '{_name}'");
        }

        // 6. Закритий конструктор
        private Department(string name)
        {
            _name = name; 
            _dean = "System";
            Faculty = "—";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Department] Закритий конструктор: системна кафедра '{_name}'");
        }

        public static Department CreateSystem(string name) => new Department(name);

        private string BuildInfo() =>
            $"Кафедра: {_name} | Декан: {_dean} | Факультет: {Faculty}";


        public void AddCourse(Course course) => _courses.Add(course);
        public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);
        public void AddStudent(Student student) => _students.Add(student);

        /// Prints general department information.
        public void GetInfo()
        {
            Console.WriteLine($"[Department] '{_name}', Dean: {_dean}");
            Console.WriteLine($"  Courses: {_courses.Count}, Teachers: {_teachers.Count}, Students: {_students.Count}");
        }

        /// Displays the current schedule (stub).
        public void ManageSchedule()
        {
            Console.WriteLine($"[Department] '{_name}': managing schedule for {_courses.Count} course(s)...");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  Кафедра      : {_name}");
            Console.WriteLine($"  Декан        : {_dean}");
            Console.WriteLine($"  Факультет    : {Faculty}");
            Console.WriteLine($"  Курсів       : {_courses.Count}");
            Console.WriteLine($"  Викладачів   : {_teachers.Count}");
            Console.WriteLine($"  Студентів    : {_students.Count}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
        }

        public override string ToString() => $"Department '{_name}' (Dean: {_dean})";
    }
}