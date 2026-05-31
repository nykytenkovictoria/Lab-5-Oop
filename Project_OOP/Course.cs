// Course.cs
// Навчальний курс платформи цифрового університету

namespace DigitalUniversity
{
    public class Course
    {
        private string _courseId;
        private string _title;
        private int _credits;
        private int _enrolledCount;
        private int _maxCapacity;
        private static int _totalCourses;
        private string _classroom = "";
        private List<string> _enrolledStudentIds = new();
        private static List<Course> _allCourses = new();
        public string TeacherId { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _title = value;
            }
        }

        public string ClassRoom {
            get => _classroom;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _classroom = value;
            }
        }
        
        public string CourseId
        {
            get => _courseId;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _courseId = value;
            }
        }

        public int Credits
        {
            get => _credits;
            set
            {
                if (value > 0 && value <= 10)
                    _credits = value;
            }
        }

        public int EnrolledCount => _enrolledCount;

        public static int TotalCourses => _totalCourses;


        public string FullInfo { get; private set; }

        public bool IsOnline { get; set; }



        // 1. Статичний конструктор
        static Course()
        {
            _totalCourses = 0;
        }

        // 2. Конструктор без параметрів
        public Course()
        {
            _courseId = Messages.Get("course", "default_id");
            _title = Messages.Get("course", "default_title");
            _credits = 1;
            _enrolledCount = 0;
            _maxCapacity = 30;
            IsOnline = false;
            FullInfo = BuildInfo();
            _totalCourses++;
            _allCourses.Add(this);
        }

        // 3. Конструктор з параметрами
        public Course(string courseId, string title, int credits)
        {
            _courseId = courseId;
            _title = title;
            _credits = credits;
            _enrolledCount = 0;
            _maxCapacity = 30;
            IsOnline = false;
            FullInfo = BuildInfo();
            _totalCourses++;
            _allCourses.Add(this);
        }

        // 4. Конструктор що викликає інший конструктор
        public Course(string courseId, string title, int credits, bool isOnline)
            : this(courseId, title, credits)
        {
            IsOnline = isOnline;
            FullInfo = BuildInfo();
        }

        // 5. Конструктор копії
        public Course(Course other)
        {
            _courseId = other._courseId + "-copy";
            _title = other._title;
            _credits = other._credits;
            _maxCapacity = other._maxCapacity;
            IsOnline = other.IsOnline;
            _enrolledCount = 0;
            FullInfo = BuildInfo();
            _totalCourses++;
            _allCourses.Add(this);
        }

        // 6. Закритий конструктор
        private Course(string courseId)
        {
            _courseId = courseId;
            _title = Messages.Get("course", "archived_title");
            _credits = 0;
            IsOnline = false;
            FullInfo = BuildInfo();
            _allCourses.Add(this);
        }

        public static Course CreateArchived(string id) => new Course(id);

        private string BuildInfo() =>
            Messages.Get("course", "build_info", _courseId, _title, _credits, IsOnline, _enrolledCount, _maxCapacity);

        public bool HasAvailableSpots() => _enrolledCount < _maxCapacity;

        public bool IsFull() => _enrolledCount >= _maxCapacity;

        public bool IsActive() => _enrolledCount > 0;

        public bool IsOnlineCourse() => IsOnline;

        public bool IsIntensive() => _credits > 4;

        public bool IsStudentEnrolled(string studentId)
        {
            return _enrolledStudentIds.Contains(studentId);
        }
        
        public List<string> GetStudentsId()
        {
            return _enrolledStudentIds;
        }


        /// Enrols a student in this course.
        public void Enroll(Student student)
        {
            if (IsFull())
            {
                Messages.Print("course", "enroll_no_spots", _title, student.Name);
                return;
            }
            if (!_enrolledStudentIds.Contains(student.Id))
            {
                _enrolledStudentIds.Add(student.Id);
                _enrolledCount = _enrolledStudentIds.Count;
                FullInfo = BuildInfo();
                Messages.Print("course", "enroll_success",
                    _title, student.Name,
                    _enrolledCount,
                    _maxCapacity);
            }
        }


        public static List<Course> GetAllCourses()
        {
            return _allCourses;
        }

        public static List<Course> GetCoursesByTeacher(string teacherId)
        {
            return _allCourses.Where(c => c.TeacherId == teacherId).ToList();
        }

        /// Returns a list of available materials (stub).
        public void GetMaterials()
        {
            Messages.Print("course", "get_materials", _title, _credits);
        }

        /// Displays course summary information.
        public void GetInfo()
        {
            PrintInfo();
            PrintEnrollmentStatus();
        }

        public void PrintEnrollmentStatus()
        {
            Messages.Print("course", "enrollment_status_header", _title);
            Messages.Print("course", "enrollment_enrolled", _enrolledCount, _maxCapacity);
            Messages.Print("course", "enrollment_has_spots", HasAvailableSpots());
            Messages.Print("course", "enrollment_is_full", IsFull());
            Messages.Print("course", "enrollment_is_active", IsActive());
            Messages.Print("course", "enrollment_is_online", IsOnlineCourse());
            Messages.Print("course", "enrollment_is_intensive", IsIntensive());
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("course", "id_label")}     : {_courseId}");
            Console.WriteLine($"  {Messages.Get("course", "title_label")}        : {_title}");
            Console.WriteLine($"  {Messages.Get("course", "credits_label")}      : {_credits}");
            Console.WriteLine($"  {Messages.Get("course", "online_label")}       : {IsOnline}");
            Console.WriteLine($"  {Messages.Get("course", "enrolled_label")}   : {_enrolledCount}");
            Console.WriteLine($"  {Messages.Get("course", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("course", "total_courses_label")}: {TotalCourses}");
        }


        public override string ToString() => Messages.Get("course", "to_string", _courseId, _title, _credits);

        // Унарний ++  : підвищити credits на 1 
        public static Course operator ++(Course c)
        {
            c._credits += 1;
            Messages.Print("course", "op_increment", c.Title, c._credits);
            return c;
        }

        // Унарний --  : знизити credits на 1 
        public static Course operator --(Course c)
        {
            c._credits -= 1;
            Messages.Print("course", "op_decrement", c.Title, c._credits);
            return c;
        }

        public static bool operator true(Course c) => c._credits > 0;
        public static bool operator false(Course c) => c._credits <= 0;

        // Бінарний +  : об'єднати два курси → новий із сумою кредитів
        public static Course operator +(Course a, Course b)
        {
            var result = new Course(
                a._courseId + "+" + b._courseId,
                a._title + " + " + b._title,
                a._credits + b._credits
            );
            Messages.Print("course", "op_plus", result.Title, result.Credits);
            return result;
        }

        // Оператори порівняння: порівнюємо по кількості кредитів
        public static bool operator ==(Course a, Course b) => a._credits == b._credits;
        public static bool operator !=(Course a, Course b) => a._credits != b._credits;

        public override bool Equals(object? obj) => obj is Course c && _credits == c._credits;

        public override int GetHashCode() => _credits.GetHashCode();
    }
}