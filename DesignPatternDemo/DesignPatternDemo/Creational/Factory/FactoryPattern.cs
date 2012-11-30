using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Creational.Factory
{
    /// <summary>
    /// Define an interface for creating an object, but let subclasses 
    /// decide which class to instantiate
    /// </summary>
    class FactoryPattern
    {
        // Factory Method Pattern Judith Bishop 2006

        interface IProduct
        {
            string ShipFrom();
        }

        class ProductA : IProduct
        {
            public String ShipFrom()
            {
                return " from South Africa";
            }
        }

        class ProductB : IProduct
        {
            public String ShipFrom()
            {
                return "from Spain";
            }
        }

        class DefaultProduct : IProduct
        {
            public String ShipFrom()
            {
                return "not available";
            }
        }

        /// <summary>
        /// The 'Creator' abstract class
        /// </summary>
        abstract class Creator
        {
            public abstract IProduct FactoryMethod();
        }


        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class ConcreteCreatorA : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ProductA();
            }
        }

        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class ConcreteCreatorB : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ProductB();
            }
        }

        public static void Demo()
        {
            // An array of creators
            Creator[] creators = new Creator[2];

            creators[0] = new ConcreteCreatorA();
            creators[1] = new ConcreteCreatorB();

            // Iterate over creators and create products
            foreach (Creator creator in creators)
            {
                IProduct product = creator.FactoryMethod();
                Console.WriteLine("Created {0}",
                  product.GetType().Name);
            }
 
        }
    }
}
