using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Middle Man类除了调用别的对象之外不做任何事情，所以没有存在的必要
namespace LosTechies.DaysOfRefactoring.SampleCode.RemoveMiddleMan.Before
{
    public class Account
    { }
    public class Customer
    { }

    public class Consumer
    {
        public AccountManager AccountManager { get; set; }

        public Consumer(AccountManager accountManager)
        {
            AccountManager = accountManager;
        }

        public void Get(int id)
        {
            Account account = AccountManager.GetAccount(id);
        }
    }

    /// <summary>
    /// Middle Man
    /// </summary>
    public class AccountManager
    {
        public AccountDataProvider DataProvider { get; set; }

        public AccountManager(AccountDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public Account GetAccount(int id)
        {
            return DataProvider.GetAccount(id);
        }
    }

    public class AccountDataProvider
    {
        public Account GetAccount(int id)
        {
            // get account
            throw new NotImplementedException();
        }
    }
}

namespace LosTechies.DaysOfRefactoring.SampleCode.RemoveMiddleMan.After
{
    public class Account
    { }
    public class Customer
    { }

    public class Consumer
    {
        public AccountDataProvider AccountDataProvider { get; set; }

        public Consumer(AccountDataProvider dataProvider)
        {
            AccountDataProvider = dataProvider;
        }

        public void Get(int id)
        {
            Account account = AccountDataProvider.GetAccount(id);
        }
    }

    public class AccountDataProvider
    {
        public Account GetAccount(int id)
        {
            // get account
            throw new NotImplementedException();
        }
    }
}