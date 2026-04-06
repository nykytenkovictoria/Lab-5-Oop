using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task_5
{



    class ComputerCollection : IEnumerable<Computer>
    {
        private Computer[] computers;
        private int count;

        public ComputerCollection(int capacity)
        {
            computers = new Computer[capacity];
            count = 0;
        }

        // Індексатор
        public Computer this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException($"Індекс {index} виходить за межі ({count} елементів)");
                return computers[index];
            }
            set
            {
                if (index < 0 || index >= computers.Length)
                    throw new IndexOutOfRangeException($"Індекс {index} виходить за межі");
                computers[index] = value;
                if (index >= count) count = index + 1;
            }
        }

        public int Count => count;

        public void Add(Computer c)
        {
            if (count >= computers.Length)
                throw new InvalidOperationException("Колекція заповнена");
            computers[count++] = c;
        }

        public IEnumerator<Computer> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return computers[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void DisplayAll()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Комп'ютер [{i}] ---");
                computers[i].DisplayInfo();
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (StreamWriter w = new StreamWriter("lab5_results.txt", false, Encoding.UTF8))
            {

                void Print(string text)
                {
                    Console.WriteLine(text);
                    w.WriteLine(text);
                }

                void Section(string title)
                {
                    string line = new string('=', 50);
                    Print($"\n{line}\n{title}\n{line}");
                }


                Section("1. КОНСТРУКТОРИ");

                Computer c1 = new Computer();
                Computer c2 = new Computer("MacBook Pro", 65000, "Apple", 12, 32, 1024, "M2 Pro", 450, 500);
                Computer c3 = new Computer(c2);
                c3.ModelName = "MacBook Pro (копія)";

                Print("--- Без параметрів ---");
                c1.DisplayInfo(); w.WriteLine(c1.GetInfo());

                Print("\n--- З параметрами ---");
                c2.DisplayInfo(); w.WriteLine(c2.GetInfo());

                Print("\n--- Копіювання ---");
                c3.DisplayInfo(); w.WriteLine(c3.GetInfo());

                Printer p1 = new Printer();
                Printer p2 = new Printer("Canon PIXMA", 8000, "Canon", 12,
                    "Inkjet", 15, true, true, 0.5m, 80, 100);
                Printer p3 = new Printer(p2);
                p3.ModelName = "Canon PIXMA (копія)";

                Print("\n--- Принтер (без параметрів) ---");
                p1.DisplayInfo(); w.WriteLine(p1.GetInfo());

                Print("\n--- Принтер (з параметрами) ---");
                p2.DisplayInfo(); w.WriteLine(p2.GetInfo());

                Print("\n--- Принтер (копія) ---");
                p3.DisplayInfo(); w.WriteLine(p3.GetInfo());


                Section("2. ПЕРЕВАНТАЖЕННЯ МЕТОДІВ");

                Monitor mon = new Monitor("LG UltraGear 27GP950", 22000, "LG", 24,
                    27, "4K", "IPS", 144, 230, 250);

                Tablet tab = new Tablet("iPad Pro 12.9", 45000, "Apple", 12,
                    12.9, true, true, "5G", 0, 820, 900);

                tab.UpdateRatingByReviews();

                Print("--- DisplayInfo ---");
                c2.DisplayInfo(); w.WriteLine(c2.GetInfo());
                mon.DisplayInfo(); w.WriteLine(mon.GetInfo());
                tab.DisplayInfo(); w.WriteLine(tab.GetInfo());
                p2.DisplayInfo(); w.WriteLine(p2.GetInfo());

                Print("\n--- Ціна з брендом ---");
                Print($"Комп'ютер: {c2.CalculatePriceWithBrandFactor():C}");
                Print($"Монітор:   {mon.CalculatePriceWithBrandFactor():C}");
                Print($"Планшет:   {tab.CalculatePriceWithBrandFactor():C}");
                Print($"Принтер:   {p2.CalculatePriceWithBrandFactor():C}");

                Print("\n--- Ціна з характеристиками ---");
                Print($"Комп'ютер: {c2.CalculatePriceWithConfiguration():C}");
                Print($"Монітор:   {mon.CalculatePriceWithSizeAndFeatures():C}");
                Print($"Планшет:   {tab.CalculatePriceWithFeatures():C}");
                Print($"Принтер:   {p2.CalculatePriceWithFeatures():C}");

                Print("\n--- Вартість експлуатації ---");
                Print($"Комп'ютер: {c2.CalculateOperatingCost(3, 2000, 1500):C}");
                Print($"Монітор:   {mon.CalculateOperatingCost(3, 500, 800):C}");
                Print($"Планшет:   {tab.CalculateOperatingCost(3, 1000, 300):C}");
                Print($"Принтер:   {p2.CalculateOperatingCost(3, 1000, 600):C}");

                Print("\n--- Користь і шкода ---");
                Print($"[Комп'ютер]\n{c2.CalculateBenefitAndHarm(8)}");
                Print($"[Монітор]\n{mon.CalculateBenefitAndHarm(8)}");
                Print($"[Планшет]\n{tab.CalculateBenefitAndHarm(8)}");
                Print($"[Принтер]\n{p2.CalculateBenefitAndHarm(8)}");


                Section("3. БІНАРНІ ОПЕРАТОРИ");

                Computer cA = new Computer("Dell XPS", 40000, "Dell", 24, 16, 512, "i7", 200, 250);
                Computer cB = new Computer("HP Envy", 35000, "HP", 12, 8, 256, "i5", 180, 200);

                Computer cPlus = cA + 5000m;
                Computer cMinus = cA - 10000m;

                Print($"cA: {cA.Price:C}");
                Print($"cB: {cB.Price:C}");
                Print($"cA + 5000 = {cPlus.Price:C}");
                Print($"cA - 10000 = {cMinus.Price:C}");
                Print($"cA > cB: {cA > cB}");
                Print($"cA == cB: {cA == cB}");


                Section("4. УНАРНІ ОПЕРАТОРИ");

                Print($"Рейтинг до: {c2.Rating}%");
                c2++;
                Print($"Після ++: {c2.Rating}%");
                c2--;
                Print($"Після --: {c2.Rating}%");

                Computer cNeg = -c2;
                Print($"Інверсія: {cNeg.Rating}%");


                Section("5. ІНДЕКСАТОР");

                ComputerCollection collection = new ComputerCollection(5);

                collection[0] = new Computer("MacBook Air", 52000, "Apple", 12, 16, 512, "M3", 400, 450);
                collection[1] = new Computer("Dell XPS 15", 48000, "Dell", 24, 32, 1024, "i9", 310, 350);
                collection[2] = new Computer("Lenovo ThinkPad", 36000, "Lenovo", 24, 16, 512, "i7", 260, 300);

                Print("--- Всі ---");
                collection.DisplayAll();

                Print("\n--- [1] ---");
                collection[1].DisplayInfo();

                Print("\n--- Оновлення ---");
                collection[0].UpdateReviews(440, 450);

                Section("ЗАВЕРШЕНО");
                Print("Результати збережено у файл");
            }

            Console.ReadKey();
        }
    }
}
