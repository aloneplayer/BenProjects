using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BabyGames.Common;

namespace BabyGames.Tetris
{
    public class BlockCross:BlockBase
    {
        protected override void InitBlock()
        {
            Matrix = new int[,]
            {
                {0,1,0,0},
                {1,1,1,0},
                {0,1,0,0},
                {0,0,0,0}
            };

            maxIndex = 0;
        }

        public override int[,] GetRotatedMatrix()
        {
            return Matrix;
        }

        public override Color Color
        {
            get { return Utilities.GetColor("#115555"); }
        }
        public override int YOffset
        {
            get { return 2; }
        }
    }
}
