using System;
using System.Collections.Generic;
using System.Text;

//概念：提升方法是指将一个很多继承类都要用到的方法提升到基类中。
//正文：这样就能减少代码量，同时让类的结构更清晰。
//如下代码所示，Turn方法在子类Car 和Motorcycle "都" 会用到，所以应把它提到基类中。
namespace LosTechies.DaysOfRefactoring.PullUpMethod.Before
{
    public abstract class Vehicle
    {
        // other methods
    }

    public class Car : Vehicle
    {
        public void Turn(Direction direction)
        {
            // code here
        }
    }

    public class Motorcycle : Vehicle
    {
    }

    public enum Direction
    {
        Left,
        Right
    }
}

namespace LosTechies.DaysOfRefactoring.PullUpMethod.After
{
    public abstract class Vehicle
    {
        public void Turn(Direction direction)
        {
            // code here
        }
    }

    public class Car : Vehicle
    {
    }

    public class Motorcycle : Vehicle
    {
    }

    public enum Direction
    {
        Left,
        Right
    }
}