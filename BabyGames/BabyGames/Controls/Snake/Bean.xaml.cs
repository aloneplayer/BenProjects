﻿using System;
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

namespace BabyGames.Controls.Snake
{
    public partial class Bean : UserControl
    {
        public Bean()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Bean_Loaded);
        }

        void Bean_Loaded(object sender, RoutedEventArgs e)
        {
            this.BeanAnimation.Begin();
        }
    }
}
