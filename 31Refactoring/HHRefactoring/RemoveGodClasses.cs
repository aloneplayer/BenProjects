using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//一些类明确违反了SRP原则（单一原则），这些类通常以“Utils”或“Manager”后缀结尾
namespace LosTechies.DaysOfRefactoring.SampleCode.RemoveGodClasses.Before
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public decimal Balance { get; set; }
    }

    public class Order
    { }

    public class CustomerService
    {
        public decimal CalculateOrderDiscount(IEnumerable<Product> products, Customer customer)
        {
            // do work
            throw new NotImplementedException();
        }

        public bool CustomerIsValid(Customer customer, Order order)
        {
            // do work
            throw new NotImplementedException();
        }

        public IEnumerable<string> GatherOrderErrors(IEnumerable<Product> products, Customer customer)
        {
            // do work
            throw new NotImplementedException();
        }

        public void Register(Customer customer)
        {
            // do work
        }

        public void ForgotPassword(Customer customer)
        {
            // do work
        }
    }  
}

namespace LosTechies.DaysOfRefactoring.SampleCode.RemoveGodClasses.After
{
    public class Product
    {
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public decimal Balance { get; set; }
    }

    public class Order
    { }

    public class CustomerOrderService
    {
        public decimal CalculateOrderDiscount(IEnumerable<Product> products, Customer customer)
        {
            // do work
            throw new NotImplementedException();
        }

        public bool CustomerIsValid(Customer customer, Order order)
        {
            // do work
            throw new NotImplementedException();
        }

        public IEnumerable<string> GatherOrderErrors(IEnumerable<Product> products, Customer customer)
        {
            // do work
            throw new NotImplementedException();
        }
    }

    public class CustomerRegistrationService
    {

        public void Register(Customer customer)
        {
            // do work
        }

        public void ForgotPassword(Customer customer)
        {
            // do work
        }
    }
}