using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Reminder
{
    class KeyboardBlocker
    {
        public static bool off()
        {
            if (IsAdministrator())
            {
                BlockInput(true);//锁定鼠标及键盘
                return true;
            }
            else
                return false;
        }
        public static bool on()
        {
            if (IsAdministrator())
            {
                BlockInput(false);//解除键盘鼠标锁定
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 是否是管理员权限
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        [DllImport("user32.dll")]
        static extern void BlockInput(bool Block);

    }
}
