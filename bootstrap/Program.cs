using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace console
{
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }
    class Program
    {

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        static void Main(string[] args)
        {
            Process[] arrPro = Process.GetProcessesByName("测试控件");
            Process pro = null;
            if (arrPro.Length == 0)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                pro = new Process();
                info.FileName = "\\测试控件.ext";
                pro.StartInfo = info;
            }
            else
            {
                pro = arrPro[0];
            }
            trySendMessage("你好啊", pro);
        }

        private static void trySendMessage(string message, Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            byte[] sarr = System.Text.Encoding.Default.GetBytes(message);
            int len = sarr.Length;
            COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)Convert.ToInt16(123);//可以是任意值
            cds.cbData = len + 1;//指定lpData内存区域的字节数
            cds.lpData = message;//发送给目标窗口所在进程的数据
            SendMessage(hWnd, 0x004A, 0, ref cds);
        }

        private static string Read()
        {
            var stdin = Console.OpenStandardInput();
            int length = 0;
            byte[] bytes = new byte[4];
            length = System.BitConverter.ToInt32(bytes, 0);

            string input = "";
            for (int i = 0; i < length; i++)
            {
                input += (char)stdin.ReadByte();
            }
            return input;
        }

        private static void Write(string message)
        {
            var stdout = Console.OpenStandardOutput();
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
            stdout.Write(bytes, 0, bytes.Length);
            stdout.Flush();
        }
    }
}
