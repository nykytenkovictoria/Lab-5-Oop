// IReportable.cs
// Інтерфейс для об'єктів, що можуть подавати звіти

namespace DigitalUniversity
{

    // Student і Teacher — обидва можуть подавати OnlineReport.

    public interface IReportable
    {
        void SubmitReport(OnlineReport report);
        bool CanSubmitReport();
    }
}
