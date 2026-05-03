// Course.cs
// Навчальний курс платформи цифрового університету

namespace DigitalUniversity
{
    public class Course
    {
        private string _courseId;
        private string _title;
        private int _credits;

        public string Title => _title;
        public string CourseId => _courseId;

        public Course(string courseId, string title, int credits)
        {
            _courseId = courseId;
            _title = title;
            _credits = credits;
        }

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

        public override string ToString() => $"Course [{_courseId}] '{_title}' ({_credits} cr.)";
    }
}