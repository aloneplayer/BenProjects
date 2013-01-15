using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace UnityDemo.Constructor
{
    class Monitor: IMonitor
    {
        private INotify notify;

        public Monitor(INotify n, string name)
        {
            notify = n;
        }

        /// <summary>
        /// 指明哪个构造函数是需要被注入的
        /// </summary>
        /// <param name="n"></param>
        [InjectionConstructor]
        public Monitor(INotify n)
        {
            notify = n;
        }
      
        public void Alarm()
        {
            notify.Send();
        }
    }
}
