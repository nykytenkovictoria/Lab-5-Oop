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
        private static int _totalCourses;

        public string Title
        {
            get => _title;
            set { 
                if (!string.IsNullOrWhiteSpace(value)) 
                    _title = value; 
            }
        }
        public string CourseId
        {
            get => _courseId;
            set { 
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
            Console.WriteLine("[Course] Статичний конструктор: клас ініціалізовано.");
        }

        // 2. Конструктор без параметрів
        public Course()
        {
            _courseId = "C-000"; 
            _title = "Без назви"; 
            _credits = 1;
            _enrolledCount = 0;
            IsOnline = false;
            FullInfo = BuildInfo();
            _totalCourses++;
            Console.WriteLine($"[Course] Конструктор без параметрів: створено '{_title}'");
        }

        // 3. Конструктор з параметрами
        public Course(string courseId, string title, int credits)
        {
            _courseId = courseId; 
            _title = title; 
            _credits = credits;
            _enrolledCount = 0;
            IsOnline = false;
            FullInfo = BuildInfo();
            _totalCourses++;
            Console.WriteLine($"[Course] Конструктор з параметрами: '{_title}', {_credits} кредити");
        }

        // 4. Конструктор що викликає інший конструктор
        public Course(string courseId, string title, int credits, bool isOnline)
            : this(courseId, title, credits)
        {
            IsOnline = isOnline;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Course] Конструктор виклику іншого: онлайн={IsOnline}");
        }

        // 5. Конструктор копії
        public Course(Course other)
        {
            _courseId = other._courseId + "-copy";
            _title = other._title;
            _credits = other._credits;
            IsOnline = other.IsOnline;
            _enrolledCount = 0;
            FullInfo = BuildInfo();
            _totalCourses++;
            Console.WriteLine($"[Course] Конструктор копії: скопійовано '{_title}'");
        }

        // 6. Закритий конструктор
        private Course(string courseId)
        {
            _courseId = courseId;
            _title = "Архівний курс";
            _credits = 0;
            IsOnline = false;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Course] Закритий конструктор: архівний [{_courseId}]");
        }

        public static Course CreateArchived(string id) => new Course(id);

        private string BuildInfo() =>
            $"[{_courseId}] {_title} | Кредити: {_credits} | Онлайн: {IsOnline} | Зараховано: {_enrolledCount}";

        /// Enrols a student in this course.
        public void Enroll(Student student)
        {
            Console.WriteLine($"[Course] '{_title}': student {student.Name} enrolled.");
        }

        /// Returns a list of available materials (stub).
        public void GetMaterials()
        {
            Console.WriteLine($"[Course] '{_title}': loading course materials ({_credits} credits)...");
        }

        /// Displays course summary information.
        public void GetInfo()
        {
            Console.WriteLine($"[Course] ID={_courseId}, Title='{_title}', Credits={_credits}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  ID курсу     : {_courseId}");
            Console.WriteLine($"  Назва        : {_title}");
            Console.WriteLine($"  Кредити      : {_credits}");
            Console.WriteLine($"  Онлайн       : {IsOnline}");
            Console.WriteLine($"  Зараховано   : {_enrolledCount}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього курсів: {TotalCourses}");
        }


        public override string ToString() => $"Course [{_courseId}] '{_title}' ({_credits} cr.)";
    }
}