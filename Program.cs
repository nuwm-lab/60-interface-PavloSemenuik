using System;

namespace LabWork
{
    // Абстрактний клас: Загальне рівняння
    public abstract class Equation
    {
        public abstract void SetCoefficients(); // Введення коефіцієнтів
        public abstract void PrintEquation();  // Виведення рівняння
        public abstract bool IsRoot(double x); // Перевірка, чи є число коренем
        public abstract void FindRoots();      // Пошук коренів
    }

    // Інтерфейс для рівнянь
    public interface IEquation
    {
        void SetCoefficients();
        void PrintEquation();
        bool IsRoot(double x);
        void FindRoots();
    }

    // Квадратичне рівняння
    public class QuadraticEquation : Equation, IEquation
    {
        protected double coeffA, coeffB, coeffC;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для квадратичного рівняння:");
            coeffA = ReadCoefficient("coeffA (a): ");
            coeffB = ReadCoefficient("coeffB (b): ");
            coeffC = ReadCoefficient("coeffC (c): ");
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Квадратичне рівняння: {coeffA}x^2 + {coeffB}x + {coeffC} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = coeffA * x * x + coeffB * x + coeffC;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            double discriminant = coeffB * coeffB - 4 * coeffA * coeffC;

            if (discriminant > 0)
            {
                double root1 = (-coeffB + Math.Sqrt(discriminant)) / (2 * coeffA);
                double root2 = (-coeffB - Math.Sqrt(discriminant)) / (2 * coeffA);
                Console.WriteLine($"Два корені: x1 = {root1}, x2 = {root2}");
            }
            else if (Math.Abs(discriminant) < 1e-6)
            {
                double root = -coeffB / (2 * coeffA);
                Console.WriteLine($"Один корінь: x = {root}");
            }
            else
            {
                Console.WriteLine("Рівняння не має дійсних коренів.");
            }
        }

        private double ReadCoefficient(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Помилка: Введіть коректне число.");
            }
        }
    }

    // Кубічне рівняння
    public class CubicEquation : Equation, IEquation
    {
        private double coeffA, coeffB, coeffC, coeffD;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для кубічного рівняння:");
            coeffA = ReadCoefficient("coeffA (a): ");
            coeffB = ReadCoefficient("coeffB (b): ");
            coeffC = ReadCoefficient("coeffC (c): ");
            coeffD = ReadCoefficient("coeffD (d): ");
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Кубічне рівняння: {coeffA}x^3 + {coeffB}x^2 + {coeffC}x + {coeffD} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = coeffA * Math.Pow(x, 3) + coeffB * Math.Pow(x, 2) + coeffC * x + coeffD;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            // Простий чисельний метод (метод Ньютона) для знаходження одного кореня
            double x0 = 0; // Початкове значення
            double tolerance = 1e-6;
            int maxIterations = 100;
            int iteration = 0;

            while (iteration < maxIterations)
            {
                double fx = coeffA * Math.Pow(x0, 3) + coeffB * Math.Pow(x0, 2) + coeffC * x0 + coeffD;
                double fPrimeX = 3 * coeffA * Math.Pow(x0, 2) + 2 * coeffB * x0 + coeffC;

                if (Math.Abs(fPrimeX) < tolerance)
                {
                    Console.WriteLine("Похідна дорівнює нулю. Метод Ньютона не спрацював.");
                    return;
                }

                double x1 = x0 - fx / fPrimeX;

                if (Math.Abs(x1 - x0) < tolerance)
                {
                    Console.WriteLine($"Знайдено корінь: x = {x1}");
                    return;
                }

                x0 = x1;
                iteration++;
            }

            Console.WriteLine("Не вдалося знайти корінь за допомогою методу Ньютона.");
        }

        private double ReadCoefficient(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Помилка: Введіть коректне число.");
            }
        }
    }

    // Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            Equation equation = SelectEquation();

            if (equation == null)
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            equation.SetCoefficients();
            equation.PrintEquation();
            equation.FindRoots();

            Console.Write("Перевірте чи є число коренем. Введіть x: ");
            if (double.TryParse(Console.ReadLine(), out double x))
            {
                Console.WriteLine(equation.IsRoot(x)
                    ? $"Число {x} є коренем рівняння."
                    : $"Число {x} не є коренем рівняння.");
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }

        private static Equation SelectEquation()
        {
            Console.WriteLine("Виберіть тип рівняння:");
            Console.WriteLine("1 - Квадратичне рівняння");
            Console.WriteLine("2 - Кубічне рівняння");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            return choice switch
            {
                "1" => new QuadraticEquation(),
                "2" => new CubicEquation(),
                _ => null
            };
        }
    }
}
