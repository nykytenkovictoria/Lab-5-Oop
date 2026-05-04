using System;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Клас банку з вкладеним класом Website
    /// </summary>
    public class BankWithNested  // Додано явний модифікатор public
    {
        // Закриті поля
        private string bankName;
        private string mfoCode;
        private string swiftCode;
        private string websiteUrl;

        // Конструктор за замовчуванням
        public BankWithNested()
        {
            bankName = string.Empty;
            mfoCode = string.Empty;
            swiftCode = string.Empty;
            websiteUrl = string.Empty;
        }

        // Конструктор з параметрами
        public BankWithNested(string bankName, string mfoCode, string swiftCode, string websiteUrl)
        {
            this.bankName = bankName;
            this.mfoCode = mfoCode;
            this.swiftCode = swiftCode;
            this.websiteUrl = websiteUrl;
        }

        // Властивості
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        public string MfoCode
        {
            get { return mfoCode; }
            set { mfoCode = value; }
        }

        public string SwiftCode
        {
            get { return swiftCode; }
            set { swiftCode = value; }
        }

        public string WebsiteUrl
        {
            get { return websiteUrl; }
            set { websiteUrl = value; }
        }

        // Метод виведення
        public void PrintToConsole()
        {
            Console.WriteLine($"Банк: {bankName}");
            Console.WriteLine($"МФО: {mfoCode}");
            Console.WriteLine($"SWIFT: {swiftCode}");
            Console.WriteLine($"Сайт: {websiteUrl}");
        }

        // Вкладений клас Website
        public class Website  // Цей клас автоматично буде public, тому що зовнішній клас public
        {
            // Поля вкладеного класу
            private string url;
            private string internetBankName;

            // Конструктор
            public Website(string url, string internetBankName)
            {
                this.url = url;
                this.internetBankName = internetBankName;
            }

            // Властивості
            public string Url
            {
                get { return url; }
                set { url = value; }
            }

            public string InternetBankName
            {
                get { return internetBankName; }
                set { internetBankName = value; }
            }

            // Метод 1: Контроль залишків по поточних рахунках
            public void CheckAccountBalance(Client client)
            {
                Console.WriteLine($"\n--- Інтернет-банк {internetBankName} ---");
                Console.WriteLine($"Контроль залишку рахунку клієнта {client.LastName} {client.FirstName}:");
                Console.WriteLine($"Поточний баланс: {client.AccountBalance:F2} грн");

                if (client.AccountBalance < 100)
                {
                    Console.WriteLine("Увага! Низький залишок на рахунку.");
                }
                else if (client.AccountBalance > 10000)
                {
                    Console.WriteLine("Рекомендуємо розглянути пропозиції по депозитах для збереження коштів.");
                }
            }

            // Метод 2: Оформлення депозиту
            public void OpenDepositOnline(Client client, decimal amount, double interestRate, int months, BankWithNested bank)
            {
                Console.WriteLine($"\n--- Оформлення депозиту через {internetBankName} ---");

                // Перевірка наявності коштів
                if (client.AccountBalance < amount)
                {
                    Console.WriteLine($"Недостатньо коштів. Доступно: {client.AccountBalance:F2} грн");
                    return;
                }

                // Зняття коштів
                client.DecreaseBalance(amount, "Депозит (онлайн)");

                // Розрахунок прибутку
                decimal profit = amount * (decimal)(interestRate / 100) * months / 12;
                decimal totalAmount = amount + profit;

                Console.WriteLine($"Депозит успішно оформлено онлайн!");
                Console.WriteLine($"Сума: {amount:F2} грн");
                Console.WriteLine($"Ставка: {interestRate}%");
                Console.WriteLine($"Термін: {months} міс.");
                Console.WriteLine($"Прибуток: {profit:F2} грн");
                Console.WriteLine($"Сума після закінчення: {totalAmount:F2} грн");
            }

            // Метод 3: Оплата комунальних послуг
            public bool PayUtility(Client client, decimal amount, string utilityType)
            {
                Console.WriteLine($"\n--- Оплата комунальних послуг через {internetBankName} ---");
                Console.WriteLine($"Послуга: {utilityType}");

                return client.DecreaseBalance(amount, $"Комунальні послуги ({utilityType})");
            }

            // Метод 4: Переказ коштів на рахунки
            public bool TransferMoney(Client fromClient, Client toClient, decimal amount)
            {
                Console.WriteLine($"\n--- Переказ коштів через {internetBankName} ---");
                Console.WriteLine($"Відправник: {fromClient.LastName} {fromClient.FirstName}");
                Console.WriteLine($"Отримувач: {toClient.LastName} {toClient.FirstName}");
                Console.WriteLine($"Сума переказу: {amount:F2} грн");

                if (fromClient.AccountBalance >= amount)
                {
                    fromClient.DecreaseBalance(amount, "Переказ коштів");
                    toClient.IncreaseBalance(amount, "Отриманий переказ");
                    Console.WriteLine("Переказ виконано успішно!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Недостатньо коштів для переказу.");
                    return false;
                }
            }

            // Виведення інформації про сайт
            public void PrintInfo()
            {
                Console.WriteLine($"Сайт банку: {url}");
                Console.WriteLine($"Інтернет-банк: {internetBankName}");
            }
        }
    }
}