﻿using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contracts;
using Services;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create host for type CalculatorService
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                //Add endpoint, which contains service contrace, biding and address
                host.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:9999/calculatorservice");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://127.0.0.1:9999/calculatorservice/metadata");
                    host.Description.Behaviors.Add(behavior);
                }

                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");
                };

                host.Open();
                Console.Read();
            }
        }

        static private void MainUsingConfig()
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
