using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//把没有必要使用异常做判断的条件尽量改为条件判断。
namespace LosTechies.DaysOfRefactoring.ReplaceException.Before
{
    interface IMicrowaveMotor
    {
        void Cook(object f);
    }
    public class InUseExceptoin : Exception
    { }


    public class Microwave
    {
        private IMicrowaveMotor Motor { get; set; }

        public bool Start(object food)
        {
            bool foodCooked = false;
            try
            {
                Motor.Cook(food);
                foodCooked = true;
            }
            catch(InUseExceptoin)
            {
                foodCooked = false;
            }

            return foodCooked;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.ReplaceException.After
{
    interface IMicrowaveMotor
    {
        void Cook(object f);
        bool IsInUse{get;}
    }


    public class Microwave
    {
        private IMicrowaveMotor Motor { get; set; }

        public bool Start(object food)
        {
            if (Motor.IsInUse)
                return false;

            Motor.Cook(food);

            return true;
        }
    }
}