using Microsoft.Win32;
using System;
using System.Numerics;
using System.Text;
using System.Windows;


namespace lab3
{
    public partial class MainWindow : Window
    {
        private string encryptPath;
        private string decryptPath;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void SelectEncryptFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                encryptPath = dlg.FileName;
                encryptFileBox.Text = encryptPath;
            }
        }
        private void SelectDecryptFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                decryptPath = dlg.FileName;
                decryptFileBox.Text = decryptPath;
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            BigInteger p = 0, q = 0, b = 0;
            try
            {
                p = BigInteger.Parse(pBox.Text);
                q = BigInteger.Parse(qBox.Text);
                b = BigInteger.Parse(bBox.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input");
                return;
            }
            if (!(Validator.Validate(p, q, b)))
                return;

            if (string.IsNullOrEmpty(encryptPath))
            {
                MessageBox.Show("Select file to encrypt");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() != true) return;

            BigInteger n = p * q;

            StringBuilder output = new StringBuilder();
            StringBuilder src = new StringBuilder();

            Encryptor.Encrypt(
                encryptPath,
                dlg.FileName,
                n,
                b,
                (s) => src.AppendLine(s),
                (s) => output.AppendLine(s)
            );

            srcOutputBox.Text = src.ToString();
            resultOutputBox.Text = output.ToString();

            MessageBox.Show("Encryption completed");
        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            BigInteger p = 0, q = 0, b = 0;
            try
            {
                p = BigInteger.Parse(pBox.Text);
                q = BigInteger.Parse(qBox.Text);
                b = BigInteger.Parse(bBox.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input");
                return;
            }
            if (!Validator.Validate(p, q, b))
                return;

            if (string.IsNullOrEmpty(decryptPath))
            {
                MessageBox.Show("Select file to decrypt");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() != true) return;

            StringBuilder src = new StringBuilder();
            StringBuilder output = new StringBuilder();

            Decryptor.Decrypt(
                decryptPath,
                dlg.FileName,
                p,
                q,
                b,
                (s) => src.AppendLine(s),
                (s) => output.AppendLine(s)
            );

            srcOutputBox.Text = src.ToString();
            resultOutputBox.Text = output.ToString();

            MessageBox.Show("Decryption completed");
        }
    }
}