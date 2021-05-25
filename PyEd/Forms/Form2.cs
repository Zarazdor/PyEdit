using System;
using System.Drawing;
using System.Windows.Forms;

namespace PyEd
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            pyPathBox.Text = Form1.PyPath;

            openFileDialog1.Title = "Select Path";
            openFileDialog1.Filter = "Executable Files(*.exe)|*.exe";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Form1.PyPath = pyPathBox.Text;
        }

        private void canselButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pyPathBox.Text = openFileDialog1.FileName;
            }
        }
    }
}
