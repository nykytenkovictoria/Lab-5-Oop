// UniversityPerson.cs
// Абстрактний базовий клас для Student і Teacher

namespace DigitalUniversity
{
    // Абстрактний базовий клас — спільна база для Student і Teacher.
    // Реалізує IUniversityMember і IReportable.
    public abstract class UniversityPerson : IUniversityMember, IReportable
    {
        protected string _id;
        protected string _name;

        public string Id => _id;
        public string Name => _name;
        public bool IsActive { get; set; }
        protected UniversityPerson(string id, string name, bool isActive = true)
        {
            _id = id;
            _name = name;
            IsActive = isActive;
            Console.WriteLine($"[UniversityPerson] Базовий конструктор: {_name} [{_id}]");
        }

        // ── Абстрактні методи
        public abstract void ViewCabinet();
        public abstract void PrintInfo();
        public abstract string GetRole();
        public abstract void SubmitReport(OnlineReport report);

        // ── Віртуальний метод
        public virtual bool CanSubmitReport() => IsActive;
        public void ShowIdentity()
        {
            Console.WriteLine($"[{GetRole()}] {_name} (ID: {_id}) | Активний: {IsActive}");
        }

        public override string ToString() => $"[{GetRole()}] {_name}";
    }
}
