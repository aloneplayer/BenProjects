using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    public class Frame
    {
        private int score;

        public int Score
        {
            get { return score; }
        }

        public void Add(int pins)
        {
            score += pins;
        }
    }
}
