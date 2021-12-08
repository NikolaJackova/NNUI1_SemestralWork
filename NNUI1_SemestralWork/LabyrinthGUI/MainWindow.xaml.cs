using LabyrinthFindingPath;
using LabyrinthFindingPath.Search;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabyrinthGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FindingPath FindingPath { get; set; }
        private GuiUtility GuiUtility { get; set; }

        #region Initialization
        public MainWindow()
        {
            InitializeComponent();
            InitializeAttributes();
        }
        private void InitializeAttributes()
        {
            RenderOptions.SetBitmapScalingMode(gridForImage, BitmapScalingMode.NearestNeighbor);
            Panel.SetZIndex(canvas, 5);
            ButtonFindPath.IsEnabled = false;
            MenuItemExport.IsEnabled = false;
            GuiUtility = new GuiUtility((SolidColorBrush)new BrushConverter().ConvertFrom("#538D5B"), (SolidColorBrush)new BrushConverter().ConvertFrom("#F97D10"));
        }
        #endregion

        #region Window Actions
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            GuiUtility.DeletePoints();
            ClearLabels();
            ButtonFindPath.IsEnabled = false;
            MenuItemExport.IsEnabled = false;
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

                gridForImage.Width = image.Source.Width;
                gridForImage.Height = image.Source.Height;
                FindingPath = new FindingPath(new System.Drawing.Bitmap(fileName));
            }
        }
        private void MenuItemExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FindingPath?.ExportPath();
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void ButtonFindPath_Click(object sender, RoutedEventArgs e)
        {
            FindingPath.SetSearch(GuiUtility.StartPoint, GuiUtility.EndPoint);
            FindingPath.Labyrinth.Agent.ChangePosition += ColorPath;
            try
            {
                FindingPath.Search();
                labelTotalIteration.Content = FindingPath.Iteration;
                labelPathCost.Content = FindingPath.Path.ToArray()[FindingPath.Path.Count - 1].PathTotal;
                MenuItemExport.IsEnabled = true;
            }
            catch (SearchException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Event Handlers
        private void Image_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement)sender);
            int row = (int)position.Y;
            int column = (int)position.X;
            if (!FindingPath.CheckValidPosition(row, column))
            {
                MessageBox.Show("Start position is not valid position!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SetStartPoint(row, column);
            EnableButton();
            ClearCanvasPath();
        }
        private void Image_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement)sender);
            int row = (int)position.Y;
            int column = (int)position.X;
            if (!FindingPath.CheckValidPosition(row, column))
            {
                MessageBox.Show("Destination position is not valid position!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SetEndPoint(row, column);
            EnableButton();
            ClearCanvasPath();
        }
        private void ColorPath(object sender, EventArgs e)
        {
            Position item = FindingPath.Labyrinth.Agent.Position;
            if (item != null)
            {
                int x = item.Column;
                int y = item.Row;

                Rectangle rectangle = GuiUtility.CreateRectangle(item.Direction);

                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
                canvas.Children.Add(rectangle);
            }
        }
        #endregion

        private void SetStartPoint(int row, int column)
        {
            canvas.Children.Remove(GuiUtility.StartPoint?.POI);
            GuiUtility.SetStartPoint(row, column);
            Panel.SetZIndex(GuiUtility.StartPoint.POI, 10);
            Canvas.SetLeft(GuiUtility.StartPoint.POI, column);
            Canvas.SetTop(GuiUtility.StartPoint.POI, row);
            labelStartPosition.Content = "[" + row + "," + column + "]";
            canvas.Children.Add(GuiUtility.StartPoint.POI);
        }
        private void SetEndPoint(int row, int column)
        {
            canvas.Children.Remove(GuiUtility.EndPoint?.POI);
            GuiUtility.SetEndPoint(row, column);
            Panel.SetZIndex(GuiUtility.EndPoint.POI, 10);
            Canvas.SetLeft(GuiUtility.EndPoint.POI, column);
            Canvas.SetTop(GuiUtility.EndPoint.POI, row);
            labelEndPosition.Content = "[" + row + "," + column + "]";
            canvas.Children.Add(GuiUtility.EndPoint.POI);
        }
        private void ClearCanvasPath()
        {
            foreach (var item in GuiUtility.RectanglePath)
            {
                canvas.Children.Remove(item);
            }
        }
        private void EnableButton()
        {
            if (canvas.Children.Contains(GuiUtility.StartPoint?.POI) && canvas.Children.Contains(GuiUtility.EndPoint?.POI))
            {
                ButtonFindPath.IsEnabled = true;
            }
        }
        private void ClearLabels()
        {
            labelEndPosition.Content = "";
            labelStartPosition.Content = "";
            labelPathCost.Content = "";
            labelTotalIteration.Content = "";
        }
    }
}
