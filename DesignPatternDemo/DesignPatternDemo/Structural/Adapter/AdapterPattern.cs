using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Structural.Adapter
{
    public class AdapterPattern
    {
        /// <summary>
        /// The 'Target' class
        /// </summary>
        class Target
        {
            public virtual void Request()
            {
                Console.WriteLine("Called Target Request()");
            }
        }

        /// <summary>
        /// The 'Adapter' class
        /// </summary>
        class Adapter : Target
        {
            private Adaptee _adaptee = new Adaptee();

            public override void Request()
            {
                // Possibly do some other work
                //  and then call SpecificRequest
                _adaptee.SpecificRequest();
            }
        }

        /// <summary>
        /// The 'Adaptee' class
        /// </summary>
        class Adaptee
        {
            public void SpecificRequest()
            {
                Console.WriteLine("Called SpecificRequest()");
            }
        }

        public static void Demo()
        {
            // Here, we need calling Request.
            // Create adapter and place a request
            Target target = new Adapter();
            target.Request();
        }
    }
}
