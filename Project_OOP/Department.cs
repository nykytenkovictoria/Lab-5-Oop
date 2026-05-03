// Department.cs
// Кафедра / підрозділ університету

using System.Collections.Generic;

namespace DigitalUniversity
{
    public class Department
    {
        private string _name;
        private string _dean;
        // Aggregation — courses exist independently of this department
        private List<Course> _courses = new();
        private List<Teacher> _teachers = new();
        private List<Student> _students = new();

        public string Name => _name;

        public Department(string name, string dean)
        {
            _name = name;
            _dean = dean;
        }

        public void AddCourse(Course course) => _courses.Add(course);
        public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);
        public void AddStudent(Student student) => _students.Add(student);

        /// Prints general department information.
        public void GetInfo()
        {
            Console.WriteLine($"[Department] '{_name}', Dean: {_dean}");
            Console.WriteLine($"  Courses: {_courses.Count}, Teachers: {_teachers.Count}, Students: {_students.Count}");
        }

        /// Displays the current schedule (stub).
        public void ManageSchedule()
        {
            Console.WriteLine($"[Department] '{_name}': managing schedule for {_courses.Count} course(s)...");
        }

        public override string ToString() => $"Department '{_name}' (Dean: {_dean})";
    }
}