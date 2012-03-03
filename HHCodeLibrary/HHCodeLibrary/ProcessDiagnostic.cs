using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HHCodeLibrary
{
    public class ProcessDiagnostic
    {
        public delegate void DealProcess(Process process);
        public static void ForEachRunningProcesses(DealProcess function)
        {
            //Retrieve the list of all processes running on the local machine.
            Process[] localAll = Process.GetProcesses();
            foreach (Process p in localAll)
            {
                function(p);
            }
        }

        public static string GetCurrentProcessName()
        {

            return Process.GetCurrentProcess().ProcessName;
        }


        public void PerformanceCounterTest()
        {

            // Delete the Performance Counter Category, if already exists
            if (PerformanceCounterCategory.Exists("SalesDistribution"))
                PerformanceCounterCategory.Delete("SalesDistribution");

            // Prepare for creation of the Performance Counter
            CounterCreationDataCollection counterCollection =
                new CounterCreationDataCollection();

            //Create the CounterCreationData
            CounterCreationData counterData = new CounterCreationData();
            counterData.CounterName = "RequestsPerSec";
            counterData.CounterType = PerformanceCounterType.NumberOfItems32;
            counterData.CounterHelp = "Requests Received per Second";
            counterCollection.Add(counterData);

            // Create the performance counter category
            PerformanceCounterCategory.Create("SalesDistribution",
                "Automated Sales Distribution System",
                PerformanceCounterCategoryType.SingleInstance, counterCollection);

            // Retrieve the performance counter for updation
            PerformanceCounter performanceCount =
                new PerformanceCounter("SalesDistribution", "RequestsPerSec", false);

            int x = 1;
            while (x <= 50)
            {
                Console.WriteLine("RequestsPerSec = {0}", performanceCount.RawValue);

                // Increment the performance counter
                performanceCount.Increment();
                System.Threading.Thread.Sleep(250);
                x = (x + 1);
            }

            // Close the performance counter
            performanceCount.Close();
        }

    }
}
