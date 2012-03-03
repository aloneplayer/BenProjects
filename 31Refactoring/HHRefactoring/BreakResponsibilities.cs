using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//本文中的“分离职责”是指当一个类有许多职责时，将部分职责分离到独立的类中，
//这样也符合面向对象的五大特征之一的单一职责原则，同时也可以使代码的结构更加清晰，维护性更高。

//和MoveMethod相似
namespace LosTechies.DaysOfRefactoring.BreakResponsibilities.Before
{
    public class Video
    {
        public void PayFee(decimal fee)
        {
        }

        public void RentVideo(Video video, Customer customer)
        {
            customer.Videos.Add(video);
        }

        public decimal CalculateBalance(Customer customer)
        {
            return customer.LateFees.Sum();
        }
    }

    public class Customer
    {
        public IList<decimal> LateFees { get; set; }
        public IList<Video> Videos { get; set; }
    }
}

namespace LosTechies.DaysOfRefactoring.BreakResponsibilities.After
{
    public class Video
    {
        public void RentVideo(Video video, Customer customer)
        {
            customer.Videos.Add(video);
        }
    }

    public class Customer
    {
        public IList<decimal> LateFees { get; set; }
        public IList<Video> Videos { get; set; }

        public void PayFee(decimal fee)
        {
        }

        public decimal CalculateBalance(Customer customer)
        {
            return customer.LateFees.Sum();
        }
    }
}
