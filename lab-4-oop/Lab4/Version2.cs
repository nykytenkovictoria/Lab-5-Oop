using System;
using System.IO;
using System.Text;

namespace task_4
{
    // Інтерфейс IComputerTechV2
    interface IComputerTechV2
    {
        string ModelName { get; set; }
        decimal Price { get; set; }
        string Manufacturer { get; set; }
        int WarrantyMonths { get; set; }

        void DisplayInfo();
        string GetInfo();
        decimal CalculatePriceWithBrandFactor();
    }

    // Базовий клас ComputerTechV2
    class ComputerTechV2 : IComputerTechV2
    {
        private string modelName;
        private decimal price;
        private string manufacturer;
        private int warrantyMonths;

        public ComputerTechV2()
        {
            this.modelName = "Unknown";
            this.price = 0;
            this.manufacturer = "Unknown";
            this.warrantyMonths = 0;
        }

        public ComputerTechV2(string modelName, decimal price, string manufacturer, int warrantyMonths)
        {
            this.modelName = modelName;
            this.price = price;
            this.manufacturer = manufacturer;
            this.warrantyMonths = warrantyMonths;
        }

        // Властивості
        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                modelName = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
            }
        }

        public int WarrantyMonths
        {
            get
            {
                return warrantyMonths;
            }
            set
            {
                warrantyMonths = value;
            }
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Модель: {modelName}");
            Console.WriteLine($"Виробник: {manufacturer}");
            Console.WriteLine($"Ціна: {price:C}");
            Console.WriteLine($"Гарантія: {warrantyMonths} місяців");
        }

        public virtual string GetInfo()
        {
            return $"Модель: {modelName}\nВиробник: {manufacturer}\nЦіна: {price:C}\nГарантія: {warrantyMonths} місяців";
        }

        public virtual decimal CalculatePriceWithBrandFactor()
        {
            decimal brandFactor = 1.0m;
            switch (manufacturer.ToLower())
            {
                case "apple": brandFactor = 1.3m; break;
                case "samsung": brandFactor = 1.15m; break;
                case "dell": case "hp": brandFactor = 1.1m; break;
                default: brandFactor = 1.0m; break;
            }
            return price * brandFactor;
        }
    }

    // Похідний клас ComputerV2
    class ComputerV2 : ComputerTechV2
    {
        private int ramGB;
        private int hddGB;
        private string processor;
        private int positiveReviews;
        private int totalReviews;

        public ComputerV2(string modelName, decimal price, string manufacturer, int warrantyMonths,
                          int ramGB, int hddGB, string processor, int positiveReviews = 0, int totalReviews = 0)
                          : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.ramGB = ramGB;
            this.hddGB = hddGB;
            this.processor = processor;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
        }

        public int RamGB
        {
            get
            {
                return ramGB;
            }
            set
            {
                ramGB = value;
            }
        }

        public int HddGB
        {
            get
            {
                return hddGB;
            }
            set
            {
                hddGB = value;
            }
        }

        public string Processor
        {
            get
            {
                return processor;
            }
            set
            {
                processor = value;
            }
        }

        public int PositiveReviews
        {
            get
            {
                return positiveReviews;
            }
            set
            {
                positiveReviews = value;
            }
        }

        public int TotalReviews
        {
            get
            {
                return totalReviews;
            }
            set
            {
                totalReviews = value;
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"RAM: {ramGB} GB");
            Console.WriteLine($"HDD: {hddGB} GB");
            Console.WriteLine($"Процесор: {processor}");
            Console.WriteLine($"Позитивні відгуки: {positiveReviews}");
            Console.WriteLine($"Всього відгуків: {totalReviews}");
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $"\nRAM: {ramGB} GB\nHDD: {hddGB} GB\nПроцесор: {processor}\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        public decimal CalculatePriceWithConfiguration()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal configFactor = 1.0m + (ramGB / 128.0m) * 0.1m + (hddGB / 2000.0m) * 0.05m;
            return basePrice * configFactor;
        }

        public int CalculateRating()
        {
            if (totalReviews == 0) return 0;
            return (positiveReviews * 100) / totalReviews;
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            Console.WriteLine("Відгуки оновлено");
        }
    }

    // Похідний клас MonitorV2
    class MonitorV2 : ComputerTechV2
    {
        private double screenSizeInches;
        private string resolution;
        private string panelType;
        private int refreshRate;
        private int positiveReviews;
        private int totalReviews;

        public MonitorV2(string modelName, decimal price, string manufacturer, int warrantyMonths,
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
        }

        public double ScreenSizeInches
        {
            get 
            { 
                return screenSizeInches; 
            }
            set 
            { 
                screenSizeInches = value; 
            }
        }

        public string Resolution
        {
            get 
            { 
                return resolution; 
            }
            set 
            { 
                resolution = value; 
            }
        }

        public string PanelType
        {
            get 
            { 
                return panelType; 
            }
            set 
            { 
                panelType = value; 
            }
        }

        public int RefreshRate
        {
            get 
            { 
                return refreshRate; 
            }
            set 
            { 
                refreshRate = value; 
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Розмір екрану: {screenSizeInches}\"");
            Console.WriteLine($"Роздільна здатність: {resolution}");
            Console.WriteLine($"Тип панелі: {panelType}");
            Console.WriteLine($"Частота оновлення: {refreshRate} Гц");
            Console.WriteLine($"Позитивні відгуки: {positiveReviews}");
            Console.WriteLine($"Всього відгуків: {totalReviews}");
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $"\nРозмір екрану: {screenSizeInches}\"\nРоздільна здатність: {resolution}\nТип панелі: {panelType}\nЧастота оновлення: {refreshRate} Гц\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        public decimal CalculatePriceWithSizeAndFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal sizeFactor = 1.0m + (decimal)(screenSizeInches / 100.0);
            return basePrice * sizeFactor;
        }

        public int CalculatePopularityRating()
        {
            if (totalReviews == 0) return 0;
            return (positiveReviews * 100) / totalReviews;
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            Console.WriteLine("Відгуки оновлено");
        }
    }

    // Похідний клас TabletV2
    class TabletV2 : ComputerTechV2
    {
        private double screenSizeInches;
        private bool hasStylus;
        private bool hasKeyboard;
        private string connectivity;
        private int popularityRating;
        private int positiveReviews;
        private int totalReviews;

        public TabletV2(string modelName, decimal price, string manufacturer, int warrantyMonths,
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

        public double ScreenSizeInches
        {
            get
            {
                return screenSizeInches;
            }
            set
            {
                screenSizeInches = value;
            }
        }

        public bool HasStylus
        {
            get
            {
                return hasStylus;
            }
            set
            {
                hasStylus = value;
            }
        }

        public bool HasKeyboard
        {
            get
            {
                return hasKeyboard;
            }
            set
            {
                hasKeyboard = value;
            }
        }

        public string Connectivity
        {
            get
            {
                return connectivity;
            }
            set
            {
                connectivity = value;
            }
        }

        public int PopularityRating
        {
            get
            {
                return popularityRating;
            }
            set
            {
                popularityRating = value;
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Розмір екрану: {screenSizeInches}\"");
            Console.WriteLine($"Наявність стилуса: {(hasStylus ? "Так" : "Ні")}");
            Console.WriteLine($"Наявність клавіатури: {(hasKeyboard ? "Так" : "Ні")}");
            Console.WriteLine($"Тип підключення: {connectivity}");
            Console.WriteLine($"Рейтинг популярності: {popularityRating}/100");
            Console.WriteLine($"Позитивні відгуки: {positiveReviews}");
            Console.WriteLine($"Всього відгуків: {totalReviews}");
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $"\nРозмір екрану: {screenSizeInches}\"\nНаявність стилуса: {(hasStylus ? "Так" : "Ні")}\nНаявність клавіатури: {(hasKeyboard ? "Так" : "Ні")}\nТип підключення: {connectivity}\nРейтинг популярності: {popularityRating}/100\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        public decimal CalculatePriceWithFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal sizeFactor = 1.0m + (decimal)(screenSizeInches / 200.0);
            decimal accessoryFactor = 1.0m;
            if (hasStylus) accessoryFactor += 0.15m;
            if (hasKeyboard) accessoryFactor += 0.2m;
            return basePrice * sizeFactor * accessoryFactor;
        }

        public void UpdateRatingByReviews()
        {
            if (totalReviews > 0)
            {
                int calculatedRating = (positiveReviews * 100) / totalReviews;
                this.popularityRating = calculatedRating;
                Console.WriteLine($"Рейтинг оновлено: {popularityRating}%");
            }
        }

        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            UpdateRatingByReviews();
        }
    }
}