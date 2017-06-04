using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagCleanerWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inputBox.Text = "";
            tagsBox.Text = "";
        }

        public Boolean Include(String sub, String[] substrings)
        {
            Char[] delimiters = { ' ', '=', '/', '<', '>' };
            String[] allowed = sub.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            String elem = "";

            if (allowed.Length > 0)
                elem = allowed[0];

            Boolean attr = true;


            foreach (var substring in substrings)
            {

                if (elem.Equals(substring))
                    attr = false;
            }

            return attr;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            String block = inputBox.Text.Trim();

            String tags = tagsBox.Text.Trim();

            Char[] delimiters = { ',', ';', ' ' };
            String[] substrings = tags.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int size = block.Length;

            String str = "";
            String new_str = "";

            int _single = 0;
            int _double = 0;
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                new_str += block[i];

                if ((block[i].Equals("'")) && (_single == 0))
                    _single = 1;
                else if ((block[i].Equals("'")) && (_single == 1))
                    _single = 0;

                if ((block[i].Equals('"')) && (_double == 0))
                    _double = 1;
                else if ((block[i].Equals('"')) && (_double == 1))
                    _double = 0;


                if (block[i].Equals('<') && (str.Length != 1) && (_single == 0) && (_double == 0))
                {
                    str += block[i];
                    count += 1;
                }
                else if (block[i].Equals('<') && ((_single > 0) || (_double > 0)))
                {
                    str += block[i];

                }

                if (block[i].Equals('<') && (str.Length == 1) && (_single == 0) && (_double == 0))
                {
                    str = "<";
                    count = 1;
                }

                if (block[i].Equals('>') && (str.Length == 1) && (_single == 0) && (_double == 0))
                {
                    str = "";
                    count = 0;
                }


                if ((str.Length > 0) && (!block[i].Equals('<')))
                {
                    str += block[i];
                }



                if (block[i].Equals('>') && (count > 0) && (_single == 0) && (_double == 0))
                {
                    count -= 1;
                }



                if ((count == 0) && (str.Length > 2))
                {

                    if (Include(str, substrings))
                    {

                        new_str = new_str.Replace(str, "");

                    }

                    str = "";
                }

            }

            outputBox.Text = new_str;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
