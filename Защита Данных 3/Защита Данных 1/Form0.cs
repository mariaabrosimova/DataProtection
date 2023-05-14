using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace Защита_Данных_1
{
    public partial class Form0 : Form
    {
        // объект класса алгоритма шифрования RC2
        RC2CryptoServiceProvider rc2CSP;
        // буфер для начального вектора по умолчанию
        byte[] IV;
        // объект класса для генерации секретного ключа из парольной фразы
        PasswordDeriveBytes pdb;
        // буфер для случайной примеси
        byte[] randBytes;
        // объекты для входного и выходного файловых потоков
        FileStream finStream, foutStream;
        // объект для потока шифрования-расшифрования
        CryptoStream CrStream;
        // буфер для ввода-вывода данных из файла
        byte[] bytes;
        // длина буфера ввода-вывода
        int numBytesToRead;
        // объект класса исключения
        ArgumentException ex;


        public Form0()
        {
            InitializeComponent();
            // создание объекта для криптоалгоритма
            rc2CSP = new RC2CryptoServiceProvider();
            // сохранение значения начального вектора по умолчанию
            IV = rc2CSP.IV;
            // определение режима блочного шифрования
            rc2CSP.Mode = CipherMode.ECB;
            // создание объекта для параметров разрешенной длины ключа
            KeySizes[] ks = rc2CSP.LegalKeySizes;
            // максимально возможная для выбора длина ключа           

        
        }

        // обработка нажатия кнопки "Выбор файла"
        private void Browse_Click(object sender, EventArgs e)
        {
            // создание объекта класса для диалога открытия файла
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // определение свойств диалогового окна
            // маска имени для отображаемых в окне файлов
            openFileDialog1.Filter = "Все файлы (*.*)|*.*";
            /* автоматическая проверка существования файла с введенным пользователем именем до закрытия диалога */
            openFileDialog1.CheckFileExists = true;
            // выбор в качестве начальной папки текущую
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            // восстановление текущей папки после закрытия диалога
            openFileDialog1.RestoreDirectory = true;
            // если пользователь выбрал файл
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                // сохранение и отображение имени выбранного файла
                InputFName.Text = openFileDialog1.FileName;
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (PassFrase1.Text.Length < 8)
                    throw ex = new ArgumentException("Пароль меньше 8 символов.");
                // создание объекта для генерации случайной примеси
                RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
                // создание буфера для случайной примеси
                randBytes = new byte[8];
                // получение примеси для секретного ключа
                rand.GetBytes(randBytes);
                // декодирование парольной фразы
                byte[] pwd = Encoding.Unicode.GetBytes(PassFrase1.Text);
                // создание объекта для генерации ключа из парольной фразы
                pdb = new PasswordDeriveBytes(pwd, randBytes); //(Byte[], Byte[]) ключ, примесь


                // генерация ключа  Возвращает криптографический ключ из объекта PasswordDeriveBytes.
                rc2CSP.Key = pdb.CryptDeriveKey("RC2", "MD2", (int)128, IV);//имя алгоритма, хэш-алгоритма, размер форм. ключа в битах, Вектор инициализации
                // создание объекта шифрования
                ICryptoTransform encryptor = rc2CSP.CreateEncryptor(rc2CSP.Key, IV);
                /* отображение имени зашифрованного файла (к имени исходного файла добавляется расширение .enc) */
                OutputFName.Text = InputFName.Text + ".enc";
                // создание объектов для файловых потоков 
                finStream = new FileStream(InputFName.Text, FileMode.Open);
                foutStream = new FileStream(OutputFName.Text, FileMode.Create);
                // запись в начало результирующего файла случайной примеси

                //Byte[] Буфер, содержащий данные для записи в поток.
                //int32 смещение с начала файла
                //Максимальное число байтов для записи.

                foutStream.Write(randBytes, 0, 8);

                // создание объекта для потока шифрования
                CrStream = new CryptoStream(foutStream, encryptor, CryptoStreamMode.Write);
                // выделение памяти для буфера ввода-вывода
                bytes = new byte[finStream.Length];
                // задание количества непрочитанных байт
                numBytesToRead = (int)finStream.Length;
                // ввод данных из исходного файла
                int n = finStream.Read(bytes, 0, numBytesToRead);
                // сохранение фактического количества прочитанных байт
                numBytesToRead = n;
                // запись в зашифрованный файл
                CrStream.Write(bytes, 0, numBytesToRead);
                // очистка памяти с конфиденциальными данными
                rc2CSP.Clear();
                // закрытие потока шифрования
                CrStream.Close();
                // закрытие файлов
                finStream.Close();
                foutStream.Close();
                File.Delete(InputFName.Text);
                MessageBox.Show(" Файл зашифрован! ");
                Encrypt.Enabled = false;
                Decrypt.Enabled = false;
                InputFName.Text = OutputFName.Text;
                label2.Text = "Encrypt";

            }
            // обработка ошибки криптографической операции
            catch (CryptographicException ex)
            {
                // вывод сообщения об ошибке
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // закрытие файлов
                finStream.Close();
                foutStream.Close();
            }
            // обработка остальных ошибок
            catch (Exception ex)
            {
                // вывод сообщения об ошибке
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // обработка изменения текста, введенного в редактируемых строках
    
        // обработка нажатия кнопки "Расшифровать"
        private void Decrypt_Click(object sender, EventArgs e)
        {
            try
            {
                /* создание объекта для исходного (зашифрованного) файлового потока */
                finStream = new FileStream(InputFName.Text, FileMode.Open);
                // создание буфера для случайной примеси
                randBytes = new byte[8];
                // чтение случайной примеси из начала зашифрованного файла
                finStream.Read(randBytes, 0, 8);
                // декодирование парольной фразы
                byte[] pwd = Encoding.Unicode.GetBytes(PassFrase1.Text);
                // создание объекта для генерации ключа из парольной фразы
                pdb = new PasswordDeriveBytes(pwd, randBytes);
                // сброс состояния объекта для алгоритма шифрования
                rc2CSP.Clear();
                /* генерация ключа (значение начального вектора варьирается по умолчанию) */
                rc2CSP.Key = pdb.CryptDeriveKey("RC2", "MD2", (int)128, IV);
                // создание объекта расшифрования
                ICryptoTransform decryptor = rc2CSP.CreateDecryptor(rc2CSP.Key, IV);
                /* сохранение и отображение имени результирующего (расшифрованного) файла */
                /* если расширение имени исходного файла равно .enc, то оно удаляется */
                if (InputFName.Text.IndexOf(".enc") != -1)
                    OutputFName.Text = InputFName.Text.Substring(0, InputFName.Text.IndexOf(".enc"));
                else MessageBox.Show(ex.Message, "Расширение имени исходного файла не .enc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /* создание объектов для результирующего файлового потока и потока расшифрования */
                foutStream = new FileStream(OutputFName.Text, FileMode.Create);
                CrStream = new CryptoStream(finStream, decryptor, CryptoStreamMode.Read);
                // выделение памяти для буфера ввода-вывода
                bytes = new byte[finStream.Length - 8];
                // задание количества непрочитанных байт
                numBytesToRead = (int)(finStream.Length) - 8;
                // ввод данных из исходного файла
                int n = CrStream.Read(bytes, 0, numBytesToRead);
                // сохранение фактического количества прочитанных байт
                numBytesToRead = n;
                // запись в расшифрованный файл
                foutStream.Write(bytes, 0, numBytesToRead);
                // очистка памяти с конфиденциальными данными
                rc2CSP.Clear();
                // закрытие потока
                CrStream.Close();
                // закрытие исходного файла
                finStream.Close();
                // закрытие результирующего файла
                foutStream.Close();
                File.Delete(InputFName.Text);
                Encrypt.Enabled = false;
                Decrypt.Enabled = false;
                InputFName.Text = OutputFName.Text;
                label2.Text = "Decrypt";
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }
            // обработка ошибки криптографической операции
            catch (CryptographicException ex)
            {
                // вывод сообщения об ошибке
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // закрытие файлов             
                finStream.Close();
                foutStream.Close();
                File.Delete(OutputFName.Text);
            }
            // обработка остальных ошибок
            catch (Exception ex)
            {
                // вывод сообщения об ошибке
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(OutputFName.Text);
            }
        }

        private void PassFrase1_TextChanged(object sender, EventArgs e)
        {
           
            /* есди выбрано имя исходного файла и введена парольная фраза с подтверждением, то кнопки "Зашифровать" и "Расшифровать" доступны */
            Encrypt.Enabled = (InputFName.Text.Length > 0) && (PassFrase1.Text.Length > 0) && (InputFName.Text.IndexOf(".enc") == -1);
            Decrypt.Enabled = (InputFName.Text.Length > 0) && (PassFrase1.Text.Length > 0) && (InputFName.Text.IndexOf(".enc") != -1);
            
        }

        private void Form0_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (label2.Text == "Decrypt")
                MessageBox.Show(" Файл не зашифрован!");
            else
                Application.Exit();
        }
   
        private void Exit_Click(object sender, EventArgs e)
        {
            // закрытие главного окна
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Decrypt")
                MessageBox.Show(" Файл не зашифрован!");
            else
            {
                Close();
            }
        }

        private void Form0_Load(object sender, EventArgs e)
        {
            if ((!(File.Exists("Info.txt"))) && (!(File.Exists("Info.txt.enc"))))
            {
                FileStream file = new FileStream("Info.txt", FileMode.Create);
                StreamWriter fnew = new StreamWriter(file);
                fnew.WriteLine("admin" + ' ' + "" + ' ' + "0" + ' ' + "0");
                fnew.Close();
            }
        }
    }
}
