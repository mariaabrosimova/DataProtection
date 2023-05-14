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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            label3.Text = "0";
            label4.Text = "0";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AB ab1;
            ab1 = new AB();
            ab1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //нажатие на кнопку ввод
        {
            string str, log, npas;
            bool b = false, l = false;
            int count = File.ReadAllLines("Info.txt").Length;
            string[] pas = new string[count];
            string[] name = new string[count];
            string[] p1 = new string[count];
            string[] p2 = new string[count];
            int n = 0, er = 0;

            FileStream file = new FileStream("Info.txt", FileMode.Open);
            StreamReader fnew = new StreamReader(file);
            for (int t = 0, i = 0, j = 0; t < count; t++, i = 0, j = 0)
            {
                str = fnew.ReadLine();

                while (b == false)
                {
                    if ((str[i] == ' ') && (b == false)) //index out of the range

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
                name[t] = str.Substring(0, i - j - 1);
                p1[t] = str.Substring(i + 1, 1);
                // p2[t] = str.Substring(i + 3, 1);
                p2[t] = str.Substring(i + 3);
                b = false;
            }
            fnew.Close();

            log = textBox1.Text;
            npas = textBox2.Text;

            for (int i = 0; i < count; i++)
            {
                //if (log == name[i])
                    if (string.Equals(name[i], log, StringComparison.CurrentCultureIgnoreCase)) //не чувствительность к регистру
                    {
                    l = true;
                    n = i;
                }
            }
            if (l == false)
                MessageBox.Show("Неправильный логин. Попробуйте еще раз");
            else
            {
                if (p1[n] == "1")
                    MessageBox.Show("Данный пользователь заблокирован");
                else
                {
                    if (npas != pas[n])
                    {
                        if (n != Convert.ToInt32(label4.Text))
                            label3.Text = "0";
                        label4.Text = n.ToString();
                        MessageBox.Show("Неправильный пароль. Попробуйте еще раз");
                        er = Convert.ToInt32(label3.Text);
                        er++;
                        label3.Text = er.ToString();
                        if (er == 3)
                        {
                            MessageBox.Show("3 раза был введен неверный пароль. Завершение работы программы");
                            Close();
                        }
                    }
                    if (npas == pas[n])
                    {                        
                        Form2 f2 = new Form2();
                        f2.label2.Text = name[n];
                        f2.label4.Text = n.ToString();
                        f2.label5.Text = p1[n];
                        f2.label6.Text = p2[n];
                        textBox2.Text = "";
                        label3.Text = "0";
                        f2.ShowDialog();
                       
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        { Form1 f1 = new Form1();
            if (!(File.Exists("Info.txt")))
            {
                FileStream file = new FileStream("Info.txt", FileMode.Create);
                StreamWriter fnew = new StreamWriter(file);
                fnew.WriteLine("admin" + ' ' + "" + ' ' + "0" + ' ' + "0");
                fnew.Close();
            }   
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
