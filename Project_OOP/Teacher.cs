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
        private static List<Teacher> _allTeachers = new();


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
        }

        // 2. Конструктор без параметрів
        public Teacher() : base("T-000", Messages.Get("teacher", "unknown_name"))
        {
            _position = Messages.Get("teacher", "default_position");
            _experienceYears = 0;
            Department = Messages.Get("teacher", "default_department");
            FullInfo = BuildInfo();
            _totalTeachers++;
            _allTeachers.Add(this);
        }

        // 3. Конструктор з параметрами
        public Teacher(string id, string name, string position) : base(id, name)
        {
            _position = position;
            _experienceYears = 0;
            Department = Messages.Get("teacher", "default_department");
            FullInfo = BuildInfo();
            _totalTeachers++;
            _allTeachers.Add(this);
        }

        // 4. Конструктор що викликає інший конструктор
        public Teacher(string id, string name, string position, int experienceYears, string department)
            : this(id, name, position)
        {
            _experienceYears = experienceYears;
            Department = department;
            FullInfo = BuildInfo();
            _allTeachers.Add(this);
        }

        // 5. Конструктор копії
        public Teacher(Teacher other) : base(other._id + "-copy", other._name)
        {
            _position = other._position;
            _experienceYears = other._experienceYears;
            Department = other.Department;
            FullInfo = BuildInfo();
            _totalTeachers++;
            _allTeachers.Add(this);
        }

        // 6. Закритий конструктор
        private Teacher(string id) : base(id, Messages.Get("teacher", "guest_name"), false)
        {
            _position = Messages.Get("teacher", "default_position");
            Department = Messages.Get("teacher", "default_department");
            FullInfo = BuildInfo();
            _allTeachers.Add(this);
        }

        public static Teacher CreateGuest(string id) => new Teacher(id);
        public bool IsSenior() => _experienceYears >= 10;

        public bool IsProfessor() => _position.Contains(Messages.Get("teacher", "professor_keyword"), StringComparison.OrdinalIgnoreCase);

        /// Чи перевантажений викладач (більше 4 курсів)
        public bool IsOverloaded() => _coursesCount > 4;

        /// Чи може брати нові курси (не перевантажений)
        public bool CanTakeMoreCourses() => _coursesCount < 5;

        /// Чи належить до кафедри
        /// 

        public static List<Teacher> GetAllTeachers()
        {
            return _allTeachers;
        }
        public bool BelongsToDepartment(string deptName) =>
            Department.Equals(deptName, StringComparison.OrdinalIgnoreCase);

        public override string GetRole() => Messages.Get("teacher", "role");

        private string BuildInfo() =>
            Messages.Get("teacher", "build_info", _id, _name, _position, _experienceYears, Department);

        // Opens the teacher's personal electronic cabinet.
        public override void ViewCabinet()
        {
            Messages.Print("teacher", "cabinet_open", _name, _position);
            Console.WriteLine(Messages.Get("teacher", "cabinet_info", _position, _experienceYears, Department));
            CheckStatus();
        }

        // Grades a student for a given course.
        public void GradeStudent(Student student, Course course, int grade)
        {
            student.GPA = grade;
            Messages.Print("teacher", "grade_student", _name, student.Name, grade, course.Title);
            if (student.IsExcellentStudent())
                Messages.Print("teacher", "excellent_status", student.Name);
        }

        // Posts educational material to a course.
        public void PostMaterial(Course course, string materialTitle)
        {
            Messages.Print("teacher", "post_material", _name, materialTitle, course.Title);
        }

        public override bool CanSubmitReport() => IsActive && _coursesCount > 0;

        public void ViewSchedule()
        {
            Messages.Print("teacher", "view_schedule", _name, _position);

            var teachingCourses = GetTeachingCourses();

            if (teachingCourses.Count == 0)
            {
                Messages.Print("teacher", "schedule_no_courses");
                return;
            }

            Messages.Print("teacher", "schedule_header", teachingCourses.Count);

            foreach (var course in teachingCourses)
            {
                Messages.Print("teacher", "course_title", course.Title, course.CourseId, course.Credits, course.EnrolledCount);

                var students = GetEnrolledStudents(course);

                if (students.Count > 0)
                {
                    foreach (var student in students)
                    {
                        if (student != null)
                            Console.WriteLine(student.FullInfo);
                    }
                }
                else
                {
                    Messages.Print("teacher", "no_students");
                }
                Console.WriteLine();
            }
        }

        private List<Course> GetTeachingCourses()
        {
            return Course.GetCoursesByTeacher(_id);
        }

        private List<Student> GetEnrolledStudents(Course course)
        {
            var students = course.GetStudentsId();
            var allStudents = Student.GetAllStudents();
            var listStundents = new List<Student>();
            foreach (var studentId in students)
            {
                var student = allStudents.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                    listStundents.Add(student);
            }

            return listStundents;
        }

        // Submits a report to the system.
        public override void SubmitReport(OnlineReport report)
        {
            if (CanSubmitReport())
            {
                Messages.Print("teacher", "submit_report", _name, report.Type);
                report.Submit();
            }
        }


        public void AssignCourse(Course course)
        {
            if (!CanTakeMoreCourses())
            {
                Messages.Print("teacher", "overloaded_cannot_take", _name, course.Title);
                return;
            }
            course.TeacherId = _id;
            _coursesCount++;
            FullInfo = BuildInfo();
            Messages.Print("teacher", "assign_course", _name, course.Title, _coursesCount);
        }


        public void CheckStatus()
        {
            Messages.Print("teacher", "check_status", _name);
            Messages.Print("teacher", "predicate_senior", IsSenior());
            Messages.Print("teacher", "predicate_professor", IsProfessor());
            Messages.Print("teacher", "predicate_overloaded", IsOverloaded());
            Messages.Print("teacher", "predicate_can_take", CanTakeMoreCourses());
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("teacher", "id")}           : {_id}");
            Console.WriteLine($"  {Messages.Get("teacher", "name_label")}         : {_name}");
            Console.WriteLine($"  {Messages.Get("teacher", "position_label")}       : {_position}");
            Console.WriteLine($"  {Messages.Get("teacher", "experience_label")}       : {_experienceYears} {Messages.Get("teacher", "years")}");
            Console.WriteLine($"  {Messages.Get("teacher", "department_label")}      : {Department}");
            Console.WriteLine($"  {Messages.Get("teacher", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("teacher", "total_teachers_label")} : {TotalTeachers}");
        }

        public override string ToString() => Messages.Get("teacher", "to_string", _id, _name, _position);

        public static Teacher operator ++(Teacher t)
        {
            t._experienceYears++;
            t.FullInfo = t.BuildInfo();
            Messages.Print("teacher", "op_increment", t.Name, t.ExperienceYears);
            return t;
        }

        // Оператори порівняння: порівнюємо по досвіду
        public static bool operator >(Teacher a, Teacher b) => a._experienceYears > b._experienceYears;
        public static bool operator <(Teacher a, Teacher b) => a._experienceYears < b._experienceYears;
        public static bool operator >=(Teacher a, Teacher b) => a._experienceYears >= b._experienceYears;
        public static bool operator <=(Teacher a, Teacher b) => a._experienceYears <= b._experienceYears;

        public override bool Equals(object? obj) => obj is Teacher t && _experienceYears == t._experienceYears;
        public override int GetHashCode() => _experienceYears.GetHashCode();
    }
}