using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//发现一个方法中存在过多的局部变量时，你可以通过使用提取方法对象重构来引入一些方法，
//每个方法完成任务的一个步骤，

//它和ExtractMethod最大的区别就是一个通过方法返回需要的数据，
//另一个则是通过字段来存储方法的结果值。
namespace LosTechies.DaysOfRefactoring.ExtractMethodObject.Before
{
    public class OrderLineItem
    {
        public decimal Price { get; private set; }
    }

    public class Order
    {
        private IList<OrderLineItem> OrderLineItems { get; set; }
        private IList<decimal> Discounts { get; set; }
        private decimal Tax { get; set; }

        //--Big method!
        public decimal Calculate()
        {
            decimal subTotal = 0m;

            // Total up line items
            foreach (OrderLineItem lineItem in OrderLineItems)
            {
                subTotal += lineItem.Price;
            }

            // Subtract Discounts
            foreach (decimal discount in Discounts)
                subTotal -= discount;

            // Calculate Tax
            decimal tax = subTotal * Tax;

            // Calculate GrandTotal
            decimal grandTotal = subTotal + tax;

            return grandTotal;
        }
    }
}


namespace LosTechies.DaysOfRefactoring.ExtractMethodObject.After
{
    public class OrderLineItem
    {
        public decimal Price { get; private set; }
    }

    public class Order
    {
        public IEnumerable<OrderLineItem> OrderLineItems { get; private set; }
        public IEnumerable<decimal> Discounts { get; private set; }
        public decimal Tax { get; private set; }

        public decimal Calculate()
        {
            return new OrderCalculator(this).Calculate();
        }
    }

    public class OrderCalculator
    {
        private decimal SubTotal { get; set; }
        private IEnumerable<OrderLineItem> OrderLineItems { get; set; }
        private IEnumerable<decimal> Discounts { get; set; }
        private decimal Tax { get; set; }

        public OrderCalculator(Order order)
        {
            OrderLineItems = order.OrderLineItems;
            Discounts = order.Discounts;
            Tax = order.Tax;
        }

        public decimal Calculate()
        {
            CalculateSubTotal();

            SubtractDiscounts();

            CalculateTax();

            return SubTotal;
        }

        private void CalculateSubTotal()
        {
            // Total up line items
            foreach (OrderLineItem lineItem in OrderLineItems)
                SubTotal += lineItem.Price;
        }

        private void SubtractDiscounts()
        {
            // Subtract Discounts
            foreach (decimal discount in Discounts)
                SubTotal -= discount;
        }

        private void CalculateTax()
        {
            // Calculate Tax
            SubTotal += SubTotal * Tax;
        }
    }
}
