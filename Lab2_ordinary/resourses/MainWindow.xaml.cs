using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace Lab2_ordinary
{
    public partial class MainWindow : Window
    {
        private string selectedFile;
        private Logic logic = new Logic();
        string keyStream1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                selectedFile = dialog.FileName;
                FilePathBox.Text = selectedFile;
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string key1 = SeedBox1.Text;

            if (!Logic.IsValidKey(key1))
            {
                MessageBox.Show("Ключ должен состоять из 36 символов 0 и 1.");
                return;
            }

            byte[] data = File.ReadAllBytes(selectedFile);
            var result = logic.Encrypt(key1, data);

            byte[] encrypted = result.Item1;
            keyStream1 = result.Item2;
            KeyStreamBox.Text = keyStream1;

            OriginalBox.Text = Logic.BytesToBits(data);
            EncryptedBox.Text = Logic.BytesToBits(encrypted);
            File.WriteAllBytes(selectedFile + ".enc", encrypted);
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string key1 = SeedBox1.Text;

            byte[] data = File.ReadAllBytes(selectedFile);

            var result = logic.Encrypt(key1,data);
            byte[] decrypted = result.Item1;
            keyStream1 = result.Item2;

            string outputFile = selectedFile + ".dec";

            File.WriteAllBytes(outputFile, decrypted);

            OriginalBox.Text = Logic.BytesToBits(data);
            EncryptedBox.Text = Logic.BytesToBits(decrypted);

            Process.Start(new ProcessStartInfo(outputFile)
            {UseShellExecute = true});
        }
        
    }
}