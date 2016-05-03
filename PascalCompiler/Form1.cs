using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PascalCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFormat();
            SymbolTable.Clear();
            ErrorHandler.Clear();

            LexicalAnalizer.Analize(richTextBox1);
            SyntaxAnalizer.program(Set.Null());

            ShowErrors();
        }

        private void ClearFormat()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = richTextBox1.Font;
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
        }

        private void ShowErrors()
        {
            listBox1.Items.Clear();
            label1.Text = "Lexical errors: " + ErrorHandler.Lerrors.Count.ToString();
            label2.Text = "Syntax errors: " + ErrorHandler.Serrors.Count.ToString();
            if (ErrorHandler.Lerrors.Count != 0 || ErrorHandler.Serrors.Count != 0)
            {
                for (int i = 0; i < ErrorHandler.Lerrors.Count; i++)
                {
                    int index = ErrorHandler.Lerrors[i].position;
                    int length = ErrorHandler.Lerrors[i].length;
                    string message = "Lexical Error: " + ErrorHandler.Lerrors[i].message + " ,Index: " + index.ToString();
                    listBox1.Items.Add(message);
                    MakeRed(index, length);
                }

                for (int i = 0; i < ErrorHandler.Serrors.Count; i++)
                {
                    int index = ErrorHandler.Serrors[i].position;
                    int length = ErrorHandler.Serrors[i].length;
                    string message = "Syntax Error: " + ErrorHandler.Serrors[i].message + " ,Index: " + index.ToString();
                    listBox1.Items.Add(message);
                    MakeRed(index, length);
                }
            }
            else
                MessageBox.Show("Pascal code accepted!\nNo errors found.", "Accepted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void MakeRed(int index, int length)
        {
            richTextBox1.Select(index, length);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Underline);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < ErrorHandler.Lerrors.Count)
            {
                for (int i = 0; i < ErrorHandler.Lerrors.Count; i++)
                    if (listBox1.SelectedIndex == i)
                    {
                        int index = ErrorHandler.Lerrors[i].position;
                        int length = ErrorHandler.Lerrors[i].length;
                        richTextBox1.Select(index, length);
                        richTextBox1.Focus();
                        return;
                    }
            }
            else
            {
                for (int i = 0; i < ErrorHandler.Serrors.Count; i++)
                    if (listBox1.SelectedIndex - ErrorHandler.Lerrors.Count == i)
                    {
                        int index = ErrorHandler.Serrors[i].position;
                        int length = ErrorHandler.Serrors[i].length;
                        richTextBox1.Select(index, length);
                        richTextBox1.Focus();
                        return;
                    }
            }
            

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            

        }


    }
}
