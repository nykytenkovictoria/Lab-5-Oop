using System;
using System.Collections.Generic;
using System.Text;
using task_5;

namespace task_5
{
    class Tablet : ComputerTech
    {
        private double screenSizeInches;
        private bool hasStylus;
        private bool hasKeyboard;
        private string connectivity;
        private int popularityRating;
        private int positiveReviews;
        private int totalReviews;

        // Конструктор 1
        public Tablet() : base()
        {
            screenSizeInches = 0; hasStylus = false; hasKeyboard = false;
            connectivity = "WiFi"; popularityRating = 0;
            positiveReviews = 0; totalReviews = 0;
        }

        // Конструктор 2
        public Tablet(string modelName, decimal price, string manufacturer, int warrantyMonths,
                      double screenSizeInches, bool hasStylus, bool hasKeyboard, string connectivity,
                      int popularityRating = 0, int positiveReviews = 0, int totalReviews = 0)
                      : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.screenSizeInches = screenSizeInches;
            this.hasStylus = hasStylus;
            this.hasKeyboard = hasKeyboard;
            this.connectivity = connectivity;
            this.popularityRating = popularityRating;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
        }

        // Конструктор 3: копіювання
        public Tablet(Tablet other) : base(other)
        {
            screenSizeInches = other.screenSizeInches;
            hasStylus = other.hasStylus;
            hasKeyboard = other.hasKeyboard;
            connectivity = other.connectivity;
            popularityRating = other.popularityRating;
            positiveReviews = other.positiveReviews;
            totalReviews = other.totalReviews;
        }

        public double ScreenSizeInches { get { return screenSizeInches; } set { screenSizeInches = value; } }
        public bool HasStylus { get { return hasStylus; } set { hasStylus = value; } }
        public bool HasKeyboard { get { return hasKeyboard; } set { hasKeyboard = value; } }
        public string Connectivity { get { return connectivity; } set { connectivity = value; } }
        public int PopularityRating { get { return popularityRating; } set { popularityRating = value; } }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Екран: {screenSizeInches}\" | Стилус: {(hasStylus ? "Так" : "Ні")} | Клавіатура: {(hasKeyboard ? "Так" : "Ні")}");
            Console.WriteLine($"Підключення: {connectivity} | Рейтинг: {popularityRating}/100");
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                $"\nЕкран: {screenSizeInches}\"\nСтилус: {(hasStylus ? "Так" : "Ні")}" +
                $"\nКлавіатура: {(hasKeyboard ? "Так" : "Ні")}\nПідключення: {connectivity}" +
                $"\nРейтинг: {popularityRating}/100";
        }

        public decimal CalculatePriceWithFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal sizeFactor = 1.0m + (decimal)(screenSizeInches / 200.0);
            decimal accessoryFactor = 1.0m;
            if (hasStylus) accessoryFactor += 0.15m;
            if (hasKeyboard) accessoryFactor += 0.2m;
            decimal connectivityFactor = connectivity.ToLower() == "5g" ? 1.25m :
                                         connectivity.ToLower() == "lte" ? 1.15m : 1.0m;
            return basePrice * sizeFactor * accessoryFactor * connectivityFactor;
        }

        public override decimal CalculateOperatingCost(int years, decimal repairCostPerYear, decimal electricityPerYear)
        {
            decimal baseOpCost = base.CalculateOperatingCost(years, repairCostPerYear, electricityPerYear);
            decimal mobilePlan = connectivity.ToLower() != "wifi" ? 500m * 12 * years : 0;
            return baseOpCost + mobilePlan;
        }

        public override string CalculateBenefitAndHarm(int hoursPerDay)
        {
            decimal mobilityBonus = 15000m; // можливість працювати будь-де
            decimal healthCost = hoursPerDay * 250m;
            string extras = hasStylus ? "\n  + Стилус: творчий потенціал +5000 грн/рік" : "";
            return $"Мобільність: +{mobilityBonus:C}/рік{extras}\n" +
                   $"Витрати на здоров'я (гіподинамія, зір): {healthCost:C}/рік";
        }

        public void UpdateRatingByReviews()
        {
            if (totalReviews > 0)
            {
                popularityRating = Math.Min(100, (positiveReviews * 100) / totalReviews);
                Console.WriteLine($"Рейтинг оновлено: {popularityRating}%");
            }
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            positiveReviews = newPositive;
            totalReviews = newTotal;
            UpdateRatingByReviews();
        }

        public int PredictPopularity(int monthsOnMarket)
        {
            int base_ = popularityRating;
            if (monthsOnMarket < 3) base_ += 10;
            else if (monthsOnMarket > 12) base_ -= 5;
            if (hasStylus && hasKeyboard) base_ += 15;
            return Math.Min(100, Math.Max(0, base_));
        }

        // ---- Бінарні оператори ----
        public static Tablet operator +(Tablet t, decimal amount)
        {
            Tablet result = new Tablet(t);
            result.Price += amount;
            return result;
        }

        public static Tablet operator -(Tablet t, decimal amount)
        {
            Tablet result = new Tablet(t);
            result.Price = Math.Max(0, result.Price - amount);
            return result;
        }

        public static bool operator ==(Tablet a, Tablet b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.popularityRating == b.popularityRating;
        }

        public static bool operator !=(Tablet a, Tablet b) => !(a == b);
        public static bool operator >(Tablet a, Tablet b) => a.Price > b.Price;
        public static bool operator <(Tablet a, Tablet b) => a.Price < b.Price;

        public override bool Equals(object obj) => obj is Tablet t && popularityRating == t.popularityRating;
        public override int GetHashCode() => popularityRating.GetHashCode();

        // ---- Унарні оператори ----
        public static Tablet operator ++(Tablet t)
        {
            Tablet result = new Tablet(t);
            result.popularityRating = Math.Min(100, result.popularityRating + 5);
            return result;
        }

        public static Tablet operator --(Tablet t)
        {
            Tablet result = new Tablet(t);
            result.popularityRating = Math.Max(0, result.popularityRating - 5);
            return result;
        }

        public static Tablet operator -(Tablet t)
        {
            Tablet result = new Tablet(t);
            result.popularityRating = 100 - result.popularityRating;
            return result;
        }
    }
}
