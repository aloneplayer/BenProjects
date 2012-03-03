using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using BabyGames.Data;

namespace BabyGames
{
    public partial class ArithmeticBlock : UserControl
    {
        public Arithmetic Data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public ArithmeticBlock()
        {
            InitializeComponent();


        }
        public void Refresh()
        {
            this.textBlock.Text = this.Data.ToString();
        }

        public void HighLight()
        {
            border.BorderThickness = new Thickness(3);
            border.Margin = new Thickness(0);

        }
        public void RemoveHighLight()
        {
            border.BorderThickness = new Thickness(1);
            border.Margin = new Thickness(1);
        }

        public void Hint()
        {
            border.BorderBrush = new SolidColorBrush(Colors.Red);
            border.BorderThickness = new Thickness(3);
            border.Margin = new Thickness(0);

        }

        public void RemoveHint()
        {
            border.BorderBrush = new SolidColorBrush(Colors.Blue);
            border.BorderThickness = new Thickness(1);
            border.Margin = new Thickness(1);
        }

    }
}
