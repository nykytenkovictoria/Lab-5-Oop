using System;
using System.Collections.Generic;
using System.Text;
using task_5;

namespace task_5
{
    class Monitor : ComputerTech
    {
        private double screenSizeInches;
        private string resolution;
        private string panelType;
        private int refreshRate;
        private int positiveReviews;
        private int totalReviews;
        private int rating;

        // Конструктор 1
        public Monitor() : base()
        {
            screenSizeInches = 0; resolution = "Unknown";
            panelType = "Unknown"; refreshRate = 60;
            positiveReviews = 0; totalReviews = 0; rating = 0;
        }

        // Конструктор 2
        public Monitor(string modelName, decimal price, string manufacturer, int warrantyMonths,
                       double screenSizeInches, string resolution, string panelType, int refreshRate,
                       int positiveReviews = 0, int totalReviews = 0)
                       : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.screenSizeInches = screenSizeInches;
            this.resolution = resolution;
            this.panelType = panelType;
            this.refreshRate = refreshRate;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
            this.rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
        }

        // Конструктор 3: копіювання
        public Monitor(Monitor other) : base(other)
        {
            screenSizeInches = other.screenSizeInches;
            resolution = other.resolution;
            panelType = other.panelType;
            refreshRate = other.refreshRate;
            positiveReviews = other.positiveReviews;
            totalReviews = other.totalReviews;
            rating = other.rating;
        }

        public double ScreenSizeInches { get { return screenSizeInches; } set { screenSizeInches = value; } }
        public string Resolution { get { return resolution; } set { resolution = value; } }
        public string PanelType { get { return panelType; } set { panelType = value; } }
        public int RefreshRate { get { return refreshRate; } set { refreshRate = value; } }
        public int Rating { get { return rating; } set { rating = value; } }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Екран: {screenSizeInches}\" | {resolution} | {panelType} | {refreshRate} Гц");
            Console.WriteLine($"Рейтинг: {rating}% ({positiveReviews}/{totalReviews} відгуків)");
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                $"\nРозмір: {screenSizeInches}\"\nРоздільна здатність: {resolution}" +
                $"\nТип панелі: {panelType}\nЧастота: {refreshRate} Гц\nРейтинг: {rating}%";
        }

        public decimal CalculatePriceWithSizeAndFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal sizeFactor = 1.0m + (decimal)(screenSizeInches / 100.0);
            decimal resFactor = resolution.Contains("4K") ? 1.3m :
                                resolution.Contains("2K") ? 1.15m : 1.05m;
            decimal panelFactor = panelType.ToLower() == "ips" ? 1.2m :
                                  panelType.ToLower() == "va" ? 1.1m : 1.0m;
            decimal refreshFactor = 1.0m + (refreshRate / 240.0m) * 0.2m;
            return basePrice * sizeFactor * resFactor * panelFactor * refreshFactor;
        }

        public override decimal CalculateOperatingCost(int years, decimal repairCostPerYear, decimal electricityPerYear)
        {
            decimal baseOpCost = base.CalculateOperatingCost(years, repairCostPerYear, electricityPerYear);
            // Монітор споживає менше — знижка 10%
            return baseOpCost * 0.9m;
        }

        public override string CalculateBenefitAndHarm(int hoursPerDay)
        {
            decimal visualComfortBonus = panelType.ToLower() == "ips" ? 1000m : 500m;
            decimal eyeStrain = hoursPerDay * 300m;
            return $"Комфорт зображення ({panelType}): +{visualComfortBonus:C}/рік\n" +
                   $"Витрати на зір: {eyeStrain:C}/рік";
        }

        public int CalculatePopularityRating()
        {
            if (totalReviews == 0) return 0;
            return Math.Min(100, (int)((positiveReviews * 100.0 / totalReviews) *
                (resolution.Contains("4K") ? 1.2 : 1.0) *
                (panelType.ToLower() == "ips" ? 1.1 : 1.0)));
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            positiveReviews = newPositive;
            totalReviews = newTotal;
            rating = totalReviews > 0 ? (positiveReviews * 100) / totalReviews : 0;
            Console.WriteLine($"Відгуки оновлено. Рейтинг: {rating}%");
        }

        // ---- Бінарні оператори ----
        public static Monitor operator +(Monitor m, decimal amount)
        {
            Monitor result = new Monitor(m);
            result.Price += amount;
            return result;
        }

        public static Monitor operator -(Monitor m, decimal amount)
        {
            Monitor result = new Monitor(m);
            result.Price = Math.Max(0, result.Price - amount);
            return result;
        }

        public static bool operator ==(Monitor a, Monitor b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.rating == b.rating;
        }

        public static bool operator !=(Monitor a, Monitor b) => !(a == b);
        public static bool operator >(Monitor a, Monitor b) => a.Price > b.Price;
        public static bool operator <(Monitor a, Monitor b) => a.Price < b.Price;

        public override bool Equals(object obj) => obj is Monitor m && rating == m.rating;
        public override int GetHashCode() => rating.GetHashCode();

        // ---- Унарні оператори ----
        public static Monitor operator ++(Monitor m)
        {
            Monitor result = new Monitor(m);
            result.rating = Math.Min(100, result.rating + 5);
            return result;
        }

        public static Monitor operator --(Monitor m)
        {
            Monitor result = new Monitor(m);
            result.rating = Math.Max(0, result.rating - 5);
            return result;
        }

        public static Monitor operator -(Monitor m)
        {
            Monitor result = new Monitor(m);
            result.rating = 100 - result.rating;
            return result;
        }
    }

}
