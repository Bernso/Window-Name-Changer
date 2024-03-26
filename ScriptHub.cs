using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Code_Executor_With_UI
{
    public partial class ScriptHub : Form
    {
        ExploitAPI module = new ExploitAPI(); 
        public ScriptHub()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wb = new WebClient();
            string Script = wb.DownloadString("https://pastebin.com/raw/fzQgv20F");
            module.SendScript(Script);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
