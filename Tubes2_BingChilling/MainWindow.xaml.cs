using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace Tubes2_BingChilling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (System.IO.Path.GetExtension(filePath) != ".txt")
                {
                    MessageBox.Show("Please select a .txt file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Do something with the selected file path
                    // string selectedFilePath = openFileDialog.FileName;
                    // Update the TextBox with the selected filename
                    // FileName.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
                    // Read the content of the selected file
                    string fileContent = File.ReadAllText(openFileDialog.FileName);

                    // Parse the file content into a two-dimensional array of characters
                    char[,] matrix = ParseMatrix(fileContent);

                    // Display the matrix in the TextBox control
                    DisplayMatrix(matrix);
                }
            }
        }
        private char[,] ParseMatrix(string fileContent)
        {
            // Split the file content into lines
            string[] lines = fileContent.Split('\n', '\r');

            // Determine the matrix size from the first line
            int n = lines[0].Trim().Length;

            // Create a two-dimensional array of characters to store the matrix data
            char[,] matrix = new char[n, n];

            // Parse the lines into the matrix array
            for (int i = 0; i < n; i++)
            {
                string line = lines[i].Trim();

                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = line[j];
                }
            }

            return matrix;
        }
        private void DisplayMatrix(char[,] matrix)
        {
            // Clear the TextBox control
            display.Clear();

            // Determine the matrix size
            int n = matrix.GetLength(0);

            // Iterate over the matrix and display the characters on the TextBox control
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    display.Text += matrix[i, j] + " ";
                }

                display.Text += Environment.NewLine;
            }
        }
        private void Filename_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Start(object sender, RoutedEventArgs e) 
        {
            int steps = 0;
            int nodes = 0;
            string route = string.Empty;

            // Start the Stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Perform some process
            showProgress(sender, e);

            // Stop the Stopwatch and output the elapsed time to a TextBox control
            stopwatch.Stop();

            timeBox.Text = string.Format("Time taken: {0}", stopwatch.Elapsed.ToString());
            routeBox.Text = route;
            stepsBox.Text = steps.ToString();
            nodesBox.Text = nodes.ToString();
        }
        private void showProgress(object sender, RoutedEventArgs e)
        {
            // Create a new BackgroundWorker object and set its properties
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            // Set the event handlers for the worker's events
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            // Start the worker
            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Simulate work by sleeping for 50 milliseconds
                System.Threading.Thread.Sleep(50);

                // Report progress to the UI thread
                (sender as BackgroundWorker).ReportProgress(i);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the progress bar value with the progress reported by the worker
            progress.Value = e.ProgressPercentage;
        }

        private void stepsBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    


}
