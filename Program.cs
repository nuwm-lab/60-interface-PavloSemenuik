using System;

namespace LabWork
{
    // Абстрактний клас: Загальне рівняння
    public abstract class Equation
    {
        public abstract void SetCoefficients(); // Абстрактний метод для введення коефіцієнтів
        public abstract void PrintEquation();  // Абстрактний метод для виведення рівняння
        public abstract bool IsRoot(double x); // Абстрактний метод для перевірки кореня
        public abstract void FindRoots();      // Абстрактний метод для пошуку коренів
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
        protected double b2, b1, b0;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для квадратичного рівняння:");
            Console.Write("b2: ");
            b2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b1: ");
            b1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b0: ");
            b0 = Convert.ToDouble(Console.ReadLine());
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Квадратичне рівняння: {b2}x^2 + {b1}x + {b0} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = b2 * x * x + b1 * x + b0;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            double discriminant = b1 * b1 - 4 * b2 * b0;
            if (discriminant > 0)
            {
                double root1 = (-b1 + Math.Sqrt(discriminant)) / (2 * b2);
                double root2 = (-b1 - Math.Sqrt(discriminant)) / (2 * b2);
                Console.WriteLine($"Два корені: x1 = {root1}, x2 = {root2}");
            }
            else if (Math.Abs(discriminant) < 1e-6)
            {
                double root = -b1 / (2 * b2);
                Console.WriteLine($"Один корінь: x = {root}");
            }
            else
            {
                Console.WriteLine("Рівняння не має дійсних коренів.");
            }
        }
    }

    // Кубічне рівняння
    public class CubicEquation : Equation, IEquation
    {
        private double a3, a2, a1, a0;

        public override void SetCoefficients()
        {
            Console.WriteLine("Введіть коефіцієнти для кубічного рівняння:");
            Console.Write("a3: ");
            a3 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a2: ");
            a2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a1: ");
            a1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a0: ");
            a0 = Convert.ToDouble(Console.ReadLine());
        }

        public override void PrintEquation()
        {
            Console.WriteLine($"Кубічне рівняння: {a3}x^3 + {a2}x^2 + {a1}x + {a0} = 0");
        }

        public override bool IsRoot(double x)
        {
            double result = a3 * x * x * x + a2 * x * x + a1 * x + a0;
            return Math.Abs(result) < 1e-6;
        }

        public override void FindRoots()
        {
            Console.WriteLine("Пошук коренів для кубічного рівняння потребує спеціального алгоритму.");
        }
    }

    // Головна програма
    class Program
    {
        static void Main(string[] args)
        {
            Equation equation;

            Console.WriteLine("Виберіть тип рівняння:");
            Console.WriteLine("1 - Квадратичне рівняння");
            Console.WriteLine("2 - Кубічне рівняння");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                equation = new QuadraticEquation();
            }
            else if (choice == "2")
            {
                equation = new CubicEquation();
            }
            else
            {
                Console.WriteLine("Невірний вибір!");
                return;
            }

            equation.SetCoefficients();
            equation.PrintEquation();
            equation.FindRoots();

            Console.Write("Перевірте чи є число коренем. Введіть x: ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(equation.IsRoot(x)
                ? $"Число {x} є коренем рівняння."
                : $"Число {x} не є коренем рівняння.");
        }
    }
}
