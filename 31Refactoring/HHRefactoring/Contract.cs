using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//契约式设计规定方法应该对输入和输出进行验证，这样你便可以保证你得到的数据是可以工作的
namespace LosTechies.DaysOfRefactoring.SampleCode.Day25_DesignByContract
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public decimal Balance { get; set; }
    }

    public class CashRegister
    {
        public decimal TotalOrder(IEnumerable<Product> products, Customer customer)
        {
            decimal orderTotal = products.Sum(product => product.Price);

            customer.Balance += orderTotal;

            return orderTotal;
        }
    }
}


namespace LosTechies.DaysOfRefactoring.SampleCode.DesignByContract.After
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public decimal Balance { get; set; }
    }

    public class CashRegister
    {
        public decimal TotalOrder(IEnumerable<Product> products, Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer", "Customer cannot be null");
            if (products.Count() == 0)
                throw new ArgumentException("Must have at least one product to total", "products");

            decimal orderTotal = products.Sum(product => product.Price);

            customer.Balance += orderTotal;

            if (orderTotal == 0)
                throw new ArgumentOutOfRangeException("orderTotal", "Order Total should not be zero");

            return orderTotal;
        }
    }
}