using System;
using System.Threading;
using System.Windows.Forms;

namespace Reminder
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            bool createdNew;
            Mutex mutex = new Mutex(true, "SedentaryReminder_SingleInstance", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("程序运行中，见右下角系统托盘");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }
    }
}
