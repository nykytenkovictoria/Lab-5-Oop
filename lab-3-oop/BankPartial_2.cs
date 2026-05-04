using System;
using System.IO;

namespace BankApp
{
    /// <summary>
    /// Друга частина часткового класу BankPartial
    /// </summary>
    public partial class BankPartial  // Додано явний модифікатор public
    {
        // Метод 2: Прийом вкладів з розрахунком прибутку по депозиту (в другій частині)
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

        // Метод 3: Відкриття та ведення рахунків
        public void OpenAndManageAccount(Client client, decimal openingFee)
        {
            Console.WriteLine($"\n--- Відкриття рахунку для клієнта {client.LastName} {client.FirstName} ---");

            if (client.AccountBalance >= openingFee)
            {
                client.DecreaseBalance(openingFee, "Комісія за відкриття рахунку");
                Console.WriteLine($"Рахунок успішно відкрито. Комісія за відкриття: {openingFee:F2} грн");
            }
            else
            {
                Console.WriteLine($"Недостатньо коштів для оплати комісії за відкриття рахунку.");
            }
        }
    }
}