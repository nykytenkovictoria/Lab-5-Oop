using System;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Клас, що представляє клієнта банку
    /// </summary>
    public class Client  // Додано явний модифікатор public
    {
        // Закриті поля
        private string firstName;
        private string lastName;
        private string passport;
        private string address;
        private string identificationCode;
        private string phone;
        private decimal accountBalance;

        // Конструктор за замовчуванням
        public Client()
        {
            firstName = string.Empty;
            lastName = string.Empty;
            passport = string.Empty;
            address = string.Empty;
            identificationCode = string.Empty;
            phone = string.Empty;
            accountBalance = 0;
        }

        // Конструктор з параметрами
        public Client(string firstName, string lastName, string passport, string address,
                     string identificationCode, string phone)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.passport = passport;
            this.address = address;
            this.identificationCode = identificationCode;
            this.phone = phone;
            this.accountBalance = 0;
        }

        // Властивості для доступу до закритих полів
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string IdentificationCode
        {
            get { return identificationCode; }
            set { identificationCode = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public decimal AccountBalance
        {
            get { return accountBalance; }
            set { accountBalance = value; }
        }

        // Метод введення з консолі
        public void InputFromConsole()
        {
            Console.Write("Введіть ім'я клієнта: ");
            firstName = Console.ReadLine();

            Console.Write("Введіть прізвище клієнта: ");
            lastName = Console.ReadLine();

            Console.Write("Введіть паспортні дані: ");
            passport = Console.ReadLine();

            Console.Write("Введіть адресу прописки: ");
            address = Console.ReadLine();

            Console.Write("Введіть ідентифікаційний код: ");
            identificationCode = Console.ReadLine();

            Console.Write("Введіть телефон: ");
            phone = Console.ReadLine();
        }

        // Метод виведення на консоль
        public void PrintToConsole()
        {
            Console.WriteLine($"Клієнт: {lastName} {firstName}");
            Console.WriteLine($"Паспорт: {passport}");
            Console.WriteLine($"Адреса: {address}");
            Console.WriteLine($"Ідентифікаційний код: {identificationCode}");
            Console.WriteLine($"Телефон: {phone}");
            Console.WriteLine($"Баланс рахунку: {accountBalance:F2} грн");
        }

        // Метод запису в текстовий файл
        public void WriteToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine($"Клієнт: {lastName} {firstName}");
                writer.WriteLine($"Паспорт: {passport}");
                writer.WriteLine($"Адреса: {address}");
                writer.WriteLine($"Ідентифікаційний код: {identificationCode}");
                writer.WriteLine($"Телефон: {phone}");
                writer.WriteLine($"Баланс рахунку: {accountBalance:F2} грн");
                writer.WriteLine(new string('-', 40));
            }
        }

        // Метод зменшення суми на рахунку (оплата)
        public bool DecreaseBalance(decimal amount, string paymentType)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сума оплати має бути додатною!");
                return false;
            }

            if (accountBalance >= amount)
            {
                accountBalance -= amount;
                Console.WriteLine($"Оплата {paymentType} на суму {amount:F2} грн виконана успішно.");
                Console.WriteLine($"Новий баланс: {accountBalance:F2} грн");
                return true;
            }
            else
            {
                Console.WriteLine($"Недостатньо коштів на рахунку. Необхідно: {amount:F2} грн, доступно: {accountBalance:F2} грн");
                return false;
            }
        }

        // Метод поповнення рахунку
        public void IncreaseBalance(decimal amount, string incomeType)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сума поповнення має бути додатною!");
                return;
            }

            accountBalance += amount;
            Console.WriteLine($"Поповнення рахунку ({incomeType}) на суму {amount:F2} грн виконано успішно.");
            Console.WriteLine($"Новий баланс: {accountBalance:F2} грн");
        }
    }
}