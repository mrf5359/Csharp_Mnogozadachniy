using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb3_spz
{
    public partial class Form1 : Form
    {
        private readonly int maxThreads = 5; // Макс потоков
        private readonly int maxCount = 10; // Макс чисел

        private readonly SemaphoreSlim semaphore; // Семафор для ограничения потоків

        public Form1()
        {
            InitializeComponent();
            semaphore = new SemaphoreSlim(maxThreads);
            MouseClick += Form1_MouseClick; // Обробоотчик нажатия ЛКМ
        }

        private async void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                await semaphore.WaitAsync(); // Ожидание доступа

                Task.Run(() =>
                {
                    try
                    {
                        for (int i = 1; i <= maxCount; i++)
                        {
                            Thread.Sleep(100);

                            // Вывоод числа в позиции курсора
                            Invoke(new Action(() =>
                            {
                                Point cursorPos = PointToClient(Cursor.Position);
                                Cursor.Position = PointToScreen(cursorPos); // Обновление положения курсора
                                Cursor.Clip = new Rectangle(Location, Size); // Ограничение курсора по области окна 
                                Graphics g = CreateGraphics();
                                g.DrawString(i.ToString(), Font, Brushes.Black, cursorPos);
                                g.Dispose();
                            }));
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Очистка семофора
                    }
                });
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            MouseClick -= Form1_MouseClick; // Удаление обработчика ЛКМ
        }
    }
}
