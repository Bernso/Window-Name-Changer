using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Code_Executor_With_UI
{
    public partial class SelectWindow : Form
    {
        // Define the event
        public event Action<IntPtr> WindowSelected;

        public SelectWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the window handle from the TextBox
            string windowHandleText = textBox1.Text.Trim();
            IntPtr selectedWindowHandle;

            if (IntPtr.Size == 4)
            {
                // 32-bit platform
                if (int.TryParse(windowHandleText, out int intValue))
                {
                    selectedWindowHandle = new IntPtr(intValue);
                }
                else
                {
                    MessageBox.Show("Invalid window handle format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (IntPtr.Size == 8)
            {
                // 64-bit platform
                if (long.TryParse(windowHandleText, out long longValue))
                {
                    selectedWindowHandle = new IntPtr(longValue);
                }
                else
                {
                    MessageBox.Show("Invalid window handle format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unsupported platform.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Raise the event and pass the selected window handle
            WindowSelected?.Invoke(selectedWindowHandle);

            // Close the form
            this.Close();
        }

        // Event handler for TextBox.TextChanged
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add any logic here that you want to execute when the text in textBox1 changes
            // For example, you can validate the input or enable/disable the button based on the text length or content
        }
    }
}
