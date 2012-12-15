using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RushHour
{
    /// <summary>
    /// Interaction logic for Car.xaml
    /// </summary>
    public partial class Car : UserControl
    {
        public Orientation Orientation { set; get; }
        public int Length { set; get; }
        public Color Color { set; get; }
        public int Row { set; get; }
        public int Column { set; get; }

        public Car(Orientation orientation, int length, Color color, int row, int column)
        {
            InitializeComponent();

            Orientation = orientation;
            Length = length;
            Color = color;
            Row = row;
            Column = column;
            DrawCar();
        }

        private void DrawCar()
        {
            SolidColorBrush sb = new SolidColorBrush(Color);
            p1.Fill = sb;
            p2.Fill = sb;
            p3.Fill = sb;
            p4.Fill = sb;
            p5.Fill = sb;
            p6.Fill = sb;
            p7.Fill = sb;
            p8.Fill = sb;
            p9.Fill = sb;
            p10.Fill = sb;
            p11.Fill = sb;
            p12.Fill = sb;

            if (Length == 2)
            {
                scaleCar.ScaleY = 0.68;
            }
            if (Orientation == Orientation.Horizontal)
            {
                rotateCar.Angle = -90.0;
                translateCar.Y = 55.0;
            }
            else
            {
                translateCar.X = 2.0;
                translateCar.Y = 2.0;
            }
        }
    }
}
