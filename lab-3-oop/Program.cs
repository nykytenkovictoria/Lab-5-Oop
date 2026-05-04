using System;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Демонстрація Версії 1 (базові класи)
            DemoVersion1();

            // Демонстрація Версії 2 (з вкладеним класом)
            DemoVersion2();

            // Демонстрація Версії 3 (часткові класи та статичний клас)
            DemoVersion3();

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        static void DemoVersion1()
        {
            Console.WriteLine("\n========== ВЕРСІЯ 1: БАЗОВІ КЛАСИ ==========");

            // Створення клієнта через конструктор з параметрами
            Client client1 = new Client("Іван", "Петренко", "МН123456", "м. Київ, вул. Хрещатик, 1", "1234567890", "050-123-45-67");
            client1.IncreaseBalance(5000, "Зарплата");

            Console.WriteLine("\nІнформація про клієнта 1:");
            client1.PrintToConsole();

            // Створення клієнта через конструктор за замовчуванням та введення з консолі
            Console.WriteLine("\nВведіть дані для другого клієнта:");
            Client client2 = new Client();
            client2.InputFromConsole();
            client2.IncreaseBalance(30000, "Зарплатня");

            Console.WriteLine("\nІнформація про клієнта 2:");
            client2.PrintToConsole();

            // Створення банку
            Bank bank = new Bank("ПриватБанк", "320649", "PBANUA2X", "https://privatbank.ua");

            Console.WriteLine("\nІнформація про банк:");
            bank.PrintToConsole();

            // Операції з клієнтами
            Console.WriteLine("\n--- Операції з клієнтом 1 ---");
            client1.DecreaseBalance(1200, "Комунальні платежі");
            client1.IncreaseBalance(2000, "Переказ від родичів");

            // Кредитні операції
            bank.IssueLoan(10000, 15.5, 12, client1);

            // Депозитні операції
            bank.AcceptDeposit(5000, 12, 6, client1);

            // Відкриття рахунку
            bank.OpenAndManageAccount(client1, 100);

            // Запис у файли
            client1.WriteToFile("clients.txt");
            client2.WriteToFile("clients.txt");
            bank.WriteToFile("banks.txt");

            Console.WriteLine("\nДані збережено у файли clients.txt та banks.txt");
        }

        static void DemoVersion2()
        {
            Console.WriteLine("\n========== ВЕРСІЯ 2: ВКЛАДЕНИЙ КЛАС WEBSITE ==========");

            // Створення клієнтів
            Client client1 = new Client("Марія", "Шевченко", "ВН789012", "м. Львів, вул. Франка, 10", "0987654321", "097-123-45-67");
            client1.IncreaseBalance(8000, "Зарплата");

            Client client2 = new Client("Петро", "Коваленко", "АК345678", "м. Одеса, вул. Дерибасівська, 5", "1122334455", "063-987-65-43");
            client2.IncreaseBalance(2000, "Стипендія");

            // Створення банку з вкладеним класом
            BankWithNested bank = new BankWithNested("Монобанк", "322345", "MONOBANK", "https://monobank.ua");

            // Створення об'єкта вкладеного класу Website
            BankWithNested.Website website = new BankWithNested.Website("https://monobank.ua", "Monobank Online");

            Console.WriteLine("\nІнформація про банк та сайт:");
            website.PrintInfo();

            // Демонстрація методів вкладеного класу
            Console.WriteLine("\n--- Демонстрація методів Інтернет-банку ---");

            // Контроль залишків
            website.CheckAccountBalance(client1);

            // Оплата комунальних послуг
            website.PayUtility(client1, 850, "Електроенергія");

            // Оформлення депозиту онлайн
            website.OpenDepositOnline(client1, 3000, 10, 9, bank);

            // Переказ коштів
            website.TransferMoney(client1, client2, 500);

            // Запис у файл
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("website_operations.txt"))
            {
                writer.WriteLine("Операції через Інтернет-банк:");
                writer.WriteLine($"Клієнт: {client1.LastName} {client1.FirstName}");
                writer.WriteLine($"Баланс після операцій: {client1.AccountBalance:F2}");
                writer.WriteLine($"Клієнт: {client2.LastName} {client2.FirstName}");
                writer.WriteLine($"Баланс після операцій: {client2.AccountBalance:F2}");
            }

            Console.WriteLine("\nДані збережено у файл website_operations.txt");
        }

        static void DemoVersion3()
        {
            Console.WriteLine("\n========== ВЕРСІЯ 3: ЧАСТКОВІ КЛАСИ ТА СТАТИЧНИЙ КЛАСС NBU ==========");

            // Створення клієнта
            Client client = new Client("Олена", "Кравчук", "КМ901234", "м. Харків, вул. Сумська, 15", "5566778899", "066-111-22-33");
            client.IncreaseBalance(15000, "Зарплата");

            // Створення часткового класу BankPartial
            BankPartial bank = new BankPartial("Ощадбанк", "300465", "OSCHADUA", "https://oschadbank.ua");

            Console.WriteLine("\nІнформація про банк:");
            bank.PrintInfo();

            // Демонстрація методів з різних частин часткового класу
            Console.WriteLine("\n--- Метод з першої частини (кредити) ---");
            var loanResult = bank.IssueLoan(20000, 18, 24, client);

            Console.WriteLine("\n--- Метод з другої частини (депозити) ---");
            bank.AcceptDeposit(5000, 12, 12, client);

            Console.WriteLine("\n--- Метод з другої частини (відкриття рахунку) ---");
            bank.OpenAndManageAccount(client, 250);

            // Демонстрація статичного класу NBU
            Console.WriteLine("\n========== СТАТИЧНИЙ КЛАС NBU ==========");

            // Виведення поточних курсів
            NBU.PrintExchangeRates();
            NBU.PrintMoneySupply();

            // Регулювання курсу
            NBU.RegulateExchangeRate("USD", 5000000, true);  // Купівля валюти
            NBU.RegulateExchangeRate("EUR", 3000000, false); // Продаж валюти

            // Емісія грошей
            NBU.EmitMoney(50000000);

            // Виведення оновлених даних
            Console.WriteLine("\nПісля операцій НБУ:");
            NBU.PrintExchangeRates();
            NBU.PrintMoneySupply();

            // Запис інформації про клієнта
            client.WriteToFile("final_client.txt");

            Console.WriteLine("\nДані збережено у файли nbu_operations.txt та final_client.txt");
        }
    }
}