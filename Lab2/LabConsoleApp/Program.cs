using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LabConsoleApp
{
    class Program
    {
        //  перша функцію з  DLL
        [DllImport("MyLabLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FindMax(int[] arr, int size);

        // другу функцію
        [DllImport("MyLabLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int HexCharToDec(char hex);

        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("--- 1. Тестування функції FindMax (Випадкові числа) ---");

            
            int[] myArray = new int[10];
            Random rand = new Random();

            Console.Write("Згенерований масив: ");
            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = rand.Next(-50, 100); 
                Console.Write(myArray[i] + (i < myArray.Length - 1 ? ", " : ""));
            }
            Console.WriteLine();

            // Виклик з DLL
            int max = FindMax(myArray, myArray.Length);
            Console.WriteLine("Максимальний елемент: " + max);
            Console.WriteLine();


            Console.WriteLine("--- 2. Тестування функції HexCharToDec (Інтерактивний режим) ---");
            Console.WriteLine("Вводьте символи (0-9, A-F). Для виходу введіть 'Q'.\n");

            // цикл для зручної взаємодії
            while (true)
            {
                Console.Write("Введіть шістнадцятковий символ: ");
                string input = Console.ReadLine();

                // пусте введення
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                char c = input[0];

                // Перевірка на команду виходу
                if (char.ToUpper(c) == 'Q')
                {
                    Console.WriteLine("Завершення роботи...");
                    break;
                }

                // Виклик функції з DLL
                int result = HexCharToDec(c);

                if (result != -1)
                {
                    Console.WriteLine("-> Символ '" + c + "' в десятковій системі: " + result + "\n");
                }
                else
                {
                    Console.WriteLine("-> Помилка: '" + c + "' не є правильною шістнадцятковою цифрою!\n");
                }
            }
        }
    }
}