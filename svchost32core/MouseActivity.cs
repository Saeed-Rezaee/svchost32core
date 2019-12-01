using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace svchost32core
{
    public class MouseActivity
    {
        public static void MoveCursorPosition(int x, int y)
        {
            // Set the cursor position
            System.Windows.Forms.Cursor.Position = new Point(x, y);

            DoClickMouse(0x2); // Left mouse button down
            DoClickMouse(0x4); // Left mouse button up
        }
        public static void ClickSomePoint(int x, int y)
        {
            // Set the cursor position
            System.Windows.Forms.Cursor.Position = new Point(x, y);

            DoClickMouse(0x2); // Left mouse button down
            DoClickMouse(0x4); // Left mouse button up
        }

        static void DoClickMouse(int mouseButton)
        {
            var input = new INPUT()
            {
                dwType = 0, // Mouse input
                mi = new MOUSEINPUT() { dwFlags = mouseButton }
            };

            if (SendInput(1, ref input, Marshal.SizeOf(input)) == 0)
            {
                throw new Exception();
            }
        }

        #region Mouse Click
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            int dx;
            int dy;
            int mouseData;
            public int dwFlags;
            int time;
            IntPtr dwExtraInfo;
        }
        struct INPUT
        {
            public uint dwType;
            public MOUSEINPUT mi;
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint cInputs, ref INPUT input, int size);
        #endregion

        #region Mouse Move
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        public enum MouseFlags
        {
            MOUSEEVENTF_ABSOLUTE = 0x8000,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100
        }

        public enum DataFlags
        {
            XBUTTON1 = 0x0001,
            XBUTTON2 = 0x0002
        }
        #endregion
    }
}
