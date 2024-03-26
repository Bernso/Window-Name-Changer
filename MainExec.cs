using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Code_Executor_With_UI
{
    public partial class MainExec : Form
    {
        ExploitAPI module = new ExploitAPI();
        private IntPtr targetWindowHandle;

        public MainExec()
        {
            InitializeComponent();
        }

        Point lastPoint;

        private void button1_Click_1(object sender, EventArgs e)
        {
            string targetProcessName = richTextBox1.Text.Trim();
            targetWindowHandle = FindWindowByProcessName(targetProcessName);

            if (targetWindowHandle != IntPtr.Zero)
            {
                SendMessageToTargetWindow();
            }
            else
            {
                MessageBox.Show("Failed to find a window associated with the specified process name.");
            }
        }

        private void SendMessageToTargetWindow()
        {
            if (targetWindowHandle != IntPtr.Zero)
            {
                try
                {
                    // Read Python code from fastColoredTextBox1
                    string pythonCode = fastColoredTextBox1.Text;

                    // Check if the Python code contains a specific command
                    if (pythonCode.Contains("quit()"))
                    {
                        // Send the Python code to the target window to execute
                        SendMessage(targetWindowHandle, WM_SETTEXT, IntPtr.Zero, Marshal.StringToHGlobalUni(pythonCode));
                    }
                    else
                    {
                        // If the command is not found, display an error message
                        MessageBox.Show("The Python code does not contain the 'quit()' command.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to send message to the target window: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to find a window associated with the specified process name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            module.LaunchExploit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            fastColoredTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                fastColoredTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // Add filter for text files
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(fastColoredTextBox1.Text);
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Open the ScriptHub form
            ScriptHub scriptHubForm = new ScriptHub();
            scriptHubForm.ShowDialog();
        }


        private void SelectWindowForm_WindowSelected(IntPtr windowHandle)
        {
            // Handle the selected window handle received from SelectWindow form
            targetWindowHandle = windowHandle;
            MessageBox.Show("Target window selected successfully!");
        }

        private IntPtr FindWindowByProcessName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                return processes[0].MainWindowHandle;
            }
            else
            {
                return IntPtr.Zero;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Add any necessary initialization code here
            MessageBox.Show("Form loaded successfully!"); // Example initialization code
        }


        // Add necessary Win32 API declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETTEXT = 0x000C;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
