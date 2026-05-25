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
            set
            {
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
        }

        public OnlineReport()
        {
            _reportId = Messages.Get("onlinereport", "default_id");
            _type = Messages.Get("onlinereport", "default_type");
            Author = Messages.Get("onlinereport", "default_author");
            _submitted = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalReports++;
        }

        public OnlineReport(string reportId, string type)
        {
            _reportId = reportId;
            _type = type;
            Author = Messages.Get("onlinereport", "default_author");
            _submitted = false;
            _archived = false;
            FullInfo = BuildInfo();
            _totalReports++;
        }

        public OnlineReport(string reportId, string type, string author)
            : this(reportId, type)
        {
            Author = author;
            FullInfo = BuildInfo();
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
        }

        private OnlineReport(string reportId)
        {
            _reportId = reportId;
            _type = Messages.Get("onlinereport", "system_type");
            Author = Messages.Get("onlinereport", "system_author");
            FullInfo = BuildInfo();
        }

        public static OnlineReport CreateSystem(string id) => new OnlineReport(id);

        public bool IsPending() => !_submitted && !_archived;

        /// Чи звіт вже закритий (поданий та архівований)
        public bool IsClosed() => _submitted && _archived;

        /// Чи звіт має автора
        public bool HasAuthor() => !string.IsNullOrEmpty(Author) && Author != Messages.Get("onlinereport", "default_author");
        private string BuildInfo() =>
            Messages.Get("onlinereport", "build_info", _reportId, _type, Author, _submitted, _archived);

        // Generates report content (stub — logic to be implemented in later versions)
        public void Generate()
        {
            Messages.Print("onlinereport", "generate_header", _reportId, _type);
            Messages.Print("onlinereport", "generate_status", Author,
                (IsPending() ?
                Messages.Get("onlinereport", "status_pending") 
                :
                Messages.Get("onlinereport", "status_processed")
                ));
        }

        // Marks the report as submitted.
        public void Submit()
        {
            _submitted = true;
            Messages.Print("onlinereport", "submit_success", _reportId, _type);
        }

        // Archives the report.
        public void Archive()
        {
            _archived = true;
            Messages.Print("onlinereport", "archive", _reportId, _submitted);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"  {Messages.Get("onlinereport", "id_label")}     : {_reportId}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "type_label")}          : {_type}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "author_label")}        : {Author}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "submitted_label")}       : {_submitted}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "archived_label")}   : {_archived}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "fullinfo_label")}     : {FullInfo}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "pending_label")}       : {IsPending()}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "closed_label")}     : {IsClosed()}");
            Console.WriteLine($"  {Messages.Get("onlinereport", "total_reports_label")}: {TotalReports}");
        }

        public override string ToString() => Messages.Get("onlinereport", "to_string", _reportId, _type, _submitted);
    }
}