using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace UnityDemo.PropertyInjection
{
    class Monitor : IMonitor
    {
        [Dependency]
        public INotify Notify { get; set; }

        public void Alarm()
        {
            Notify.Send();
        }
    }
}
