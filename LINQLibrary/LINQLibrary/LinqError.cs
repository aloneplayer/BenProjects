using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQLibrary
{
    public class LinqError
    {
        private List<Product> products = new List<Product>() { 
                    new Product { Name = "Gun", Count = 10, Price = 10 },
                    new Product { Name = "Cup", Count = 5, Price = 7.5 },
                    new Product { Name = "Pen", Count = 3, Price = 5.5 } 
                };

        public List<Product> GetSomeProduct()
        {
            var result = products.Where(T => T.Price > 6);
            return result.ToList();
        }

        public List<Product> GetSomeProduct_Error()
        {
            var result = products.Where(T =>
            {
                int a = 6;
                return T.Price > a;
            });
            return result.ToList();
        }

        ////return this.QueryAll().Where(item =>
        ////    {
        ////        var currentStatus = MerchantStatus.New;
        ////        if (item.StatusEffectiveDate == null || item.StatusEffectiveDate.Value <= DateTime.Now)
        ////        {
        ////            currentStatus = item.NextStatus;
        ////        }
        ////        else
        ////        {
        ////            currentStatus = item.LastStatus;
        ////        }
        ////        if (currentStatus == MerchantStatus.Suspended || currentStatus == MerchantStatus.Retired)
        ////        {
        ////            return item;
        ////        }
        ////    });
    }
}
