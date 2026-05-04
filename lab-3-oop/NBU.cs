using System;
using System.Collections.Generic;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Статичний клас, що представляє Національний банк України
    /// </summary>
    public static class NBU  // Додано явний модифікатор public
    {
        // Клас для зберігання курсів валют
        private class ExchangeRate
        {
            public string Currency { get; set; }
            public decimal Rate { get; set; }
            public DateTime Date { get; set; }
        }

        private static List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
        private static decimal moneySupply = 1000000000; // Загальна грошова маса в обігу

        // Статичний конструктор
        static NBU()
        {
            // Ініціалізація курсів валют
            exchangeRates.Add(new ExchangeRate { Currency = "USD", Rate = 36.5m, Date = DateTime.Now });
            exchangeRates.Add(new ExchangeRate { Currency = "EUR", Rate = 39.8m, Date = DateTime.Now });
            exchangeRates.Add(new ExchangeRate { Currency = "GBP", Rate = 45.2m, Date = DateTime.Now });
            exchangeRates.Add(new ExchangeRate { Currency = "PLN", Rate = 8.7m, Date = DateTime.Now });
        }

        /// <summary>
        /// Метод 1: Регулювання курсу грошової одиниці України по відношенню до іноземних валют
        /// </summary>
        public static void RegulateExchangeRate(string currency, decimal operationAmount, bool isBuying)
        {
            Console.WriteLine($"\n--- НБУ: Регулювання курсу {currency} ---");

            // Пошук поточного курсу
            var rate = exchangeRates.Find(r => r.Currency == currency);
            if (rate == null)
            {
                Console.WriteLine($"Валюта {currency} не знайдена в базі НБУ");
                return;
            }

            decimal oldRate = rate.Rate;
            decimal newRate = oldRate;

            if (isBuying)
            {
                // Купівля валюти - зміцнення гривні (зниження курсу)
                newRate = oldRate - (operationAmount / 1000000);
                Console.WriteLine($"НБУ купує {currency} на фінансових ринках. Курс знижується.");
            }
            else
            {
                // Продаж валюти - ослаблення гривні (підвищення курсу)
                newRate = oldRate + (operationAmount / 1000000);
                Console.WriteLine($"НБУ продає {currency} на фінансових ринках. Курс підвищується.");
            }

            // Оновлення курсу
            rate.Rate = Math.Max(10, Math.Min(100, newRate)); // Обмеження курсу в розумних межах
            rate.Date = DateTime.Now;

            Console.WriteLine($"Старий курс {currency}: {oldRate:F2} грн");
            Console.WriteLine($"Новий курс {currency}: {rate.Rate:F2} грн");
            Console.WriteLine($"Зміна: {(rate.Rate - oldRate):F2} грн ({(rate.Rate - oldRate) / oldRate * 100:F2}%)");

            // Запис у файл
            using (StreamWriter writer = new StreamWriter("nbu_operations.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: Регулювання курсу {currency}");
                writer.WriteLine($"Операція: {(isBuying ? "Купівля" : "Продаж")} на суму {operationAmount} ум.од.");
                writer.WriteLine($"Старий курс: {oldRate:F2}, Новий курс: {rate.Rate:F2}");
                writer.WriteLine(new string('-', 30));
            }
        }

        /// <summary>
        /// Метод 2: Емісія грошей в обіг
        /// </summary>
        public static void EmitMoney(decimal amount)
        {
            Console.WriteLine($"\n--- НБУ: Емісія грошей в обіг ---");

            if (amount <= 0)
            {
                Console.WriteLine("Сума емісії має бути додатною!");
                return;
            }

            decimal oldMoneySupply = moneySupply;
            moneySupply += amount;

            // Розрахунок інфляції (спрощена модель)
            decimal inflationRate = amount / oldMoneySupply * 100;

            Console.WriteLine($"Стара грошова маса: {oldMoneySupply:N0} грн");
            Console.WriteLine($"Емісія: {amount:N0} грн");
            Console.WriteLine($"Нова грошова маса: {moneySupply:N0} грн");
            Console.WriteLine($"Прогнозований рівень інфляції: {inflationRate:F2}%");

            // Запис у файл
            using (StreamWriter writer = new StreamWriter("nbu_operations.txt", true))
            {
                writer.WriteLine($"{DateTime.Now}: Емісія грошей");
                writer.WriteLine($"Сума емісії: {amount:N0} грн");
                writer.WriteLine($"Стара грошова маса: {oldMoneySupply:N0} грн");
                writer.WriteLine($"Нова грошова маса: {moneySupply:N0} грн");
                writer.WriteLine($"Прогнозована інфляція: {inflationRate:F2}%");
                writer.WriteLine(new string('-', 30));
            }
        }

        /// <summary>
        /// Виведення поточних курсів валют
        /// </summary>
        public static void PrintExchangeRates()
        {
            Console.WriteLine("\n--- Поточні курси валют (НБУ) ---");
            foreach (var rate in exchangeRates)
            {
                Console.WriteLine($"{rate.Currency}: {rate.Rate:F2} грн (оновлено: {rate.Date})");
            }
        }

        /// <summary>
        /// Виведення інформації про грошову масу
        /// </summary>
        public static void PrintMoneySupply()
        {
            Console.WriteLine($"\n--- Грошова маса в обігу: {moneySupply:N0} грн ---");
        }
    }
}