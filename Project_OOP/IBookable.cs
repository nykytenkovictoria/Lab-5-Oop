// IBookable.cs — Version 5
// Інтерфейс для ресурсів, що можна бронювати

namespace DigitalUniversity
{

    // Реалізує Classroom — аудиторії можна бронювати і звільняти.
    public interface IBookable
    {
        bool IsBooked { get; }
        void Book(Teacher teacher, Course course);
        void Release();
        bool IsFree();
    }
}