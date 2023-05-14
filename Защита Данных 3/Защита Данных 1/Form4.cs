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
    public partial class Form4 : Form
    {
        CheckBox[] checkBoxes;
        TextBox[] textboxn;
        TextBox[] textboxp;
        TextBox textcount;

        public Form4()
        {
            InitializeComponent();

            string str, name, p1, p2;
            int i = 0, j = 0, k = 0, t = 0;
            int y = 47;
            int count = File.ReadAllLines("Info.txt").Length;
            checkBoxes = new CheckBox[count * 2];
            textcount = new TextBox();
            textcount.Text = count.ToString();
            textboxn = new TextBox[count];
            textboxp = new TextBox[count];

            FileStream file = new FileStream("Info.txt", FileMode.Open);
            StreamReader fnew = new StreamReader(file);

            while (!fnew.EndOfStream)
            {
                str = fnew.ReadLine();

                while (str[i] != ' ')
                {
                    i++;
                }
                name = str.Substring(0, i);

                j = i + 1;
                while (str[j] != ' ')
                {
                    j++;
                }
                p1 = (str.Substring(j + 1, 1));
                p2 = (str.Substring(j + 3, 1));

                textboxn[k] = new TextBox
                {
                    Name = "Textbox" + k,
                    Text = name,
                    ReadOnly = true,
                    Location = new Point(32, y),
                    Size = new Size(100, 20),
                };
                checkBoxes[0 + k * 2] = new CheckBox
                {
                    Name = "checkBox1" + k * 2,
                    Text = "",
                    Location = new Point(32 + 135, y + 3),
                    Size = new Size(15, 14),
                };
                checkBoxes[1 + t * 2] = new CheckBox
                {
                    Name = "checkBox2" + (1 + t * 2),
                    Text = "",
                    Location = new Point(32 + 185, y + 3),
                    Size = new Size(15, 14),
                };
                textboxp[k] = new TextBox
                {
                    Name = "Textbn" + k,
                    Text = p2,
                    Location = new Point(32 + 210, y),
                    Size = new Size(17, 20),
                };
                var label = new Label
                {
                    Name = "0-8" + k,
                    Text = "0-8",
                    Location = new Point(32 + 230, y + 3),
                    Size = new Size(28, 13),
                };
                this.Controls.Add(textboxn[k]);
                this.Controls.Add(checkBoxes[0 + k * 2]);
                this.Controls.Add(checkBoxes[1 + t * 2]);
                this.Controls.Add(textboxp[k]);
                this.Controls.Add(label);

                checkBoxes[0].Enabled = false;
                checkBoxes[1].Enabled = false;
                textboxp[0].Enabled = false;
                if (label.Name == "0-80")
                    label.Enabled = false;

                if (p1 == "1")
                    checkBoxes[0 + k * 2].Checked = true;
                else
                    checkBoxes[0 + k * 2].Checked = false;
                if (p2 != "0")
                    checkBoxes[1 + t * 2].Checked = true;
                else
                    checkBoxes[1 + t * 2].Checked = false;

                y += 25;
                i = 0;
                j = 0;
                k++;
                t++;

                Size = new Size(334, 118 + 28 * k);
                button1.Location = new Point(215, 50 + 28 * k);
                button2.Location = new Point(33, 50 + 28 * k);
            }
            fnew.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str, p1, p2;
            int count = Convert.ToInt32(textcount.Text);
            string[] pas = new string[count];
            bool b = false, f = false;
            int num;

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

            FileStream file2 = new FileStream("Info.txt", FileMode.Open);
            StreamWriter fnew2 = new StreamWriter(file2);

            for (int k = 0, i = 0; i < count; k += 2, i++)
            {
                if (checkBoxes[k].Checked)
                    p1 = "1";
                else
                    p1 = "0";

                p2 = textboxp[i].Text;
                if (checkBoxes[k + 1].Checked)
                {
                    if (((!(int.TryParse(p2, out num))) || (Convert.ToInt32(p2) > 8) || (Convert.ToInt32(p2) < 0)))
                    {
                        MessageBox.Show(" Неверное значение! ");
                        f = true;
                    }
                }
                else
                    p2 = "0";
                if (f == false)
                    fnew2.WriteLine(textboxn[i].Text + ' ' + pas[i] + ' ' + p1 + ' ' + p2);
            }
            fnew2.Close();
            if (f == false)
                Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBoxesChanged(object sender, EventArgs e)
        {

        }
    }
}
