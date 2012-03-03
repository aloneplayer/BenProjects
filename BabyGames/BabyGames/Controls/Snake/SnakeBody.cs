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

namespace BabyGames.Controls
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public abstract class SnakeBody:UserControl
    {
        private Direction moveDirection;
        public Direction MoveDirection
        {
            get
            {
                return moveDirection;
            }
            set
            {
                moveDirection = value;

                RotateTransform rt = this.GetRT();

                switch (value)
                {
                    case Direction.Up:
                        rt.Angle = 0;
                        break;
                    case Direction.Down:
                        rt.Angle = 190;
                        break;
                    case Direction.Left:
                        rt.Angle = 270;
                        break;
                    case Direction.Right:
                        rt.Angle = 90;
                        break;
                }
            }
        }
        protected abstract RotateTransform GetRT();
    }
}
