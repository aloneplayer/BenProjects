using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//把代码中的双重否定语句修改成简单的肯定语句
namespace LosTechies.DaysOfRefactoring.SampleCode.DoubleNegative.Before
{
    public class Product
    { 
    
    }
    public class Order
    {
        public void Checkout(IEnumerable<Product> products, Customer customer)
        {
            if (!customer.IsNotFlagged)
            {
                // the customer account is flagged
                // log some errors and return
                return;
            }

            // normal order processing
        }
    }

    public class Customer
    {
        public decimal Balance { get; private set; }

        public bool IsNotFlagged
        {
            get { return Balance < 30m; }
        }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.DoubleNegative.After
{
    public class Product
    {

    }
    public class Order
    {
        public void Checkout(IEnumerable<Product> products, Customer customer)
        {
            if (customer.IsFlagged)
            {
                // the customer account is flagged
                // log some errors and return
                return;
            }

            // normal order processing
        }
    }

    public class Customer
    {
        public decimal Balance { get; private set; }

        public bool IsFlagged
        {
            get { return Balance >= 30m; }
        }
    }
}