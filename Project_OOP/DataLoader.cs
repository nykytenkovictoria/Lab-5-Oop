// DataLoader.cs — Version 3
// Завантаження об'єктів із txt-файлів


namespace DigitalUniversity
{

    // Lines starting with '#' or empty lines are ignored.
    // Fields are separated by '|'.
    public static class DataLoader
    {

        private static IEnumerable<string[]> ReadLines(string path)
        {
            if (!File.Exists(path))
            {
                Messages.Print("dataloader", "file_not_found", path);
                yield break;
            }

            foreach (var raw in File.ReadAllLines(path))
            {
                var line = raw.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith('#'))
                    continue;
                yield return line.Split('|', System.StringSplitOptions.TrimEntries);
            }
        }

        // Формат: ID | Прізвище | Ім'я | Група | Курс | GPA | Пропуски
        public static List<Student> LoadStudents(string path)
        {
            var list = new List<Student>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 7)
                {
                    var s = new Student(
                   id: f[0],
                   name: $"{f[1]} {f[2]}",
                   group: f[3],
                   course: f[4],
                   gpa: double.TryParse(f[5], out var g) ? g : 0,
                   missedClasses: int.TryParse(f[6], out var missed) ? missed : 0
                    );
                    list.Add(s);
                    Messages.Print("dataloader",
                        "student_loaded_full",
                        s.Name, s.Group);
                }
                else if (f.Length == 5)
                {
                    var s = new Student(
                        id: f[0],
                        name: $"{f[1]} {f[2]}",
                        group: f[3],
                        course: f[4]);
                    list.Add(s);
                    Messages.Print("dataloader",
                        "student_loaded_basic",
                        s.Name, s.Group);
                }

            }
            return list;
        }

        // Формат: ID | Прізвище | Ім'я | Посада | Досвід | Кафедра
        public static List<Teacher> LoadTeachers(string path)
        {
            var list = new List<Teacher>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 6)
                {
                    var t = new Teacher(
                        id: f[0],
                        name: $"{f[1]} {f[2]}",
                        position: f[3],
                        experienceYears: int.TryParse(f[4], out var e) ? e : 0,
                        department: f[5]
                    );
                    list.Add(t);
                    Messages.Print("dataloader",
                        "teacher_loaded_full",
                        t.Name, t.Position);
                }
                else if (f.Length == 4)
                {
                    var t = new Teacher(
                        id: f[0],
                        name: $"{f[1]} {f[2]}",
                        position: f[3]);
                    list.Add(t);
                    Messages.Print("dataloader",
                        "teacher_loaded_basic",
                        t.Name, t.Position);
                }

            }
            return list;
        }

        // Формат: ID | Назва | Кредити | Онлайн
        public static List<Course> LoadCourses(string path)
        {
            var list = new List<Course>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 4)
                {
                    var c = new Course(
                        courseId: f[0],
                        title: f[1],
                        credits: int.TryParse(f[2], out var cr) ? cr : 1,
                        isOnline: bool.TryParse(f[3], out var o) && o
                    );
                    list.Add(c);
                    Messages.Print("dataloader", 
                        "course_loaded_full",
                        c.Title, c.Credits);
                }
                else if (f.Length == 3)
                {
                    var c = new Course(
                        courseId: f[0],
                        title: f[1],
                        credits: int.TryParse(f[2], out var cr) ? cr : 1
                    );
                    list.Add(c);
                    Messages.Print("dataloader",
                        "course_loaded_basic",
                        c.Title, c.Credits);
                }
            }
            return list;
        }


        // Формат: Назва | Декан | Факультет
        public static List<Department> LoadDepartments(string path)
        {
            var list = new List<Department>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 3)
                {
                    var d = new Department(f[0], f[1], f[2]);
                    list.Add(d);
                    Messages.Print("dataloader",
                        "department_loaded_full",
                        d.Name, d.Dean);
                }
                else if (f.Length == 2)
                {
                    var d = new Department(f[0], f[1]);
                    list.Add(d);
                    Messages.Print("dataloader",
                        "department_loaded_basic",
                        d.Name, d.Dean);
                }
            }
            return list;
        }

        // Формат: Факультет | Керівник | Активний
        public static List<Decanat> LoadDecanats(string path)
        {
            var list = new List<Decanat>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 3)
                {
                    var d = new Decanat(
                        faculty: f[0],
                        headName: f[1],
                        isActive: bool.TryParse(f[2], out var a) && a
                    );
                    list.Add(d);
                    Messages.Print("dataloader",
                        "decanat_loaded_full",
                        d.Faculty, d.HeadName);
                }
                else if (f.Length == 2)
                {
                    var d = new Decanat(
                    faculty: f[0],
                    headName: f[1]
                );
                    list.Add(d);
                    Messages.Print("dataloader",
                        "decanat_loaded_basic",
                        d.Faculty, d.HeadName);
                }
            }
            return list;
        }

        // Формат: Номер | Місць | Корпус
        public static List<Classroom> LoadClassrooms(string path)
        {
            var list = new List<Classroom>();
            foreach (var f in ReadLines(path))
            {
                if (f.Length >= 3)
                {
                    var r = new Classroom(
                        roomNumber: f[0],
                        capacity: int.TryParse(f[1], out var cap) ? cap : 0,
                        location: f[2]
                    );
                    list.Add(r);
                    Messages.Print("dataloader",
                        "classroom_loaded_full",
                        r.RoomNumber, r.Capacity, r.Location);
                }
                else if (f.Length == 2)
                {
                    var r = new Classroom(
                        roomNumber: f[0],
                        capacity: int.TryParse(f[1], out var cap) ? cap : 0
                    );
                    list.Add(r);
                    Messages.Print("dataloader", 
                        "classroom_loaded_basic",
                        r.RoomNumber, r.Capacity, r.Location);
                }
            }
            return list;
        }

        // Рядок 1: Розташування | Місць | Відкрита
        public static Library LoadLibrary(string path)
        {

            foreach (var f in ReadLines(path))
            {
                if (f.Length == 2)
                {
                    var library = new Library(
                        location: f[0],
                        capacity: int.TryParse(f[1], out var cap) ? cap : 0);
                    Messages.Print("dataloader", 
                        "library_loaded_basic",
                        library.Location, library.Capacity);
                    return library;
                }
                else if (f.Length >= 3)
                {
                    var library = new Library(
                        location: f[0],
                        capacity: int.TryParse(f[1], out var cap) ? cap : 0,
                        isOpen: bool.TryParse(f[2], out var isOpen) ? isOpen : false);
                    Messages.Print("dataloader", 
                        "library_loaded_full", 
                        library.Location, library.Capacity, library.IsOpen);
                    return library;
                }
            }
            return new Library();
        }

        public static void LoadBookToLibrary(string path, Library library)
        {
            foreach (var f in ReadLines(path))
            {
                if (f.Length < 1) continue;
                var bookName = f[0];
                library.AddBook(bookName);
                Messages.Print(
                    "dataloader", 
                    "book_added", 
                    bookName,
                    library.Location
                    );
            }
        }
    }
}