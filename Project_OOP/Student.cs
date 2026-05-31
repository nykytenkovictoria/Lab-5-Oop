// Student.cs
// Електронний кабінет студента

using System.Dynamic;

namespace DigitalUniversity
{

    public class Student : UniversityPerson
    {
        private string _group;
        private string _course;
        private double _gpa;
        private int _missedClasses;
        private static int _totalStudents;
        private static List<Student> _allStudents = new();

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
        }

        // 2. Конструктор без параметрів
        public Student() : base("S-000", Messages.Get("student", "unknown_name"), false)
        {
            _group = "—";
            _course = "—";
            _gpa = 0.0;
            _missedClasses = 0;
            FullInfo = BuildInfo();
            _totalStudents++;
            _allStudents.Add(this);
        }

        public Student(string id, string name, string group, string course) : base(id, name, true)
        {
            _group = group;
            _course = course;
            _gpa = 0.0;
            _missedClasses = 0;
            FullInfo = BuildInfo();
            _totalStudents++;
            _allStudents.Add(this);
        }

        // 4. Конструктор що викликає інший конструктор
        public Student(string id, string name, string group, string course, double gpa, int missedClasses)
            : this(id, name, group, course)
        {
            _gpa = gpa;
            _missedClasses = missedClasses;
            FullInfo = BuildInfo();
        }

        // 5. Конструктор копії
        public Student(Student other) : base(other._id + "-copy", other._name, other.IsActive)
        {
            _group = other._group;
            _course = other._course;
            _gpa = other._gpa;
            _missedClasses = other._missedClasses;
            FullInfo = BuildInfo();
            _totalStudents++;
            _allStudents.Add(this);
        }


        // 6. Закритий конструктор
        private Student(string id) : base(id, Messages.Get("student", "system_name"), false)
        {
            _group = "—";
            _course = "—";
            _missedClasses = 0;
            FullInfo = BuildInfo();
            _allStudents.Add(this);
        }

        public static Student CreateSystem(string id) => new Student(id);


        private string BuildInfo() =>
            Messages.Get("student", "build_info", _id, _name, _group, _course, _gpa);

        public override string GetRole() => Messages.Get("student", "role");

        // Opens the student's personal electronic cabinet.
        public override void ViewCabinet()
        {
            Messages.Print("student", "cabinet_open", _name, _group, _course);
            PrintInfo();
            CheckStatus();
        }

        public override bool CanSubmitReport() => IsActive && !HasDebt();

        // Submits a report or assignment.
        public override void SubmitReport(OnlineReport report)
        {
            if (CanSubmitReport())
            {
                Messages.Print("student", "submit_report", _name, report.Type);
                report.Submit();
            }

        }

        // Views the current class schedule
        public void ViewSchedule()
        {
            Messages.Print("student", "view_schedule", _name, _group);

            var enrolledCourses = GetEnrolledCourses();

            if (enrolledCourses.Count == 0)
            {
                Messages.Print("student", "schedule_no_courses");
                return;
            }

            Messages.Print("student", "schedule_total_courses", enrolledCourses.Count);

            foreach (var course in enrolledCourses)
            {
                var teacher = GetCourseTeacher(course);
                string teacherName = teacher != null ? teacher.Name : Messages.Get("student", "unknown_teacher");

                Messages.Print("student", "schedule_course",
                    course.CourseId,
                    course.Title,
                    teacherName,
                    course.Credits,
                    course.IsOnlineCourse() ? "Так" : "Ні",
                    course.ClassRoom
                    );
            }
        }

        private List<Course> GetEnrolledCourses()
        {
            var allCourses = Course.GetAllCourses();
            return allCourses.Where(c => c.IsStudentEnrolled(_id)).ToList();
        }

        private Teacher GetCourseTeacher(Course course)
        {
            var allTeachers = Teacher.GetAllTeachers();
            var teacherId = course.TeacherId;
            var teacherOfCourse = allTeachers.Where(t => t.Id == teacherId).First();
            return teacherOfCourse;
        }


        public static List<Student> GetAllStudents()
        {
            return _allStudents;
        }

        public void CheckStatus()
        {
            Messages.Print("student", "check_status", _name);
            Messages.Print("student", "predicate_excellent", IsExcellentStudent());
            Messages.Print("student", "predicate_risk", IsAtRisk());
            Messages.Print("student", "predicate_debt", HasDebt());
            Messages.Print("student", "predicate_scholarship", IsEligibleForScholarship());
            Messages.Print("student", "predicate_active", IsEnrolledAndActive());
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("student", "id")}           : {_id}");
            Console.WriteLine($"  {Messages.Get("student", "name_label")}         : {_name}");
            Console.WriteLine($"  {Messages.Get("student", "group_label")}        : {_group}");
            Console.WriteLine($"  {Messages.Get("student", "course_label")}        : {_course}");
            Console.WriteLine($"  {Messages.Get("student", "gpa_label")}          : {_gpa}");
            Console.WriteLine($"  {Messages.Get("student", "active_label")}     : {IsActive}");
            Console.WriteLine($"  {Messages.Get("student", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("student", "total_students_label")} : {TotalStudents}");
        }

        public override string ToString() => Messages.Get("student", "to_string", _id, _name, _group);

        // Бінарний -  : різниця GPA між двома студентами 
        public static double operator -(Student a, Student b)
        {
            double diff = Math.Abs(a._gpa - b._gpa);
            Messages.Print("student", "op_minus", a.Name, b.Name, diff);
            return diff;
        }

        // Унарний ++  : підвищити GPA на 1 бал
        public static Student operator ++(Student s)
        {
            s._gpa = Math.Min(100, s._gpa + 1);
            s.FullInfo = s.BuildInfo();
            Messages.Print("student", "op_increment", s.Name, s.GPA);
            return s;
        }

        // Унарний --  : знизити GPA на 1 бал
        public static Student operator --(Student s)
        {
            s._gpa = Math.Max(0, s._gpa - 1);
            s.FullInfo = s.BuildInfo();
            Messages.Print("student", "op_decrement", s.Name, s.GPA);
            return s;
        }


        // Унарний !   : чи НЕ відмінник
        public static bool operator !(Student s) => !s.IsExcellentStudent();


        // true / false : студент "активний"
        public static bool operator true(Student s) => s.IsActive;
        public static bool operator false(Student s) => !s.IsActive;

        // Оператори порівняння
        public static bool operator >(Student a, Student b) => a._gpa > b._gpa;
        public static bool operator <(Student a, Student b) => a._gpa < b._gpa;
        public static bool operator >=(Student a, Student b) => a._gpa >= b._gpa;
        public static bool operator <=(Student a, Student b) => a._gpa <= b._gpa;

        public override bool Equals(object? obj) => obj is Student s && _gpa == s._gpa;

        public override int GetHashCode() => _gpa.GetHashCode();
    }
}