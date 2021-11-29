using LabyrinthFindingPath;
using LabyrinthFindingPath.Search;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabyrinthGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Labyrinth Labyrinth { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(Canvas, BitmapScalingMode.NearestNeighbor);
        }
        private void ButtonFindPath_Click(object sender, RoutedEventArgs e)
        {
            AStarSearch aStarSearch = new AStarSearch(Labyrinth, new Position(7, 3));
            Stack<AStarNode> path = aStarSearch.SearchPath(out int iteration);

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
                Binding bindingWidth = new Binding("ActualWidth") { ElementName = "Canvas" };
                Binding bindingHeight = new Binding("ActualHeight") { ElementName = "Canvas" };
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(openFileDialog.FileName)),
                    Stretch = Stretch.Uniform
                };
                image.SetBinding(WidthProperty, bindingWidth);
                image.SetBinding(HeightProperty, bindingHeight);
                Canvas.Children.Add(image);
                Labyrinth = new Labyrinth(new System.Drawing.Bitmap(openFileDialog.FileName), new Position(1,1));
            }
        }
    }
}
