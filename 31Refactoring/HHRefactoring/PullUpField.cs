using System;
using System.Collections.Generic;
using System.Text;

namespace LosTechies.DaysOfRefactoring.PullUpField.Before
{
    public abstract class Account
    {
    }

    public class CheckingAccount : Account
    {
        private decimal _minimumCheckingBalance = 5m;
    }

    public class SavingsAccount : Account
    {
        private decimal _minimumSavingsBalance = 5m;
    }
}

namespace LosTechies.DaysOfRefactoring.PullUpField.After
{
    public abstract class Account
    {
        protected decimal _minimumBalance = 5m;  //Pull up it!!
    }

    public class CheckingAccount : Account
    {
    }

    public class SavingsAccount : Account
    {
    }
}