using System;
using System.Collections.Generic;

namespace Lab1
{
    
    // БАЗОВИЙ КЛАС
    // -------------------------------------------------------------------------
    public abstract class Engine
    {
      
        public double Power { get; set; }
        public string Name { get; set; }

        // Конструктор за замовчуванням
        public Engine()
        {
            Name = "Невідомий двигун";
            Power = 0;
        }

        // Параметризований конструктор
        public Engine(string name, double power)
        {
            Name = name;
            Power = power;
        }

        // Абстрактний метод (інтерфейс для похідних класів) - ПІЗНЄ ЗВ'ЯЗУВАННЯ
        public abstract void ShowCharacteristics();

        // Звичайний метод - РАННЄ ЗВ'ЯЗУВАННЯ
        // Цей метод буде викликатися для змінних типу Engine, навіть якщо об'єкт є похідним
        public void GetBasicDescription()
        {
            Console.WriteLine($"[Early Binding] Це просто базовий клас Двигун: {Name}.");
        }

        // Перевизначення Equals для порівняння значень
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Engine other = (Engine)obj;
            return this.Name == other.Name && this.Power == other.Power;
        }

        // Бажано також перевизначати GetHashCode при перевизначенні Equals
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Power);
        }
    }

   
    // ДВИГУН ВНУТРІШНЬОГО ЗГОРЯННЯ (ICE)
    // -------------------------------------------------------------------------
    public class InternalCombustionEngine : Engine
    {
        public double Volume { get; set; }           // Об'єм (л)
        public double FuelConsumption { get; set; }  // Витрата (л/100км)

        // Конструктор за замовчуванням
        public InternalCombustionEngine() : base() { }

        // Параметризований конструктор
        public InternalCombustionEngine(string name, double hp, double volume, double consumption)
            : base(name, hp)
        {
            Volume = volume;
            FuelConsumption = consumption;
        }

        // Реалізація поліморфного методу (Late Binding)
        public override void ShowCharacteristics()
        {
            Console.WriteLine($"--- {Name} (ДВЗ) ---");
            Console.WriteLine($"Робочий об'єм: {Volume} л");
            Console.WriteLine($"Потужність: {Power} к.с.");
            Console.WriteLine($"Витрата пального: {FuelConsumption} л/100 км");
        }

        // Метод, що приховує базовий (для демонстрації різниці зв'язування),
        // але при виклику через reference Engine він не спрацює.
        public new void GetBasicDescription()
        {
            Console.WriteLine($"[Shadowing] Це конкретний ДВЗ: {Name}");
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            InternalCombustionEngine other = (InternalCombustionEngine)obj;
            return this.Volume == other.Volume && this.FuelConsumption == other.FuelConsumption;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Volume, FuelConsumption);
    }

   
    // ЕЛЕКТРОДВИГУН
    // -------------------------------------------------------------------------
    public class ElectricEngine : Engine
    {
        public double Voltage { get; set; } // Напруга (В)
        public int Phases { get; set; }     // Кількість фаз

        public ElectricEngine() : base() { }

        public ElectricEngine(string name, double kw, double voltage, int phases)
            : base(name, kw)
        {
            Voltage = voltage;
            Phases = phases;
        }

        public override void ShowCharacteristics()
        {
            Console.WriteLine($"--- {Name} (Електро) ---");
            Console.WriteLine($"Потужність: {Power} кВт");
            Console.WriteLine($"Напруга: {Voltage} В");
            Console.WriteLine($"Кількість фаз: {Phases}");
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            ElectricEngine other = (ElectricEngine)obj;
            return this.Voltage == other.Voltage && this.Phases == other.Phases;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Voltage, Phases);
    }

    // ВІЧНИЙ ДВИГУН (Perpetuum Mobile)
    // -------------------------------------------------------------------------
    public class PerpetuumMobile : Engine
    {
        public string MagicSource { get; set; }

        public PerpetuumMobile() : base()
        {
            MagicSource = "Магія";
        }

        public PerpetuumMobile(string name) : base(name, double.PositiveInfinity)
        {
            MagicSource = "Космічна енергія";
        }

        public override void ShowCharacteristics()
        {
            Console.WriteLine($"--- {Name} ---");
            Console.WriteLine("Статус: ПОМИЛКА 404.");
            Console.WriteLine("Причина: Закони термодинаміки забороняють існування цього об'єкта.");
            Console.WriteLine($"Джерело енергії: {MagicSource} (не підтверджено наукою).");
        }

        // Equals + перевірка типу
        public override bool Equals(object obj)
        {
            // Спроба перетворити прийшовший об'єкт у PerpetuumMobile
            PerpetuumMobile other = obj as PerpetuumMobile;

            // Якщо перетворення не вдалося (прийшло щось ліве або null) - повертаємо false
            if (other == null)
            {
                return false;
            }


            //   порівнюємо всі поля
            return this.Name == other.Name &&
                   this.Power == other.Power &&
                   this.MagicSource == other.MagicSource;
        }
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), MagicSource);
    }

    // MAIN
    // -------------------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення списку елементів базового класу
            List<Engine> engines = new List<Engine>();

            // Заповнення посиланнями на похідні класи
            engines.Add(new InternalCombustionEngine("BMW M5 Engine", 600, 4.4, 15.5));
            engines.Add(new ElectricEngine("Tesla Model S Motor", 750, 400, 3));
            engines.Add(new PerpetuumMobile("Da Vinci Wheel"));

            //  ще такий самий двигун для тесту Equals
            engines.Add(new ElectricEngine("Tesla Model S Motor", 750, 400, 3));

            Console.WriteLine("=== Демонстрація поліморфізму (Пізнє зв'язування) ===");
            Console.WriteLine("Викликаємо метод ShowCharacteristics() з масиву Engine[]:\n");

            foreach (var engine in engines)
            {
                // ПІЗНЄ зв'язування
       
                engine.ShowCharacteristics();
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("=== Демонстрація різниці: Раннє vs Пізнє зв'язування ===");

            Engine myEngine = new InternalCombustionEngine("Test ICE", 100, 1.6, 7.0);

            // Пізнє (Virtual/Override): Викликається метод КЛАСУ-СПАДКОЄМЦЯ (InternalCombustionEngine)
            Console.Write("Late Binding (ShowCharacteristics): ");
            // Це виведе повну інфу, бо метод віртуальний
            // (ми просто викличемо його, щоб показати, що він знаходить реалізацію)
            Console.WriteLine("Викликається реалізація Derived класу.");

            // Раннє (Non-virtual): Викликається метод БАЗОВОГО ТИПУ посилання (Engine)
            Console.Write("Early Binding (GetBasicDescription): ");
            myEngine.GetBasicDescription();

            Console.WriteLine("\nПояснення: Хоча об'єкт є 'InternalCombustionEngine', метод GetBasicDescription \n" +
                              "не є віртуальним, тому компілятор 'пришив' виклик до типу посилання 'Engine'.");

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("=== Демонстрація методу Equals (порівняння значень) ===");

            // Беремо Теслу (індекс 1) і її копію (індекс 3)
            Engine engineA = engines[1];
            Engine engineB = engines[3]; // Та ж сама Тесла, але новий об'єкт (new)
            Engine engineC = engines[0]; // BMW

            Console.WriteLine($"Engine A: {engineA.Name}, Power: {engineA.Power}");
            Console.WriteLine($"Engine B: {engineB.Name}, Power: {engineB.Power}");

            // Порівняння посилань
            Console.WriteLine($"ReferenceEquals (посилання): {ReferenceEquals(engineA, engineB)} (False, бо це різні об'єкти в пам'яті)");

            // Порівняння значень через наш override Equals
            Console.WriteLine($"Equals (значення): {engineA.Equals(engineB)} (True, бо поля однакові)");

            // Порівняння різних об'єктів
            Console.WriteLine($"Equals (Tesla vs BMW): {engineA.Equals(engineC)}");

            Console.ReadLine();
        }
    }
}