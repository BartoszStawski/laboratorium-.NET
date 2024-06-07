using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SymmetricEncryptionTest
{
    public partial class Form1 : Form
    {
        private SymmetricAlgorithm algorithm;

        public Form1()
        {
            InitializeComponent();
            comboBoxAlgorithm.SelectedIndex = 0;
        }

        private void comboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxAlgorithm.SelectedItem.ToString())
            {
                case "DES":
                    algorithm = DES.Create();
                    break;
                case "AES":
                    algorithm = Aes.Create();
                    break;
            }
        }

        private void buttonGenerateKeyIV_Click(object sender, EventArgs e)
        {
            algorithm.GenerateKey();
            algorithm.GenerateIV();
            textBoxKey.Text = BitConverter.ToString(algorithm.Key).Replace("-", "");
            textBoxIV.Text = BitConverter.ToString(algorithm.IV).Replace("-", "");
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            byte[] plainText = Encoding.ASCII.GetBytes(textBoxPlainTextASCII.Text);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            byte[] cipherText = Encrypt(plainText, algorithm.Key, algorithm.IV);
            stopwatch.Stop();
            textBoxCipherTextASCII.Text = Encoding.ASCII.GetString(cipherText);
            textBoxCipherTextHEX.Text = BitConverter.ToString(cipherText).Replace("-", "");
            labelEncryptTime.Text = $"Time/message at encryption: {stopwatch.ElapsedMilliseconds} ms";
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            byte[] cipherText = ConvertHexStringToByteArray(textBoxCipherTextHEX.Text);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            byte[] plainText = Decrypt(cipherText, algorithm.Key, algorithm.IV);
            stopwatch.Stop();
            textBoxPlainTextASCII.Text = Encoding.ASCII.GetString(plainText);
            textBoxPlainTextHEX.Text = BitConverter.ToString(plainText).Replace("-", "");
            labelDecryptTime.Text = $"Time/message at decryption: {stopwatch.ElapsedMilliseconds} ms";
        }

        private byte[] Encrypt(byte[] plainText, byte[] key, byte[] iv)
        {
            ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv);
            return PerformCryptography(plainText, encryptor);
        }

        private byte[] Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv);
            return PerformCryptography(cipherText, decryptor);
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        private byte[] ConvertHexStringToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }
    }
}
