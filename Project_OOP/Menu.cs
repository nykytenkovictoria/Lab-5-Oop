using DigitalUniversity;

static class Menu
{
    static List<Student> _students = null!;
    static List<Teacher> _teachers = null!;
    static List<Course> _courses = null!;
    static List<Classroom> _classrooms = null!;
    static Library _library = null!;
    static Department _dept = null!;
    static Decanat _decanat = null!;

    public static void Run(
        List<Student> students,
        List<Teacher> teachers,
        List<Course> courses,
        List<Classroom> classrooms,
        Library library,
        Department dept,
        Decanat decanat)
    {
        _students = students;
        _teachers = teachers;
        _courses = courses;
        _classrooms = classrooms;
        _library = library;
        _dept = dept;
        _decanat = decanat;

        while (true)
        {
            PrintMainMenu();
            var key = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (key)
            {
                case "1": CreateStudent(); break;
                case "2": CreateTeacher(); break;
                case "3": CreateCourse(); break;
                case "4": CreateClassroom(); break;
                case "5": AssignTeacherToCourse(); break;
                case "6": AssignStudentAndBookRoom(); break;
                case "7": ViewScheduleMenu(); break;
                case "8": ViewCabinetMenu(); break;
                case "9": BorrowBook(); break;
                case "10": AddBook(); break;
                case "11": OnlineReportMenu(); break;
                case "12": CourseStatistics(); break;
                case "13": DecanatProcessRequest(); break;
                case "14": DecanatGenerateReport(); break;
                case "15": DecanatManageStudents(); break;
                case "0":
                    Messages.Print("menu", "exit");
                    return;
                default:
                    Messages.Print("menu", "unknown_command");
                    break;
            }

            Pause();
        }
    }

    static void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("========== ЦИФРОВИЙ УНІВЕРСИТЕТ — МЕНЮ ==========");
        Console.WriteLine("  1 - " + Messages.Get("menu", "create_student"));
        Console.WriteLine("  2 - " + Messages.Get("menu", "create_teacher"));
        Console.WriteLine("  3 - " + Messages.Get("menu", "create_course"));
        Console.WriteLine("  4 - " + Messages.Get("menu", "create_classroom"));
        Console.WriteLine("  5 - " + Messages.Get("menu", "assign_teacher"));
        Console.WriteLine("  6 - " + Messages.Get("menu", "assign_student"));
        Console.WriteLine("  7 - " + Messages.Get("menu", "view_schedule"));
        Console.WriteLine("  8 - " + Messages.Get("menu", "view_cabinet"));
        Console.WriteLine("  9 - " + Messages.Get("menu", "borrow_book"));
        Console.WriteLine(" 10 - " + Messages.Get("menu", "add_book"));
        Console.WriteLine(" 11 - " + Messages.Get("menu", "online_report"));
        Console.WriteLine(" 12 - " + Messages.Get("menu", "course_stats"));
        Console.WriteLine(" 13 - " + Messages.Get("menu", "decanat_process"));
        Console.WriteLine(" 14 - " + Messages.Get("menu", "decanat_report"));
        Console.WriteLine(" 15 - " + Messages.Get("menu", "decanat_students"));
        Console.WriteLine("  0 - " + Messages.Get("menu", "exit_menu"));
        Console.WriteLine("================================================");
        Console.Write($"  {Messages.Get("menu", "choice")}: ");
    }
    static void CreateStudent()
    {
        Title(Messages.Get("menu", "create_student"));
        string id = Ask(Messages.Get("menu", "ask_id"));
        string name = Ask(Messages.Get("menu", "ask_name"));
        string group = Ask(Messages.Get("menu", "ask_group"));
        string year = Ask(Messages.Get("menu", "ask_course"));
        double gpa = AskDouble(Messages.Get("menu", "ask_gpa"), 0, 100);
        int miss = (int)AskDouble(Messages.Get("menu", "ask_missed"), 0, 200);

        var s = new Student(id, name, group, year, gpa, miss);
        _students.Add(s);
        _dept.AddStudent(s);
        _decanat.AddStudent(s);
        s.PrintInfo();
        Messages.Print("menu", "student_created");
    }

    static void CreateTeacher()
    {
        Title(Messages.Get("menu", "create_teacher"));
        string id = Ask(Messages.Get("menu", "ask_id"));
        string name = Ask(Messages.Get("menu", "ask_name"));
        string pos = Ask(Messages.Get("menu", "ask_position"));
        int exp = (int)AskDouble(Messages.Get("menu", "ask_experience"), 0, 60);
        string dept = Ask(Messages.Get("menu", "ask_department"));

        var t = new Teacher(id, name, pos, exp, dept);
        _teachers.Add(t);
        _dept.AddTeacher(t);
        t.PrintInfo();
        Messages.Print("menu", "teacher_created");
    }

    static void CreateCourse()
    {
        Title(Messages.Get("menu", "create_course"));
        string id = Ask(Messages.Get("menu", "ask_course_id"));
        string title = Ask(Messages.Get("menu", "ask_course_title"));
        int credits = (int)AskDouble(Messages.Get("menu", "ask_credits"), 1, 10);
        bool online = AskBool(Messages.Get("menu", "ask_online"));

        var c = new Course(id, title, credits, online);
        _courses.Add(c);
        _dept.AddCourse(c);

        c.PrintInfo();
        Messages.Print("menu", "course_created");
    }

    static void CreateClassroom()
    {
        Title(Messages.Get("menu", "create_classroom"));
        string number = Ask(Messages.Get("menu", "ask_room_number"));
        int capacity = (int)AskDouble(Messages.Get("menu", "ask_capacity"), 1, 500);
        string location = Ask(Messages.Get("menu", "ask_location"));

        var r = new Classroom(number, capacity, location);
        _classrooms.Add(r);
        _library.AddClassroom(r);
        r.PrintInfo();
        Messages.Print("menu", "classroom_created");
    }

    static void AssignTeacherToCourse()
    {
        Title(Messages.Get("menu", "assign_teacher"));
        if (!HasTeachers() || !HasCourses()) return;

        ListTeachers();
        int ti = PickIndex(Messages.Get("menu", "pick_teacher"), _teachers.Count);
        ListCourses();
        int ci = PickIndex(Messages.Get("menu", "pick_course"), _courses.Count);

        var t = _teachers[ti];
        var c = _courses[ci];
        t.AssignCourse(c);
        AssignClassroomToCourse(t, c);
    }

    static void AssignClassroomToCourse(Teacher t, Course c)
    {
        if (!c.IsOnline)
        {
            Title(Messages.Get("menu", "assign_classroom"));
            ListFreeClassrooms();
            if (_classrooms.Count > 0)
            {
                int classroomIndex = PickIndex(Messages.Get("menu", "pick_classroom"), _classrooms.Count);
                var classroom = _classrooms[classroomIndex];
                classroom.Book(t, c);
            }
            else
            {
                Messages.Print("menu", "no_classrooms");
            }
        }
    }

    static void AssignStudentAndBookRoom()
    {
        Title(Messages.Get("menu", "assign_student"));
        if (!HasStudents() || !HasCourses()) return;

        ListStudents();
        int si = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
        ListCourses();
        int ci = PickIndex(Messages.Get("menu", "pick_course"), _courses.Count);

        var s = _students[si];
        var c = _courses[ci];
        c.Enroll(s);
        s.CheckStatus();
    }

    static void ViewScheduleMenu()
    {
        Title(Messages.Get("menu", "view_schedule"));
        Messages.Print("menu", "schedule_choice");
        var k = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (k == "1")
        {
            if (!HasStudents()) return;
            ListStudents();
            int i = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
            _students[i].ViewSchedule();
        }
        else if (k == "2")
        {
            if (!HasTeachers()) return;
            ListTeachers();
            int i = PickIndex(Messages.Get("menu", "pick_teacher"), _teachers.Count);
            _teachers[i].ViewSchedule();
        }
        else Messages.Print("menu", "unknown_option");
    }

    static void ViewCabinetMenu()
    {
        Title(Messages.Get("menu", "view_cabinet"));
        Messages.Print("menu", "cabinet_choice");
        var k = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (k == "1")
        {
            if (!HasStudents()) return;
            ListStudents();
            int i = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
            _students[i].ViewCabinet();
        }
        else if (k == "2")
        {
            if (!HasTeachers()) return;
            ListTeachers();
            int i = PickIndex(Messages.Get("menu", "pick_teacher"), _teachers.Count);
            _teachers[i].ViewCabinet();
        }
        else Messages.Print("menu", "unknown_option");
    }

    static void BorrowBook()
    {
        Title(Messages.Get("menu", "borrow_book"));
        if (!HasStudents()) return;

        _library.GetCatalog();
        string title = Ask(Messages.Get("menu", "ask_book_title"));
        ListStudents();
        int i = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
        _library.BorrowBook(_students[i], title);
    }

    static void AddBook()
    {
        Title(Messages.Get("menu", "add_book"));
        string title = Ask(Messages.Get("menu", "ask_book_title"));
        _library.AddBook(title);
        Messages.Print("menu", "book_added");
    }

    static void OnlineReportMenu()
    {
        Title(Messages.Get("menu", "online_report"));
        Messages.Print("menu", "report_choice");
        var k = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (k == "1")
        {
            string id = Ask(Messages.Get("menu", "ask_report_id"));
            string type = Ask(Messages.Get("menu", "ask_report_type"));
            string author = _teachers.Count > 0 ? _teachers[PickIndex(Messages.Get("menu", "pick_author"), _teachers.Count)].Name : "System";

            var rep = new OnlineReport(id, type, author);
            rep.Generate();
            Console.WriteLine($"  {Messages.Get("menu", "is_pending")}: {rep.IsPending()}");
            Console.WriteLine($"  {Messages.Get("menu", "has_author")}: {rep.HasAuthor()}");

            if (AskBool(Messages.Get("menu", "ask_submit")))
            {
                rep.Submit();
                if (AskBool(Messages.Get("menu", "ask_archive"))) rep.Archive();
            }
            Console.WriteLine($"  {Messages.Get("menu", "is_closed")}: {rep.IsClosed()}");
            rep.PrintInfo();
        }
        else if (k == "2")
        {
            _decanat.GenerateReport();
        }
        else Messages.Print("menu", "unknown_option");
    }

    static void CourseStatistics()
    {
        Title(Messages.Get("menu", "course_stats"));
        if (!HasCourses()) return;
        ListCourses();
        int ci = PickIndex(Messages.Get("menu", "pick_course"), _courses.Count);
        var c = _courses[ci];

        c.PrintInfo();
        c.PrintEnrollmentStatus();
    }

    static void DecanatGenerateReport()
    {
        Title(Messages.Get("menu", "decanat_process"));
        _decanat.GenerateReport();
    }

    static void DecanatProcessRequest()
    {
        Title(Messages.Get("menu", "decanat_report"));
        if (!HasStudents()) return;

        ListStudents();
        int si = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
        string requestType = Ask(Messages.Get("menu", "exapmle_report"));

        _decanat.ProcessRequest(_students[si], requestType);
    }

    static void DecanatManageStudents()
    {
        Title(Messages.Get("menu", "decanat_process"));
        _decanat.ManageStudents();

        Console.WriteLine();
        Console.WriteLine(Messages.Get("menu", "search_student"));
        ListStudents();
        int si = PickIndex(Messages.Get("menu", "pick_student"), _students.Count);
        string id = _students[si].Id;
        if (!string.IsNullOrEmpty(id))
        {
            bool found = _decanat.HasStudent(id);
            if (found)
            {
                var s = _students.Find(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                s?.CheckStatus();
            }
            else
                Messages.Print("decanat", "not_found", id);
        }
    }

    static void Title(string t)
        => Console.WriteLine($"\n  ── {t} ──\n");

    static void Pause()
    {
        Messages.Print("menu", "pause");
        Console.ReadLine();
    }

    static string Ask(string prompt)
    {
        Console.Write($"  {prompt}: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    static double AskDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write($"  {prompt} ({min}–{max}): ");
            if (double.TryParse(Console.ReadLine()?.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double v)
                && v >= min && v <= max)
                return v;
            Messages.Print("menu", "invalid_number", min, max);
        }
    }

    static bool AskBool(string prompt)
    {
        Console.Write($"  {prompt}: ");
        return Console.ReadLine()?.Trim().ToLower() is "y" or "yes" or "т" or "так";
    }

    static int PickIndex(string prompt, int count)
    {
        while (true)
        {
            Console.Write($"  {prompt} (0–{count - 1}): ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out int i) && i >= 0 && i < count)
                return i;
            Messages.Print("menu", "invalid_index", count - 1);
        }
    }

    static void ListStudents()
    {
        Console.WriteLine($"  {Messages.Get("menu", "students_list")}:");
        for (int i = 0; i < _students.Count; i++)
            Console.WriteLine($"    [{i}] {_students[i].FullInfo}");
    }

    static void ListTeachers()
    {
        Console.WriteLine($"  {Messages.Get("menu", "teachers_list")}:");
        for (int i = 0; i < _teachers.Count; i++)
            Console.WriteLine($"    [{i}] {_teachers[i].FullInfo}");
    }

    static void ListCourses()
    {
        Console.WriteLine($"  {Messages.Get("menu", "courses_list")}:");
        for (int i = 0; i < _courses.Count; i++)
            Console.WriteLine($"    [{i}] {_courses[i].FullInfo}");
    }

    static void ListFreeClassrooms()
    {
        Console.WriteLine($"  {Messages.Get("menu", "classrooms_list")}:");
        bool found = false;
        for (int i = 0; i < _classrooms.Count; i++)
            if (!_classrooms[i].IsBooked)
            {
                Console.WriteLine($"    [{i}] {_classrooms[i].FullInfo}");
                found = true;
            }
        if (!found) Messages.Print("menu", "no_free_classrooms");
    }

    static bool HasStudents()
    {
        if (_students.Count == 0) { Messages.Print("menu", "no_students_warning"); return false; }
        return true;
    }

    static bool HasTeachers()
    {
        if (_teachers.Count == 0) { Messages.Print("menu", "no_teachers_warning"); return false; }
        return true;
    }

    static bool HasCourses()
    {
        if (_courses.Count == 0) { Messages.Print("menu", "no_courses_warning"); return false; }
        return true;
    }
}