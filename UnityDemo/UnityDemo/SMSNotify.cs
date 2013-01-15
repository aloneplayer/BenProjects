using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityDemo
{
    public class SMSNotify : INotify
    {
        public void Send()
        {
            Console.WriteLine("Send SMS Notify...");
        }
    }
}
