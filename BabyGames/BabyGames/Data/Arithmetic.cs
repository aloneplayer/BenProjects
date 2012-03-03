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

namespace BabyGames.Data
{

    public class Arithmetic
    {
        public int Operater1 { get; set; }
        public int Operater2 { get; set; }

        public OperationType Operation { get; set; }

        public int Result { get; set; }

        public override string ToString()
        {
            string strOperate = "+";
            if (Operation == OperationType.Subtract)
            {
                strOperate = "-";
            }

            return string.Format("{0} {1} {2}", Operater1, strOperate, Operater2);
        }
    }

    public enum OperationType
    {
        Plus = 0,
        Subtract,
    }
}
