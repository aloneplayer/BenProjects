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

namespace BabyGames.Controls.Tetris
{
    public partial class BlockUnit : UserControl
    {
        public BlockUnit()
        {
            InitializeComponent();
        }

        public double Top
        {
            set
            {
                rectBlock.SetValue(Canvas.TopProperty, value);
            }
        }

        public double Left
        {
            set
            {
                rectBlock.SetValue(Canvas.LeftProperty, value);
            }
        }

        public Color? Color
        {
            set
            {
                if (value == null)
                {
                    rectBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    gradientStop.Color = (Color)value;
                    rectBlock.Visibility = Visibility.Visible;
                }
            }
            get
            {
                if (rectBlock.Visibility == Visibility.Collapsed)
                    return null;
                else
                    return gradientStop.Color;
            }
        }
    }
}
