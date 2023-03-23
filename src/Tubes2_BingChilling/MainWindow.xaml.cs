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

using Services;
using System.Reflection;
using System.Windows.Threading;

namespace Tubes2_BingChilling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        string pathFile;
        Maze m;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Multiselect = false;
            
            if (openFileDialog.ShowDialog() == true)
            {

                string filePath = openFileDialog.FileName;
                // set the file path
                this.pathFile = filePath;
                FileName.Text = System.IO.Path.GetFileName(filePath);
                if (System.IO.Path.GetExtension(filePath) != ".txt")
                {
                    MessageBox.Show("Please select a .txt file.", "Invalid File Extension", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
        }
        private void Visualize(object sender, RoutedEventArgs e)
        {
            // Clear maze grid
            mazeGrid.RowDefinitions.Clear();
            mazeGrid.ColumnDefinitions.Clear();

            // Check that there is an input file
            if (string.IsNullOrEmpty(this.pathFile))
            {
                MessageBox.Show("Please select a file", "No File is Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            // Read the input file
            string[] lines = File.ReadAllLines(this.pathFile);

            // Concatenate all the lines into a single string and remove any spaces
            string content = string.Concat(lines).Replace(" ", "");

            // Check that the input file contains only the allowed characters
            string validChars = "KTRX";
            if (!content.All(c => validChars.Contains(c)))
            {
                MessageBox.Show("Invalid input file. Make sure the file only contains the characters: K, T, R, and X.", "Invalid characters", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Initialize maze grid
            m = new Maze(this.pathFile);

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
        }

        private async void Start(object sender, RoutedEventArgs e)
        {
            // Check that there is an input file
            if (string.IsNullOrEmpty(this.pathFile))
            {
                MessageBox.Show("Please select a file", "No File is Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            // Set up variables and objects
            int nodes = 0;
            List<Tuple<string, int, int, int>> resultPath;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Check which algorithm was selected
            if (bfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.runTSP());
                resultPath = bfs.getResultPath();

            }
            else if (dfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.runTSP());
                resultPath = dfs.getResultPath();

            }
            else if (bfsButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.run());
                resultPath = bfs.getResultPath();

            }
            else if (dfsButton.IsChecked == true)
            {
                // Similar code as above for DFS algorithm
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.run());
                resultPath = dfs.getResultPath();
                
            }
            else
            {
                MessageBox.Show("Please select a desired algorithm ", "No algorithm is selected", MessageBoxButton.OK, MessageBoxImage.Error);
                Tuple<string, int, int, int> emptyPath = Tuple.Create("None", 0, 0, 0);
                List<Tuple<string, int, int, int>> emptyList = new List<Tuple<string, int, int, int>>();
                emptyList.Add(emptyPath);
                resultPath = emptyList;
            }
            // Visualize the grid step by step 
            for (int i = 0; i < resultPath.Count; i++)
            {
                await Task.Delay(500); // introduce a 0.5 second delay between each step

                Tuple<string, int, int, int> currentStep = resultPath[i];

                // Update the UI
                Dispatcher.Invoke(() =>
                {
                    int row = currentStep.Item3;
                    int col = currentStep.Item4;
                    int count = currentStep.Item2;

                    TextBox res = new TextBox();
                    SolidColorBrush resBrush;

                    if (count == 1)
                    {
                        resBrush = new SolidColorBrush(Colors.LightGreen);
                    }
                    else if (count == 2)
                    {
                        resBrush = new SolidColorBrush(Colors.Green);
                    }
                    else if (count == 3)
                    {
                        resBrush = new SolidColorBrush(Colors.DarkGreen);
                    }
                    else if (count == 4)
                    {
                        resBrush = new SolidColorBrush(Colors.DarkOliveGreen);
                    }
                    else
                    {
                        resBrush = new SolidColorBrush(Colors.ForestGreen);
                    }

                    Grid.SetColumn(res, col);
                    Grid.SetRow(res, row);
                    res.Background = resBrush;
                    mazeGrid.Children.Add(res);
                });
            }
            // Stop the Stopwatch
            stopwatch.Stop();
            // convert time into double
            double timeCount = stopwatch.Elapsed.TotalSeconds;
            timeBox.Text = string.Format("{0} ms",timeCount);
            // Output the route to routeBox
            for (int i = 0; i < resultPath.Count; i++)
            {
                routeBox.Text += resultPath[i].ToString();
            }
            // Output the number of steps
            stepsBox.Text = "";
            // Output the number of nodes;
            nodesBox.Text = resultPath.Count.ToString();
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

    }

    


}
