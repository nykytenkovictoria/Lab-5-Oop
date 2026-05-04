using System;
using System.IO;
using System.Text;

namespace task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TestVersion1();
            TestVersion2();
            TestVersion3();
            TestVersion4();

            Console.WriteLine("\nВсі версії виконано. Дані збережено у файли.");
            Console.ReadKey();
        }

        // ================= VERSION 1 =================
        static void TestVersion1()
        {
            string path = "computer_tech_data_v1.txt";

            using (StreamWriter w = new StreamWriter(path, false, Encoding.UTF8))
            {
                Console.WriteLine("\n===== ВЕРСІЯ 1 =====");
                w.WriteLine("===== ВЕРСІЯ 1 =====");

                // ===== БАЗОВИЙ КЛАС =====
                Console.WriteLine("\n===== БАЗОВИЙ КЛАС =====");
                w.WriteLine("\n===== БАЗОВИЙ КЛАС =====");

                ComputerTechV1 a = new ComputerTechV1();
                a.DisplayInfo();
                w.WriteLine(a.GetInfo());

                ComputerTechV1 b = new ComputerTechV1("Base Model", 15000, "Apple", 6);
                b.DisplayInfo();
                w.WriteLine(b.GetInfo());

                a.ModelName = "Custom Model";
                a.Price = 20000;

                w.WriteLine($"\nОновлена модель: {a.ModelName}");
                w.WriteLine($"Оновлена ціна: {a.Price}");
                Console.WriteLine($"\nОновлена модель: {a.ModelName}");
                Console.WriteLine($"Оновлена ціна: {a.Price}");

                decimal brandPrice = b.CalculatePriceWithBrandFactor();
                Console.WriteLine($"Ціна з бренд-фактором: {brandPrice}");
                w.WriteLine($"Ціна з бренд-фактором: {brandPrice}");

                // ===== КОМП'ЮТЕР =====
                Console.WriteLine("\n\n===== КОМП'ЮТЕР =====");
                w.WriteLine("\n\n===== КОМП'ЮТЕР =====");

                ComputerV1 c = new ComputerV1("MacBook Pro", 65000, "Apple", 12,
                    32, 1024, "M2 Pro", 450, 500);

                c.DisplayInfo();
                w.WriteLine(c.GetInfo());

                decimal configPrice = c.CalculatePriceWithConfiguration();
                int rating = c.CalculateRating();

                Console.WriteLine($"Ціна з конфігурацією: {configPrice}");
                Console.WriteLine($"Рейтинг: {rating}%");

                w.WriteLine($"Ціна з конфігурацією: {configPrice}");
                w.WriteLine($"Рейтинг: {rating}%");

                c.UpdateReviews(480, 500);
                int newRating = c.CalculateRating();

                Console.WriteLine($"Новий рейтинг: {newRating}%");
                w.WriteLine($"Новий рейтинг: {newRating}%");

                // ===== МОНИТОР =====
                w.WriteLine("\n\n===== МОНИТОР =====");
                Console.WriteLine("\n\n===== МОНИТОР =====");

                MonitorV1 m = new MonitorV1("LG UltraGear", 18000, "LG", 24,
                    27, "4K", "IPS", 144, 230, 250);

                m.DisplayInfo();
                w.WriteLine(m.GetInfo());

                decimal mPrice = m.CalculatePriceWithSizeAndFeatures();
                int mRating = m.CalculatePopularityRating();

                Console.WriteLine($"Ціна: {mPrice}");
                Console.WriteLine($"Рейтинг: {mRating}%");

                w.WriteLine($"Ціна: {mPrice}");
                w.WriteLine($"Рейтинг: {mRating}%");

                // ===== ПЛАНШЕТ =====
                Console.WriteLine("\n\n===== ПЛАНШЕТ =====");
                w.WriteLine("\n\n===== ПЛАНШЕТ =====");

                TabletV1 t = new TabletV1("iPad Pro", 35000, "Apple", 12,
                    12.9, true, true, "5G", 85, 820, 900);

                t.DisplayInfo();
                w.WriteLine(t.GetInfo());

                decimal tPrice = t.CalculatePriceWithFeatures();
                t.UpdateRatingByReviews();

                int tRating = t.PopularityRating;
                int prediction = t.PredictPopularity(6);

                Console.WriteLine($"Ціна: {tPrice}");
                Console.WriteLine($"Рейтинг: {tRating}%");
                Console.WriteLine($"Прогноз: {prediction}%");

                w.WriteLine($"Ціна: {tPrice}");
                w.WriteLine($"Рейтинг: {tRating}%");
                w.WriteLine($"Прогноз: {prediction}%");
            }
        }

        // ================= VERSION 2 =================
        static void TestVersion2()
        {
            string path = "computer_tech_data_v2.txt";

            using (StreamWriter w = new StreamWriter(path, false, Encoding.UTF8))
            {
                Console.WriteLine("\n===== ВЕРСІЯ 2 =====");
                w.WriteLine("===== ВЕРСІЯ 2 =====");

                ComputerTechV2 baseObj = new ComputerTechV2("Base", 10000, "Test", 6);
                baseObj.DisplayInfo();
                w.WriteLine(baseObj.GetInfo());

                IComputerTechV2[] arr =
                {
                    new ComputerV2("MacBook Pro", 65000, "Apple", 12, 32, 1024, "M2", 450, 500),
                    new MonitorV2("LG UltraGear", 18000, "LG", 24, 27, "4K", "IPS", 144, 230, 250),
                    new TabletV2("iPad Pro", 35000, "Apple", 12, 12.9, true, true, "5G", 85, 820, 900)
                };

                for (int i = 0; i < arr.Length; i++)
                {
                    w.WriteLine($"\n--- Пристрій {i + 1} ---");
                    Console.WriteLine($"\n--- Пристрій {i + 1} ---");

                    arr[i].DisplayInfo();
                    w.WriteLine(arr[i].GetInfo());

                    decimal price = arr[i].CalculatePriceWithBrandFactor();

                    Console.WriteLine($"Ціна: {price}");
                    w.WriteLine($"Ціна: {price}");
                }
            }
        }

        // ================= VERSION 3 =================
        static void TestVersion3()
        {
            string path = "computer_tech_data_v3.txt";

            using (StreamWriter w = new StreamWriter(path, false, Encoding.UTF8))
            {
                Console.WriteLine("\n===== ВЕРСІЯ 3 =====");
                w.WriteLine("===== ВЕРСІЯ 3 =====");

                ComputerTechV3[] arr =
                {
                    new ComputerV3("MacBook", 65000, "Apple", 12, 32, 1024, "M2", 450, 500),
                    new MonitorV3("LG", 18000, "LG", 24, 27, "4K", "IPS", 144, 230, 250),
                    new TabletV3("iPad", 35000, "Apple", 12, 12.9, true, true, "5G", 85, 820, 900)
                };

                foreach (var x in arr)
                {
                    w.WriteLine("\n----------------------");
                    Console.WriteLine("\n----------------------");
                    x.DisplayInfo();
                    w.WriteLine(x.GetInfo());

                    decimal finalPrice = x.CalculateFinalPrice();

                    Console.WriteLine($"Фінальна ціна: {finalPrice}");
                    w.WriteLine($"Фінальна ціна: {finalPrice}");
                }
            }
        }

        // ================= VERSION 4 =================
        static void TestVersion4()
        {
            string path = "computer_tech_data_v4.txt";

            using (StreamWriter w = new StreamWriter(path, false, Encoding.UTF8))
            {
                Console.WriteLine("\n===== ВЕРСІЯ 4 =====");
                w.WriteLine("===== ВЕРСІЯ 4 =====");

                ComputerV4[] list =
                {
                    new ComputerV4("MacBook", 50000, "Apple", 12, 16, 512, "M2", 13.3, 450, 500),
                    new ComputerV4("Dell", 35000, "Dell", 24, 32, 1024, "i7", 15.6, 230, 250),
                    new ComputerV4("HP", 18000, "HP", 12, 8, 256, "i5", 14.0, 180, 200)
                };

                // сортування за ціною
                Array.Sort(list);

                w.WriteLine("\n--- Сортування за ціною ---");
                Console.WriteLine("\n--- Сортування за ціною ---");
                foreach (var x in list)
                {
                    Console.WriteLine($"{x.ModelName} {x.Price}");
                    w.WriteLine($"{x.ModelName} {x.Price}");
                }

                // сортування за розміром
                Array.Sort(list, ComputerV4.SortByScreenSize());

                w.WriteLine("\n--- Сортування за розміром ---");
                Console.WriteLine("\n--- Сортування за розміром ---");
                foreach (var x in list)
                {
                    Console.WriteLine($"{x.ModelName} {x.ScreenSize}");
                    w.WriteLine($"{x.ModelName} {x.ScreenSize}");
                }

                var one = list[0];

                w.WriteLine("\n--- Тест одного об'єкта ---");
                Console.WriteLine("\n--- Тест одного об'єкта ---");
                one.DisplayInfo();
                w.WriteLine(one.GetInfo());

                decimal brandPrice = one.CalculatePriceWithBrandFactor();
                decimal configPrice = one.CalculatePriceWithConfiguration();
                int rating = one.CalculateRating();

                Console.WriteLine(brandPrice);
                Console.WriteLine(configPrice);
                Console.WriteLine(rating);

                w.WriteLine(brandPrice);
                w.WriteLine(configPrice);
                w.WriteLine(rating);

                one.UpdateReviews(490, 500);
                int newRating = one.CalculateRating();

                Console.WriteLine(newRating);
                w.WriteLine(newRating);
            }
        }
    }
}