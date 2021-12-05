using LabyrinthFindingPath;
using LabyrinthFindingPath.Search;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LabyrinthGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FindingPath FindingPath { get; set; }
        public PointOfInterest StartPoint { get; set; }
        public PointOfInterest EndPoint { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(gridForImage, BitmapScalingMode.NearestNeighbor);
            Panel.SetZIndex(canvas, 10);
        }
        private void ButtonFindPath_Click(object sender, RoutedEventArgs e)
        {
            FindingPath.SetSearch(StartPoint, EndPoint);
            try
            {
                FindingPath.Search();
                ColorPath();
            }
            catch (SearchException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ColorPath()
        {
            DispatcherTimer drawingTimer = new DispatcherTimer();
            drawingTimer.Tick += new EventHandler(DrawingTimer_Tick);
            drawingTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            drawingTimer.Start();
        }

        private void DrawingTimer_Tick(object sender, EventArgs e)
        {
            AStarNode item = FindingPath.GetPathItem();
            if (item != null)
            {
                int x = item.Position.Column;
                int y = item.Position.Row;

                Rectangle point = new Rectangle
                {
                    Width = 1,
                    Height = 1
                };
                /*LinearGradientBrush gradientFillRectangle = new LinearGradientBrush();
                gradientFillRectangle.StartPoint = new Point(0, 0.5);
                gradientFillRectangle.EndPoint = new Point(1, 0.5);
                gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
                gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
                gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
                gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));*/
                point.Fill = Brushes.Red;
                Canvas.SetLeft(point, x);
                Canvas.SetTop(point, y);
                canvas.Children.Add(point);
            }
            else
            {
                ((DispatcherTimer)sender).Stop();
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.bmp)|*.png;*.bmp"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = openFileDialog.FileName;
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(fileName)),
                    Stretch = Stretch.Uniform
                };
                image.MouseLeftButtonDown += Image_LeftMouseDown;
                image.MouseRightButtonDown += Image_RightMouseDown;
                gridForImage.Children.Add(image);
                FindingPath = new FindingPath(new System.Drawing.Bitmap(fileName));
            }
        }
        private void Image_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement)sender);
            int row = (int)position.Y;
            int column = (int)position.X;
            try
            {
                FindingPath.CheckValidPosition(row, column);
            }
            catch (LabyrinthException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (StartPoint != null)
            {
                canvas.Children.Remove(StartPoint.POI);
            }
            StartPoint = new PointOfInterest(new Rectangle
            {
                Width = 1,
                Height = 1,
                Fill = Brushes.Red
            }, row, column);
            Canvas.SetLeft(StartPoint.POI, column);
            Canvas.SetTop(StartPoint.POI, row);
            canvas.Children.Add(StartPoint.POI);
        }
        private void Image_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement)sender);
            int row = (int)position.Y;
            int column = (int)position.X;
            try
            {
                FindingPath.CheckValidPosition(row, column);
            }
            catch (LabyrinthException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (EndPoint != null)
            {
                canvas.Children.Remove(EndPoint.POI);
            }
            EndPoint = new PointOfInterest(new Rectangle
            {
                Width = 1,
                Height = 1,
                Fill = Brushes.Green
            }, row, column);
            Canvas.SetLeft(EndPoint.POI, column);
            Canvas.SetTop(EndPoint.POI, row);
            canvas.Children.Add(EndPoint.POI);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
