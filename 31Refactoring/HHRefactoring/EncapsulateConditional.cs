using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//件表达式提取成可读性更好的属性或者方法，
//如果条件表达式不需要参数则可以提取成属性，如果条件表达式需要参数则可以提取成方法。
namespace LosTechies.DaysOfRefactoring.EncapsulateConditional.Before
{
    public class RemoteControl
    {
        private string[] Functions { get; set; }
        private string Name { get; set; }
        private int CreatedYear { get; set; }

        public string PerformCoolFunction(string buttonPressed)
        {
            // Determine if we are controlling some extra function
            // that requires special conditions
            if (Functions.Length > 1 && Name == "RCA" && CreatedYear > DateTime.Now.Year - 2)
                return "doSomething";

            return string.Empty;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.EncapsulateConditional.After
{
    public class RemoteControl
    {
        private string[] Functions { get; set; }
        private string Name { get; set; }
        private int CreatedYear { get; set; }

        private bool HasExtraFunctions
        {
            get { return Functions.Length > 1 && Name == "RCA" && CreatedYear > DateTime.Now.Year - 2; }
        }

        public string PerformCoolFunction(string buttonPressed)
        {
            // Determine if we are controlling some extra function
            // that requires special conditions
            if (HasExtraFunctions)
                return "doSomething";


            return string.Empty;
        }
    }
}