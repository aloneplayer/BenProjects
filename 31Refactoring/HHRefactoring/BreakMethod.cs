using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//将一个大方法分解为名字有意义且可读性更好的若干个小方法。
namespace LosTechies.DaysOfRefactoring.SampleCode.BreakMethod.Before
{
    public class CashRegister
    {
        public CashRegister()
        {
            Tax = 0.06m;
        }

        private decimal Tax { get; set; }

        /// <summary>
        /// Big Method
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="products"></param>
        /// <param name="payment"></param>
        public void AcceptPayment(Customer customer, IEnumerable<Product> products, decimal payment)
        {
            decimal subTotal = 0m;
            foreach (Product product in products)
            {
                subTotal += product.Price;
            }

            foreach (Product product in products)
            {
                subTotal -= product.AvailableDiscounts;
            }

            decimal grandTotal = subTotal * Tax;

            customer.DeductFromAccountBalance(grandTotal);
        }
    }

    public class Customer
    {
        public void DeductFromAccountBalance(decimal amount)
        {
            // deduct from balance
        }
    }

    public class Product
    {
        public decimal Price { get; set; }
        public decimal AvailableDiscounts { get; set; }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.BreakMethod.After
{
    public class CashRegister
    {
        public CashRegister()
        {
            Tax = 0.06m;
        }

        private decimal Tax { get; set; }
        private IEnumerable<Product> Products { get; set; }

        public void AcceptPayment(Customer customer, IEnumerable<Product> products, decimal payment)
        {
            decimal subTotal = CalculateSubtotal();

            subTotal = SubtractDiscounts(subTotal);

            decimal grandTotal = AddTax(subTotal);

            SubtractFromCustomerBalance(customer, grandTotal);
        }

        private void SubtractFromCustomerBalance(Customer customer, decimal grandTotal)
        {
            customer.DeductFromAccountBalance(grandTotal);
        }

        private decimal AddTax(decimal subTotal)
        {
            return subTotal * Tax;
        }

        private decimal SubtractDiscounts(decimal subTotal)
        {
            foreach (Product product in Products)
            {
                subTotal -= product.AvailableDiscounts;
            }
            return subTotal;
        }

        private decimal CalculateSubtotal()
        {
            decimal subTotal = 0m;
            foreach (Product product in Products)
            {
                subTotal += product.Price;
            }
            return subTotal;
        }
    }

    public class Customer
    {
        public void DeductFromAccountBalance(decimal amount)
        {
            // deduct from balance
        }
    }

    public class Product
    {
        public decimal Price { get; set; }
        public decimal AvailableDiscounts { get; set; }
    }
}