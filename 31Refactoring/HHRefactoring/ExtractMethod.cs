using System;
using System.Collections.Generic;
using System.Text;

//把某些复杂的过程按照功能提取成各个小方法，提高可读性、维护性。
namespace LosTechies.DaysOfRefactoring.ExtractMethod.Before
{
    public class Receipt
    {
        private IList<decimal> Discounts { get; set; }
        private IList<decimal> ItemTotals { get; set; }

        public decimal CalculateGrandTotal()  //Big Method
        {
            decimal subTotal = 0m;
            foreach (decimal itemTotal in ItemTotals)
                subTotal += itemTotal;

            if (Discounts.Count > 0)
            {
                foreach (decimal discount in Discounts)
                    subTotal -= discount;
            }

            decimal tax = subTotal * 0.065m;

            subTotal += tax;

            return subTotal;
        }
    }
}

namespace LosTechies.DaysOfRefactoring.ExtractMethod.After
{
    public class Receipt
    {
        private IList<decimal> Discounts { get; set; }
        private IList<decimal> ItemTotals { get; set; }

        public decimal CalculateGrandTotal()       //Bingo!
        {
            decimal subTotal = CalculateSubTotal();

            subTotal = CalculateDiscounts(subTotal);

            subTotal = CalculateTax(subTotal);

            return subTotal;
        }

        private decimal CalculateTax(decimal subTotal)
        {
            decimal tax = subTotal * 0.065m;

            subTotal += tax;
            return subTotal;
        }

        private decimal CalculateDiscounts(decimal subTotal)
        {
            if (Discounts.Count > 0)
            {
                foreach (decimal discount in Discounts)
                    subTotal -= discount;
            }
            return subTotal;
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0m;
            foreach (decimal itemTotal in ItemTotals)
                subTotal += itemTotal;
            return subTotal;
        }
    }
}