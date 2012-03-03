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

namespace BabyGames.Controls
{
    public partial class GameSplashDialog : ChildWindow
    {
        public GameSplashDialog()
        {
            InitializeComponent();
        }

        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

