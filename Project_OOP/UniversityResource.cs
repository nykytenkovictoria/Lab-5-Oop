// UniversityResource.cs
// Абстрактний базовий клас для ресурсів університету

namespace DigitalUniversity
{
    // Абстрактний базовий клас для Classroom і Library.
    // location, capacity.
    public abstract class UniversityResource
    {
        protected string _location;
        protected int _capacity;

        public string Location
        {
            get => _location;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _location = value;
            }
        }
        public int Capacity
        {
            get => _capacity;
            set
            {
                if (value > 0)
                    _capacity = value;
            }
        }

        protected UniversityResource(string location, int capacity)
        {
            _location = location;
            _capacity = capacity;
            Console.WriteLine($"[UniversityResource] Базовий конструктор: {_location}, місць={_capacity}");
        }

        public abstract void GetInfo();
        public abstract bool IsAvailable();

        public virtual void PrintCapacity()
            => Console.WriteLine($"[Resource] '{_location}': місць = {_capacity}");
    }
}