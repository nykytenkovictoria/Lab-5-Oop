using System;
using System.Collections.Generic;
using System.Text;

namespace task_5
{
    class ComputerTech
    {
        private string modelName;
        private decimal price;
        private string manufacturer;
        private int warrantyMonths;

        // конструктор 
        public ComputerTech()
        {
            this.modelName = "Unknown";
            this.price = 0;
            this.manufacturer = "Unknown";
            this.warrantyMonths = 0;
        }

        // Конструктор 2: з параметрами
        public ComputerTech(string modelName, decimal price, string manufacturer, int warrantyMonths)
        {
            this.modelName = modelName;
            this.price = price;
            this.manufacturer = manufacturer;
            this.warrantyMonths = warrantyMonths;
        }

        // Конструктор 3: копіювання
        public ComputerTech(ComputerTech other)
        {
            this.modelName = other.modelName;
            this.price = other.price;
            this.manufacturer = other.manufacturer;
            this.warrantyMonths = other.warrantyMonths;
        }

        public string ModelName { get { return modelName; } set { modelName = value; } }
        public decimal Price { get { return price; } set { price = value; } }
        public string Manufacturer { get { return manufacturer; } set { manufacturer = value; } }
        public int WarrantyMonths { get { return warrantyMonths; } set { warrantyMonths = value; } }

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

        // вартість бренду
        public virtual decimal CalculatePriceWithBrandFactor()
        {
            decimal brandFactor = 1.0m;
            switch (manufacturer.ToLower())
            {
                case "apple": brandFactor = 1.3m; break;
                case "samsung": brandFactor = 1.15m; break;
                case "dell":
                case "hp": brandFactor = 1.1m; break;
                case "lenovo": brandFactor = 1.05m; break;
            }
            return price * brandFactor;
        }

        // вартість експлуатації 
        public virtual decimal CalculateOperatingCost(int years, decimal repairCostPerYear, decimal electricityPerYear)
        {
            return (repairCostPerYear + electricityPerYear) * years;
        }

        // користь
        public virtual string CalculateBenefitAndHarm(int hoursPerDay)
        {
            decimal timeSavedPerYear = hoursPerDay * 0.3m * 365;
            decimal healthCostPerYear = hoursPerDay > 6 ? 5000 : 2000;
            return $"Економія часу: {timeSavedPerYear} год/рік\nВитрати на здоров'я: {healthCostPerYear:C}/рік";
        }
    }
}
