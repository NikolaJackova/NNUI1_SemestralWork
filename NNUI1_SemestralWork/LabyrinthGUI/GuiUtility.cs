using LabyrinthFindingPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LabyrinthGUI
{
    class GuiUtility
    {
        public PointOfInterest StartPoint { get; set; }
        public PointOfInterest EndPoint { get; set; }
        public List<Rectangle> RectanglePath { get; private set; }
        private SolidColorBrush StartBrush { get; set; }
        private SolidColorBrush EndBrush { get; set; }
        private RotateTransform UpRotate { get; set; }
        private RotateTransform DownRotate { get; set; }
        private RotateTransform LeftRotate { get; set; }
        private RotateTransform RightRotate { get; set; }

        public GuiUtility(SolidColorBrush startBrush, SolidColorBrush endBrush)
        {
            UpRotate = new RotateTransform(0, 0.5, 0.5);
            DownRotate = new RotateTransform(180, 0.5, 0.5);
            RightRotate = new RotateTransform(90, 0.5, 0.5);
            LeftRotate = new RotateTransform(-90, 0.5, 0.5);
            RectanglePath = new List<Rectangle>();

            StartBrush = startBrush;
            EndBrush = endBrush;
        }
        public void SetStartPoint(int row, int column)
        {
            StartPoint = new PointOfInterest(new Rectangle
            {
                Width = 1,
                Height = 1,
                Fill = StartBrush
            }, row, column);
        }
        public void SetEndPoint(int row, int column)
        {
            EndPoint = new PointOfInterest(new Rectangle
            {
                Width = 1,
                Height = 1,
                Fill = EndBrush
            }, row, column);
        }
        public void DeletePoints()
        {
            StartPoint = null;
            EndPoint = null;
        }
        public Rectangle CreateRectangle(Direction direction)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = 1,
                Height = 1,
                Fill = CreateLinearGradientBrush()
            };
            switch (direction)
            {
                case Direction.NORTH:
                    rectangle.RenderTransform = UpRotate;
                    break;
                case Direction.WEST:
                    rectangle.RenderTransform = LeftRotate;
                    break;
                case Direction.SOUTH:
                    rectangle.RenderTransform = DownRotate;
                    break;
                case Direction.EAST:
                    rectangle.RenderTransform = RightRotate;
                    break;
                default:
                    MessageBox.Show("Unsupported direction!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            RectanglePath.Add(rectangle);
            return rectangle;
        }
        private LinearGradientBrush CreateLinearGradientBrush()
        {
            LinearGradientBrush gradientFillRectangle = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5)
            };
            gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.Green, 0));
            gradientFillRectangle.GradientStops.Add(new GradientStop(Colors.Red, 1));
            return gradientFillRectangle;
        }
    }
}
