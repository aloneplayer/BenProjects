using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Behavioral.TemplateMethod
{
    /// <summary>
    /// Define the skeleton of an algorithm in an operation, deferring some steps to subclasses. 
    /// </summary>
    class TemplateMethodPattern
    {
        public abstract class AbstractCar
        {
            protected abstract string StartUp();
            protected abstract string Run();
            protected abstract string Stop();

            public void DriveOnTheRoad()
            {
                Console.WriteLine(StartUp());
                Console.WriteLine(Run());
                Console.WriteLine(Stop());
            }
        }
        public class BORA : AbstractCar
        {
            protected override string StartUp()
            {
                return "BORA is StartUp";
            }

            protected override string Run()
            {
                return "BORA is Running";
            }

            protected override string Stop()
            {
                return "BORA is Stoped";
            }
        }

        public class Context
        {
            public static void Drive(AbstractCar car)
            {
                car.DriveOnTheRoad();
            }
        }

        static void Demo(string[] args)
        {
            Context.Drive(new BORA());
        }
    }
}
