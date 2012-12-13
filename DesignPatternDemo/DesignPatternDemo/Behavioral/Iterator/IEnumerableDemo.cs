using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DesignPatternDemo.Behavioral.Iterator
{
    class IEnumerableDemo
    {
        class MonthCollection : IEnumerable
        {
            string[] months = { "January", "February", "March", 
                                "April", "May", "June",
                                "July", "August", "September", 
                                "October","November", "December"};

            public IEnumerator GetEnumerator()
            {
                // Generates values from the collection
                foreach (string element in months)
                    yield return element;
            }
        }

        static void Demo()
        {

            MonthCollection collection = new MonthCollection();
            // Consumes values generated from collection's GetEnumerator method
            foreach (string n in collection)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine("\n");
        }
    }
}
