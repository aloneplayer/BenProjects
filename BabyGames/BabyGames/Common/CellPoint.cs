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

namespace BabyGames
{
    public class CellPoint
    {
        public CellPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            CellPoint point = obj as CellPoint;
            if (this.X == point.X && this.Y == point.Y)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
        public static bool operator ==(CellPoint p1, CellPoint p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y)
                return true;

            return false;
        }

        public static bool operator !=(CellPoint p1, CellPoint p2)
        {
            return !(p1 == p2);
        }
    }
}
