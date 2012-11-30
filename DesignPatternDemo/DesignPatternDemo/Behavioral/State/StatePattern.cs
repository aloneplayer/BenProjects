using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Behavioral.State
{
    /// <summary>
    /// can be seen as a dynamic version of the 
    /// Strategy pattern. 
    /// When the state inside an object changes, it can
    /// change its behavior by switching to a set of different 
    /// operations.
    /// </summary>
    class StatePattern
    {
        /// <summary>
        /// The 'State' abstract class
        /// </summary>
        abstract class State
        {
            public abstract void Handle(Context context);
        }

        /// <summary>
        /// A 'ConcreteState' class
        /// </summary>
        class ConcreteStateA : State
        {
            public override void Handle(Context context)
            {
                context.State = new ConcreteStateB();
            }
        }

        /// <summary>
        /// A 'ConcreteState' class
        /// </summary>
        class ConcreteStateB : State
        {
            public override void Handle(Context context)
            {
                context.State = new ConcreteStateA();
            }
        }

        /// <summary>
        /// The 'Context' class
        /// </summary>
        class Context
        {
            private State _state;

            // Constructor
            public Context(State state)
            {
                this.State = state;
            }

            // Gets or sets the state
            public State State
            {
                get { return _state; }
                set
                {
                    _state = value;
                    Console.WriteLine("State: " +
                      _state.GetType().Name);
                }
            }

            public void Request()
            {
                _state.Handle(this);
            }
        }

        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Demo()
        {
            // Setup context in a state
            Context c = new Context(new ConcreteStateA());

            // Issue requests, which toggles state
            c.Request();
            c.Request();

            c.State = new ConcreteStateB();
            c.Request();
            c.Request();

            // Wait for user
            Console.ReadKey();
        }
    }
}
