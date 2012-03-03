﻿using System;
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
    public class BlockN2 : BlockBase
    {
        protected override void InitBlock()
        {
            Matrix = new int[,]
            {
                {0,1,0,0},
                {0,1,1,0},
                {0,0,1,0},
                {0,0,0,0}
            };

            maxIndex = 1;
        }

        public override int[,] GetRotatedMatrix()
        {
            switch (GetNextIndex())
            {
                case 0:
                    return new int[,]
                    {
                        {0,1,0,0},
                        {0,1,1,0},
                        {0,0,1,0},
                        {0,0,0,0}
                    };
                case 1:
                    return new int[,]
                    {
                        {0,0,1,1},
                        {0,1,1,0},
                        {0,0,0,0},
                        {0,0,0,0}
                    };
                default:
                    return Matrix;
            }
        }

        public override Color Color
        {
            get { return Utilities.GetColor("#22ccaa"); }
        }
        public override int YOffset
        {
            get { return 2; }
        }
    }
}
