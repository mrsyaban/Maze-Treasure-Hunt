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
                this.pathFile = filePath;
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

                }
            }
        }
        private void Start(object sender, RoutedEventArgs e) 
        {
            int nodes = 0;

            // Start the Stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Tuple<string, int, int, int>> resultPath;


            // BFS search with TSP
            if (bfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                // Set up TSP algorithm
            }
            // DFS search with TSP
            else if (bfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                // Set up TSP algorithm
            }
            else if (bfsButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);
                bfs.run();

                resultPath = bfs.getResultPath();
                for (int i = 0; i < resultPath.Count; i++)
                {
                    routeBox.Text += resultPath[i].ToString();

                }

            }
            // DFS search
            else if (dfsButton.IsChecked == true)
            {
                DFS dfs = new DFS(this.pathFile);
                dfs.run();

                resultPath = dfs.getResultPath();
                for (int i = 0; i < resultPath.Count; i++)
                {
                    routeBox.Text += resultPath[i].ToString();

                }
                
            }
            // No radio button is checked
            else
            {
                MessageBox.Show("Please select a desired algorithm ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Tuple<string, int, int, int> emptyPath = Tuple.Create("None", 0, 0, 0);
                List<Tuple<string, int, int, int>> emptyList = new List<Tuple<string, int, int, int>>();
                emptyList.Add(emptyPath);
                resultPath = emptyList;
            }
            TextBox res;
            int count = 0;

            for (int k = 0; k < resultPath.Count; k++)
            {
                int row = resultPath[k].Item3;
                int col = resultPath[k].Item4;
                count = resultPath[k].Item2;
                for (int j = 0; j < mazeGrid.ColumnDefinitions.Count; j++)
                {
                    for (int i = 0; i < mazeGrid.RowDefinitions.Count; i++)
                    {
                        if (i == row && j == col)
                        {
                            res = new TextBox();
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


                        }
                    }

                }
            }
            // Perform some process
            showProgress(sender, e);

            // Stop the Stopwatch and output the elapsed time to a TextBox control
            stopwatch.Stop();

            timeBox.Text = string.Format("{0} ms", stopwatch.Elapsed.TotalMilliseconds.ToString());
            stepsBox.Text = resultPath.Count.ToString();
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

        
        private void Filename_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }

    


}
