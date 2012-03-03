using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//使用多态代替条件判断
//如果你需要检查对象的类型或者根据类型执行一些操作时，一种很好的办法就是将算法封装到类中，并利用多态性进行抽象调用。

//有点像 Strategy
namespace LosTechies.DaysOfRefactoring.SampleCode.ReplaceWithPolymorphism.Before
{
    public class Product
    {
        public decimal Price { get; set; }
    }
    public abstract class Customer
    {
    }

    public class Employee : Customer
    {
    }

    public class NonEmployee : Customer
    {
    }

    public class OrderProcessor
    {
        public decimal ProcessOrder(Customer customer, IEnumerable<Product> products)
        {
            // do some processing of order
            decimal orderTotal = products.Sum(p => p.Price);

            Type customerType = customer.GetType();
            if (customerType == typeof(Employee))
            {
                orderTotal -= orderTotal * 0.15m;
            }
            else if (customerType == typeof(NonEmployee))
            {
                orderTotal -= orderTotal * 0.05m;
            }

            return orderTotal;
        }
    }
}


namespace LosTechies.DaysOfRefactoring.SampleCode.ReplaceWithPolymorphism.After
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public abstract class Customer
    {
        public abstract decimal DiscountPercentage { get; }
    }

    public class Employee : Customer
    {
        public override decimal DiscountPercentage
        {
            get { return 0.15m; }
        }
    }

    public class NonEmployee : Customer
    {
        public override decimal DiscountPercentage
        {
            get { return 0.05m; }
        }
    }

    public class OrderProcessor
    {
        public decimal ProcessOrder(Customer customer, IEnumerable<Product> products)
        {
            // do some processing of order
            decimal orderTotal = products.Sum(p => p.Price);

            orderTotal -= orderTotal * customer.DiscountPercentage;

            return orderTotal;
        }
    }
}