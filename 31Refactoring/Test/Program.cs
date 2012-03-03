using System;
using System.Collections.Generic;
using System.Text;

//-
using LosTechies.DaysOfRefactoring.EncapsulateCollection.Before;
using System.Diagnostics;




namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            //order.OrderLines.Add(new OrderLine());
            //order.OrderLines.Add(new OrderLine());

            order.AddOrderLine(new OrderLine());
            order.AddOrderLine(new OrderLine());

            foreach (OrderLine o in order.OrderLines)
            {
                Debug.WriteLine(o.ToString());
            }
        }
    }
}
