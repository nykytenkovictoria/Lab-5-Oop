using System;
using System.Collections.Generic;
using System.Text;
using task_5;

namespace task_5
{
    class Printer : ComputerTech
    {
        private string printerType;   // Inkjet / Laser / MFP
        private int pagesPerMinute;
        private bool hasWifi;
        private bool hasDuplex;
        private decimal inkCostPerPage;
        private int rating;
        private int positiveReviews;
        private int totalReviews;

        // Конструктор 1
        public Printer() : base()
        {
            printerType = "Inkjet"; pagesPerMinute = 0;
            hasWifi = false; hasDuplex = false;
            inkCostPerPage = 0; rating = 0;
            positiveReviews = 0; totalReviews = 0;
        }

        // Конструктор 2
        public Printer(string modelName, decimal price, string manufacturer, int warrantyMonths,
                       string printerType, int pagesPerMinute, bool hasWifi, bool hasDuplex,
                       decimal inkCostPerPage, int positiveReviews = 0, int totalReviews = 0)
                       : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.printerType = printerType;
            this.pagesPerMinute = pagesPerMinute;
            this.hasWifi = hasWifi;
            this.hasDuplex = hasDuplex;
            this.inkCostPerPage = inkCostPerPage;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
            this.rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
        }

        // Конструктор 3: копіювання
        public Printer(Printer other) : base(other)
        {
            printerType = other.printerType;
            pagesPerMinute = other.pagesPerMinute;
            hasWifi = other.hasWifi;
            hasDuplex = other.hasDuplex;
            inkCostPerPage = other.inkCostPerPage;
            rating = other.rating;
            positiveReviews = other.positiveReviews;
            totalReviews = other.totalReviews;
        }

        public string PrinterType { get { return printerType; } set { printerType = value; } }
        public int PagesPerMinute { get { return pagesPerMinute; } set { pagesPerMinute = value; } }
        public bool HasWifi { get { return hasWifi; } set { hasWifi = value; } }
        public bool HasDuplex { get { return hasDuplex; } set { hasDuplex = value; } }
        public decimal InkCostPerPage { get { return inkCostPerPage; } set { inkCostPerPage = value; } }
        public int Rating { get { return rating; } set { rating = value; } }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Тип: {printerType} | {pagesPerMinute} стор/хв");
            Console.WriteLine($"WiFi: {(hasWifi ? "Так" : "Ні")} | Дуплекс: {(hasDuplex ? "Так" : "Ні")}");
            Console.WriteLine($"Вартість друку: {inkCostPerPage:C}/стор | Рейтинг: {rating}%");
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                $"\nТип принтера: {printerType}\nСтор/хв: {pagesPerMinute}" +
                $"\nWiFi: {(hasWifi ? "Так" : "Ні")}\nДуплекс: {(hasDuplex ? "Так" : "Ні")}" +
                $"\nВартість друку: {inkCostPerPage:C}/стор\nРейтинг: {rating}%";
        }

        // Вартість з характеристиками
        public decimal CalculatePriceWithFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal typeFactor = printerType.ToLower() == "laser" ? 1.3m :
                                 printerType.ToLower() == "mfp" ? 1.5m : 1.0m;
            decimal wifiFactor = hasWifi ? 1.1m : 1.0m;
            decimal duplexFactor = hasDuplex ? 1.05m : 1.0m;
            return basePrice * typeFactor * wifiFactor * duplexFactor;
        }

        // Вартість експлуатації: витратні матеріали
        public override decimal CalculateOperatingCost(int years, decimal repairCostPerYear, decimal electricityPerYear)
        {
            decimal baseOpCost = base.CalculateOperatingCost(years, repairCostPerYear, electricityPerYear);
            int pagesPerYear = pagesPerMinute * 60 * 5 * 250; // 5 год/день, 250 дн/рік
            decimal inkCostPerYear = inkCostPerPage * pagesPerYear;
            if (hasDuplex) inkCostPerYear *= 0.5m; // дуплекс економить папір
            return baseOpCost + inkCostPerYear * years;
        }

        // Користь/шкода
        public override string CalculateBenefitAndHarm(int hoursPerDay)
        {
            decimal officeBenefit = pagesPerMinute * 10m * 250 * hoursPerDay; // вартість часу
            decimal tonerDust = printerType.ToLower() == "laser" ? 3000m : 500m;
            return $"Економія на аутсорсингу друку: {officeBenefit:C}/рік\n" +
                   $"Шкода (пил від тонера/чорнила): {tonerDust:C}/рік на здоров'я";
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            positiveReviews = newPositive;
            totalReviews = newTotal;
            rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
            Console.WriteLine($"Відгуки оновлено. Рейтинг: {rating}%");
        }

        // ---- Бінарні оператори ----
        public static Printer operator +(Printer p, decimal amount)
        {
            Printer result = new Printer(p);
            result.Price += amount;
            return result;
        }

        public static Printer operator -(Printer p, decimal amount)
        {
            Printer result = new Printer(p);
            result.Price = Math.Max(0, result.Price - amount);
            return result;
        }

        public static bool operator ==(Printer a, Printer b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.rating == b.rating;
        }

        public static bool operator !=(Printer a, Printer b) => !(a == b);
        public static bool operator >(Printer a, Printer b) => a.Price > b.Price;
        public static bool operator <(Printer a, Printer b) => a.Price < b.Price;

        public override bool Equals(object obj) => obj is Printer p && rating == p.rating;
        public override int GetHashCode() => rating.GetHashCode();

        // ---- Унарні оператори ----
        public static Printer operator ++(Printer p)
        {
            Printer result = new Printer(p);
            result.rating = Math.Min(100, result.rating + 5);
            return result;
        }

        public static Printer operator --(Printer p)
        {
            Printer result = new Printer(p);
            result.rating = Math.Max(0, result.rating - 5);
            return result;
        }

        public static Printer operator -(Printer p)
        {
            Printer result = new Printer(p);
            result.rating = 100 - result.rating;
            return result;
        }
    }
}
