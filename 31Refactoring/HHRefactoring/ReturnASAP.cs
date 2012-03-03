using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//尽快返回
//把原来复杂的条件判断等语句用尽快返回的方式简化代码。
namespace LosTechies.DaysOfRefactoring.SampleCode.ReturnASAP.Before
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    { }

    public class Order
    {
        public Customer Customer { get; private set; }

        public decimal CalculateOrder(Customer customer, IEnumerable<Product> products, decimal discounts)
        {
            Customer = customer;
            decimal orderTotal = 0m;

            if (products.Count() > 0)
            {
                orderTotal = products.Sum(p => p.Price);
                if (discounts > 0)
                {
                    orderTotal -= discounts;
                }
            }

            return orderTotal;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.ReturnASAP.After
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    { }

    public class Order
    {
        public Customer Customer { get; private set; }

        public decimal CalculateOrder(Customer customer, IEnumerable<Product> products, decimal discounts)
        {
            if (products.Count() == 0)
                return 0;

            Customer = customer;
            decimal orderTotal = products.Sum(p => p.Price);

            if (discounts == 0)
                return orderTotal;

            orderTotal -= discounts;

            return orderTotal;
        }
    }
}



