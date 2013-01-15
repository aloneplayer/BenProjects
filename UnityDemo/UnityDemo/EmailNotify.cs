using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityDemo
{
    public class EmailNotify : INotify
    {
        public void Send()
        {
            Console.WriteLine("Send Email Notify...");
        }
    }
}
