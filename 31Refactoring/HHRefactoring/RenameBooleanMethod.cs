using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//当一个方法带有大量的bool 参数时，会导致方法很容易被误解并产生非预期的行为，

//根据布尔型参数的数量，我们可以决定提取出若干个独立的方法来。具体代码如下：
namespace LosTechies.DaysOfRefactoring.SampleCode.RenameBooleanMethod.Before
{
    public class Customer
    { }

    public class BankAccount
    {
        public void CreateAccount(Customer customer, bool withChecking, bool withSavings, bool withStocks)
        {
            // do work
        }
    }
}


namespace LosTechies.DaysOfRefactoring.SampleCode.RenameBooleanMethod.After
{
    public class Customer
    { }

    public class BankAccount
    {
        public void CreateAccountWithChecking(Customer customer)
        {
            CreateAccount(customer, true, false);
        }

        public void CreateAccountWithCheckingAndSavings(Customer customer)
        {
            CreateAccount(customer, true, true);
        }

        private void CreateAccount(Customer customer, bool withChecking, bool withSavings)
        {
            // do work
        }
    }
}