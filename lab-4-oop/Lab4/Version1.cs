using System;
using System.IO;
using System.Text;

namespace task_4
{
    class ComputerTechV1 // Базовий клас
    {
        private string modelName;
        private decimal price;
        private string manufacturer;
        private int warrantyMonths;

     
        public ComputerTechV1()
        {
            this.modelName = "Unknown";
            this.price = 0;
            this.manufacturer = "Unknown";
            this.warrantyMonths = 0;
        }

        public ComputerTechV1(string modelName, decimal price, string manufacturer, int warrantyMonths)
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

        // виведення значень полів
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Модель: {modelName}");
            Console.WriteLine($"Виробник: {manufacturer}");
            Console.WriteLine($"Ціна: {price:C}");
            Console.WriteLine($"Гарантія: {warrantyMonths} місяців");
        }

        // запису в файл
        public virtual string GetInfo()
        {
            return $"Модель: {modelName}\nВиробник: {manufacturer}\nЦіна: {price:C}\nГарантія: {warrantyMonths} місяців";
        }

        // розрахунку вартості з урахуванням бренду
        public virtual decimal CalculatePriceWithBrandFactor()
        {
            decimal brandFactor = 1.0m;
            switch (manufacturer.ToLower())
            {
                case "apple":
                    brandFactor = 1.3m;
                    break;
                case "samsung":
                    brandFactor = 1.15m;
                    break;
                case "dell":
                case "hp":
                    brandFactor = 1.1m;
                    break;
                case "lenovo":
                    brandFactor = 1.05m;
                    break;
                default:
                    brandFactor = 1.0m;
                    break;
            }
            return price * brandFactor;
        }
    }

    // Похідний клас
    class ComputerV1 : ComputerTechV1
    {
        private int ramGB;
        private int hddGB;
        private string processor;
        private int positiveReviews;
        private int totalReviews;

        
        public ComputerV1(string modelName, decimal price, string manufacturer, int warrantyMonths,
                          int ramGB, int hddGB, string processor, int positiveReviews = 0, int totalReviews = 0)
                          : base(modelName, price, manufacturer, warrantyMonths)
        {
            this.ramGB = ramGB;
            this.hddGB = hddGB;
            this.processor = processor;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;
        }

        // Властивості
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

        // метод виведення
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"RAM: {ramGB} GB");
            Console.WriteLine($"HDD: {hddGB} GB");
            Console.WriteLine($"Процесор: {processor}");
            Console.WriteLine($"Позитивні відгуки: {positiveReviews}");
            Console.WriteLine($"Всього відгуків: {totalReviews}");
        }

        // метод для запису в файл
        public override string GetInfo()
        {
            return base.GetInfo() + $"\nRAM: {ramGB} GB\nHDD: {hddGB} GB\nПроцесор: {processor}\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        // розрахунку вартості з урахуванням комплектації
        public decimal CalculatePriceWithConfiguration()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();
            decimal configFactor = 1.0m + (ramGB / 128.0m) * 0.1m + (hddGB / 2000.0m) * 0.05m;
            if (processor.ToLower().Contains("i7") || processor.ToLower().Contains("ryzen 7"))
                configFactor += 0.1m;
            else if (processor.ToLower().Contains("i9") || processor.ToLower().Contains("ryzen 9"))
                configFactor += 0.2m;
            return basePrice * configFactor;
        }

        // визначення рейтингу за відгуками
        public int CalculateRating()
        {
            if (totalReviews == 0) return 0;
            return (positiveReviews * 100) / totalReviews;
        }

        // оновлення відгуків
        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            Console.WriteLine("Відгуки оновлено");
        }
    }

    // Похідний клас MonitorV1
    class MonitorV1 : ComputerTechV1
    {
        private double screenSizeInches;
        private string resolution;
        private string panelType;
        private int refreshRate;
        private int positiveReviews;
        private int totalReviews;

        
        public MonitorV1(string modelName, decimal price, string manufacturer, int warrantyMonths,
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

        // Властивості
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

        // метод виведення
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

        // запису в файл
        public override string GetInfo()
        {
            return base.GetInfo() + $"\nРозмір екрану: {screenSizeInches}\"\nРоздільна здатність: {resolution}\nТип панелі: {panelType}\nЧастота оновлення: {refreshRate} Гц\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        //розрахунку вартості з урахуванням розміру та характеристик
        public decimal CalculatePriceWithSizeAndFeatures()
        {
            decimal basePrice = base.CalculatePriceWithBrandFactor();

            decimal sizeFactor = 1.0m + (decimal)(screenSizeInches / 100.0);

            decimal resolutionFactor = 1.0m;
            if (resolution.Contains("4K") || resolution.Contains("3840"))
                resolutionFactor = 1.3m;
            else if (resolution.Contains("2K") || resolution.Contains("2560"))
                resolutionFactor = 1.15m;
            else if (resolution.Contains("Full HD") || resolution.Contains("1920"))
                resolutionFactor = 1.05m;

            decimal panelFactor = panelType.ToLower() == "ips" ? 1.2m :
                                 panelType.ToLower() == "va" ? 1.1m : 1.0m;

            decimal refreshFactor = 1.0m + (refreshRate / 240.0m) * 0.2m;

            return basePrice * sizeFactor * resolutionFactor * panelFactor * refreshFactor;
        }

        // визначення популярності
        public int CalculatePopularityRating()
        {
            if (totalReviews == 0) return 0;

            double resolutionFactor = resolution.Contains("4K") ? 1.2 :
                                      resolution.Contains("2K") ? 1.1 : 1.0;
            double panelFactor = panelType.ToLower() == "ips" ? 1.15 :
                                panelType.ToLower() == "va" ? 1.05 : 1.0;
            double refreshFactor = 1.0 + (refreshRate / 240.0) * 0.2;

            int baseRating = (positiveReviews * 100) / totalReviews;
            int totalRating = (int)(baseRating * resolutionFactor * panelFactor * refreshFactor);
            return Math.Min(100, (int)totalRating);
        }

        // оновлення відгуків
        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            Console.WriteLine("Відгуки оновлено");
        }
    }

    // Похідний клас TabletV1
    class TabletV1 : ComputerTechV1
    {
        private double screenSizeInches;
        private bool hasStylus;
        private bool hasKeyboard;
        private string connectivity;
        private int popularityRating;
        private int positiveReviews;
        private int totalReviews;

        // параметри
        public TabletV1(string modelName, decimal price, string manufacturer, int warrantyMonths,
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

        // Властивості
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

        //  метод виведення
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

        //  метод для запису в файл
        public override string GetInfo()
        {
            return base.GetInfo() + $"\nРозмір екрану: {screenSizeInches}\"\nНаявність стилуса: {(hasStylus ? "Так" : "Ні")}\nНаявність клавіатури: {(hasKeyboard ? "Так" : "Ні")}\nТип підключення: {connectivity}\nРейтинг популярності: {popularityRating}/100\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        // розрахунку вартості з урахуванням характеристик
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

        // оновлення рейтингу за відгуками
        public void UpdateRatingByReviews()
        {
            if (totalReviews > 0)
            {
                int calculatedRating = (positiveReviews * 100) / totalReviews;
                this.popularityRating = calculatedRating;
                Console.WriteLine($"Рейтинг оновлено: {popularityRating}%");
            }
        }

        // оновлення відгуків
        public void UpdateReviews(int newPositive, int newTotal)
        {
            this.positiveReviews = newPositive;
            this.totalReviews = newTotal;
            UpdateRatingByReviews();
        }

        // прогнозування популярності
        public int PredictPopularity(int monthsOnMarket)
        {
            int basePrediction = popularityRating;
            if (monthsOnMarket < 3)
                basePrediction += 10;
            else if (monthsOnMarket > 12)
                basePrediction -= 5;

            if (hasStylus && hasKeyboard)
                basePrediction += 15;

            return Math.Min(100, Math.Max(0, basePrediction));
        }
    }
}