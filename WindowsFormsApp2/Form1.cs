using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void UpdateOutput(object sender, EventArgs e)
        {
            String source = inputTextBox.Text;
            try
            {
                object output = Evaluator.Evaluate(source);
                outputTextBox.Text = String.Format("{0:0.#####}", (Double)output);
            } 
            catch (Exception ex)
            {
                outputTextBox.Text = "";
                Debug.WriteLine(ex.Message);
            }
        }

        private void EqualsAction(object sender, EventArgs e)
        {
            String source = inputTextBox.Text;
            try
            {
                object output = Evaluator.Evaluate(source);
                inputTextBox.Text = String.Format("{0:0.#####}", (Double)output);
                outputTextBox.Text = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void textButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            inputTextBox.Text += btn.Text;
        }

        private void ClearInput(object sender, EventArgs e)
        {
            inputTextBox.Text = "";
        }

        private void TrimInput(object sender, EventArgs e)
        {
            int length = inputTextBox.Text.Length;
            if (length != 0)
                inputTextBox.Text = inputTextBox.Text.Substring(0, length - 1);
        }
    }
}
