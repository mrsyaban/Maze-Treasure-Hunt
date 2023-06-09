﻿using System;
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
            List<Tuple<int, int, int>> historyPath;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Check which algorithm was selected
            if (bfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.runTSP());
                resultPath = bfs.getResultPath();
                historyPath = bfs.getHistoryPath();

            }
            else if (dfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.runTSP());
                resultPath = dfs.getResultPath();
                historyPath = dfs.getHistoryPath();

            }
            else if (bfsButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.run());
                resultPath = bfs.getResultPath();
                historyPath = bfs.getHistoryPath();

            }
            else if (dfsButton.IsChecked == true)
            {
                // Similar code as above for DFS algorithm
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.run());
                resultPath = dfs.getResultPath();
                historyPath = dfs.getHistoryPath();
                
            }
            else
            {
                MessageBox.Show("Please select a desired algorithm ", "No algorithm is selected", MessageBoxButton.OK, MessageBoxImage.Error);
                Tuple<string, int, int, int> emptyPath = Tuple.Create("None", 0, 0, 0);
                List<Tuple<string, int, int, int>> emptyList = new List<Tuple<string, int, int, int>>();
                emptyList.Add(emptyPath);
                resultPath = emptyList;
                historyPath = new List<Tuple<int, int, int>>();
            }
            // Visualize the searching
            for (int i = 0; i < historyPath.Count; i++)
            {
                await Task.Delay(300); // introduce a 0.5 second delay between each step

                Tuple<int, int, int> currentStep = historyPath[i];

                // Update the UI
                Dispatcher.Invoke(() =>
                {
                    int row = currentStep.Item2;
                    int col = currentStep.Item3;
                    int count = currentStep.Item1;

                    TextBox res = new TextBox();
                    TextBox past = new TextBox();
                    SolidColorBrush resBrush;
                    SolidColorBrush pastBrush;

                    if (i == 0)
                    {
                        if (count == 1)
                        {
                            resBrush = new SolidColorBrush(Colors.LightBlue);
                        }
                        else if (count == 2)
                        {
                            resBrush = new SolidColorBrush(Colors.Blue);
                        }
                        else
                        {
                            resBrush = new SolidColorBrush(Colors.DarkBlue);
                        }
                        
                        Grid.SetColumn(res, col);
                        Grid.SetRow(res, row);
                        res.Background = resBrush;
                        mazeGrid.Children.Add(res);
                    }
                    else // i >= 1
                    {
                        if (count == 1)
                        {
                            resBrush = new SolidColorBrush(Colors.LightBlue);
                        }
                        else if (count == 2)
                        {
                            resBrush = new SolidColorBrush(Colors.Blue);
                        }
                        else
                        {
                            resBrush = new SolidColorBrush(Colors.DarkBlue);
                        }

                        pastBrush = new SolidColorBrush(Colors.Pink);

                        Tuple<int, int, int> pastStep = historyPath[i - 1];

                        int pastRow = pastStep.Item2;
                        int pastCol = pastStep.Item3;

                        Grid.SetColumn(past, pastCol);
                        Grid.SetRow(past, pastRow);
                        past.Background = pastBrush;
                        mazeGrid.Children.Add(past);

                        Grid.SetColumn(res, col);
                        Grid.SetRow(res, row);
                        res.Background = resBrush;
                        mazeGrid.Children.Add(res);
                    }
                });
            }



            // Visualize the grid step by step 
            for (int i = 0; i < resultPath.Count; i++)
            {
                await Task.Delay(300); // introduce a 0.5 second delay between each step

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

            stepSlider.ValueChanged += StepSlider_ValueChanged;
            // convert time into double
            double timeCount = stopwatch.Elapsed.TotalSeconds;
            timeBox.Text = string.Format("{0} ms",timeCount);
            // Output the route to routeBox
            routeBox.Clear();
            for (int i = 0; i < resultPath.Count; i++)
            {
                routeBox.Text += resultPath[i].Item1;
                routeBox.Text += "-";
            }
            // Output the number of steps
            stepsBox.Text = resultPath.Count.ToString();
            // Output the number of nodes;
            nodesBox.Text = historyPath.Count.ToString();
        }
        private async void StepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            List<Tuple<int, int, int>> resultPath;
            if (bfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.runTSP());
                resultPath = bfs.getHistoryPath();

            }
            else if (dfsButton.IsChecked == true && tspButton.IsChecked == true)
            {
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.runTSP());
                resultPath = dfs.getHistoryPath();

            }
            else if (bfsButton.IsChecked == true)
            {
                BFS bfs = new BFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => bfs.run());
                resultPath = bfs.getHistoryPath();

            }
            else if (dfsButton.IsChecked == true)
            {
                // Similar code as above for DFS algorithm
                DFS dfs = new DFS(this.pathFile);

                // Execute the search algorithm on a separate thread using Task.Run()
                await Task.Run(() => dfs.run());
                resultPath = dfs.getHistoryPath();

            }
            else
            {
                MessageBox.Show("Please select a desired algorithm ", "No algorithm is selected", MessageBoxButton.OK, MessageBoxImage.Error);
                Tuple<int, int, int> emptyPath = Tuple.Create(0, 0, 0);
                List<Tuple<int, int, int>> emptyList = new List<Tuple<int, int, int>>();
                emptyList.Add(emptyPath);
                resultPath = emptyList;
            }
            int step = (int)e.NewValue;
            int maxStep = (int)(resultPath.Count - 1) * (int)(e.NewValue / stepSlider.Maximum);

            if (step >= 0 && step <= maxStep)
            {

                for (int i = 0; i <= maxStep; i++)
                {
                    int currentStepIndex = (int)((double)i / maxStep * (resultPath.Count - 1));
                    Tuple<int, int, int> currentStep = resultPath[i / (((int)stepSlider.TickFrequency))];

                    int row = currentStep.Item2;
                    int col = currentStep.Item3;

                    TextBox res = new TextBox();
                    TextBox past = new TextBox();
                    SolidColorBrush resBrush;
                    SolidColorBrush pastBrush;

                    if (currentStepIndex == 0)
                    {
                        resBrush = new SolidColorBrush(Colors.Blue);
                        Grid.SetColumn(res, col);
                        Grid.SetRow(res, row);
                        res.Background = resBrush;
                        mazeGrid.Children.Add(res);
                    }
                    else
                    {
                        resBrush = new SolidColorBrush(Colors.Yellow);
                        Grid.SetColumn(res, col);
                        Grid.SetRow(res, row);
                        res.Background = resBrush;
                        mazeGrid.Children.Add(res);
                    }


                }
            }
        }

    }

    


}
