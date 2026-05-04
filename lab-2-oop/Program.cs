using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string choice;

            do
            {
                Console.WriteLine("\n=== МЕНЮ ПРОГРАМИ ===");
                Console.WriteLine("1. Завдання 1: Сортування масиву включенням");
                Console.WriteLine("2. Завдання 2: Пошук простих чисел (решето Ератосфена)");
                Console.WriteLine("3. Завдання 3: НСД для сусідніх елементів");
                Console.WriteLine("4. Завдання 4: Пошук центрованих трикутних чисел");
                Console.WriteLine("5. Завдання 5: Бінарний пошук елемента в масиві");
                Console.WriteLine("6. Завдання 6: Робота з матрицею телеканалів");
                Console.WriteLine("7. Завдання 7: Видалення рядків з матриці");
                Console.WriteLine("8. Завдання 8: Розв'язання нелінійного рівняння");
                Console.WriteLine("9. Завдання 9: Обробка рядка");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт меню: ");

                choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Task1_SortArray();
                        break;
                    case "2":
                        Task2_PrimeNumbers();
                        break;
                    case "3":
                        Task3_GCD();
                        break;
                    case "4":
                        Task4_CenteredTriangularNumbers();
                        break;
                    case "5":
                        Task5_BinarySearch();
                        break;
                    case "6":
                        Task6_TVChannelsMatrix();
                        break;
                    case "7":
                        Task7_DeleteRows();
                        break;
                    case "8":
                        Task8_BisectionMethod();
                        break;
                    case "9":
                        Task9_StringProcessing();
                        break;
                    case "0":
                        Console.WriteLine("Програма завершена.");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            } while (choice != "0");
        }

        /// <summary>
        /// Генерація випадкових значень для масиву
        /// </summary>
        /// <param name="Vals">посилання на масив</param>
        /// <param name="min">мінімальне значення</param>
        /// <param name="max">максимальне значення</param>
        static void ValsGenerator(int[] Vals, int min, int max)
        {
            // Random - клас для генерації псевдовипадкових чисел
            Random aRand = new Random();
            // заповнення масиву псевдовипадковими числами
            for (int i = 0; i < Vals.Length; i++)
                Vals[i] = aRand.Next(min, max + 1);
        }

        /// <summary>
        /// виведення масиву на консоль
        /// </summary>
        /// <param name="Vals">посилання на масив</param>
        static void PrintArray(int[] Vals)
        {
            for (int i = 0; i < Vals.Length; i++)
                Console.Write("\t" + Vals[i]);
            Console.WriteLine();
        }

        static void InputArrayParameters(out int size, out int minValue, out int maxValue)
        {
            Console.Write("Введіть кількість елементів масиву: ");
            size = int.Parse(Console.ReadLine());

            Console.Write("Введіть мінімальне значення: ");
            minValue = int.Parse(Console.ReadLine());

            Console.Write("Введіть максимальне значення: ");
            maxValue = int.Parse(Console.ReadLine());
        }

        static int CalculateGCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static bool IsCenteredTriangular(int number)
        {
            if (number < 1) return false;

            // Центровані трикутні числа: 1, 4, 10, 19, 31, 46, 64, 85, 109, ...
            // Формула: CTn = (3n² - 3n + 2)/2, де n = 1, 2, 3, ...

            // Розв'язуємо квадратне рівняння: 3n² - 3n + 2 - 2*number = 0
            // 3n² - 3n + (2 - 2*number) = 0

            double discriminant = 9 - 12 * (2 - 2 * number);

            if (discriminant < 0) return false;

            double sqrtDiscriminant = Math.Sqrt(discriminant);
            double n1 = (3 + sqrtDiscriminant) / 6;
            double n2 = (3 - sqrtDiscriminant) / 6;

            // Перевіряємо, чи є корінь цілим додатним числом
            return (Math.Abs(n1 - Math.Round(n1)) < 0.0001 && n1 > 0) ||
                   (Math.Abs(n2 - Math.Round(n2)) < 0.0001 && n2 > 0);
        }

        // Метод для завдання 1: Сортування масиву включенням
        static void Task1_SortArray()
        {
            Console.WriteLine("=== ЗАВДАННЯ 1: СОРТУВАННЯ ВКЛЮЧЕННЯМ ===");

            // Введення параметрів масиву з використанням int.Parse()
            InputArrayParameters(out int size, out int minValue, out int maxValue);

            // Створення та генерація масиву
            int[] array = new int[size];
            ValsGenerator(array, minValue, maxValue);

            // Виведення початкового масиву
            Console.WriteLine("\nПочатковий масив:");
            PrintArray(array);

            // Сортування включенням
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }

            // Виведення відсортованого масиву
            Console.WriteLine("\nВідсортований масив (за зростанням):");
            PrintArray(array);
        }

        static void Task2_PrimeNumbers()
        {
            Console.WriteLine("=== ЗАВДАННЯ 2: ПОШУК ПРОСТИХ ЧИСЕЛ (РЕШЕТО ЕРАТОСФЕНА) ===");

            // Введення параметрів масиву
            InputArrayParameters(out int size, out int minValue, out int maxValue);

            // Створення та генерація масиву
            int[] array = new int[size];
            ValsGenerator(array, minValue, maxValue);

            // Виведення згенерованого масиву
            Console.WriteLine("\nЗгенерований масив:");
            PrintArray(array);

            // Знаходимо максимальне значення в масиві для решета Ератосфена
            int maxArrayValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxArrayValue)
                    maxArrayValue = array[i];
            }

            // Решето Ератосфена для знаходження простих чисел до maxArrayValue
            bool[] isPrime = new bool[maxArrayValue + 1];

            // Спочатку вважаємо всі числа простими (крім 0 і 1)
            for (int i = 2; i <= maxArrayValue; i++)
                isPrime[i] = true;

            // Реалізація решета Ератосфена
            for (int i = 2; i * i <= maxArrayValue; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= maxArrayValue; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            // Пошук простих чисел у нашому масиві
            int[] primeNumbers = new int[array.Length];
            int primeCount = 0;

            for (int i = 0; i < array.Length; i++)
            {
                // Перевіряємо, чи є число простим (числа мають бути >= 2)
                if (array[i] >= 2 && isPrime[array[i]])
                {
                    primeNumbers[primeCount] = array[i];
                    primeCount++;
                }
            }

            // Виведення результатів
            if (primeCount > 0)
            {
                Console.WriteLine("\nПрості числа в масиві:");

                // Створюємо масив точної довжини для виведення
                int[] resultPrimes = new int[primeCount];
                for (int i = 0; i < primeCount; i++)
                {
                    resultPrimes[i] = primeNumbers[i];
                }

                PrintArray(resultPrimes);
            }
            else
            {
                Console.WriteLine("\nПрості числа не знайдені в масиві.");
            }
        }

        static void Task3_GCD()
        {
            Console.WriteLine("=== ЗАВДАННЯ 3: НАЙБІЛЬШИЙ СПІЛЬНИЙ ДІЛЬНИК ДЛЯ СУСІДНІХ ЕЛЕМЕНТІВ ===");

            // Введення параметрів масиву
            InputArrayParameters(out int size, out int minValue, out int maxValue);

            // Створення та генерація масиву
            int[] array = new int[size];
            ValsGenerator(array, minValue, maxValue);

            // Виведення згенерованого масиву
            Console.WriteLine("\nЗгенерований масив:");
            PrintArray(array);

            // Обчислення НСД для кожної пари сусідніх елементів
            int[] gcdArray = new int[size - 1]; // на один менше, ніж кількість елементів

            for (int i = 0; i < size - 1; i++)
            {
                gcdArray[i] = CalculateGCD(array[i], array[i + 1]);
            }

            // Виведення масиву НСД
            Console.WriteLine("\nМасив найбільших спільних дільників для сусідніх елементів:");
            PrintArray(gcdArray);
        }

        static void Task4_CenteredTriangularNumbers()
        {
            Console.WriteLine("=== ЗАВДАННЯ 4: ПОШУК ЦЕНТРОВАНИХ ТРИКУТНИХ ЧИСЕЛ ===");

            // Введення параметрів масиву
            InputArrayParameters(out int size, out int minValue, out int maxValue);

            // Створення та генерація масиву
            int[] array = new int[size];
            ValsGenerator(array, minValue, maxValue);

            // Виведення згенерованого масиву
            Console.WriteLine("\nЗгенерований масив:");
            PrintArray(array);

            // Пошук центрованих трикутних чисел
            Console.WriteLine("\nЦентровані трикутні числа в масиві:");
            bool found = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (IsCenteredTriangular(array[i]))
                {
                    Console.WriteLine($"Число {array[i]} є центрованим трикутним числом (індекс {i})");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Центровані трикутні числа не знайдені в масиві.");
            }
        }

        // Метод для завдання 5: Бінарний пошук елемента в масиві
        static void Task5_BinarySearch()
        {
            Console.WriteLine("=== ЗАВДАННЯ 5: БІНАРНИЙ ПОШУК ЕЛЕМЕНТА В МАСИВІ ===");

            // Введення параметрів масиву
            InputArrayParameters(out int size, out int minValue, out int maxValue);

            // Створення та генерація масиву
            int[] array = new int[size];
            ValsGenerator(array, minValue, maxValue);

            // Виведення згенерованого масиву
            Console.WriteLine("\nЗгенерований масив:");
            PrintArray(array);

            // Сортування масиву для бінарного пошуку
            Array.Sort(array);
            Console.WriteLine("\nВідсортований масив:");
            PrintArray(array);

            // Введення ключа пошуку
            Console.Write("\nВведiть ключ пошуку елемента в масивi: ");
            int key = int.Parse(Console.ReadLine());

            // Бінарний пошук
            bool sign = false; // пошук не здійснено
            int nom = -1;
            int begin = 0;
            int end = array.Length; // ліва і права границі масиву

            while (begin < end)
            {
                // реалізація алгоритму бінарного пошуку
                int c = begin + (end - begin) / 2; // середина діапазону пошуку

                if (key < array[c])
                    end = c; // зсув діапазону пошуку вліво
                else if (key > array[c])
                    begin = c + 1; // зсув діапазону пошуку вправо
                else
                {
                    sign = true;
                    nom = c;
                    break;
                }
            }

            if (sign == true)
            {
                Console.WriteLine($"\nЕлемент {key} знайдено з iндексом {nom}");

                // Пошук всіх входжень (ліворуч і праворуч від знайденого індексу)
                Console.WriteLine($"Всі входження елемента {key}:");

                // Пошук ліворуч
                int leftIndex = nom - 1;
                while (leftIndex >= 0 && array[leftIndex] == key)
                {
                    Console.Write($"\t{leftIndex}");
                    leftIndex--;
                }

                // Виведення початкового індексу
                Console.Write($"\t{nom}");

                // Пошук праворуч
                int rightIndex = nom + 1;
                while (rightIndex < array.Length && array[rightIndex] == key)
                {
                    Console.Write($"\t{rightIndex}");
                    rightIndex++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"\nЕлемент {key} не знайдено в масиві.");
            }
        }

        // Метод для завдання 6: Робота з матрицею телеканалів
        static void Task6_TVChannelsMatrix()
        {
            Console.WriteLine("=== ЗАВДАННЯ 6: МАТРИЦЯ ТЕЛЕКАНАЛІВ ===");

            // Введення параметрів матриці
            Console.Write("Введіть кількість телеканалів (рядків): ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Введіть кількість телепрограм (стовпців): ");
            int cols = int.Parse(Console.ReadLine());

            Console.Write("Введіть мінімальне значення кількості програм: ");
            int minValue = int.Parse(Console.ReadLine());

            Console.Write("Введіть максимальне значення кількості програм: ");
            int maxValue = int.Parse(Console.ReadLine());

            // Генерація матриці
            int[,] matrix = new int[rows, cols];
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(minValue, maxValue + 1);
                }
            }

            // Виведення матриці
            Console.WriteLine("\nМатриця телеканалів (рядки - телеканали, стовпці - програми):");
            PrintMatrix(matrix);

            // 1. Визначення телеканалу з найбільшою кількістю телепрограм
            int maxChannelIndex = 0;
            int maxChannelSum = 0;

            for (int i = 0; i < rows; i++)
            {
                int channelSum = 0;
                for (int j = 0; j < cols; j++)
                {
                    channelSum += matrix[i, j];
                }

                if (channelSum > maxChannelSum)
                {
                    maxChannelSum = channelSum;
                    maxChannelIndex = i;
                }
            }

            Console.WriteLine($"\nТелеканал з найбільшою кількістю телепрограм: канал {maxChannelIndex + 1}");
            Console.WriteLine($"Загальна кількість програм: {maxChannelSum}");

            // 2. Визначення телепрограми, яку транслюють усі телеканали
            Console.WriteLine("\nТелепрограми, які транслюють усі телеканали:");

            bool foundAnyProgram = false;
            for (int j = 0; j < cols; j++)
            {
                bool allChannelsHave = true;
                int programCount = matrix[0, j];

                for (int i = 1; i < rows; i++)
                {
                    if (matrix[i, j] == 0) // Якщо хоча б на одному каналі програма відсутня
                    {
                        allChannelsHave = false;
                        break;
                    }
                }

                if (allChannelsHave && programCount > 0)
                {
                    Console.WriteLine($"Програма {j + 1} (стовпець {j})");
                    foundAnyProgram = true;
                }
            }

            if (!foundAnyProgram)
            {
                Console.WriteLine("Немає програм, які транслюються на всіх каналах.");
            }

            // 3. Середня кількість телепрограм по всіх телеканалах
            int totalPrograms = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    totalPrograms += matrix[i, j];
                }
            }

            double averagePrograms = (double)totalPrograms / rows;
            Console.WriteLine($"\nСередня кількість телепрограм по всіх телеканалах: {averagePrograms:F2}");
        }

        /// <summary>
        /// Виведення матриці на консоль
        /// </summary>
        /// <param name="matrix">матриця для виведення</param>
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                Console.Write($"Канал {i + 1}:\t");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }


        // Метод для завдання 7: Видалення рядків з матриці
        static void Task7_DeleteRows()
        {
            Console.WriteLine("=== ЗАВДАННЯ 7: ВИДАЛЕННЯ РЯДКІВ З МАТРИЦІ ===");

            // Спочатку треба створити матрицю (використовуємо ту саму логіку, що в завданні 6)
            Console.Write("Введіть кількість телеканалів (рядків): ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Введіть кількість телепрограм (стовпців): ");
            int cols = int.Parse(Console.ReadLine());

            Console.Write("Введіть мінімальне значення кількості програм: ");
            int minValue = int.Parse(Console.ReadLine());

            Console.Write("Введіть максимальне значення кількості програм: ");
            int maxValue = int.Parse(Console.ReadLine());

            // Генерація матриці
            int[,] matrix = new int[rows, cols];
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(minValue, maxValue + 1);
                }
            }

            // Виведення початкової матриці
            Console.WriteLine("\nПочаткова матриця телеканалів:");
            PrintMatrix(matrix);

            // Введення номерів рядків для видалення
            Console.Write("\nВведіть кількість телеканалів для видалення: ");
            int deleteCount = int.Parse(Console.ReadLine());

            bool[] rowsToDelete = new bool[rows];
            for (int k = 0; k < deleteCount; k++)
            {
                Console.Write($"Введіть номер телеканалу для видалення (1-{rows}): ");
                int rowToDelete = int.Parse(Console.ReadLine()) - 1; // переводимо в індекс масиву

                if (rowToDelete >= 0 && rowToDelete < rows)
                {
                    rowsToDelete[rowToDelete] = true;
                }
                else
                {
                    Console.WriteLine("Невірний номер каналу!");
                    k--; // повторюємо введення
                }
            }

            // Підрахунок кількості рядків, що залишаються
            int remainingRows = 0;
            for (int i = 0; i < rows; i++)
            {
                if (!rowsToDelete[i])
                    remainingRows++;
            }

            // Створення нової матриці без видалених рядків
            int[,] newMatrix = new int[remainingRows, cols];
            int newRowIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                if (!rowsToDelete[i])
                {
                    for (int j = 0; j < cols; j++)
                    {
                        newMatrix[newRowIndex, j] = matrix[i, j];
                    }
                    newRowIndex++;
                }
            }

            // Виведення перетвореної матриці
            Console.WriteLine("\nПеретворена матриця (після видалення телеканалів):");

            if (remainingRows > 0)
            {
                PrintMatrix(newMatrix);

                // Додаткова інформація
                Console.WriteLine($"\nКількість видалених телеканалів: {deleteCount}");
                Console.WriteLine($"Кількість телеканалів, що залишились: {remainingRows}");
            }
            else
            {
                Console.WriteLine("Всі телеканали видалено. Матриця порожня.");
            }
        }

        // Метод для завдання 8: Розв'язання нелінійного рівняння методом бісекції
        static void Task8_BisectionMethod()
        {
            Console.WriteLine("=== ЗАВДАННЯ 8: РОЗВ'ЯЗАННЯ НЕЛІНІЙНОГО РІВНЯННЯ ===");
            Console.WriteLine("Рівняння: 3x - e^x = 0");

            // Введення даних
            Console.Write("Введіть ліву межу інтервалу (a): ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введіть праву межу інтервалу (b): ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Введіть точність: ");
            double eps = double.Parse(Console.ReadLine());

            // Знаходження кореня
            double root = FindRoot(a, b, eps);

            if (!double.IsNaN(root))
            {
                Console.WriteLine($"\nКорінь: x = {root:F6}");
                Console.WriteLine($"Перевірка: 3*{root:F6} - e^{root:F6} = {CheckRoot(root):F6}");
            }
        }

        // Функція f(x) = 3x - e^x
        static double F(double x)
        {
            return 3 * x - Math.Exp(x);
        }

        // Перевірка кореня
        static double CheckRoot(double x)
        {
            return 3 * x - Math.Exp(x);
        }

        // Метод бісекції
        static double FindRoot(double a, double b, double eps)
        {
            double fa = F(a);
            double fb = F(b);

            // Перевірка наявності кореня
            if (fa * fb > 0)
            {
                Console.WriteLine("На інтервалі немає кореня");
                return double.NaN;
            }

            double c;
            int iter = 0;

            do
            {
                c = (a + b) / 2;
                double fc = F(c);

                iter++;
                Console.WriteLine($"Ітер {iter}: a={a:F4}, b={b:F4}, c={c:F4}, F(c)={fc:F4}");

                if (Math.Abs(fc) < eps)
                    return c;

                if (fa * fc < 0)
                {
                    b = c;
                    fb = fc;
                }
                else
                {
                    a = c;
                    fa = fc;
                }

            } while (Math.Abs(b - a) > eps);

            return (a + b) / 2;
        }

        // Метод для завдання 9: Обробка рядка
        static void Task9_StringProcessing()
        {
            Console.WriteLine("=== ЗАВДАННЯ 9: ОБРОБКА РЯДКА ===");

            // Введення рядка
            Console.WriteLine("Введіть рядок символів (алфавітні, цифрові та розділові символи):");
            string input = Console.ReadLine();

            Console.WriteLine($"\nВведений рядок: {input}");

            // 1. Видалення всіх символів, які не є буквами
            string lettersOnly = "";
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    lettersOnly += c;
                }
            }

            Console.WriteLine($"\nРядок тільки з букв: {lettersOnly}");

            // 2. Введення букв для пошуку
            Console.Write("\nВведіть першу букву для пошуку: ");
            char firstLetter = Console.ReadLine()[0];

            Console.Write("Введіть останню букву для пошуку: ");
            char lastLetter = Console.ReadLine()[0];

            // Розбиваємо рядок на слова
            string[] words = input.Split(new char[] { ' ', ',', '.', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"\nСлова в рядку:");
            foreach (string word in words)
            {
                Console.Write($"'{word}' ");
            }
            Console.WriteLine();

            // Підрахунок слів, які починаються і закінчуються на задані букви
            int count = 0;
            foreach (string word in words)
            {
                if (word.Length > 0)
                {
                    char firstChar = word[0];
                    char lastChar = word[word.Length - 1];

                    if (char.ToLower(firstChar) == char.ToLower(firstLetter) &&
                        char.ToLower(lastChar) == char.ToLower(lastLetter))
                    {
                        count++;
                        Console.WriteLine($"Знайдено: '{word}' (починається на '{firstChar}', закінчується на '{lastChar}')");
                    }
                }
            }

            // Виведення результату
            Console.WriteLine($"\nКількість слів, які починаються на '{firstLetter}' і закінчуються на '{lastLetter}': {count}");
        }

        // Альтернативний метод з використанням StringBuilder
        static void Task9_StringProcessing_Alternative()
        {
            Console.WriteLine("=== ЗАВДАННЯ 9: ОБРОБКА РЯДКА ===");

            // Введення рядка
            Console.WriteLine("Введіть рядок символів (алфавітні, цифрові та розділові символи):");
            string input = Console.ReadLine();

            Console.WriteLine($"\nВведений рядок: {input}");

            // 1. Видалення всіх символів, які не є буквами (з використанням StringBuilder)
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                }
            }
            string lettersOnly = sb.ToString();

            Console.WriteLine($"\nРядок тільки з букв: {lettersOnly}");

            // 2. Введення букв для пошуку
            Console.Write("\nВведіть першу букву для пошуку: ");
            char firstLetter = Console.ReadLine()[0];

            Console.Write("Введіть останню букву для пошуку: ");
            char lastLetter = Console.ReadLine()[0];

            // Розбиваємо рядок на слова
            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\t' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // Підрахунок слів, які починаються і закінчуються на задані букви
            int count = 0;
            foreach (string word in words)
            {
                if (word.Length > 0)
                {
                    if (char.ToLower(word[0]) == char.ToLower(firstLetter) &&
                        char.ToLower(word[word.Length - 1]) == char.ToLower(lastLetter))
                    {
                        count++;
                    }
                }
            }

            // Виведення результату
            Console.WriteLine($"\nВсього слів у рядку: {words.Length}");
            Console.WriteLine($"Кількість слів, які починаються на '{firstLetter}' і закінчуються на '{lastLetter}': {count}");
        }
    }
}