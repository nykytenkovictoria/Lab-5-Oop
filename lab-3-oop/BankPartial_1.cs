using System;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Перша частина часткового класу BankPartial
    /// </summary>
    public partial class BankPartial  // Додано явний модифікатор public
    {
        // Закриті поля
        private string bankName;
        private string mfoCode;
        private string swiftCode;
        private string websiteUrl;

        // Конструктор
        public BankPartial(string bankName, string mfoCode, string swiftCode, string websiteUrl)
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
        public void PrintInfo()
        {
            Console.WriteLine($"Банк: {bankName}");
            Console.WriteLine($"МФО: {mfoCode}");
            Console.WriteLine($"SWIFT: {swiftCode}");
            Console.WriteLine($"Сайт: {websiteUrl}");
        }

        // Метод 1: Видача кредитів під відсотки (в першій частині)
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
    }
}