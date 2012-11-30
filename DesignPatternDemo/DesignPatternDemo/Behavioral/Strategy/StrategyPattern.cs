using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Behavioral.Strategy
{
    /// <summary>
    /// removing an algorithm from its host class and putting 
    /// it in a separate class
    /// </summary>
    class StrategyPattern
    {
        // Strategy interface
        interface IStrategy
        {
            int Move(Context c);
        }

        // Strategy 1
        class Strategy1 : IStrategy
        {
            public int Move(Context c)
            {
                return ++c.Counter;
            }
        }

        // Strategy 2
        class Strategy2 : IStrategy
        {
            public int Move(Context c)
            {
                return --c.Counter;
            }
        }

        // The Context
        class Context
        {
            // Context state
            public const int start = 5;
            public int Counter = 5;

            // Strategy aggregation
            IStrategy strategy = new Strategy1();

            // Algorithm invokes a strategy method
            public int Algorithm()
            {
                return strategy.Move(this);
            }

            public IStrategy Strategy
            {
                get { return strategy; }
                set { strategy = value; }
            }
            // Changing strategies
            public void SwitchStrategy()
            {
                if (strategy is Strategy1)
                    strategy = new Strategy2();
                else
                    strategy = new Strategy1();
            }
        }

        static void Demo()
        {
            Context context = new Context();
            context.SwitchStrategy();
            Random r = new Random(37);
            for (int i = Context.start; i <= Context.start + 15; i++)
            {
                if (r.Next(3) == 2)
                {
                    Console.Write("|| ");
                    context.SwitchStrategy();
                }
                Console.Write(context.Algorithm() + " ");
            }

            context.Strategy = new Strategy2();

            Console.Write(context.Algorithm() + " ");
        }
    }
}
