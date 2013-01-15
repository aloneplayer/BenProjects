using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace UnityDemo.MethodCallInjection
{
    public class Monitor : IMonitor
    {
        private INotify notify;

        [InjectionMethod]
        public void GetNotify(INotify n)
        {
            notify = n;
        }

        public void Alarm()
        {
            notify.Send();
        }
    }
}
