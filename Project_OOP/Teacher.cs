// Teacher.cs
// Електронний кабінет викладача



namespace DigitalUniversity
{
    public class Teacher : UniversityPerson
    {
        private string _position;
        private int _experienceYears;
        private int _coursesCount;
        private static int _totalTeachers;


        public string Position
        {
            get => _position;
            set => _position = value;
        }

        public int ExperienceYears
        {
            get => _experienceYears;
            set
            {
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
        public Teacher() : base("T-000", "Невідомий")
        {
            _position = "—";
            _experienceYears = 0;
            Department = "—";
            FullInfo = BuildInfo();
            _totalTeachers++;
            Console.WriteLine($"[Teacher] Конструктор без параметрів: створено '{_name}'");
        }

        // 3. Конструктор з параметрами
        public Teacher(string id, string name, string position) : base(id, name)
        {
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
        public Teacher(Teacher other) : base(other._id + "-copy", other._name)
        {
            _position = other._position;
            _experienceYears = other._experienceYears;
            Department = other.Department;
            FullInfo = BuildInfo();
            _totalTeachers++;
            Console.WriteLine($"[Teacher] Конструктор копії: скопійовано '{_name}'");
        }

        // 6. Закритий конструктор
        private Teacher(string id) : base(id, "Guest", false)
        {
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

        public override string GetRole() => "Teacher";

        private string BuildInfo() =>
            $"[{_id}] {_name} | Посада: {_position} | Досвід: {_experienceYears} р. | Кафедра: {Department}";

        // Opens the teacher's personal electronic cabinet.
        public override void ViewCabinet()
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

        public override bool CanSubmitReport() => IsActive && _coursesCount > 0;

        // Submits a report to the system.
        public override void SubmitReport(OnlineReport report)
        {
            if (CanSubmitReport()) 
            {
                Console.WriteLine($"[Teacher] {_name} submits report: {report.Type}");
                report.Submit();
            }
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

        public override void PrintInfo()
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

        public static Teacher operator ++(Teacher t)
        {
            t._experienceYears++;
            t.FullInfo = t.BuildInfo();
            Console.WriteLine($"[Teacher op++] {t.Name}: досвід = {t.ExperienceYears} р.");
            return t;
        }

        // Оператори порівняння: порівнюємо по досвіду
        public static bool operator ==(Teacher a, Teacher b) => a._experienceYears == b._experienceYears;
        public static bool operator !=(Teacher a, Teacher b) => a._experienceYears != b._experienceYears;
        public static bool operator >(Teacher a, Teacher b) => a._experienceYears > b._experienceYears;
        public static bool operator <(Teacher a, Teacher b) => a._experienceYears < b._experienceYears;
        public static bool operator >=(Teacher a, Teacher b) => a._experienceYears >= b._experienceYears;
        public static bool operator <=(Teacher a, Teacher b) => a._experienceYears <= b._experienceYears;

        public override bool Equals(object? obj) => obj is Teacher t && _experienceYears == t._experienceYears;
        public override int GetHashCode() => _experienceYears.GetHashCode();
    }
}
