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

        public string Type => _type;
        public string ReportId => _reportId;

        public OnlineReport(string reportId, string type)
        {
            _reportId = reportId;
            _type = type;
            _submitted = false;
            _archived = false;
        }

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

        public override string ToString() => $"OnlineReport [{_reportId}] '{_type}' | submitted={_submitted}";
    }
}