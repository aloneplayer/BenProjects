using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//满足面向对象五大原则之一的单一职责
namespace LosTechies.DaysOfRefactoring.SampleCode.ExtractSubclass.Before
{
    public class NonRegistrationAction
{}
    public class Registration
    {
        public NonRegistrationAction Action { get; set; }
        public decimal RegistrationTotal { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}


namespace LosTechies.DaysOfRefactoring.SampleCode.ExtractSubclass.After
{
    public class NonRegistrationAction
    { }
    public class Registration
    {
        public decimal RegistrationTotal { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class NonRegistration : Registration
    {
        public NonRegistrationAction Action { get; set; }
        public string Notes { get; set; }
    }
}