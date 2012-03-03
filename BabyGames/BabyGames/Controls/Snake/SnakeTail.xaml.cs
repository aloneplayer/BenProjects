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

namespace BabyGames.Controls.Snake
{
    public partial class SnakeTail : SnakeBody
    {
        public SnakeTail()
        {
            InitializeComponent();
        }

        protected override RotateTransform GetRT()
        {
            return this.rtTail;
        }
    }
}
