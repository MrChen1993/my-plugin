using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 测试控件
{
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - 250;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - 250;
            Point p = new Point(x, y);
            this.PointToScreen(p);
            this.Location = p;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form_closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                this.WindowState = FormWindowState.Maximized;
                this.notifyIcon1.Visible = true;
                this.Hide();
                return;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.notifyIcon1.Visible = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                this.notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.label2.Text = Convert.ToString(e.KeyValue);
        }

        const int WM_COPYDATA = 0x004A;
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT cds = new COPYDATASTRUCT();
                    Type t = cds.GetType();
                    cds = (COPYDATASTRUCT)m.GetLParam(t);
                    string strResult = cds.dwData.ToString() + ":" + cds.lpData;
                    this.label2.Text = strResult;
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
    }
}
