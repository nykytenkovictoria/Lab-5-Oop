
// Program.cs
// Digital University Platform Simulation
// Version 4.0
using DigitalUniversity;

class Program
{

    static string dataDir = "";
    static List<Student> students = new();
    static List<Teacher> teachers = new();
    static List<Course> courses = new();
    static List<Department> departments = new();
    static List<Decanat> decanats = new();
    static List<Classroom> classrooms = new();
    static Library library = new();
    static Department dept = new();
    static Decanat decanat = new();
    static string FindFolder()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string dataDir = Path.Combine(baseDir, "data");
        if (!Directory.Exists(dataDir))
            dataDir = Path.Combine(baseDir, "..", "..", "..", "data");
        dataDir = Path.GetFullPath(dataDir);
        return dataDir;

    }
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PrintStudentInfo();

        dataDir = FindFolder();

        PrintHeader();

        LoadAllMessages();
        LoadAllData();
        BuildUniversity();
        EnrollStudents();
        ShowStudent();
        ShowTeacher();
        ShowCourse();
        ShowDepartmentReports();
        ShowLibrary();
        ShowDecanat();
        ShowOnlineReport();
        PrintSummary();

        PrintFooter();
    }
    static void PrintStudentInfo()
    {
        Console.WriteLine("ПІБ студента : Никитенко Вікторія Олегівна");
        Console.WriteLine("Курс         : 1");
        Console.WriteLine("Група        : IPZ-12");
        Console.WriteLine("Варіант      : Цифровий університет");
        Console.WriteLine("Версія       : 6.0");
        Console.WriteLine();
    }

    static void PrintFooter()
    {
        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine(">>> Фініш імітації <<<");
        Console.WriteLine("==============================================");
    }

    static void PrintHeader()
    {
        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine(">>> Старт імітації <<<");
        Console.WriteLine("==============================================");
    }


    static void LoadAllMessages()
    {
        Messages.Load(Path.Combine(dataDir, "messages.json"));
    }

    static void LoadAllData()
    {

        students = LoadStudents();
        teachers = LoadTeachers();
        courses = LoadCourses();
        departments = LoadDepartments();
        decanats = LoadDecanats();
        classrooms = LoadClassrooms();
        library = LoadLibrary();
    }

    static List<Student> LoadStudents()
    {
        var list = DataLoader.LoadStudents(Path.Combine(dataDir, "students.txt"));
        Console.WriteLine();
        return list;
    }

    static List<Teacher> LoadTeachers()
    {
        var list = DataLoader.LoadTeachers(Path.Combine(dataDir, "teachers.txt"));
        Console.WriteLine();
        return list;
    }

    static List<Course> LoadCourses()
    {
        var list = DataLoader.LoadCourses(Path.Combine(dataDir, "courses.txt"));
        Console.WriteLine();
        return list;
    }

    static List<Department> LoadDepartments()
    {
        var list = DataLoader.LoadDepartments(Path.Combine(dataDir, "department.txt"));
        Console.WriteLine();
        return list;
    }

    static List<Decanat> LoadDecanats()
    {
        var list = DataLoader.LoadDecanats(Path.Combine(dataDir, "decanat.txt"));
        Console.WriteLine();
        return list;
    }

    static List<Classroom> LoadClassrooms()
    {
        var list = DataLoader.LoadClassrooms(Path.Combine(dataDir, "classrooms.txt"));
        Console.WriteLine();
        return list;
    }

    static Library LoadLibrary()
    {
        var lib = DataLoader.LoadLibrary(Path.Combine(dataDir, "library.txt"));
        DataLoader.LoadBookToLibrary(Path.Combine(dataDir, "books.txt"), lib);
        Console.WriteLine();
        return lib;
    }

    static void BuildUniversity()
    {
        Console.WriteLine("=== Побудова структури університету ===\n");

        dept = departments.First();
        decanat = decanats.First();

        foreach (var c in courses) dept.AddCourse(c);
        foreach (var t in teachers)
            if (t.BelongsToDepartment(dept.Name)) dept.AddTeacher(t);
        foreach (var s in students) dept.AddStudent(s);
        foreach (var s in students) decanat.AddStudent(s);
        foreach (var r in classrooms) library.AddClassroom(r);

        dept.GetInfo();
    }


    static void EnrollStudents()
    {
        Console.WriteLine("\n=== Запис студентів на курси Перегляд розкладу ===\n");

        var oop = courses.Find(c => c.CourseId == "CS101");
        var db = courses.Find(c => c.CourseId == "CS102");

        if (oop)
            foreach (var s in students) oop.Enroll(s);

        if (db)
            foreach (var s in students.FindAll(s => s.Group == "ІПЗ-21")) 
                db.Enroll(s);

        if (teachers.Count > 0)
            if (oop)
            {
                
                teachers[0].AssignCourse(oop);
                teachers[0].ViewSchedule();
                teachers[1].AssignCourse(db);
                teachers[1].ViewSchedule();
            }
        foreach (var s in students.FindAll(s => s.Group == "ІПЗ-21"))
            s.ViewSchedule();
    }

    static void ShowStudent()
    {
        Console.WriteLine("\n=== Онлайн кабінет студентів ===\n");
        foreach (var s in students)
        {
            s.ViewCabinet();
        }
    }

    static void ShowTeacher()
    {
        Console.WriteLine("\n=== Онлайн кабінет викладачів ===\n");
        foreach (var t in teachers)
        {
            t.ViewCabinet();
        }
    }

    static void ShowCourse()
    {
        Console.WriteLine("\n=== Курси ===\n");
        foreach (var c in courses)
        {
            c.PrintInfo();
        }
    }

    static void ShowDepartmentReports()
    {
        Console.WriteLine("\n=== Кафедра — відмінники та ризик ===\n");
        dept.ListExcellentStudents();
        dept.ListAtRiskStudents();
        dept.ManageSchedule();
    }

    static void ShowLibrary()
    {
        Console.WriteLine("\n=== Бібліотека — пошук та видача ===\n");

        library.GetCatalog();
        library.SearchBook("Design");

        if (students.Count > 0)
            library.BorrowBook(students[0], "C# in Depth – Jon Skeet");

        var oop = courses.Find(c => c.CourseId == "CS101");
        if (classrooms.Count > 0 && teachers.Count > 0)
        {
            classrooms[0].Book(teachers[0], oop);
            classrooms[0].GetSchedule();
        }

        Console.WriteLine($"  Є вільні аудиторії : {library.HasFreeClassrooms()}");
    }


    static void ShowDecanat()
    {
        Console.WriteLine("\n=== Деканат — обробка запитів ===\n");

        if (students.Count >= 2)
        {
            decanat.ProcessRequest(students[0], "Академічна довідка");
            decanat.ProcessRequest(students[1], "Витяг із залікової книжки");
        }

        decanat.GenerateReport();
        Console.WriteLine($"  Є незакриті запити : {decanat.HasPendingRequests()}");
        Console.WriteLine($"  Операційний        : {decanat.IsOperational()}");
    }

    static void ShowOnlineReport()
    {
        Console.WriteLine("\n=== Онлайн-звітність ===\n");

        var report = new OnlineReport("RPT-001", "Місячний прогрес",
                                      teachers.Count > 0 ? teachers[0].Name : "System");
        report.Generate();
        Console.WriteLine($"  IsPending  : {report.IsPending()}");

        if (teachers.Count > 0) teachers[0].SubmitReport(report);
        if (students.Count > 0) students[0].SubmitReport(report);

        report.Archive();
        Console.WriteLine($"  IsClosed   : {report.IsClosed()}");
    }

    static void PrintSummary()
    {
        Console.WriteLine("\n=== Підсумок університету ===\n");
        dept.GetInfo();
    }

}