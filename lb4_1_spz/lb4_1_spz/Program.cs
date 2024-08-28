using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;

namespace lb4_1_spz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputName = args.Length > 0 ? args[0] : "";

                // Чтение данных из MemoryMappedFile
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(inputName))
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        string input = reader.ReadString();

                        // Выполнение логики подсчета символов
                        int letterCount = 0;
                        int digitCount = 0;
                        int spaceCount = 0;

                        foreach (char c in input)
                        {
                            if (char.IsLetter(c))
                            {
                                letterCount++;
                            }
                            else if (char.IsDigit(c))
                            {
                                digitCount++;
                            }
                            else if (char.IsWhiteSpace(c))
                            {
                                spaceCount++;
                            }
                        }

                        // Запись результатов в стандартный вывод
                        Console.WriteLine($"Количество букв: {letterCount}");
                        Console.WriteLine($"Количество чисел: {digitCount}");
                        Console.WriteLine($"Количество пробелов: {spaceCount}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обработке данных: " + ex.Message);
            }
        }
    }
}
