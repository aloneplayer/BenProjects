using System;
using System.Collections.Generic;
using System.Text;

namespace LosTechies.DaysOfRefactoring.Rename.Before
{
    public class Person
    {
        public string FN { get; set; }

        public decimal ClcHrlyPR()
        {
            // code to calculate hourly payrate
            return 0m;
        }
    }
}
namespace LosTechies.DaysOfRefactoring.Rename.After
{
    // Changed the class name to Employee
    public class Employee
    {
        public string FirstName { get; set; }

        public decimal CalculateHourlyPay()
        {
            // code to calculate hourly payrate
            return 0m;
        }
    }
}