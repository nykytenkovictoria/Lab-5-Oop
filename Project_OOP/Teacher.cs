// Teacher.cs
// Електронний кабінет викладача

namespace DigitalUniversity
{
    public class Teacher
    {
        private string _id;
        private string _name;
        private string _position;
        private int _experienceYears;
        private int _coursesCount;
        private static int _totalTeachers;

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

        public string Position
        {
            get => _position;
            set => _position = value;
        }

        public int ExperienceYears
        {
            get => _experienceYears;
            set { 
                if (value >= 0) 
                    _experienceYears = value;
            }
        }

        public static int TotalTeachers => _totalTeachers;

        public string Department { get; set; }

        public string FullInfo { get; private set; }

        // 1. Статичний конструктор
        static Teacher()
        {
            _totalTeachers = 0;
            Console.WriteLine("[Teacher] Статичний конструктор: клас ініціалізовано.");
        }

        // 2. Конструктор без параметрів
        public Teacher()
        {
            _id = "T-000"; 
            _name = "Невідомий"; 
            _position = "—";
            _experienceYears = 0; 
            Department = "—";
            FullInfo = BuildInfo();
            _totalTeachers++;
            Console.WriteLine($"[Teacher] Конструктор без параметрів: створено '{_name}'");
        }

        // 3. Конструктор з параметрами
        public Teacher(string id, string name, string position)
        {
            _id = id; 
            _name = name;
            _position = position;
            _experienceYears = 0; 
            Department = "—";
            FullInfo = BuildInfo();
            _totalTeachers++;
            Console.WriteLine($"[Teacher] Конструктор з параметрами: створено '{_name}', посада: {_position}");
        }

        // 4. Конструктор що викликає інший конструктор
        public Teacher(string id, string name, string position, int experienceYears, string department)
            : this(id, name, position)
        {
            _experienceYears = experienceYears;
            Department = department;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Teacher] Конструктор виклику іншого: досвід={_experienceYears} р., кафедра={Department}");
        }

        // 5. Конструктор копії
        public Teacher(Teacher other)
        {
            _id = other._id + "-copy";
            _name = other._name;
            _position = other._position;
            _experienceYears = other._experienceYears;
            Department = other.Department;
            FullInfo = BuildInfo();
            _totalTeachers++;
            Console.WriteLine($"[Teacher] Конструктор копії: скопійовано '{_name}'");
        }

        // 6. Закритий конструктор
        private Teacher(string id)
        {
            _id = id; _name = "Guest";
            _position = "—";
            Department = "—";
            FullInfo = BuildInfo();
            Console.WriteLine($"[Teacher] Закритий конструктор: гостьовий [{_id}]");
        }

        public static Teacher CreateGuest(string id) => new Teacher(id);
        public bool IsSenior() => _experienceYears >= 10;

        public bool IsProfessor() => _position.Contains("Проф", StringComparison.OrdinalIgnoreCase);

        /// Чи перевантажений викладач (більше 4 курсів)
        public bool IsOverloaded() => _coursesCount > 4;

        /// Чи може брати нові курси (не перевантажений)
        public bool CanTakeMoreCourses() => _coursesCount < 5;

        /// Чи належить до кафедри
        public bool BelongsToDepartment(string deptName) =>
            Department.Equals(deptName, StringComparison.OrdinalIgnoreCase);

        private string BuildInfo() =>
            $"[{_id}] {_name} | Посада: {_position} | Досвід: {_experienceYears} р. | Кафедра: {Department}";

        // Opens the teacher's personal electronic cabinet.
        public void ViewCabinet()
        {
            Console.WriteLine($"[Teacher] {_name} ({_position}) opens electronic cabinet.");
            Console.WriteLine($"  Посада: {_position} | Досвід: {_experienceYears} р. | Кафедра: {Department}");
            Console.WriteLine($"  Рівень: {(IsProfessor() ? "Професор" : IsSenior() ?
                "Старший викладач" : "Молодший викладач")}");
        }

        // Grades a student for a given course.
        public void GradeStudent(Student student, Course course, int grade)
        {
            student.GPA = grade;
            Console.WriteLine($"[Teacher] {_name} виставляє {student.Name} оцінку {grade} з '{course.Title}'");
            if (student.IsExcellentStudent())
                Console.WriteLine($"{student.Name} отримав статус відмінника!");
        }

        // Posts educational material to a course.
        public void PostMaterial(Course course, string materialTitle)
        {
            Console.WriteLine($"[Teacher] {_name} posts '{materialTitle}' to course '{course.Title}'");
        }

        // Submits a report to the system.
        public void SubmitReport(OnlineReport report)
        {
            Console.WriteLine($"[Teacher] {_name} submits report: {report.Type}");
            report.Submit();
        }


        public void AssignCourse(Course course)
        {
            if (!CanTakeMoreCourses())
            {
                Console.WriteLine($"[Teacher] {_name}:" +
                $" перевантажений, не може взяти '{course.Title}'"); return;
            }
            _coursesCount++;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Teacher] {_name}" +
                $" призначений на курс '{course.Title}' (курсів: {_coursesCount})");
        }


        public void CheckStatus()
        {
            Console.WriteLine($"[Teacher] Статус {_name}:");
            Console.WriteLine($"  Старший викладач   : {IsSenior()}");
            Console.WriteLine($"  Професор           : {IsProfessor()}");
            Console.WriteLine($"  Перевантажений     : {IsOverloaded()}");
            Console.WriteLine($"  Може брати курси   : {CanTakeMoreCourses()}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  ID           : {_id}");
            Console.WriteLine($"  Ім'я         : {_name}");
            Console.WriteLine($"  Посада       : {_position}");
            Console.WriteLine($"  Досвід       : {_experienceYears} років");
            Console.WriteLine($"  Кафедра      : {Department}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього викл. : {TotalTeachers}");
        }

        public override string ToString() => $"Teacher [{_id}] {_name}, {_position}";
    }
}
