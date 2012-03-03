using System;
using System.Collections.Generic;
using System.Text;

//也就是把个别子类使用到的方法从基类移到子类里面去。

//重构后的代码如下，同时如果在父类Animal 中如果没有其他的字段或者公用方法的话，
//可以考虑把Bark方法做成一个接口，从而去掉Animal 类。
namespace LosTechies.DaysOfRefactoring.PushDownMethod.Before
{
    public abstract class Animal
    {
        public void Bark()
        {
            // code to bark
        }
    }

    public class Dog : Animal
    {
    }

    public class Cat : Animal
    {
    }
}

namespace LosTechies.DaysOfRefactoring.PushDownMethod.After
{
    public abstract class Animal
    {
    }

    public class Dog : Animal
    {
        public void Bark()
        {
            // code to bark
        }
    }

    public class Cat : Animal
    {
    }
}