namespace Защита_Данных_1
{
    partial class Form0
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Decrypt = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.Button();
            this.PassFrase1 = new System.Windows.Forms.TextBox();
            this.InputFName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.OutputFName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Парольная фраза";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 182);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 24;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Имя исходного файла";
            // 
            // Decrypt
            // 
            this.Decrypt.Enabled = false;
            this.Decrypt.Location = new System.Drawing.Point(22, 169);
            this.Decrypt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(136, 35);
            this.Decrypt.TabIndex = 21;
            this.Decrypt.Text = "Расшифровать";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Enabled = false;
            this.Encrypt.Location = new System.Drawing.Point(224, 169);
            this.Encrypt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(132, 35);
            this.Encrypt.TabIndex = 20;
            this.Encrypt.Text = "Зашифровать";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(224, 74);
            this.Browse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(128, 35);
            this.Browse.TabIndex = 19;
            this.Browse.Text = "Выбор файла";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // PassFrase1
            // 
            this.PassFrase1.Location = new System.Drawing.Point(224, 129);
            this.PassFrase1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PassFrase1.Name = "PassFrase1";
            this.PassFrase1.Size = new System.Drawing.Size(588, 26);
            this.PassFrase1.TabIndex = 17;
            this.PassFrase1.TextChanged += new System.EventHandler(this.PassFrase1_TextChanged);
            // 
            // InputFName
            // 
            this.InputFName.Location = new System.Drawing.Point(224, 34);
            this.InputFName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InputFName.Name = "InputFName";
            this.InputFName.Size = new System.Drawing.Size(588, 26);
            this.InputFName.TabIndex = 15;
            this.InputFName.TextChanged += new System.EventHandler(this.PassFrase1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 182);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 30;
            this.label4.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(699, 169);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 31;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OutputFName
            // 
            this.OutputFName.Location = new System.Drawing.Point(816, 34);
            this.OutputFName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OutputFName.Name = "OutputFName";
            this.OutputFName.Size = new System.Drawing.Size(13, 26);
            this.OutputFName.TabIndex = 32;
            this.OutputFName.Visible = false;
            // 
            // Form0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 218);
            this.Controls.Add(this.OutputFName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Decrypt);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.PassFrase1);
            this.Controls.Add(this.InputFName);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form0";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расшифрование файла учетных записей";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form0_FormClosed);
            this.Load += new System.EventHandler(this.Form0_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Decrypt;
        private System.Windows.Forms.Button Encrypt;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox PassFrase1;
        private System.Windows.Forms.TextBox InputFName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox OutputFName;

    }
}