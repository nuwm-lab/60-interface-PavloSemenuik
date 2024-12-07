using System;

namespace LabWork
{
    // Enum для вибору типу рівняння
    public enum EquationType
    {
        Quadratic,
        Cubic
    }

    // Абстрактний клас: Загальне рівняння
    public abstract class Equation
    {
        public abstract void SetCoefficients(); // Введення коефіцієнтів
        public abstract void PrintEquation();  // Виведення рівняння
        public abstract bool IsRoot(double x); // Перевірка, чи є число коренем
        public abstract void FindRoots();      // Пошук коренів

        // Метод для зчитування коефіцієнтів (спільний для всіх типів рівнянь)
        protected double ReadCoefficient(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    return double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: Введіть коректне число.");
                }
            }
        }
    }

    // Квадратичне рівняння
    public class QuadraticEquation : Equation
    {
        private double coefficientA, coefficientB, coefficientC;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для квадратичного рівняння:");
            coefficientA = ReadCoefficient("Коефіцієнт A (a): ");
            coefficientB = ReadCoefficient("Коефіцієнт B (b): ");
            coefficientC = ReadCoefficient("Коефіцієнт C (c): ");
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Квадратичне рівняння: {coefficientA}x^2 + {coefficientB}x + {coefficientC} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = coefficientA * x * x + coefficientB * x + coefficientC;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            double discriminant = coefficientB * coefficientB - 4 * coefficientA * coefficientC;

            if (discriminant > 0)
            {
                double root1 = (-coefficientB + Math.Sqrt(discriminant)) / (2 * coefficientA);
                double root2 = (-coefficientB - Math.Sqrt(discriminant)) / (2 * coefficientA);
                Console.WriteLine($"Два корені: x1 = {root1}, x2 = {root2}");
            }
            else if (Math.Abs(discriminant) < 1e-6)
            {
                double root = -coefficientB / (2 * coefficientA);
                Console.WriteLine($"Один корінь: x = {root}");
            }
            else
            {
                Console.WriteLine("Рівняння не має дійсних коренів.");
            }
        }
    }

    // Кубічне рівняння
    public class CubicEquation : Equation
    {
        private double coefficientA, coefficientB, coefficientC, coefficientD;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для кубічного рівняння:");
            coefficientA = ReadCoefficient("Коефіцієнт A (a): ");
            coefficientB = ReadCoefficient("Коефіцієнт B (b): ");
            coefficientC = ReadCoefficient("Коефіцієнт C (c): ");
            coefficientD = ReadCoefficient("Коефіцієнт D (d): ");
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Кубічне рівняння: {coefficientA}x^3 + {coefficientB}x^2 + {coefficientC}x + {coefficientD} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = coefficientA * Math.Pow(x, 3) + coefficientB * Math.Pow(x, 2) + coefficientC * x + coefficientD;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            Console.WriteLine("Знаходимо корені за формулою Кардано...");
            double delta0 = Math.Pow(coefficientB, 2) - 3 * coefficientA * coefficientC;
            double delta1 = 2 * Math.Pow(coefficientB, 3) - 9 * coefficientA * coefficientB * coefficientC + 27 * Math.Pow(coefficientA, 2) * coefficientD;

            double discriminant = Math.Pow(delta1, 2) - 4 * Math.Pow(delta0, 3);

            if (discriminant > 0)
            {
                Console.WriteLine("Одне дійсне рішення та два комплексні.");
                // Реалізувати пошук дійсного кореня
            }
            else if (Math.Abs(discriminant) < 1e-6)
            {
                Console.WriteLine("Усі корені реальні та принаймні два з них рівні.");
                // Реалізувати для кратного кореня
            }
            else
            {
                Console.WriteLine("Усі корені реальні.");
                // Реалізувати для трьох дійсних коренів
            }
        }
    }

    // Клас для взаємодії з користувачем
    public class UserInteraction
    {
        public static Equation SelectEquation()
        {
            while (true)
            {
                Console.WriteLine("Виберіть тип рівняння:");
                Console.WriteLine("1 - Квадратичне рівняння");
                Console.WriteLine("2 - Кубічне рівняння");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                if (choice == "1") return new QuadraticEquation();
                if (choice == "2") return new CubicEquation();

                Console.WriteLine("Помилка: Введіть 1 або 2.");
            }
        }
    }

    // Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Equation equation = UserInteraction.SelectEquation();
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
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
