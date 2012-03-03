using System;
using System.Collections.Generic;
using System.Text;

//指在根本没有父子关系的类中使用继承是不合理的，可以用Delegate的方式来代替。
//总结:在很大程度上解决了滥用继承的情况，很多设计模式也用到了这种思想（比如桥接模式、适配器模式、策略模式等）。
namespace LosTechies.DaysOfRefactoring.ReplaceInheritance.Before
{
    public class Sanitation
    {
        public string WashHands()
        {
            return "Cleaned!";
        }
    }

    public class Child : Sanitation
    {
    }
}

namespace LosTechies.DaysOfRefactoring.ReplaceInheritance.After
{
    public class Sanitation
    {
        public string WashHands()
        {
            return "Cleaned!";
        }
    }

    public class Child
    {
        private Sanitation Sanitation { get; set; }

        public Child()
        {
            Sanitation = new Sanitation();
        }

        public string WashHands()
        {
            return Sanitation.WashHands();
        }
    }
}
