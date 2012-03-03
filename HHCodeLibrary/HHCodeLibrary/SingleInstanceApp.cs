using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace HHCodeLibrary
{
    class SingleInstanceApp
    {
        /// <summary>
        /// For WPF
        /// override void OnStartup
        /// </summary>
        public void Method1()
        {

            Process currProcess = Process.GetCurrentProcess();

            if (Process.GetProcessesByName(currProcess.ProcessName).Length > 1)
            {
                //Application.Current.Shutdown();
                return;
            }
        }

        /// <summary>
        /// For WinForm
        /// Call it in Main()
        /// </summary>
        public void Method2()
        {
            bool canRun = false;
            Mutex appMutex = new Mutex(true, "Mutex For My Application", out canRun);

            if (canRun)
            {
                //Application.Run();

            }
            else
            { 
                //Show message, bring the precess front...
            }
            appMutex.Close();
        }
    }
}
