using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LosTechies.DaysOfRefactoring.ExtractSuperclass.Before
{
    public class Dog
    {
        public void EatFood()
        {
            // eat some food
        }



        public void Groom()
        {
            // perform grooming
        }
    }
}

namespace LosTechies.DaysOfRefactoring.ExtractSuperclass.After
{
    public class Animal
    {
        public void EatFood()
        {
            // eat some food
        }

        public void Groom()
        {
            // perform grooming
        }
    }

    public class Dog : Animal
    {
    }
}