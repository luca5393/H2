using FiveWordsFiveLettersLib;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Concurrent;

namespace FiveWordsFiveLettersWPF
{
    public partial class MainWindow : Window
    {
        private WordSearcher wordSearcher;
        private string file;
        private ConcurrentBag<string> result;

        public MainWindow()
        {
            InitializeComponent();
            wordSearcher = new WordSearcher();
        }
        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("The file does not exist!", "File Check", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StartButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
            ProgressBar.Value = 0;
            wordSearcher.ProgressUpdated += OnProgressUpdated;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StatusText.Text = "Searching...";
            result = await Task.Run(() => wordSearcher.SearchWords(file));
            sw.Stop();

            StartButton.IsEnabled = true;

            StatusText.Text = $"Total count: {result.Count}{Environment.NewLine}" +
                $"Time for Search: {sw.ElapsedMilliseconds} ms";
            SaveButton.IsEnabled = true;
        }
        private void OnProgressUpdated(int progress, int total)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = wordSearcher.CalculatePercentage();
            });
        }
        private void FilePathTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            file = FilePathTextBox.Text;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save a Text File"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    File.WriteAllLines(filePath, result);
                    MessageBox.Show($"Data saved to {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while writing to the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        
        }
    }
}
