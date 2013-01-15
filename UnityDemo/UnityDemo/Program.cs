using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using UnityDemo.Constructor;

namespace UnityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.
            ConstructorInjectionDemo();

            //2.
            PropertyInjectionDemo();

            //3. 
            MethodCallInjectionDemo();
        }

        public static void ConstructorInjectionDemo()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMonitor, Monitor>()
                     .RegisterType<INotify, EmailNotify>();

            IMonitor monitor = container.Resolve<IMonitor>();
            monitor.Alarm();
        }

        public static void PropertyInjectionDemo()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMonitor, Monitor>()
                     .RegisterType<INotify, SMSNotify>();

            IMonitor monitor = container.Resolve<IMonitor>();
            monitor.Alarm();
        }

        public static void MethodCallInjectionDemo()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMonitor, Monitor>();
            container.RegisterType<INotify, EmailNotify>();

            IMonitor monitor = container.Resolve<IMonitor>();
            monitor.Alarm();
        }
    }
}
