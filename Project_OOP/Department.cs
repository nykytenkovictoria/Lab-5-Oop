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

        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }

        public string Dean
        {
            get => _dean;
            set => _dean = value;
        }


        public int CourseCount => _courses.Count;
        public int TeacherCount => _teachers.Count;
        public int StudentCount => _students.Count;

        public string Faculty { get; set; }

        public string FullInfo { get; private set; }


        // 2. Конструктор без параметрів
        public Department()
        {
            _name = Messages.Get("department", "default_name");
            _dean = Messages.Get("department", "default_dean");
            Faculty = Messages.Get("department", "default_faculty");
            FullInfo = BuildInfo();
        }

        // 3. Конструктор з параметрами (агрегація — курси передаються ззовні)
        public Department(string name, string dean)
        {
            _name = name;
            _dean = dean;
            Faculty = Messages.Get("department", "default_faculty");
            FullInfo = BuildInfo();
        }

        // 4. Конструктор що викликає інший конструктор
        public Department(string name, string dean, string faculty)
            : this(name, dean)
        {
            Faculty = faculty;
            FullInfo = BuildInfo();
        }

        // 5. Конструктор копії (агрегація)
        public Department(Department other)
        {
            _name = other._name + Messages.Get("department", "copy_suffix");
            _dean = other._dean;
            Faculty = other.Faculty;
            FullInfo = BuildInfo();
        }

        // 6. Закритий конструктор
        private Department(string name)
        {
            _name = name;
            _dean = Messages.Get("department", "system_dean");
            Faculty = Messages.Get("department", "default_faculty");
            FullInfo = BuildInfo();
        }

        public static Department CreateSystem(string name) => new Department(name);


        public bool IsLarge() => _students.Count > 50;

        /// Чи достатньо викладачів (хоча б 1 на 20 студентів)
        public bool HasSufficientStaff() => _teachers.Count > 0
            && _students.Count / (_teachers.Count * 20.0) <= 1;
        public bool IsActive() => _courses.Count > 0 && _teachers.Count > 0
            && _students.Count > 0;

        /// Чи є конкретний курс на кафедрі
        public bool HasCourse(string courseId) =>
            _courses.Exists(c => c.CourseId.Equals(courseId, StringComparison.OrdinalIgnoreCase));
        private string BuildInfo() =>
            Messages.Get("department", "build_info", _name, _dean, Faculty);


        public void AddCourse(Course course) => _courses.Add(course);
        public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);
        public void AddStudent(Student student) => _students.Add(student);

        /// Prints general department information.
        public void GetInfo()
        {
            Messages.Print("department", "get_info_header", FullInfo);
            Messages.Print("department", "get_info_stats", _courses.Count, _teachers.Count, _students.Count);
            Messages.Print("department", "get_info_status", IsActive(), HasSufficientStaff());
        }

        /// Displays the current schedule (stub).
        public void ManageSchedule()
        {
            Messages.Print("department", "manage_schedule_header", _name);
            foreach (var c in _courses)
                Messages.Print("department", "manage_schedule_course", 
                    c.Title,
                    (c.IsOnlineCourse() ? Messages.Get("department", "online")
                    : Messages.Get("department", "offline")
                    ));
        }

        public void ListExcellentStudents()
        {
            Messages.Print("department", "list_excellent_header", _name);
            var found = false;
            foreach (var s in _students)
                if (s.IsExcellentStudent()) 
                {
                    Messages.Print("department", "list_student_gpa", s.Name, s.GPA);
                    found = true; 
                }
            if (!found) 
                Messages.Print("department", "list_excellent_none");
        }

        public void ListAtRiskStudents()
        {
            Messages.Print("department", "list_atrisk_header");
            var found = false;
            foreach (var s in _students)
                if (s.IsAtRisk())
                {
                    Messages.Print("department", "list_student_gpa", s.Name, s.GPA);
                    found = true; 
                }
            if (!found) 
                Messages.Print("department", "list_atrisk_none");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("department", "name_label")}      : {_name}");
            Console.WriteLine($"  {Messages.Get("department", "dean_label")}        : {_dean}");
            Console.WriteLine($"  {Messages.Get("department", "faculty_label")}    : {Faculty}");
            Console.WriteLine($"  {Messages.Get("department", "courses_label")}       : {_courses.Count}");
            Console.WriteLine($"  {Messages.Get("department", "teachers_label")}   : {_teachers.Count}");
            Console.WriteLine($"  {Messages.Get("department", "students_label")}    : {_students.Count}");
            Console.WriteLine($"  {Messages.Get("department", "fullinfo_label")}     : {FullInfo}");
        }

        public override string ToString() => Messages.Get("department", "to_string", _name, _dean);
    }
}