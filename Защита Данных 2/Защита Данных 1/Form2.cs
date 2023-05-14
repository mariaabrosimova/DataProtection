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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.label4.Text = label2.Text;//логин
            f3.label5.Text = label3.Text;//пароль  
            

            f3.label6.Text = label4.Text;//номер строки&
            f3.label7.Text = label5.Text;//p1
            f3.label8.Text = label6.Text;//p2
            f3.label9.Text = label7.Text; //key
            

            f3.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string str;
            bool b = false;
            int n;
            int count = File.ReadAllLines("Info.txt").Length;
            string[] pas = new string[count];

            FileStream file = new FileStream("Info.txt", FileMode.Open);
            StreamReader fnew = new StreamReader(file);
            for (int t = 0, i = 0, j = 0; t < count; t++, i = 0, j = 0)
            {
                str = fnew.ReadLine();

                while (b == false)
                {
                    if ((str[i] == '_') && (b == false))
                    {
                        if (str[i + 1] == '_')
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
                            while (str[i + 1] != '_');
                        }
                    }
                    i++;
                }
                pas[t] = str.Substring(i - j, j);
                b = false;
            }
            fnew.Close();
            n = Convert.ToInt32(label4.Text);

            if ((label2.Text != "admin")||(pas[n]==""))
            {
                this.button2.Enabled = false;
                this.button4.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4;
            f4 = new Form4();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f5;
            f5 = new Form5();
            f5.ShowDialog();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
