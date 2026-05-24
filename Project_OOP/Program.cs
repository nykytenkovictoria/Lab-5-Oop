
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
        ShowStudentPredicates();
        ShowTeacherPredicates();
        ShowCoursePredicates();
        ShowDepartmentReports();
        ShowLibrary();
        ShowDecanat();
        ShowOnlineReport();
        ShowOperators();
        PrintSummary();

        PrintFooter();
    }
    static void PrintStudentInfo()
    {
        Console.WriteLine("ПІБ студента : Никитенко Вікторія Олегівна");
        Console.WriteLine("Курс         : 1");
        Console.WriteLine("Група        : IPZ-12");
        Console.WriteLine("Варіант      : Цифровий університет");
        Console.WriteLine("Версія       : 4.0");
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
        Console.WriteLine("\n=== Запис студентів на курси ===\n");

        var oop = courses.Find(c => c.CourseId == "CS101");
        var db = courses.Find(c => c.CourseId == "CS102");

        if (oop)
            foreach (var s in students) oop.Enroll(s);

        if (db)
            foreach (var s in students.FindAll(s => s.Group == "ІПЗ-21")) db.Enroll(s);

        if (teachers.Count > 0)
            if (oop)
            {
                teachers[0].AssignCourse(oop);
            }

    }

    static void ShowStudentPredicates()
    {
        Console.WriteLine("\n=== Предикати — статус студентів ===\n");
        foreach (var s in students)
        {
            s.ViewCabinet();
        }
    }

    static void ShowTeacherPredicates()
    {
        Console.WriteLine("\n=== Предикати — статус викладачів ===\n");
        foreach (var t in teachers)
        {
            t.ViewCabinet();
        }
    }

    static void ShowCoursePredicates()
    {
        Console.WriteLine("\n=== Предикати — курси ===\n");
        foreach (var c in courses)
        {
            Console.WriteLine($"  [{c.Title}]");
            Console.WriteLine($"    Активний      : {c.IsActive()}");
            Console.WriteLine($"    Є місця       : {c.HasAvailableSpots()}");
            Console.WriteLine($"    Онлайн        : {c.IsOnlineCourse()}");
            Console.WriteLine($"    Інтенсивний   : {c.IsIntensive()}");
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


    static void ShowOperators()
    {
        Console.WriteLine("\n=== Перевантаження операторів ===\n");

        // ── Student operators ─────────────────────────────────
        Console.WriteLine("  -- Student --");
        var sA = students[0];
        var sB = students[1];

        // Бінарний -  : різниця GPA
        double gpaDiff = sA - sB;

        // Унарний ++ / --
        sA++;
        sB--;

        // Унарний !
        Console.WriteLine($"  !sA (не відмінник): {!sA}");
        Console.WriteLine($"  !sB (не відмінник): {!sB}");

        // true / false
        Console.WriteLine($"  {sA.Name} активний (if sA): {(sA ? "так" : "ні")}");

        // Порівняння
        Console.WriteLine($"  sA > sB  (GPA): {sA > sB}");
        Console.WriteLine($"  sA < sB  (GPA): {sA < sB}");
        Console.WriteLine($"  sA == sB (GPA): {sA == sB}");
        Console.WriteLine($"  sA != sB (GPA): {sA != sB}");

        // ── Teacher operators ─────────────────────────────────
        Console.WriteLine("\n  -- Teacher --");
        var tA = teachers[0];
        var tB = teachers[1];

        // Унарний ++
        tA++;
        Console.WriteLine($"  tA після ++: досвід={tA.ExperienceYears} р.");

        // Порівняння
        Console.WriteLine($"  tA > tB  (досвід): {tA > tB}");
        Console.WriteLine($"  tA < tB  (досвід): {tA < tB}");
        Console.WriteLine($"  tA == tB (досвід): {tA == tB}");

        // ── Course operators ──────────────────────────────────
        Console.WriteLine("\n  -- Course --");
        var cA = courses[0];
        var cB = courses[1];

        // Унарний ++
        cA++;
        cB--; 
        Console.WriteLine($"після ++: '{cA.Title}', кредитів={cA.Credits}");
        Console.WriteLine($"після --: '{cB.Title}', кредитів={cB.Credits}");
        // Бінарний +
        var cMerged = cA + cB;
        Console.WriteLine($"  Об'єднаний курс: '{cMerged.Title}', кредитів={cMerged.Credits}");

        // Порівняння
        Console.WriteLine($"  cA > cB  (кредити): {cA > cB}");
        Console.WriteLine($"  cA == cB (кредити): {cA == cB}");
        Console.WriteLine($"  cA >= cB (кредити): {cA >= cB}");

        // ── Classroom operators ───────────────────────────────
        Console.WriteLine("\n  -- Classroom --");
        var rA = classrooms[0];
        var rB = classrooms[1];
        // Бінарний +
        var rMerged = rA + rB;
        Console.WriteLine($"  Об'єднана аудиторія: {rMerged.RoomNumber}, місць={rMerged.Capacity}");

        // Порівняння
        Console.WriteLine($"  rA < rB  (місця): {rA < rB}");
        Console.WriteLine($"  rA == rB (місця): {rA == rB}");
        Console.WriteLine($"  rB >= rA (місця): {rB >= rA}");
    }

}