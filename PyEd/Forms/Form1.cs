using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PyEd
{
    public partial class Form1 : Form
    {
        bool debc, ter = false;
        string Nowfile;
        public static string theme = "lg";
        public static string PyPath = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Programs\Python\Python39\python.exe";

        public Form1()
        {
            InitializeComponent();

            charss.Text = "Chars count: " + richTextBox1.Text.Length;
            strr.Text = "Strings count: " + richTextBox1.Lines.Length;

            createToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.N;

            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            selectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;

            startApplicationToolStripMenuItem.ShortcutKeys = Keys.F5;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e) //NoName file creation
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Opening
        {
            openFileDialog1.Filter = "Python file(*.py)|*.py|Text file(*.txt)|*.txt|All files(*.)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Nowfile = openFileDialog1.FileName;
                this.Text = "PyEdit: " + Nowfile;
                richTextBox1.LoadFile(Nowfile, (RichTextBoxStreamType)1);

                try
                {
                    if (Path.GetExtension(Nowfile) == ".py")
                    {
                        terminalToolStripMenuItem.Enabled = true;
                        applicationConsoleToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        terminalToolStripMenuItem.Enabled = false;
                        applicationConsoleToolStripMenuItem.Enabled = false;
                    }
                }
                catch
                { }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Saving
        {
            try
            {
                richTextBox1.SaveFile(Nowfile, (RichTextBoxStreamType)1);
            }
            catch
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) //Saving As
        {
            saveFileDialog1.Filter = "Python file(*.py)|*.py|Text file(*.txt)|*.txt|All files(*.)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Nowfile = saveFileDialog1.FileName;
                this.Text = "PyEdit: " + Nowfile;
                richTextBox1.SaveFile(Nowfile, (RichTextBoxStreamType)1);

                try
                {
                    if (Path.GetExtension(Nowfile) == ".py")
                    {
                        terminalToolStripMenuItem.Enabled = true;
                        applicationConsoleToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        terminalToolStripMenuItem.Enabled = false;
                        applicationConsoleToolStripMenuItem.Enabled = false;
                    }
                }
                catch
                { }
            }
        }

        private void terminalToolStripMenuItem_Click(object sender, EventArgs e) //Terminal opening
        {
            if (!ter)
            {
                applicationConsole.Visible = false;
                applicationConsoleToolStripMenuItem.Checked = false;
                debc = false;
                terminalConsole.Visible = true;
                terminalToolStripMenuItem.Checked = true;
                ter = true;
                consoleToolStripStatusLabel.Text = "Console: Terminal";
            }
            else
            {
                terminalConsole.Visible = false;
                terminalToolStripMenuItem.Checked = false;
                ter = false;
                consoleToolStripStatusLabel.Text = "Console: None";
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) //Undo
        {
            if (richTextBox1.CanUndo == true && richTextBox1.Focused)
            {
                richTextBox1.Undo();
                richTextBox1.ClearUndo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e) //Redo
        {
            if (richTextBox1.Focused)
            {
                richTextBox1.Redo();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) //Copy
        {
            if (richTextBox1.SelectionLength > 0 && richTextBox1.Focused)
            {
                richTextBox1.Copy();
            }
            if (terminalConsole.SelectionLength > 0 && terminalConsole.Focused)
            {
                terminalConsole.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) //Paste
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true && richTextBox1.Focused)
            {
                if (richTextBox1.SelectionLength > 0)
                {
                    richTextBox1.SelectionStart = richTextBox1.SelectionStart + richTextBox1.SelectionLength;
                    richTextBox1.Paste();
                }
                richTextBox1.Paste();
            }
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true && terminalConsole.Focused)
            {
                if (terminalConsole.SelectionLength > 0)
                {
                    terminalConsole.SelectionStart = terminalConsole.SelectionStart + terminalConsole.SelectionLength;
                    terminalConsole.Paste();
                }
                terminalConsole.Paste();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) //Cut
        {
            if (richTextBox1.SelectedText != "" && richTextBox1.Focused)
            {
                richTextBox1.Cut();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) //Select All
        {
            if (richTextBox1.Focused)
            {
                richTextBox1.SelectAll();
            }
        }

        private void startApplicationToolStripMenuItem_Click(object sender, EventArgs e) //Start Application
        {
            if (Nowfile == null)
            {
                createToolStripMenuItem_Click(sender, e);
            }
            saveToolStripMenuItem_Click(sender, e);


            if (richTextBox1.Text == "")
            {
                applicationConsole.AppendText("[" + DateTime.Now + "] " + "<" + Nowfile + ">\n" + Environment.NewLine);
            }
            else
            {
                try
                {
                    richTextBox1.SaveFile(Nowfile, (RichTextBoxStreamType)1);
                    ProcessStartInfo startInfo = new ProcessStartInfo(PyPath, Nowfile);

                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    Process proc = Process.Start(startInfo);

                    StreamReader stout = proc.StandardOutput;
                    StreamReader stout2 = proc.StandardError;
                    string result = stout.ReadToEnd();
                    string result2 = stout2.ReadToEnd();
                    stout.Close();
                    stout2.Close();
                    if (result == "")
                    {
                        applicationConsole.BackColor = Color.Red;
                        applicationConsole.AppendText("[" + DateTime.Now + "] " + "<" + Nowfile + ">" + Environment.NewLine + result2);
                    }
                    else
                    {
                        applicationConsole.BackColor = SystemColors.Control;
                        applicationConsole.AppendText("[" + DateTime.Now + "] " + "<" + Nowfile + ">" + Environment.NewLine + result);
                    }

                    proc.Close();
                }
                catch
                {
                    applicationConsole.AppendText("[" + DateTime.Now + "] " + "<" + Nowfile + ">" + Environment.NewLine + "$INTERPRETER DOES NOT EXIST$ -----> " + PyPath);
                }
            }
        }

        private void applicationConsoleToolStripMenuItem_Click(object sender, EventArgs e) //Application Console
        {
            if (!debc)
            {
                terminalConsole.Visible = false;
                terminalToolStripMenuItem.Checked = false;
                ter = false;
                applicationConsole.Visible = true;
                applicationConsoleToolStripMenuItem.Checked = true;
                debc = true;
                consoleToolStripStatusLabel.Text = "Console: Application";
            }
            else
            {
                applicationConsole.Visible = false;
                applicationConsoleToolStripMenuItem.Checked = false;
                debc = false;
                consoleToolStripStatusLabel.Text = "Console: None";
            }
        }

        public void richTextBox1_TextChanged(object sender, EventArgs e) //Chars&Strings count
        {
            charss.Text = "Chars count: " + richTextBox1.Text.Length;
            strr.Text = "Strings count: " + richTextBox1.Lines.Length;
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e) //Preferences
        {
            Form2 newform = new Form2();
            newform.Show();
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e) //GitHub Link
        {
            Process.Start(@"https://github.com/Zarazdor/PyEdit");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) //About
        {
            Form3 newform = new Form3();
            newform.Show();
        }

        /*private void richTextBox1_KeyUp(object sender, KeyEventArgs e) //Auto \t
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = richTextBox1.Lines[richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart)-1];

                try
                {
                    if (str[str.Length - 1] == Convert.ToChar(":"))
                    {
                        if (str[0] != Convert.ToChar("\t"))
                        {
                            richTextBox1.AppendText("\t");
                        }
                        else if (str[0] == Convert.ToChar("\t"))
                        {
                            if (str[1] == Convert.ToChar("\t"))
                            {
                                richTextBox1.AppendText("\t");
                            }
                        }
                    }
                }
                catch
                { }
            }
        }*/

        private void terminalConsole_KeyDown(object sender, KeyEventArgs e) //Terminal Commands
        {
            if (e.KeyCode == Keys.Enter)
            {
                string comm = terminalConsole.Lines[terminalConsole.GetLineFromCharIndex(terminalConsole.SelectionStart)];

                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", @"/c " + comm);
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                Process proc = Process.Start(startInfo);

                StreamReader stout = proc.StandardOutput;
                StreamReader stout2 = proc.StandardError;
                string result = stout.ReadToEnd();
                string result2 = stout2.ReadToEnd();
                stout.Close();
                stout2.Close();
                if (result == "") 
                { 
                    terminalConsole.AppendText("\n>>> " + result2); 
                }
                else 
                { 
                    terminalConsole.AppendText("\n>>> " + result); 
                }

                proc.Close();
            }
        }
    }
}
