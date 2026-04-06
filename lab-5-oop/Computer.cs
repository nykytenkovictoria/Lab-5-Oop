
using System;
using System.Collections.Generic;
using System.Text;

namespace task_5
{
    class Computer : ComputerTech
    {
        private int ramGB;
        private int hddGB;
        private string processor;
        private int positiveReviews;
        private int totalReviews;
        private int rating;


        public Computer() : base()
        {
            ramGB = 0; hddGB = 0; processor = "Unknown";
            positiveReviews = 0; totalReviews = 0; rating = 0;
        }


        public Computer(string modelName, decimal price, string manufacturer, int warrantyMonths,
                        int ramGB, int hddGB, string processor,
                        int positiveReviews = 0, int totalReviews = 0)
                        : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.ramGB = ramGB;
            this.hddGB = hddGB;
            this.processor = processor;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
            this.rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
        }


        public Computer(Computer other) : base(other)
        {
            this.ramGB = other.ramGB;
            this.hddGB = other.hddGB;
            this.processor = other.processor;
            this.positiveReviews = other.positiveReviews;
            this.totalReviews = other.totalReviews;
            this.rating = other.rating;
        }

        public int RamGB { get { return ramGB; } set { ramGB = value; } }
        public int HddGB { get { return hddGB; } set { hddGB = value; } }
        public string Processor { get { return processor; } set { processor = value; } }
        public int PositiveReviews { get { return positiveReviews; } set { positiveReviews = value; } }
        public int TotalReviews { get { return totalReviews; } set { totalReviews = value; } }
        public int Rating { get { return rating; } set { rating = value; } }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"RAM: {ramGB} GB | HDD: {hddGB} GB | Процесор: {processor}");
            Console.WriteLine($"Рейтинг: {rating}% ({positiveReviews}/{totalReviews} відгуків)");
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                $"\nRAM: {ramGB} GB\nHDD: {hddGB} GB\nПроцесор: {processor}" +
                $"\nРейтинг: {rating}%\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        // вартість з конфігурацією
        public decimal CalculatePriceWithConfiguration()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal configFactor = 1.0m + (ramGB / 128.0m) * 0.1m + (hddGB / 2000.0m) * 0.05m;
            if (processor.ToLower().Contains("i9") || processor.ToLower().Contains("ryzen 9"))
                configFactor += 0.2m;
            else if (processor.ToLower().Contains("i7") || processor.ToLower().Contains("ryzen 7"))
                configFactor += 0.1m;
            return basePrice * configFactor;
        }

        // вартість експлуатації (override)
        public override decimal CalculateOperatingCost(int years, decimal repairCostPerYear, decimal electricityPerYear)
        {
            decimal baseOpCost = base.CalculateOperatingCost(years, repairCostPerYear, electricityPerYear);
            // модернізація RAM/HDD через кожні 3 роки
            decimal upgradeCost = (years / 3) * 3000m;
            return baseOpCost + upgradeCost;
        }

        // користь/шкода
        public override string CalculateBenefitAndHarm(int hoursPerDay)
        {
            decimal productivityGain = hoursPerDay * 50m * 365; // 50 грн/год економія
            decimal healthCost = hoursPerDay > 8 ? 8000m : (hoursPerDay > 4 ? 4000m : 2000m);
            decimal eyeStrainCost = hoursPerDay * 200m; // витрати на зір
            return $"Продуктивність: +{productivityGain:C}/рік\n" +
                   $"Витрати на здоров'я: {healthCost + eyeStrainCost:C}/рік\n" +
                   $"  - Зір/хребет/психіка: {healthCost:C}\n" +
                   $"  - Очні краплі/окуліст: {eyeStrainCost:C}";
        }

        public int CalculateRating()
        {
            if (totalReviews == 0) return 0;
            return (positiveReviews * 100) / totalReviews;
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            positiveReviews = newPositive;
            totalReviews = newTotal;
            rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
            Console.WriteLine($"Відгуки оновлено. Рейтинг: {rating}%");
        }

        //  Бінарні оператори
        // збільшення ціни на суму
        public static Computer operator +(Computer c, decimal amount)
        {
            Computer result = new Computer(c);
            result.Price += amount;
            return result;
        }

        // зменшення ціни на суму
        public static Computer operator -(Computer c, decimal amount)
        {
            Computer result = new Computer(c);
            result.Price = Math.Max(0, result.Price - amount);
            return result;
        }

        // ==
        public static bool operator ==(Computer a, Computer b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.rating == b.rating;
        }

        public static bool operator !=(Computer a, Computer b) => !(a == b);

        // > <
        public static bool operator >(Computer a, Computer b) => a.Price > b.Price;
        public static bool operator <(Computer a, Computer b) => a.Price < b.Price;

        public override bool Equals(object obj) => obj is Computer c && rating == c.rating;
        public override int GetHashCode() => rating.GetHashCode();

        //Унарні оператори 
        //збільшення рейтингу на 5 (сучасна техніка)
        public static Computer operator ++(Computer c)
        {
            Computer result = new Computer(c);
            result.rating = Math.Min(100, result.rating + 5);
            return result;
        }

        //зменшення рейтингу
        public static Computer operator --(Computer c)
        {
            Computer result = new Computer(c);
            result.rating = Math.Max(0, result.rating - 5);
            return result;
        }

        // унарний  (100 - rating)
        public static Computer operator -(Computer c)
        {
            Computer result = new Computer(c);
            result.rating = 100 - result.rating;
            return result;
        }
    }
}
