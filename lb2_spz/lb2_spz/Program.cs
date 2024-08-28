using System;
using System.Diagnostics;
using System.Threading;

namespace lb2_spz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Файлы для редактора
            string[] files = { "file1.txt", "file2.txt", "file3.txt", "file4.txt", "file5.txt" };

            //Запуск процесов
            LaunchProcesses("mspaint", 3, files);
            LaunchProcesses("notepad", 5, files);
            LaunchProcesses("winword", 2, files);
            LaunchProcesses("excel", 1, files);

            Console.WriteLine("Все дочерние процессы запущены. Для завершения нажмите любую клавишу.");
            Console.ReadKey();
        }
        static void LaunchProcesses(string program, int count, string[] files)
        {
            for (int i = 0; i < count; i++)
            {
                // Новый процес
                Process process = new Process();

                // Название программы
                process.StartInfo.FileName = program;

                // Установка аргументов командной строки (файлов для редактирования)
                if (files.Length > i)
                {
                    process.StartInfo.Arguments = files[i];
                }
                else
                {
                    Console.WriteLine("Недостаточно файлов для запуска всех процессов.");
                    break;
                }

                process.Start();

                process.WaitForExit();
    
                Console.WriteLine($"Закрыто {program} - файл {files[i]} отредактированный.");
            }
        }
    }
}
