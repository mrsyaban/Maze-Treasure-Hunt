using System;
using TreasureHunterAlgo;
using System.IO;
using System.Windows.Media;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Runtime.InteropServices.ComTypes;


namespace Tubes2_BingChilling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Maze m;
        public MainWindow()
        {
            InitializeComponent();

            // binding to grid in xaml
            ////this.Maze = Find
            //Maze.RowDefinitions.Add(new RowDefinition());
            //Maze.RowDefinitions.Add(new RowDefinition());
            //var cell = new Grid();
            //cell.SetValue(Label.ContentProperty, " testtt");
            //cell.SetValue(Grid.RowProperty, 0);
            //cell.SetValue(Grid.ColumnProperty, 0);
            //cell.SetValue(BackgroundProperty, Brushes.White);
            
            //Maze.Children.Add(cell);
        }
        private void initializeMaze(object sender, EventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            double width = mainWindow.ActualWidth;
            double height = mainWindow.ActualHeight;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Browse(object sender, RoutedEventArgs e)
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
                    m = new Maze(filePath);
                    for (int i = 0; i < this.m.Width; i++)
                    {
                        mazeGrid.RowDefinitions.Add(new RowDefinition());
                    }
                    for (int i = 0; i < this.m.Length; i++)
                    {
                        mazeGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    TextBox cell;
                    for (int i = 0; i < this.m.Width; i++)
                    {
                        for (int j = 0; j < this.m.Length; j++)
                        {
                            cell = new TextBox();
                            SolidColorBrush brush;
                            if (this.m.Content[i][j] == "X")
                            {
                                brush = new SolidColorBrush(Colors.Black);
                            }
                            else if (this.m.Content[i][j] == "K")
                            {
                                brush = new SolidColorBrush(Colors.Red);
                            }
                            else if (this.m.Content[i][j] == "T")
                            {
                                brush = new SolidColorBrush(Colors.Gold);
                            }
                            else
                            {
                                brush = new SolidColorBrush(Colors.White);
                            }
                            cell.Background = brush;
                            Grid.SetColumn(cell, j);
                            Grid.SetRow(cell, i);
                            mazeGrid.Children.Add(cell);
                        }
                    }
                    //TextBox cell;
                    //for (int j = 0; j < this.solution.Length; j++)
                    //{
                    //    cell = get the textbox with solutions coordinate;
                    //    SolidColorBrush brush;
                    //    count++;
                    //    if (count == 1)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Black);
                    //    }
                    //    else if (count == 2)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Red);
                    //    }
                    //    else if (count == 3)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Gold);
                    //    }
                    //    else if (count == 4)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Gold);
                    //    }
                    //    else if (count == 5)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Gold);
                    //    }

                    //    cell.Background = brush;
                    //    Grid.SetColumn(cell, j);
                    //    Grid.SetRow(cell, i);
                    //    mazeGrid.Children.replace(cell);

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

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int> dataObjects = new List<int>();
            // add data objects to the list

            myListView.ItemsSource = dataObjects;

        }
    }

    


}
