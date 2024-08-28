using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace lb4_spz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите ряд:");
                string input = Console.ReadLine();

                // Создание MemoryMappedFile
                using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("mmfString", 1000))
                {
                    
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(input);
                    }

                    // Создание нового процесса для запуска второй программы
                    using (Process procB = new Process())
                    {
                        procB.StartInfo.FileName = @"C:\\Users\\mrfun\\source\\repos\\c#\\lb4_1_spz\\lb4_1_spz\\bin\\Debug\\lb4_1_spz.exe";
                        procB.StartInfo.Arguments = "mmfString"; 
                        procB.StartInfo.UseShellExecute = false;
                        procB.StartInfo.RedirectStandardOutput = true;
                        procB.StartInfo.CreateNoWindow = true;

                        procB.Start();

                        using (StreamReader reader = procB.StandardOutput)
                        {
                            string output = reader.ReadToEnd();
                            Console.WriteLine(output);
                        }

                        Console.WriteLine("Нажмите любую клавишу для завершения...");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при запуске процесса: " + ex.Message);
            }
        }
    }
}
