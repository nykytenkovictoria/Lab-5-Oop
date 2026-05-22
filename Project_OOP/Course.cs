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
            _maxCapacity = 30;
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
            _maxCapacity = 30;
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
            _maxCapacity = other._maxCapacity;
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
            $"[{_courseId}] {_title} | Кредити: {_credits} | Онлайн: {IsOnline} | Зараховано: {_enrolledCount}/{_maxCapacity}";

        public bool HasAvailableSpots() => _enrolledCount < _maxCapacity;

        public bool IsFull() => _enrolledCount >= _maxCapacity;

        public bool IsActive() => _enrolledCount > 0;

        public bool IsOnlineCourse() => IsOnline;

        public bool IsIntensive() => _credits > 4;


        /// Enrols a student in this course.
        public void Enroll(Student student)
        {
            if (IsFull())
            { Console.WriteLine($"[Course] '{_title}': немає місць для {student.Name}"); return; }
            _enrolledCount++;
            FullInfo = BuildInfo();
            Console.WriteLine($"[Course] '{_title}':" +
                $" зараховано {student.Name}. Місць зайнято: {_enrolledCount}/{_maxCapacity}");
        }

        /// Returns a list of available materials (stub).
        public void GetMaterials()
        {
            Console.WriteLine($"[Course] '{_title}': loading course materials ({_credits} credits)...");
        }

        /// Displays course summary information.
        public void GetInfo()
        {
            PrintInfo();
            PrintEnrollmentStatus();
        }

        public void PrintEnrollmentStatus()
        {
            Console.WriteLine($"[Course] '{_title}' — стан запису:");
            Console.WriteLine($"  Зараховано    : {_enrolledCount}/{_maxCapacity}");
            Console.WriteLine($"  Є місця       : {HasAvailableSpots()}");
            Console.WriteLine($"  Заповнений    : {IsFull()}");
            Console.WriteLine($"  Активний      : {IsActive()}");
            Console.WriteLine($"  Онлайн        : {IsOnlineCourse()}");
            Console.WriteLine($"  Інтенсивний   : {IsIntensive()}");
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

        // Унарний ++  : підвищити credits на 1 
        public static Course operator ++(Course c)
        {
            c._credits += 1;
            Console.WriteLine($"[Course op++] {c.Title}: credits підвищено до {c._credits}");
            return c;
        }

        // Унарний --  : знизити credits на 1 
        public static Course operator --(Course c)
        {
            c._credits += 1;
            Console.WriteLine($"[Course op--] {c.Title}: credits знижено до {c._credits}");
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
            Console.WriteLine($"[Course op+] Об'єднано курси: '{result.Title}', кредитів={result.Credits}");
            return result;
        }

        // Оператори порівняння: порівнюємо по кількості кредитів
        public static bool operator ==(Course a, Course b) => a._credits == b._credits;
        public static bool operator !=(Course a, Course b) => a._credits != b._credits;
        public static bool operator >(Course a, Course b) => a._credits > b._credits;
        public static bool operator <(Course a, Course b) => a._credits < b._credits;
        public static bool operator >=(Course a, Course b) => a._credits >= b._credits;
        public static bool operator <=(Course a, Course b) => a._credits <= b._credits;

        public override bool Equals(object? obj) => obj is Course c && _credits == c._credits;

        public override int GetHashCode() => _credits.GetHashCode();
    }
}