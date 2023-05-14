using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Защита_Данных_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked)
            {
                int n = Convert.ToInt32(label6.Text);
                string pas = textBox2.Text;
                string name = label4.Text;//имя                   
                string p1 = label7.Text; //p1
                string p2 = label8.Text; //p2

                int count = File.ReadAllLines("Info.txt").Length;
                string[] str = new string[count];

                FileStream file = new FileStream("Info.txt", FileMode.Open);
                StreamReader fnew = new StreamReader(file);

                for (int i = 0; i < count; i++)
                    str[i] = fnew.ReadLine();
                fnew.Close();

                FileStream file2 = new FileStream("Info.txt", FileMode.Create);
                StreamWriter fnew2 = new StreamWriter(file2);
                for (int i = 0; i < count; i++)
                {
                    if (i != n)
                        fnew2.WriteLine(str[i]);
                    else
                        fnew2.WriteLine(name + ' ' + pas + ' ' + p1 + ' ' + p2);
                }
                fnew2.Close();
                Close();
            }
            else
                MessageBox.Show(" Пароли не совпадают! ");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string str;
            bool b = false;
            int count = File.ReadAllLines("Info.txt").Length;
            string[] pas = new string[count];

            FileStream file = new FileStream("Info.txt", FileMode.Open);
            StreamReader fnew = new StreamReader(file);
            for (int t = 0, i = 0, j = 0; t < count; t++, i = 0, j = 0)
            {
                str = fnew.ReadLine();
                while (b == false)
                {
                    if ((str[i] == ' ') && (b == false))
                    {
                        if (str[i + 1] == ' ')
                        {
                            b = true;
                        }
                        else
                        {
                            do
                            {
                                i++;
                                j++;
                                b = true;
                            }
                            while (str[i + 1] != ' ');
                        }
                    }
                    i++;
                }
                pas[t] = str.Substring(i - j, j);

                b = false;
            }
            fnew.Close();
            int n = Convert.ToInt32(label6.Text);
            label5.Text = pas[n];
            if (pas[n] == "")
                checkBox1.Checked = true;
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pas, opas;
            string p2 = label8.Text;
            pas = textBox1.Text;
            opas = label5.Text;
            if (pas == opas)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string npas1, npas2;
            npas1 = textBox2.Text;
            npas2 = textBox3.Text;
            int p2 = Convert.ToInt32(label8.Text);
            if (npas1 == npas2)
            {
                if (npas1.Length < p2)
                    MessageBox.Show(" Минимальная длинна пароля: " + p2 + ' ');
                else
                    checkBox2.Checked = true;
            }
            else
                checkBox2.Checked = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string npas1, npas2;
            npas1 = textBox2.Text;
            npas2 = textBox3.Text;
            int p2 = Convert.ToInt32(label8.Text);
            if (npas1 == npas2)
            {
                if (npas1.Length < p2)
                    MessageBox.Show(" Минимальная длинна пароля: " + p2);
                else
                    checkBox2.Checked = true;
            }
            else
                checkBox2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void окноToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
