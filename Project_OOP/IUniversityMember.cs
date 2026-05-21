// IUniversityMember.cs
// Інтерфейс для всіх учасників університету

namespace DigitalUniversity
{
    // Спільний інтерфейс для Student і Teacher.
    // ID, імені та метод ViewCabinet().
    public interface IUniversityMember
    {
        string Id { get; }
        string Name { get; }
        bool IsActive { get; set; }

        void ViewCabinet();
        void PrintInfo();
        string GetRole();
    }
}

