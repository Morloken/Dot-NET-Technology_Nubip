using System;

namespace IntegralWithEvents
{
    delegate double FunctionDelegate(double x);
    delegate void EventHandler(char key, string name);

    class EventSystem
    {
        public event EventHandler KeyPressed;

        public void OnKeyPress(char key, string name)
        {
            KeyPressed?.Invoke(key, name);
        }
    }

    class Program
    {
        static double CalculateIntegral(FunctionDelegate func, double a, double b, int n)
        {
            double dx = (b - a) / n;
            double sum = 0;

            for (int i = 1; i <= n; i++)
            {
                double x = a + i * dx;
                sum += func(x) * dx;
            }

            return sum;
        }

        static double AnalyticalIntegral(double a, double b)
        {
            return (Math.Pow(b, 3) - Math.Pow(a, 3)) / 3.0;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double a = 1, b = 3;
            int rectangles = 1000;

            Console.WriteLine("Варіант 9:\n");

            FunctionDelegate func1 = x => 1 / Math.Exp(x);
            double int1 = CalculateIntegral(func1, a, b, rectangles);
            Console.WriteLine($"f(x) = 1/e^x: {int1:F6}");

            FunctionDelegate func2 = x => 1 / Math.Sqrt(x * x);
            double int2 = CalculateIntegral(func2, a, b, rectangles);
            Console.WriteLine($"f(x) = 1/√x²: {int2:F6}");

            FunctionDelegate func3 = x => (1 / x) * Math.Cos(x);
            double int3 = CalculateIntegral(func3, a, b, rectangles);
            Console.WriteLine($"f(x) = (1/x)*cos(x): {int3:F6}\n");

            double analytical = AnalyticalIntegral(a, b);
            double numerical = CalculateIntegral(x => x * x, a, b, rectangles);
            double error = Math.Abs(analytical - numerical);

            Console.WriteLine($"f(x) = x² на [{a}, {b}]:");
            Console.WriteLine($"Аналітично: {analytical:F6}");
            Console.WriteLine($"Чисельно:   {numerical:F6}");
            Console.WriteLine($"Похибка:    {error:F6}\n");

            EventSystem evt = new EventSystem();
            string name = "Микита";
            char key = char.ToLower(name[0]);

            evt.KeyPressed += (k, fullName) =>
            {
                Console.WriteLine($"Введена клавіша '{k}' для '{fullName}'");
            };

            Console.WriteLine($"Натисніть '{key}':");

            while (char.ToLower(Console.ReadKey(true).KeyChar) != key)
                Console.WriteLine($"Потрібна '{key}'");

            evt.OnKeyPress(key, name);

            Console.ReadKey();
        }
    }
}