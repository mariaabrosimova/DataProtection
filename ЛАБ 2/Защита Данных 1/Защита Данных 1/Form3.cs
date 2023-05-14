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
        Form2 f2;
        public Form3()
        {
            InitializeComponent();
        }
        public string preparing_key_pas (string pas, string pass_key, out string new_pass_key) 
        {  string encrypted_password = string.Empty;
            int key_length = pass_key.Length;
            int pas_length = pas.Length;
            string new_pas= pas.Substring(0);

            if (key_length > pas_length)
            {
                new_pass_key = pass_key.Substring(0, pas_length); //Если длина ключа больше длины открытого текста, то ключ усекается до нужной длины.

            }
            else {
                new_pass_key = pass_key;
                int x = pas_length % key_length;
                if (x != 0)
                {
                    int dop = key_length - x; //сколько нулей нужно добавить в последний блок
                    new_pas +=new string(' ', dop);
                }
                

            }
            return new_pas;
        }
        public string Decode(string pas, string key)
        {
            string decoded_pas = "";

            int n = pas.Length / key.Length;
            string buf_block = "";
            string result = "";
            string res = "";
            int a;
            a = 0;
            char buf;
            for (int i = 0; i < n; i++)
            {
                Dictionary<int, string> elem = new Dictionary<int, string>();
                buf_block = pas.Substring(a, key.Length);
                for (int j = 0; j < key.Length; j++)
                {
                    elem.Add(Int32.Parse(char.ToString(key[j])), buf_block.Substring(j, 1));
                }
                foreach (var item in elem.OrderBy(x => x.Key))
                {
                    result += item.Value;
                }

                a += key.Length;



            }
            //result = result.Replace(" ", "");
            return result;
        }

        public int[] Encrypt(string pas, string key, out string key_str) 
        {   string sort_alp= String.Concat(key.OrderBy(ch => ch)); //отсортированный ключ
           
            int[] position = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
                {
                    int m =  sort_alp.IndexOf(key[i]);
                    position[i] = m;
                    StringBuilder sb = new StringBuilder(sort_alp);
                    sb[m] = '@';
                    sort_alp= sb.ToString();
                }
            key_str = string.Join("", position);
            return position; 
        }

        public string Code(string pas, int[] key)
        {
            int n = pas.Length / key.Length;
            string buf_block = "";
            string result = "";
            int begin = 0;
            
            for (int i = 0; i < n; i++)
            {
                buf_block = pas.Substring(begin, key.Length);
                for (int j = 0; j < key.Length; j++)
                    result += buf_block[key[j]];
                begin += key.Length;
            }
           
                return result;
        }
  
        public void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked)
            {
                int n = Convert.ToInt32(label6.Text); //строка изменений
                string pas = textBox2.Text; //новый пароль
                string pas_n = "";
                string name = label4.Text;//имя                   
                string p1 = label7.Text; //p1
                string p2 = label8.Text; //p2
                string key = label9.Text; //
                
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
                    if (i == n)
                    {
                        pas_n = preparing_key_pas(pas, name, out key); //подготовленный пароль с добавлением _
                        int[] position;
                        string key_str = "";
                        position = Encrypt(name, key,out key_str);//
                        

                        string key_sort = string.Join("", position);//key;  
                        string encrypt_pass = Code(pas_n,  position);

                        string decoded_pass = Decode( encrypt_pass , key_sort); //подставляем сортированный ключ то есть логин
                        fnew2.WriteLine(name + '_' + encrypt_pass + '_' + p1 + '_' + p2 + '_' + key /*+ i + n*/+ '#' + key_sort+ '#' + encrypt_pass + '#' + decoded_pass);
                        
                    }
                    else { fnew2.WriteLine(str[i]); }
                }
                fnew2.Close();
                Close();
            }
            else
                MessageBox.Show("Введенные пароли не совпадают");
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

            //TB0.Text = p2;
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
                    MessageBox.Show("Минимальная длина пароля: " + p2 + ' ');
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
            int p2 = Convert.ToInt32(label8.Text); //искл
            if (npas1 == npas2)
            {
                if (npas1.Length < p2)
                    MessageBox.Show(" Минимальная длина пароля: " + p2);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
