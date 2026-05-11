
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
        Console.WriteLine(">>> Старт імітації <<<\n");

        // ── Student ──────────────────────────────────────────────
        Console.WriteLine("=== Student: конструктори та аксесори ===");

        var s1 = new Student();                                              // без параметрів
        var s2 = new Student("S-001", "Коваленко Дмитро", "КН-21", "2-й"); // з параметрами
        var s3 = new Student("S-002", "Мельник Оксана", "КН-21", "2-й", 88.5, 0); // виклик іншого
        var s4 = new Student(s2);                                            // копія
        var s5 = Student.CreateSystem("S-SYS");                              // закритий
        s2.GPA = 92.0; s2.IsActive = true;                                   // аксесори запис
        Console.WriteLine($"  s2.GPA={s2.GPA}," +
            $" IsActive={s2.IsActive}," +
            $" TotalStudents={Student.TotalStudents}");
        s2.PrintInfo();

        // ── Teacher ──────────────────────────────────────────────
        Console.WriteLine("\n=== Teacher: конструктори та аксесори ===");

        var t1 = new Teacher();                                                        // без параметрів
        var t2 = new Teacher("T-001", "Петренко Ольга", "Доцент");                    // з параметрами
        var t3 = new Teacher("T-002", "Бондаренко Микола", "Професор", 15, "КН");     // виклик іншого
        var t4 = new Teacher(t2);                                                      // копія
        var t5 = Teacher.CreateGuest("T-GUEST");                                       // закритий
        t2.ExperienceYears = 10; t2.Department = "Кафедра КН";                        // аксесори запис
        Console.WriteLine($"  t2.Experience={t2.ExperienceYears}," +
            $" Department={t2.Department}," +
            $" TotalTeachers={Teacher.TotalTeachers}");
        t2.PrintInfo();

        // ── Course ───────────────────────────────────────────────
        Console.WriteLine("\n=== Course: конструктори та аксесори ===");

        var c1 = new Course();                                                  // без параметрів
        var c2 = new Course("CS101", "Об'єктно-орієнтоване програмування", 4); // з параметрами
        var c3 = new Course("CS102", "Бази даних", 3, true);                   // виклик іншого
        var c4 = new Course(c2);                                                // копія
        var c5 = Course.CreateArchived("CS-OLD");                               // закритий
        c2.Enroll(s2); c2.Enroll(s3);                                          // аксесор EnrolledCount
        Console.WriteLine($"  c2.EnrolledCount={c2.EnrolledCount}, TotalCourses={Course.TotalCourses}");
        c2.PrintInfo();

        // ── Department (агрегація) ────────────────────────────────
        Console.WriteLine("\n=== Department: агрегація + аксесори ===");

        var d1 = new Department();                                              // без параметрів
        var d2 = new Department("Комп'ютерні науки", "Проф. Шевченко");        // з параметрами
        var d3 = new Department("Математика", "Доц. Іванов", "Природничий");   // виклик іншого
        var d4 = new Department(d2);                                            // копія
        var d5 = Department.CreateSystem("Системна");                           // закритий
        d2.AddCourse(c2); d2.AddCourse(c3);
        d2.AddTeacher(t2); d2.AddStudent(s2); d2.AddStudent(s3);               // агрегація
        Console.WriteLine($"  Courses={d2.CourseCount}," +
            $" Teachers={d2.TeacherCount}," +
            $" Students={d2.StudentCount}");
        d2.PrintInfo();

        // ── Classroom + Library (композиція) ─────────────────────
        Console.WriteLine("\n=== Classroom + Library: композиція + аксесори ===");

        var r1 = new Classroom();                          // без параметрів
        var r2 = new Classroom("101-А", 30);               // з параметрами
        var r3 = new Classroom("202-Б", 60, "Корпус 2");   // виклик іншого
        var r4 = new Classroom(r2);                        // копія
        var r5 = Classroom.CreateReserve("RES-1");         // закритий

        var l1 = new Library();                                    // без параметрів
        var l2 = new Library("Головний корпус, пов. 2", 500);      // з параметрами
        var l3 = new Library("Філія", 100, false);                 // виклик іншого
        var l4 = new Library(l2);                                  // копія
        var l5 = Library.CreateReserve("Резервна");                // закритий

        l2.AddClassroom(r2); l2.AddClassroom(r3);                  // композиція
        l2.AddBook("C# in Depth"); l2.AddBook("Design Patterns"); l2.AddBook("Clean Code");
        l2.SearchBook("Design");
        l2.BorrowBook(s2, "C# in Depth");
        r2.Book(t2, c2);
        Console.WriteLine($"  Books={l2.BookCount}," +
            $" Classrooms={l2.ClassroomCount}, " +
            $"TotalRooms={Classroom.TotalRooms}");
        l2.PrintInfo();

        // ── Document + Decanat (композиція) ──────────────────────
        Console.WriteLine("\n=== Document + Decanat: композиція + аксесори ===");

        var doc1 = new Document();                                         // без параметрів
        var doc2 = new Document("DOC-001", "Академічна довідка");          // з параметрами
        var doc3 = new Document("DOC-002", "Залікова книжка", "Деканат");  // виклик іншого
        var doc4 = new Document(doc2);                                     // копія
        var doc5 = Document.CreateTemplate("TMPL-001");                    // закритий

        var dec1 = new Decanat();                                                   // без параметрів
        var dec2 = new Decanat("Факультет інформатики", "Доц. Бондаренко");         // з параметрами
        var dec3 = new Decanat("Факультет математики", "Проф. Коваль", true);       // виклик іншого
        var dec4 = new Decanat(dec2);                                               // копія
        var dec5 = Decanat.CreateSystem("Система");                                 // закритий

        dec2.AddStudent(s2); dec2.AddStudent(s3);
        dec2.ProcessRequest(s2, "Академічна довідка");     // композиція: створює Document
        dec2.ProcessRequest(s3, "Витяг із залікової");
        Console.WriteLine($"  Documents={dec2.DocumentCount}," +
            $" Students={dec2.StudentCount}, " +
            $"TotalDocs={Document.TotalDocs}");
        dec2.PrintInfo();

        // ── OnlineReport (асоціація) ──────────────────────────────
        Console.WriteLine("\n=== OnlineReport: конструктори + асоціація ===");

        var rep1 = new OnlineReport();                                               // без параметрів
        var rep2 = new OnlineReport("RPT-001", "Місячний прогрес");                  // з параметрами
        var rep3 = new OnlineReport("RPT-002", "Семестровий звіт", "Петренко О.");   // виклик іншого
        var rep4 = new OnlineReport(rep2);                                           // копія
        var rep5 = OnlineReport.CreateSystem("SYS-RPT");                             // закритий

        rep2.Generate();
        t2.SubmitReport(rep2);   // асоціація Teacher → OnlineReport
        s2.SubmitReport(rep2);   // асоціація Student → OnlineReport
        rep2.Archive();
        Console.WriteLine($"  Submitted={rep2.IsSubmitted}, " +
            $"Archived={rep2.IsArchived}," +
            $" TotalReports={OnlineReport.TotalReports}");
        rep2.PrintInfo();

        // ── Student predicates ───────────────────────────────────
        Console.WriteLine("\n=== Student: предикатні функції ===");
        s2.GPA = 92.0; s2.MissedClasses = 0;
        s3.GPA = 55.0; s3.MissedClasses = 25;
        Console.WriteLine($"  [{s2.Name}] GPA={s2.GPA}");
        Console.WriteLine($"  IsExcellentStudent : {s2.IsExcellentStudent()}");
        Console.WriteLine($"  IsAtRisk           : {s2.IsAtRisk()}");
        Console.WriteLine($"  HasDebt            : {s2.HasDebt()}");
        Console.WriteLine($"  IsEligibleForScholarship: {s2.IsEligibleForScholarship()}");
        Console.WriteLine($"  IsEnrolledAndActive: {s2.IsEnrolledAndActive()}");
        Console.WriteLine($"  [{s3.Name}] GPA={s3.GPA}, Пропуски={s3.MissedClasses}");
        Console.WriteLine($"  IsExcellentStudent : {s3.IsExcellentStudent()}");
        Console.WriteLine($"  IsAtRisk           : {s3.IsAtRisk()}");
        Console.WriteLine($"  HasDebt            : {s3.HasDebt()}");
        Console.WriteLine($"  IsEligibleForScholarship: {s3.IsEligibleForScholarship()}");

        // ── Teacher predicates ───────────────────────────────────
        Console.WriteLine("\n=== Teacher: предикатні функції ===");
        Console.WriteLine($"  [{t2.Name}] Досвід={t2.ExperienceYears}, Посада={t2.Position}");
        Console.WriteLine($"  IsSenior           : {t2.IsSenior()}");
        Console.WriteLine($"  IsProfessor        : {t2.IsProfessor()}");
        Console.WriteLine($"  IsOverloaded       : {t2.IsOverloaded()}");
        Console.WriteLine($"  CanTakeMoreCourses : {t2.CanTakeMoreCourses()}");
        Console.WriteLine($"  BelongsToDept КН   : {t2.BelongsToDepartment("Кафедра КН")}");
        t2.AssignCourse(c2); t2.AssignCourse(c3);
        Console.WriteLine($"  [{t3.Name}] Посада={t3.Position}");
        Console.WriteLine($"  IsProfessor        : {t3.IsProfessor()}");
        Console.WriteLine($"  IsSenior           : {t3.IsSenior()}");

        // ── Course predicates ────────────────────────────────────
        Console.WriteLine("\n=== Course: предикатні функції ===");
        Console.WriteLine($"  [{c2.Title}] Зараховано={c2.EnrolledCount}");
        Console.WriteLine($"  IsActive           : {c2.IsActive()}");
        Console.WriteLine($"  HasAvailableSpots  : {c2.HasAvailableSpots()}");
        Console.WriteLine($"  IsFull             : {c2.IsFull()}");
        Console.WriteLine($"  IsOnlineCourse     : {c2.IsOnlineCourse()}");
        Console.WriteLine($"  IsIntensive        : {c2.IsIntensive()}");
        Console.WriteLine($"  [{c3.Title}] Онлайн={c3.IsOnline}");
        Console.WriteLine($"  IsOnlineCourse     : {c3.IsOnlineCourse()}");
        c2.PrintEnrollmentStatus();

        // ── Department predicates ────────────────────────────────
        Console.WriteLine("\n=== Department: предикатні функції ===");
        Console.WriteLine($"  [{d2.Name}]");
        Console.WriteLine($"  IsLarge            : {d2.IsLarge()}");
        Console.WriteLine($"  IsActive           : {d2.IsActive()}");
        Console.WriteLine($"  HasSufficientStaff : {d2.HasSufficientStaff()}");
        Console.WriteLine($"  HasCourse CS101    : {d2.HasCourse("CS101")}");
        Console.WriteLine($"  HasCourse CS999    : {d2.HasCourse("CS999")}");
        d2.ListExcellentStudents();
        d2.ListAtRiskStudents();

        // ── Library + Classroom predicates ──────────────────────
        Console.WriteLine("\n=== Library + Classroom: предикатні функції ===");
        Console.WriteLine($"  IsLibraryOpen      : {l2.IsLibraryOpen()}");
        Console.WriteLine($"  HasBook 'Design'   : {l2.HasBook("Design Patterns")}");
        Console.WriteLine($"  HasBook 'Java'     : {l2.HasBook("Java")}");
        Console.WriteLine($"  HasFreeClassrooms  : {l2.HasFreeClassrooms()}");
        Console.WriteLine($"  [{r2.RoomNumber}] IsBooked={r2.IsBooked}");
        Console.WriteLine($"  IsFree             : {r2.IsFree()}");
        Console.WriteLine($"  IsLarge            : {r2.IsLarge()}");
        Console.WriteLine($"  CanFitGroup(25)    : {r2.CanFitGroup(25)}");
        Console.WriteLine($"  [{r3.RoomNumber}] Місць={r3.Capacity}");
        Console.WriteLine($"  IsLarge            : {r3.IsLarge()}");
        Console.WriteLine($"  CanFitGroup(55)    : {r3.CanFitGroup(55)}");

        // ── Document predicates ──────────────────────────────────
        Console.WriteLine("\n=== Document: предикатні функції ===");
        Console.WriteLine($"  [{doc2.DocId}] Підписано={doc2.IsSigned}");
        Console.WriteLine($"  NeedsSignature     : {doc2.NeedsSignature()}");
        Console.WriteLine($"  IsReadyToIssue     : {doc2.IsReadyToIssue()}");
        Console.WriteLine($"  IsInArchive        : {doc2.IsInArchive()}");
        doc2.Sign("Доц. Бондаренко");
        Console.WriteLine($"  Після підпису:");
        Console.WriteLine($"  NeedsSignature     : {doc2.NeedsSignature()}");
        Console.WriteLine($"  IsReadyToIssue     : {doc2.IsReadyToIssue()}");
        doc2.Archive();
        Console.WriteLine($"  Після архівування:");
        Console.WriteLine($"  IsInArchive        : {doc2.IsInArchive()}");

        // ── Decanat predicates ───────────────────────────────────
        Console.WriteLine("\n=== Decanat: предикатні функції ===");
        Console.WriteLine($"  [{dec2.Faculty}]");
        Console.WriteLine($"  IsOperational      : {dec2.IsOperational()}");
        Console.WriteLine($"  IsOverloaded       : {dec2.IsOverloaded()}");
        Console.WriteLine($"  HasPendingRequests : {dec2.HasPendingRequests()}");
        Console.WriteLine($"  HasStudent S-001   : {dec2.HasStudent("S-001")}");
        Console.WriteLine($"  HasStudent S-999   : {dec2.HasStudent("S-999")}");
        dec2.GenerateReport();

        // ── OnlineReport predicates ──────────────────────────────
        Console.WriteLine("\n=== OnlineReport: предикатні функції ===");
        var repNew = new OnlineReport("RPT-NEW", "Квартальний звіт", "Петренко О.");
        Console.WriteLine($"  [{repNew.ReportId}]");
        Console.WriteLine($"  IsPending          : {repNew.IsPending()}");
        Console.WriteLine($"  IsClosed           : {repNew.IsClosed()}");
        Console.WriteLine($"  HasAuthor          : {repNew.HasAuthor()}");
        repNew.Generate();
        repNew.Submit();
        Console.WriteLine($"  Після подачі:");
        Console.WriteLine($"  IsPending          : {repNew.IsPending()}");
        repNew.Archive();
        Console.WriteLine($"  Після архівування:");
        Console.WriteLine($"  IsClosed           : {repNew.IsClosed()}");

        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(">>> Фініш імітації <<<");
        Console.WriteLine("==============================================");
    }
    static void PrintStudentInfo()
    {
        Console.WriteLine("ПІБ студента : Никитенко Вікторія Олегівна");
        Console.WriteLine("Курс         : 1");
        Console.WriteLine("Група        : IPZ-12");
        Console.WriteLine("Варіант      : Цифровий університет");
        Console.WriteLine("Версія       : 3.0");
        Console.WriteLine();
    }

}