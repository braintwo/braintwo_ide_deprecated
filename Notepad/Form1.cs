using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public static bool DebugLog = false;
        public static string sendertype = String.Empty;
        public static string sender = String.Empty;
        public static bool Debug = false;
        String path = String.Empty;

        public Form1() => InitializeComponent();

   //     private string ReturnMessageFromFormat(string type)
       /* {
            switch (type)
            {
                case "ino":
                    return "Arduino";
                    break;
                case "cs":
                    return "C#";
                    break;
                case "cpp":
                    return "C++";
                    break;
                case "c":
                    return "C";
                    break;
                case "btwo":
                    return "Braintwo";
                    break;
                case "json":
                    return "Json";
                    break;
                case "xml":
                    return "Xml";
                    break;
                case "html":
                    return "HTML";
                    break;
                case "css":
                    return "CSS";
                    break;
                case "js":
                    return "JavaScript";
                    break;
                default:
                    return "Braintwo";
                    break;

            }
        }*/

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(path = openFileDialog1.FileName);
                string[] SplitExtension = openFileDialog1.FileName.Split('.');
                labelFormat.Text = "Braintwo";
               
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(path = saveFileDialog1.FileName, textBox1.Text);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                File.WriteAllText(path, textBox1.Text);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void exitPrompt()
        {
            DialogResult = MessageBox.Show("Do you want to save current file?",
                "Notepad",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    textBox1.Text = String.Empty;
                    path = String.Empty;;
                }
                else if (DialogResult == DialogResult.No)
                {
                    textBox1.Text = String.Empty;;
                    path = String.Empty;;
                }

            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.SelectAll();

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Cut();

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Copy();

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Paste();

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.SelectedText = String.Empty;


        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                textBox1.WordWrap = false;
                textBox1.ScrollBars = ScrollBars.Both;
                wordWrapToolStripMenuItem.Checked = false;
            }
            else
            {
                textBox1.WordWrap = true;
                textBox1.ScrollBars = ScrollBars.Vertical;
                wordWrapToolStripMenuItem.Checked = true;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = textBox1.Font = new Font(fontDialog1.Font, fontDialog1.Font.Style);
                textBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new Form2();
            aboutForm.ShowDialog();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        textBox1.SelectAll();
                        break;
                    case Keys.N:
                        e.SuppressKeyPress = true;
                        newToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.S:
                        e.SuppressKeyPress = true;
                        saveToolStripMenuItem_Click(sender, e);
                        break;
                }
            }
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.White;
            textBox1.BackColor = Color.Black;
            this.BackColor = Color.Gray;
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.Gray;
            this.BackColor = Color.Gray;
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.White;
            this.BackColor = Color.White;
        }

        class Braintwo
        {

            public class Command
            {
                public string command_;
                public Action action;
            }


            public char[] ListSyntax = new char[] {
            '_', ';',
            ':', '=',
            '+', '-',
            '>', '<',
            ']', '*',
            '/', '^',
            'R', ',',
            '.', '#',
            '[', 'P',
            '?', '0',
            '~', '@',
            'C','X',
            'Y','&',
            '1','2','3','4','5','6','7','8','9',
            '\'','\\',
            '{','}',
            '!','|','$',')'

        };

            private static Dictionary<string, Command> Commands = new Dictionary<string, Command>();

            public double currentMemory = 0;
            public double[] Memory = new double[2];
            public int pointerMemory = 0;

            public string Output;

            static void CreateCommand(string command, Action action_)
            {
                Commands.Add(command, new Command() { command_ = command, action = action_ });
            }

            static void ExecuteCommand(string command)
            {
                Commands[command].action();
            }

            public void ExecuteCode(char Char)
            {
                if (this.ListSyntax.Contains(Char))
                {
                    ExecuteCommand(Char.ToString());
                }
                else
                {
                    throw new Exception("Error to execute code " + Char + " dont exists.");
                }
            }
            int pointerMemory_param = 0;

            public void Interpreter(string code, params string[] vars_c)
            {

                string c_param = String.Empty;
                string c_memory = String.Empty;
                string c_value = String.Empty;

                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] == '$')
                    {
                        pointerMemory_param = int.Parse(code[i + 1].ToString());
                        c_param = code.Replace($"${pointerMemory_param}", vars_c[pointerMemory_param]);
                        c_memory = c_memory + c_param;
                    }
                }

                string[] xSplit = c_memory.Split('x');

                for (int i = 0; i < xSplit.Length; i++)
                {

                    try
                    {
                        if (xSplit[i][0] != '$')
                        {
                            c_value += xSplit[i];
                        }
                    }
                    catch { }
                }

                for (int i = 0; i < c_value.Length; i++)
                {
                    ExecuteCode(c_value[i]);
                }

                if (c_value == String.Empty && code.Length > 0)
                {
                    for (int i = 0; i < code.Length; i++)
                    {
                        ExecuteCode(code[i]);
                    }
                }

            }

            public Braintwo()
            {
                CreateCommand("=", new Action(delegate ()
                {
                    currentMemory = currentMemory * 2;
                }));
                CreateCommand("+", new Action(delegate ()
                {
                    currentMemory += 1;
                }));
                CreateCommand("-", new Action(delegate ()
                {
                    currentMemory -= 1;
                }));
                CreateCommand(";", new Action(delegate ()
                {
                    double backupMemory = 0;
                    backupMemory = Memory[0];
                    Memory[0] = Memory[1];
                    Memory[1] = backupMemory;
                    backupMemory = 0;
                }));
                CreateCommand(":", new Action(delegate ()
                {
                    Memory[pointerMemory] = Memory[0] - Memory[1];
                }));
                CreateCommand("_", new Action(delegate ()
                {
                    currentMemory = new Random().Next(0, 248);
                }));
                CreateCommand(">", new Action(delegate ()
                {
                    pointerMemory += 1;
                }));
                CreateCommand("<", new Action(delegate ()
                {
                    pointerMemory -= 1;
                }));
                CreateCommand("]", new Action(delegate ()
                {
                    try
                    {
                        Memory[pointerMemory] = currentMemory;
                        currentMemory = 0;
                    }
                    catch { }
                }));
                CreateCommand("*", new Action(delegate ()
                {
                    currentMemory = Memory[0] * Memory[1];
                }));
                CreateCommand("/", new Action(delegate ()
                {
                    currentMemory = Memory[0] / Memory[1];
                }));
                CreateCommand("^", new Action(delegate ()
                {
                    currentMemory = Math.Pow(Memory[0], Memory[1]);
                }));
                CreateCommand("R", new Action(delegate ()
                {
                    Memory[0] = 0;
                    Memory[1] = 0;
                    pointerMemory = 0;
                }));
                CreateCommand(",", new Action(delegate ()
                {
                    Form1.sender = String.Empty;
                    Input ip = new Input();
                    ip.Show();
                    sendertype = ",";
                    Debug = true;
                }));
                CreateCommand(".", new Action(delegate ()
                {
                    Output = Output + (char)currentMemory;
                }));
                CreateCommand("#", new Action(delegate ()
                {
                    Console.WriteLine("[cMemory]: " + currentMemory + "\n[Memory]: [" + Memory[0] + "] [" + Memory[1] + "]" + "\n[pointerMemory]: " + pointerMemory + " \n\n");
                }));
                CreateCommand("[", new Action(delegate ()
                {
                    currentMemory = Memory[pointerMemory];
                }));
                CreateCommand("P", new Action(delegate ()
                {
                    Form1.DebugLog = true;
                }));
                CreateCommand("?", new Action(delegate ()
                {
                    if (currentMemory == 0)
                    {
                        Memory[0] = 1;
                    }
                    else
                    {
                        Memory[0] = 0;
                    }
                }));
                CreateCommand("0", new Action(delegate ()
                {
                    currentMemory = 0;
                }));
                CreateCommand("~", new Action(delegate ()
                {
                    currentMemory = currentMemory * Memory[pointerMemory];
                }));
                CreateCommand("@", new Action(delegate ()
                {
                    Memory[pointerMemory] = 0;
                }));
                CreateCommand("C", new Action(delegate ()
                {
                    currentMemory = Memory[0] + Memory[1];
                }));
                CreateCommand("X", new Action(delegate ()
                {
                    currentMemory = currentMemory + 10;
                }));
                CreateCommand("Y", new Action(delegate ()
                {
                    currentMemory = currentMemory - 10;
                }));
                CreateCommand("&", new Action(delegate ()
                {
                    Console.Write(currentMemory.ToString());
                }));
                CreateCommand("1", new Action(delegate ()
                {
                    Memory[pointerMemory] += 1;
                }));
                CreateCommand("2", new Action(delegate ()
                {
                    Memory[pointerMemory] += 2;
                }));
                CreateCommand("3", new Action(delegate ()
                {
                    Memory[pointerMemory] += 3;
                }));
                CreateCommand("4", new Action(delegate ()
                {
                    Memory[pointerMemory] += 4;
                }));
                CreateCommand("5", new Action(delegate ()
                {
                    Memory[pointerMemory] += 5;
                }));
                CreateCommand("6", new Action(delegate ()
                {
                    Memory[pointerMemory] += 6;
                }));
                CreateCommand("7", new Action(delegate ()
                {
                    Memory[pointerMemory] += 7;
                }));
                CreateCommand("8", new Action(delegate ()
                {
                    Memory[pointerMemory] += 8;
                }));
                CreateCommand("9", new Action(delegate ()
                {
                    Memory[pointerMemory] += 9;
                }));
                CreateCommand("'", new Action(delegate ()
                {
                    double a = Memory[pointerMemory] * 2;
                    Memory[pointerMemory] = Memory[pointerMemory] - a;
                }));
                CreateCommand("\\", new Action(delegate ()
                {
                    Memory[pointerMemory] = (Memory[0] * Memory[1]);
                }));
                CreateCommand("{", new Action(delegate ()
                {
                    Memory[pointerMemory] = Math.Pow(Memory[0], Memory[1]);
                }));
                CreateCommand("}", new Action(delegate ()
                {
                    string a = $"0,{Memory[pointerMemory]}";
                    Memory[pointerMemory] = double.Parse(a);
                }));
                CreateCommand("!", new Action(delegate ()
                {
                    Memory[pointerMemory] = Memory[pointerMemory] * 2;
                }));
                CreateCommand("|", new Action(delegate ()
                {
                    Memory[pointerMemory] = Memory[0] + Memory[1];
                }));
                CreateCommand("$", new Action(delegate ()
                {
                    //Ignore.
                }));
                CreateCommand(")", new Action(delegate ()
                {
                    Form1.sender = String.Empty;
                    Input ip = new Input();
                    ip.Show();
                    sendertype = ")";
                    Debug = true;
                    
                }));
            }

            public void Reset()
            {
                Memory[0] = 0;
                Memory[1] = 0;
                pointerMemory = 0;
                Output = String.Empty;
                currentMemory = 0;
            }

        }
        Braintwo bt = new Braintwo();

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt.Reset();
            bt.Interpreter(textBox1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbm0.Text = bt.Memory[0].ToString();
            lbm1.Text = bt.Memory[1].ToString();
            lbptr.Text = bt.pointerMemory.ToString();
            cmr.Text = bt.currentMemory.ToString();

            if (DebugLog)
            {
                richTextBox1.Text = bt.Output;
                DebugLog = false;
            }
            if (Debug)
            {
                if (Form1.sendertype == "P")
                {
                    richTextBox1.Text = bt.Output;
                    sendertype = string.Empty;
                    Debug = false;
                }
                if (Form1.sender != String.Empty)
                {
                    if(Form1.sendertype == ",")
                    {
                        bt.currentMemory = (int)Form1.sender[0];
                        sendertype = string.Empty;
                        Debug = false;
                    }
                }
                if (Form1.sender != String.Empty)
                {
                    if (Form1.sendertype == ")")
                    {
                        double value = double.Parse(Form1.sender);
                        bt.currentMemory = value;
                        sendertype = string.Empty;
                        Debug = false;
                        
                    }
                }
            }
        }
    }
}
