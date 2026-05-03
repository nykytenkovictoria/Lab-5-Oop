// Teacher.cs
// Електронний кабінет викладача

namespace DigitalUniversity
{
    public class Teacher
    {
        private string _id;
        private string _name;
        private string _position;

        public string Id => _id;
        public string Name => _name;

        public Teacher(string id, string name, string position)
        {
            _id = id;
            _name = name;
            _position = position;
        }

        // Opens the teacher's personal electronic cabinet.</summary>
        public void ViewCabinet()
        {
            Console.WriteLine($"[Teacher] {_name} ({_position}) opens electronic cabinet.");
        }

        // Grades a student for a given course.
        public void GradeStudent(Student student, Course course, int grade)
        {
            Console.WriteLine($"[Teacher] {_name} grades {student.Name} in '{course.Title}': {grade}");
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

        public override string ToString() => $"Teacher [{_id}] {_name}, {_position}";
    }
}
