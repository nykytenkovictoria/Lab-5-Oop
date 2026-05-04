using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task_4
{
    // Клас ComputerV4 зі стандартними інтерфейсами
    class ComputerV4 : IComparable, IComparer<ComputerV4>, IEnumerable
    {
        private string modelName;
        private decimal price;
        private string manufacturer;
        private int warrantyMonths;
        private int ramGB;
        private int hddGB;
        private string processor;
        private double screenSize;
        private int positiveReviews;
        private int totalReviews;

        private static List<ComputerV4> computerList = new List<ComputerV4>();

        public ComputerV4(string modelName, decimal price, string manufacturer, int warrantyMonths,
                          int ramGB, int hddGB, string processor, double screenSize,
                          int positiveReviews = 0, int totalReviews = 0)
        {
            this.modelName = modelName;
            this.price = price;
            this.manufacturer = manufacturer;
            this.warrantyMonths = warrantyMonths;
            this.ramGB = ramGB;
            this.hddGB = hddGB;
            this.processor = processor;
            this.screenSize = screenSize;
            this.positiveReviews = positiveReviews;
            this.totalReviews = totalReviews;

            computerList.Add(this);
            Console.WriteLine($"ComputerV4({modelName}, {price:C}) створено");
        }

        // Властивості
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public int WarrantyMonths
        {
            get { return warrantyMonths; }
            set { warrantyMonths = value; }
        }

        public int RamGB
        {
            get { return ramGB; }
            set { ramGB = value; }
        }

        public int HddGB
        {
            get { return hddGB; }
            set { hddGB = value; }
        }

        public string Processor
        {
            get { return processor; }
            set { processor = value; }
        }

        public double ScreenSize
        {
            get { return screenSize; }
            set { screenSize = value; }
        }

        public int PositiveReviews
        {
            get { return positiveReviews; }
            set { positiveReviews = value; }
        }

        public int TotalReviews
        {
            get { return totalReviews; }
            set { totalReviews = value; }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Комп'ютер: {modelName}");
            Console.WriteLine($"Виробник: {manufacturer}");
            Console.WriteLine($"Ціна: {price:C}");
            Console.WriteLine($"Гарантія: {warrantyMonths} місяців");
            Console.WriteLine($"RAM: {ramGB} GB");
            Console.WriteLine($"HDD: {hddGB} GB");
            Console.WriteLine($"Процесор: {processor}");
            Console.WriteLine($"Діагональ: {screenSize}\"");
            Console.WriteLine($"Позитивні відгуки: {positiveReviews}");
            Console.WriteLine($"Всього відгуків: {totalReviews}");
        }

        public string GetInfo()
        {
            return $"Комп'ютер: {modelName}\nВиробник: {manufacturer}\nЦіна: {price:C}\nГарантія: {warrantyMonths} місяців\nRAM: {ramGB} GB\nHDD: {hddGB} GB\nПроцесор: {processor}\nДіагональ: {screenSize}\"\nПозитивні відгуки: {positiveReviews}\nВсього відгуків: {totalReviews}";
        }

        public decimal CalculatePriceWithBrandFactor()
        {
            decimal brandFactor = 1.0m;
            switch (manufacturer.ToLower())
            {
                case "apple": brandFactor = 1.3m; break;
                case "samsung": brandFactor = 1.15m; break;
                case "dell": case "hp": brandFactor = 1.1m; break;
                case "lenovo": brandFactor = 1.05m; break;
                default: brandFactor = 1.0m; break;
            }
            return price * brandFactor;
        }

        public decimal CalculatePriceWithConfiguration()
        {
            decimal basePrice = CalculatePriceWithBrandFactor();
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

        // IComparable - порівняння за ціною
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ComputerV4 otherComputer = obj as ComputerV4;
            if (otherComputer != null)
                return this.price.CompareTo(otherComputer.price);
            else
                throw new ArgumentException("Object is not a ComputerV4");
        }

        // IComparer - порівняння за ціною та габаритами
        public int Compare(ComputerV4 x, ComputerV4 y)
        {
            if (x == null || y == null) return 0;

            int priceCompare = x.price.CompareTo(y.price);

            if (priceCompare == 0)
            {
                return x.screenSize.CompareTo(y.screenSize);
            }

            return priceCompare;
        }

        // IEnumerable - для виведення списку комп'ютерів
        public IEnumerator GetEnumerator()
        {
            computerList.Sort();

            foreach (ComputerV4 computer in computerList)
            {
                yield return computer;
            }
        }

        // Додатковий компаратор для порівняння тільки за габаритами
        public static IComparer<ComputerV4> SortByScreenSize()
        {
            return (IComparer<ComputerV4>)new ScreenSizeComparer();
        }

        private class ScreenSizeComparer : IComparer<ComputerV4>
        {
            public int Compare(ComputerV4 x, ComputerV4 y)
            {
                if (x == null || y == null) return 0;
                return x.screenSize.CompareTo(y.screenSize);
            }
        }

        // Компаратор для порівняння за назвою
        public static IComparer<ComputerV4> SortByName()
        {
            return (IComparer<ComputerV4>)new NameComparer();
        }

        private class NameComparer : IComparer<ComputerV4>
        {
            public int Compare(ComputerV4 x, ComputerV4 y)
            {
                if (x == null || y == null) return 0;
                return string.Compare(x.modelName, y.modelName);
            }
        }
    }
}