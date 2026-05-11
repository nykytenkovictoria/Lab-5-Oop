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


        public bool IsLarge() => _students.Count > 50;

        /// Чи достатньо викладачів (хоча б 1 на 20 студентів)
        public bool HasSufficientStaff() => _teachers.Count > 0 
            && _students.Count / (_teachers.Count * 20.0) <= 1;
        public bool IsActive() => _courses.Count > 0 && _teachers.Count > 0 
            && _students.Count > 0;

        /// Чи є конкретний курс на кафедрі
        public bool HasCourse(string courseId) =>
            _courses.Exists(c => c.CourseId.Equals(courseId, StringComparison.OrdinalIgnoreCase));
        private string BuildInfo() =>
            $"Кафедра: {_name} | Декан: {_dean} | Факультет: {Faculty}";


        public void AddCourse(Course course) => _courses.Add(course);
        public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);
        public void AddStudent(Student student) => _students.Add(student);

        /// Prints general department information.
        public void GetInfo()
        {
            Console.WriteLine($"[Department] {FullInfo}");
            Console.WriteLine($"  Курсів: {_courses.Count} | Викладачів: {_teachers.Count} | Студентів: {_students.Count}");
            Console.WriteLine($"  Активна: {IsActive()} | Достатньо кадрів: {HasSufficientStaff()}");
        }

        /// Displays the current schedule (stub).
        public void ManageSchedule()
        {
            Console.WriteLine($"[Department] '{_name}': формування розкладу...");
            foreach (var c in _courses)
                Console.WriteLine($"Курс '{c.Title}' ({(c.IsOnlineCourse() ? "онлайн" : "очно")})");
        }

        public void ListExcellentStudents()
        {
            Console.WriteLine($"[Department] Відмінники кафедри '{_name}':");
            var found = false;
            foreach (var s in _students)
                if (s.IsExcellentStudent()) { Console.WriteLine($"{s.Name} (GPA={s.GPA})"); found = true; }
            if (!found) Console.WriteLine("  (немає відмінників)");
        }

        public void ListAtRiskStudents()
        {
            Console.WriteLine($"[Department] Студенти під загрозою відрахування:");
            var found = false;
            foreach (var s in _students)
                if (s.IsAtRisk()) { Console.WriteLine($"{s.Name} (GPA={s.GPA})"); found = true; }
            if (!found) Console.WriteLine("  (немає таких студентів)");
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