namespace SymmetricEncryptionTest
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            comboBoxAlgorithm = new ComboBox();
            buttonGenerateKeyIV = new Button();
            textBoxKey = new TextBox();
            textBoxIV = new TextBox();
            textBoxPlainTextASCII = new TextBox();
            textBoxPlainTextHEX = new TextBox();
            textBoxCipherTextASCII = new TextBox();
            textBoxCipherTextHEX = new TextBox();
            buttonEncrypt = new Button();
            buttonDecrypt = new Button();
            labelEncryptTime = new Label();
            labelDecryptTime = new Label();
            labelAlgorithm = new Label();
            labelKey = new Label();
            labelIV = new Label();
            labelPlainTextASCII = new Label();
            labelPlainTextHEX = new Label();
            labelCipherTextASCII = new Label();
            labelCipherTextHEX = new Label();
            SuspendLayout();
            // 
            // comboBoxAlgorithm
            // 
            comboBoxAlgorithm.FormattingEnabled = true;
            comboBoxAlgorithm.Items.AddRange(new object[] { "DES", "AES" });
            comboBoxAlgorithm.Location = new Point(120, 13);
            comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            comboBoxAlgorithm.Size = new Size(152, 28);
            comboBoxAlgorithm.TabIndex = 0;
            comboBoxAlgorithm.SelectedIndexChanged += comboBoxAlgorithm_SelectedIndexChanged;
            // 
            // buttonGenerateKeyIV
            // 
            buttonGenerateKeyIV.Location = new Point(16, 40);
            buttonGenerateKeyIV.Name = "buttonGenerateKeyIV";
            buttonGenerateKeyIV.Size = new Size(256, 23);
            buttonGenerateKeyIV.TabIndex = 1;
            buttonGenerateKeyIV.Text = "Generate Key and IV";
            buttonGenerateKeyIV.UseVisualStyleBackColor = true;
            buttonGenerateKeyIV.Click += buttonGenerateKeyIV_Click;
            // 
            // textBoxKey
            // 
            textBoxKey.Location = new Point(120, 69);
            textBoxKey.Name = "textBoxKey";
            textBoxKey.Size = new Size(152, 27);
            textBoxKey.TabIndex = 2;
            // 
            // textBoxIV
            // 
            textBoxIV.Location = new Point(120, 95);
            textBoxIV.Name = "textBoxIV";
            textBoxIV.Size = new Size(152, 27);
            textBoxIV.TabIndex = 3;
            // 
            // textBoxPlainTextASCII
            // 
            textBoxPlainTextASCII.Location = new Point(120, 121);
            textBoxPlainTextASCII.Name = "textBoxPlainTextASCII";
            textBoxPlainTextASCII.Size = new Size(152, 27);
            textBoxPlainTextASCII.TabIndex = 4;
            // 
            // textBoxPlainTextHEX
            // 
            textBoxPlainTextHEX.Location = new Point(120, 147);
            textBoxPlainTextHEX.Name = "textBoxPlainTextHEX";
            textBoxPlainTextHEX.Size = new Size(152, 27);
            textBoxPlainTextHEX.TabIndex = 5;
            // 
            // textBoxCipherTextASCII
            // 
            textBoxCipherTextASCII.Location = new Point(120, 173);
            textBoxCipherTextASCII.Name = "textBoxCipherTextASCII";
            textBoxCipherTextASCII.Size = new Size(152, 27);
            textBoxCipherTextASCII.TabIndex = 6;
            // 
            // textBoxCipherTextHEX
            // 
            textBoxCipherTextHEX.Location = new Point(120, 199);
            textBoxCipherTextHEX.Name = "textBoxCipherTextHEX";
            textBoxCipherTextHEX.Size = new Size(152, 27);
            textBoxCipherTextHEX.TabIndex = 7;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(16, 225);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(75, 23);
            buttonEncrypt.TabIndex = 8;
            buttonEncrypt.Text = "Encrypt";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(97, 225);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(75, 23);
            buttonDecrypt.TabIndex = 9;
            buttonDecrypt.Text = "Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // labelEncryptTime
            // 
            labelEncryptTime.AutoSize = true;
            labelEncryptTime.Location = new Point(13, 251);
            labelEncryptTime.Name = "labelEncryptTime";
            labelEncryptTime.Size = new Size(235, 20);
            labelEncryptTime.TabIndex = 10;
            labelEncryptTime.Text = "Time/message at encryption: 0 ms";
            // 
            // labelDecryptTime
            // 
            labelDecryptTime.AutoSize = true;
            labelDecryptTime.Location = new Point(13, 274);
            labelDecryptTime.Name = "labelDecryptTime";
            labelDecryptTime.Size = new Size(236, 20);
            labelDecryptTime.TabIndex = 11;
            labelDecryptTime.Text = "Time/message at decryption: 0 ms";
            // 
            // labelAlgorithm
            // 
            labelAlgorithm.AutoSize = true;
            labelAlgorithm.Location = new Point(13, 16);
            labelAlgorithm.Name = "labelAlgorithm";
            labelAlgorithm.Size = new Size(79, 20);
            labelAlgorithm.TabIndex = 12;
            labelAlgorithm.Text = "Algorithm:";
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(13, 72);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(36, 20);
            labelKey.TabIndex = 13;
            labelKey.Text = "Key:";
            // 
            // labelIV
            // 
            labelIV.AutoSize = true;
            labelIV.Location = new Point(13, 98);
            labelIV.Name = "labelIV";
            labelIV.Size = new Size(25, 20);
            labelIV.TabIndex = 14;
            labelIV.Text = "IV:";
            // 
            // labelPlainTextASCII
            // 
            labelPlainTextASCII.AutoSize = true;
            labelPlainTextASCII.Location = new Point(13, 124);
            labelPlainTextASCII.Name = "labelPlainTextASCII";
            labelPlainTextASCII.Size = new Size(114, 20);
            labelPlainTextASCII.TabIndex = 15;
            labelPlainTextASCII.Text = "Plain Text ASCII:";
            // 
            // labelPlainTextHEX
            // 
            labelPlainTextHEX.AutoSize = true;
            labelPlainTextHEX.Location = new Point(13, 150);
            labelPlainTextHEX.Name = "labelPlainTextHEX";
            labelPlainTextHEX.Size = new Size(107, 20);
            labelPlainTextHEX.TabIndex = 16;
            labelPlainTextHEX.Text = "Plain Text HEX:";
            // 
            // labelCipherTextASCII
            // 
            labelCipherTextASCII.AutoSize = true;
            labelCipherTextASCII.Location = new Point(13, 176);
            labelCipherTextASCII.Name = "labelCipherTextASCII";
            labelCipherTextASCII.Size = new Size(125, 20);
            labelCipherTextASCII.TabIndex = 17;
            labelCipherTextASCII.Text = "Cipher Text ASCII:";
            // 
            // labelCipherTextHEX
            // 
            labelCipherTextHEX.AutoSize = true;
            labelCipherTextHEX.Location = new Point(13, 202);
            labelCipherTextHEX.Name = "labelCipherTextHEX";
            labelCipherTextHEX.Size = new Size(118, 20);
            labelCipherTextHEX.TabIndex = 18;
            labelCipherTextHEX.Text = "Cipher Text HEX:";
            // 
            // Form1
            // 
            ClientSize = new Size(294, 320);
            Controls.Add(labelCipherTextHEX);
            Controls.Add(labelCipherTextASCII);
            Controls.Add(labelPlainTextHEX);
            Controls.Add(labelPlainTextASCII);
            Controls.Add(labelIV);
            Controls.Add(labelKey);
            Controls.Add(labelAlgorithm);
            Controls.Add(labelDecryptTime);
            Controls.Add(labelEncryptTime);
            Controls.Add(buttonDecrypt);
            Controls.Add(buttonEncrypt);
            Controls.Add(textBoxCipherTextHEX);
            Controls.Add(textBoxCipherTextASCII);
            Controls.Add(textBoxPlainTextHEX);
            Controls.Add(textBoxPlainTextASCII);
            Controls.Add(textBoxIV);
            Controls.Add(textBoxKey);
            Controls.Add(buttonGenerateKeyIV);
            Controls.Add(comboBoxAlgorithm);
            Name = "Form1";
            Text = "Symmetric Encryption Test";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.Button buttonGenerateKeyIV;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxIV;
        private System.Windows.Forms.TextBox textBoxPlainTextASCII;
        private System.Windows.Forms.TextBox textBoxPlainTextHEX;
        private System.Windows.Forms.TextBox textBoxCipherTextASCII;
        private System.Windows.Forms.TextBox textBoxCipherTextHEX;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.Label labelEncryptTime;
        private System.Windows.Forms.Label labelDecryptTime;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelIV;
        private System.Windows.Forms.Label labelPlainTextASCII;
        private System.Windows.Forms.Label labelPlainTextHEX;
        private System.Windows.Forms.Label labelCipherTextASCII;
        private System.Windows.Forms.Label labelCipherTextHEX;
    }
}
