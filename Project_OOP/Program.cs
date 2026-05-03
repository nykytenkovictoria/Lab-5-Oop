
// Program.cs
// Digital University Platform Simulation
// Version 1.0

using DigitalUniversity;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        PrintStudentInfo();
        RunSimulation();
    }
    static void PrintStudentInfo()
    {
        Console.WriteLine("ПІБ студента : Никитенко Вікторія Олегівна");
        Console.WriteLine("Курс         : 1");
        Console.WriteLine("Група        : IPZ-12");
        Console.WriteLine("Варіант      : Цифровий університет");
        Console.WriteLine("Версія       : 1.0");
        Console.WriteLine();
    }

    static void RunSimulation()
    {
        Console.WriteLine("=== Старт імітації ===\n");

        // 1. Department
        Console.WriteLine("[1] Initializing department...");
        var department = new Department("Computer Science", "Prof. Shevchenko");
        department.GetInfo();

        // 2. Courses
        Console.WriteLine("\n[2] Creating courses...");
        var oopCourse = new Course("CS101", "Object-Oriented Programming", 4);
        var dbCourse = new Course("CS102", "Databases", 3);

        department.AddCourse(oopCourse);
        department.AddCourse(dbCourse);

        oopCourse.GetInfo();
        dbCourse.GetInfo();
        department.GetInfo();

        // 3. Teacher
        Console.WriteLine("\n[3] Creating teacher...");
        var teacher = new Teacher("T-001", "Petrenko Olha", "Associate Professor");

        department.AddTeacher(teacher);
        teacher.ViewCabinet();
        teacher.PostMaterial(oopCourse, "Lecture 1 – Classes and Objects.pdf");

        // 4. Student
        Console.WriteLine("\n[4] Creating student...");
        var student = new Student("S-001", "Kovalenko Dmytro", "KN-21", "2nd year");

        department.AddStudent(student);
        student.ViewCabinet();

        oopCourse.Enroll(student);
        oopCourse.GetMaterials();

        // 5. Grading
        Console.WriteLine("\n[5] Grading student...");
        teacher.GradeStudent(student, oopCourse, 90);

        // 6. Library & Classrooms
        Console.WriteLine("\n[6] Library system...");
        var library = new Library("Main Building, floor 2", 500);

        var room101 = new Classroom("101-A", 30);
        var room202 = new Classroom("202-B", 60);

        library.AddClassroom(room101);
        library.AddClassroom(room202);

        library.AddBook("C# in Depth – Jon Skeet");
        library.AddBook("Design Patterns – GoF");
        library.AddBook("Clean Code – Robert Martin");

        library.GetCatalog();
        library.SearchBook("Design");

        library.BorrowBook(student, "C# in Depth – Jon Skeet");

        room101.Book(teacher, oopCourse);
        room101.GetSchedule();

        // 7. Decanat
        Console.WriteLine("\n[7] Decanat processing...");
        var decanat = new Decanat("Faculty of Informatics", "Assoc. Prof. Bondarenko");

        decanat.AddStudent(student);
        decanat.ProcessRequest(student, "Academic Certificate");
        decanat.ProcessRequest(student, "Grade Transcript");

        decanat.ManageStudents();
        decanat.GenerateReport();

        // 8. Reporting
        Console.WriteLine("\n[8] Online reporting...");
        var report = new OnlineReport("RPT-001", "Monthly Academic Progress");

        report.Generate();
        teacher.SubmitReport(report);
        student.SubmitReport(report);
        report.Archive();

        // 9. Schedule
        Console.WriteLine("\n[9] Managing schedule...");
        department.ManageSchedule();

        Console.WriteLine("\n=== Фініш імітації ===");
    }
}