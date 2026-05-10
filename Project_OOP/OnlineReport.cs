// OnlineReport.cs
// Онлайн звітність університету

namespace DigitalUniversity
{
    public class OnlineReport
    {
        private string _reportId;
        private string _type;
        private bool _submitted;
        private bool _archived;
        private static int _totalReports;

        public string ReportId
        {
            get => _reportId;
            set { 
                if (!string.IsNullOrWhiteSpace(value))
                    _reportId = value; 
            }
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }


        public bool IsSubmitted => _submitted;
        public bool IsArchived => _archived;
        public static int TotalReports => _totalReports;

        public string Author { get; set; }

        public string FullInfo { get; private set; }

        static OnlineReport()
        {
            _totalReports = 0;
            Console.WriteLine("[OnlineReport] Статичний конструктор: клас ініціалізовано.");
        }

        public OnlineReport()
        {
            _reportId = "R-000";
            _type = "—";
            Author = "—";
            _submitted = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalReports++;
            Console.WriteLine("[OnlineReport] Конструктор без параметрів: звіт створено.");
        }

        public OnlineReport(string reportId, string type)
        {
            _reportId = reportId;
            _type = type;
            Author = "—";
            _submitted = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalReports++;
            Console.WriteLine($"[OnlineReport] Конструктор з параметрами: [{_reportId}] '{_type}'");
        }

        public OnlineReport(string reportId, string type, string author)
            : this(reportId, type)
        {
            Author = author;
            FullInfo = BuildInfo();
            Console.WriteLine($"[OnlineReport] Конструктор виклику іншого: автор={Author}");
        }

        public OnlineReport(OnlineReport other)
        {
            _reportId = other._reportId + "-copy";
            _type = other._type;
            Author = other.Author;
            _submitted = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalReports++;
            Console.WriteLine($"[OnlineReport] Конструктор копії: скопійовано [{_reportId}]");
        }

        private OnlineReport(string reportId)
        {
            _reportId = reportId;
            _type = "Системний";
            Author = "System";
            FullInfo = BuildInfo();
            Console.WriteLine($"[OnlineReport] Закритий конструктор: системний [{_reportId}]");
        }

        public static OnlineReport CreateSystem(string id) => new OnlineReport(id);

        private string BuildInfo() =>
            $"[{_reportId}] {_type} | Автор: {Author} | Подано: {_submitted} | Архів: {_archived}";

        // Generates report content (stub — logic to be implemented in later versions)
        public void Generate()
        {
            Console.WriteLine($"[OnlineReport] Generating report [{_reportId}] of type '{_type}'...");
        }

        // Marks the report as submitted.
        public void Submit()
        {
            _submitted = true;
            Console.WriteLine($"[OnlineReport] Report [{_reportId}] '{_type}' submitted successfully.");
        }

        // Archives the report.
        public void Archive()
        {
            _archived = true;
            Console.WriteLine($"[OnlineReport] Report [{_reportId}] archived. (Submitted: {_submitted})");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  ID звіту     : {_reportId}");
            Console.WriteLine($"  Тип          : {_type}");
            Console.WriteLine($"  Автор        : {Author}");
            Console.WriteLine($"  Подано       : {_submitted}");
            Console.WriteLine($"  Архівовано   : {_archived}");
            Console.WriteLine($"  FullInfo     : {FullInfo}");
            Console.WriteLine($"  Всього звітів: {TotalReports}");
        }

        public override string ToString() => $"OnlineReport [{_reportId}] '{_type}' | submitted={_submitted}";
    }
}