using E_diary.Администратор;
using E_diary.Вход;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_diary
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new АвторизацияForm());
            //_4П9и3П11Расписание АвторизацияForm Administrator_Forms
        }
    }
}
