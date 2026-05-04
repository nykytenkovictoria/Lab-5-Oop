using System;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Клас, що представляє банк
    /// </summary>
    public class Bank  // Додано явний модифікатор public
    {
        // Закриті поля
        private string bankName;
        private string mfoCode;
        private string swiftCode;
        private string websiteUrl;

        // Конструктор за замовчуванням
        public Bank()
        {
            bankName = string.Empty;
            mfoCode = string.Empty;
            swiftCode = string.Empty;
            websiteUrl = string.Empty;
        }

        // Конструктор з параметрами
        public Bank(string bankName, string mfoCode, string swiftCode, string websiteUrl)
        {
            this.bankName = bankName;
            this.mfoCode = mfoCode;
            this.swiftCode = swiftCode;
            this.websiteUrl = websiteUrl;
        }

        // Властивості для доступу до закритих полів
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

        // Метод введення з консолі
        public void InputFromConsole()
        {
            Console.Write("Введіть назву банку: ");
            bankName = Console.ReadLine();

            Console.Write("Введіть МФО банку: ");
            mfoCode = Console.ReadLine();

            Console.Write("Введіть SWIFT код: ");
            swiftCode = Console.ReadLine();

            Console.Write("Введіть сайт банку: ");
            websiteUrl = Console.ReadLine();
        }

        // Метод виведення на консоль
        public void PrintToConsole()
        {
            Console.WriteLine($"Банк: {bankName}");
            Console.WriteLine($"МФО: {mfoCode}");
            Console.WriteLine($"SWIFT: {swiftCode}");
            Console.WriteLine($"Сайт: {websiteUrl}");
        }

        // Метод запису в текстовий файл
        public void WriteToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine($"Банк: {bankName}");
                writer.WriteLine($"МФО: {mfoCode}");
                writer.WriteLine($"SWIFT: {swiftCode}");
                writer.WriteLine($"Сайт: {websiteUrl}");
                writer.WriteLine(new string('-', 40));
            }
        }

        // Метод видачі кредиту
        public (decimal totalAmount, decimal monthlyPayment) IssueLoan(decimal amount, double interestRate, int months, Client client)
        {
            Console.WriteLine($"\n--- Оформлення кредиту для клієнта {client.LastName} {client.FirstName} ---");

            // Розрахунок відсотків за кредитом
            decimal interest = amount * (decimal)(interestRate / 100);
            decimal totalAmount = amount + interest;
            decimal monthlyPayment = totalAmount / months;

            Console.WriteLine($"Сума кредиту: {amount:F2} грн");
            Console.WriteLine($"Відсоткова ставка: {interestRate}%");
            Console.WriteLine($"Сума відсотків: {interest:F2} грн");
            Console.WriteLine($"Загальна сума до повернення: {totalAmount:F2} грн");
            Console.WriteLine($"Термін: {months} міс.");
            Console.WriteLine($"Щомісячний платіж: {monthlyPayment:F2} грн");

            // Зарахування коштів на рахунок клієнта
            client.IncreaseBalance(amount, "Кредит");

            // Запис у файл
            using (StreamWriter writer = new StreamWriter("loan_info.txt", true))
            {
                writer.WriteLine($"Клієнт: {client.LastName} {client.FirstName}");
                writer.WriteLine($"Сума кредиту: {amount:F2} грн");
                writer.WriteLine($"Ставка: {interestRate}%");
                writer.WriteLine($"Загальна сума: {totalAmount:F2} грн");
                writer.WriteLine($"Щомісячний платіж: {monthlyPayment:F2} грн");
                writer.WriteLine(new string('-', 30));
            }

            return (totalAmount, monthlyPayment);
        }

        // Метод прийому вкладу
        public decimal AcceptDeposit(decimal amount, double interestRate, int months, Client client)
        {
            Console.WriteLine($"\n--- Оформлення депозиту для клієнта {client.LastName} {client.FirstName} ---");

            // Перевірка наявності коштів на рахунку
            if (client.AccountBalance < amount)
            {
                Console.WriteLine($"Недостатньо коштів для відкриття депозиту. Доступно: {client.AccountBalance:F2} грн, необхідно: {amount:F2} грн");
                return 0;
            }

            // Зняття коштів з рахунку
            client.DecreaseBalance(amount, "Відкриття депозиту");

            // Розрахунок прибутку за формулою: сума прибутку = розмір депозиту * (1+(відсоткова ставка/12)*термін зберігання вкладу/100)
            decimal profit = amount * (1 + (decimal)(interestRate / 12) * months / 100);
            decimal totalAmount = amount + profit;

            Console.WriteLine($"Сума депозиту: {amount:F2} грн");
            Console.WriteLine($"Відсоткова ставка: {interestRate}% річних");
            Console.WriteLine($"Термін: {months} міс.");
            Console.WriteLine($"Прибуток: {profit:F2} грн");
            Console.WriteLine($"Сума після закінчення терміну: {totalAmount:F2} грн");

            // Запис у файл
            using (StreamWriter writer = new StreamWriter("deposit_info.txt", true))
            {
                writer.WriteLine($"Клієнт: {client.LastName} {client.FirstName}");
                writer.WriteLine($"Сума депозиту: {amount:F2} грн");
                writer.WriteLine($"Ставка: {interestRate}%");
                writer.WriteLine($"Термін: {months} міс.");
                writer.WriteLine($"Прибуток: {profit:F2} грн");
                writer.WriteLine($"Сума після закінчення: {totalAmount:F2} грн");
                writer.WriteLine(new string('-', 30));
            }

            return profit;
        }

        // Метод відкриття та ведення рахунків
        public void OpenAndManageAccount(Client client, decimal openingFee)
        {
            Console.WriteLine($"\n--- Відкриття рахунку для клієнта {client.LastName} {client.FirstName} ---");

            // Списання комісії за відкриття
            if (client.AccountBalance >= openingFee)
            {
                client.DecreaseBalance(openingFee, "Комісія за відкриття рахунку");
                Console.WriteLine($"Рахунок успішно відкрито. Комісія за відкриття: {openingFee:F2} грн");

                // Запис у файл
                using (StreamWriter writer = new StreamWriter("account_info.txt", true))
                {
                    writer.WriteLine($"Клієнт: {client.LastName} {client.FirstName}");
                    writer.WriteLine($"Відкрито рахунок. Комісія: {openingFee:F2} грн");
                    writer.WriteLine($"Дата: {DateTime.Now}");
                    writer.WriteLine(new string('-', 30));
                }
            }
            else
            {
                Console.WriteLine($"Недостатньо коштів для оплати комісії за відкриття рахунку. Необхідно: {openingFee:F2} грн");
            }
        }
    }
}