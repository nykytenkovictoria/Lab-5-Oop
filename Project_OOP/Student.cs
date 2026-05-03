// Student.cs
// Електронний кабінет студента

namespace DigitalUniversity
{
   
    public class Student
    {
        private string _id;
        private string _name;
        private string _group;
        private string _course;

        public string Id => _id;
        public string Name => _name;

        public Student(string id, string name, string group, string course)
        {
            _id = id;
            _name = name;
            _group = group;
            _course = course;
        }

        // Opens the student's personal electronic cabinet.
        public void ViewCabinet()
        {
            Console.WriteLine($"[Student] {_name} opens electronic cabinet. Group: {_group}, Course: {_course}");
        }

        // Submits a report or assignment.
        public void SubmitReport(OnlineReport report)
        {
            Console.WriteLine($"[Student] {_name} submits report: {report.Type}");
        }

        // Views the current class schedule
        public void ViewSchedule()
        {
            Console.WriteLine($"[Student] {_name} views schedule for group {_group}");
        }

        public override string ToString() => $"Student [{_id}] {_name}, group {_group}";
    }
}
