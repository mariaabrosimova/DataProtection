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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str, strn, p1, p2;
            int num;
            bool b = false, l = false, f = false;
            int max_length_password = 128;

            str = textBox1.Text;
            int count = File.ReadAllLines("Info.txt").Length;
            string[] name = new string[count];

            FileStream file = new FileStream("Info.txt", FileMode.Open);
            StreamReader fnew = new StreamReader(file);
            for (int t = 0, i = 0; t < count; t++, i = 0)
            {
                strn = fnew.ReadLine();
                while (b == false)
                {
                    if ((strn[i] == '_') && (b == false))
                        b = true;
                    i++;
                }
                name[t] = strn.Substring(0, i - 1);
                b = false;
            }
            fnew.Close();

            for (int i = 0; i < count; i++)
            {
                //if (str == name[i])
                    if (string.Equals(name[i], str, StringComparison.CurrentCultureIgnoreCase))
                    {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                    l = true;
                }
            }
            if (checkBox1.Checked)
                p1 = "1";
            else
                p1 = "0";

            p2 = textBox2.Text;
            if (checkBox2.Checked)
            {
                if (((!(int.TryParse(p2, out num))) || (Convert.ToInt32(p2) > max_length_password) || (Convert.ToInt32(p2) < 0)))
                {
                    MessageBox.Show("Некорректное значение минимального кол-ва символов");
                    f = true;
                }
            }
            else
                p2 = "0";
            if ((f == false) && (l == false))
            {
                FileStream file2 = new FileStream("Info.txt", FileMode.Append);//Append
                StreamWriter fnew2 = new StreamWriter(file2);
                fnew2.WriteLine(textBox1.Text + '_' + "" + '_' + p1 + '_' + p2 + '_' + textBox1.Text+ "#" + 0); // уже создается парольное им
                fnew2.Close();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                this.textBox2.Enabled = true;
            else
                this.textBox2.Enabled = false;
        }
    }
}
