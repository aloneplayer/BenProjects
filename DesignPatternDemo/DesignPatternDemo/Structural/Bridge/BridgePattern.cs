using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Structural.Bridge
{
    class BridgePattern
    {
        public interface IFunction
        {
            string OperationImp();
        }

        public class Bridge
        {
            IFunction function;

            public Bridge(IFunction implementation)
            {
                function = implementation;
            }

            public string Operation()
            {
                return "Abstraction" + " <<< BRIDGE >>>> " + function.OperationImp();
            }
        }

    
        class ImplementationA : IFunction
        {
            public string OperationImp()
            {
                return "ImplementationA";
            }
        }

        class ImplementationB : IFunction
        {
            public string OperationImp()
            {
                return "ImplementationB";
            }
        }

        public static void Demo()
        {
            Console.WriteLine("Bridge Pattern\n");
            Console.WriteLine(new Bridge(new ImplementationA()).Operation());
            Console.WriteLine(new Bridge(new ImplementationB()).Operation());
        }
    }
}
