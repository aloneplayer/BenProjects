using System;

namespace BenCspTest
{
    public interface IWhoAmI
    {
        void InterfaceWhoAmI();
    }
    public class BaseClass : IWhoAmI
    {
        public BaseClass()
        {
        }

        public void WhoAmI()
        {
            Console.WriteLine("WhoAmI? I'm BaseClass");
        }

        public virtual void VirtualWhoAmI()
        {
            Console.WriteLine("WhoAmI? I'm BaseClass");
        }

        public void InterfaceWhoAmI()
        {
            Console.WriteLine("InterfaceWhoAmI? I'm BaseClass");
        }
    }

    public class DerivedClass : BaseClass, IWhoAmI
    {
        // [1] Compare the IL code of the "WhoAmI" and "new WhoAmI"
        //
        public new void WhoAmI()
        {
            Console.WriteLine("WhoAmI? I'm DerivedClass");
        }

        //public void WhoAmI()
        //{
        //    Console.WriteLine("WhoAmI? I'm DerivedClass");
        //}

        public override void VirtualWhoAmI()
        {
            Console.WriteLine("VirtualWhoAmI? I'm DerivedClass");
        }

        /// <summary>
        /// Does not hide BaseClass.InterfaceWhoAmI
        /// </summary>
        void IWhoAmI.InterfaceWhoAmI()
        {
            Console.WriteLine("InterfaceWhoAmI? I'm DerivedClass");
        }
    }

    public static class InheritanceTester
    {
        public static void TestCase1()
        {
            //[1]
            BaseClass obj = new DerivedClass();
            obj.WhoAmI();
            obj.VirtualWhoAmI();

            obj.InterfaceWhoAmI();
            ((IWhoAmI)obj).InterfaceWhoAmI();
        }
    }
}