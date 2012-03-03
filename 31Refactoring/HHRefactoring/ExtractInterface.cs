using System;
using System.Collections.Generic;
using System.Text;

//让调用者使用接口，降低代码的耦合性。
//正文：RegistrationProcessor 类只使用到了ClassRegistration 类中的Create方法和Total 字段，所以可以考虑把他们做成接口给RegistrationProcessor 调用。

namespace LosTechies.DaysOfRefactoring.ExtractInterface.Before
{
    public class ClassRegistration
    {
        public void Create()
        {
            // create registration code
        }

        public void Transfer()
        {
            // class transfer code
        }

        public decimal Total { get; private set; }
    }

    public class RegistrationProcessor
    {
        public decimal ProcessRegistration(ClassRegistration registration)
        {
            registration.Create();
            return registration.Total;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.ExtractInterface.After
{
    public interface IClassRegistration
    {
        void Create();
        decimal Total { get; }
    }

    public class ClassRegistration : IClassRegistration
    {
        public void Create()
        {
            // create registration code
        }

        public void Transfer()
        {
            // class transfer code
        }

        public decimal Total { get; private set; }
    }

    public class RegistrationProcessor
    {
        public decimal ProcessRegistration(IClassRegistration registration)
        {
            registration.Create();
            return registration.Total;
        }
    }
}