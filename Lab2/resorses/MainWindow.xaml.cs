using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    public partial class MainWindow : Window
    {
        private string selectedFile;
        private Logic logic = new Logic();
        string keyStream1;
        string keyStream2;
        string keyStream3;

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
            string key2 = SeedBox2.Text;
            string key3 = SeedBox3.Text;

            if (!Logic.IsValidKey(key1) || !Logic.IsValidKey(key2) || !Logic.IsValidKey(key3))
            {
                MessageBox.Show("Ключ должен состоять из 36 символов 0 и 1.");
                return;
            }

            byte[] data = File.ReadAllBytes(selectedFile);
            var result = logic.Encrypt(key1, key2, key3, data);

            byte[] encrypted = result.Item1;
            keyStream1 = result.Item2;
            keyStream2 = result.Item3;
            keyStream3 = result.Item4;

            OriginalBox.Text = Logic.BytesToBits(data);
            EncryptedBox.Text = Logic.BytesToBits(encrypted);
            File.WriteAllBytes(selectedFile + ".enc", encrypted);
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string key1 = SeedBox1.Text;
            string key2 = SeedBox2.Text;
            string key3 = SeedBox3.Text;

            byte[] data = File.ReadAllBytes(selectedFile);

            var result = logic.Encrypt(key1, key2, key3, data);
            byte[] decrypted = result.Item1;
            keyStream1 = result.Item2;
            keyStream2 = result.Item3;
            keyStream3 = result.Item4;

            string outputFile = selectedFile + ".dec";

            File.WriteAllBytes(outputFile, decrypted);

            OriginalBox.Text = Logic.BytesToBits(data);
            EncryptedBox.Text = Logic.BytesToBits(decrypted);

            Process.Start(new ProcessStartInfo(outputFile)
            {UseShellExecute = true});
        }
        private void ShowKey1_Click(object sender, RoutedEventArgs e)
        {
            KeyStreamBox.Text = keyStream1;
        }

        private void ShowKey2_Click(object sender, RoutedEventArgs e)
        {
            KeyStreamBox.Text = keyStream2;
        }

        private void ShowKey3_Click(object sender, RoutedEventArgs e)
        {
            KeyStreamBox.Text = keyStream3;
        }
    }
}