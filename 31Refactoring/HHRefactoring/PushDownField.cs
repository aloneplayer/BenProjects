using System;
using System.Collections.Generic;
using System.Text;

//好处：节约内存
namespace LosTechies.DaysOfRefactoring.PushDownField.Before
{
    public abstract class Task
    {
        protected string _resolution;
    }

    public class BugTask : Task
    {
    }

    public class FeatureTask : Task
    {
    }
}

namespace LosTechies.DaysOfRefactoring.PushDownField.After
{
    public abstract class Task
    {
    }

    public class BugTask : Task
    {
        private string _resolution;
    }

    public class FeatureTask : Task
    {
    }
}