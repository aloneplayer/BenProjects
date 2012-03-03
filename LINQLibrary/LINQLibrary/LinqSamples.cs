using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQLibrary
{
    class LinqSample
    {
        /// <summary>
        /// OUTPUT
        ///    Three
        ///    Four
        ///    Five
        /// </summary>
        public void SkipElementinArray()
        {
            string[] arr = {"One", "Two", "Three",
                   "Four", "Five", "Six",
                   "Seven", "Eight"};
            var result = from x in arr
                             .Skip<string>(2)
                             .Take<string>(3)
                         select x;

            foreach (string str in result)
            {
                Console.WriteLine("{0}", str);
            }

            Console.ReadLine();
        }


        public void StartWithLINQ()
        {
            int[] numbers = new Int32[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numberQuery =
                from x in numbers
                where (x % 2) == 0
                select x;

            foreach (int item in numberQuery)
            {
                Console.WriteLine("x = {0}", item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCharsCount()
        {
            string str = "Hello LINQ.";
            char[] charArray = str.ToUpper().ToCharArray();

            //
            int count = (from c in charArray where c == 'L' select c).Count();

            return count;
        }

        public void SelectItems()
        {
            Product[] products = new Product[]
            {
                new Product(){Name="啤酒", Count = 100 , Price=2.5 },
                new Product(){Name="冰峰", Count = 50 , Price=1.5 },
                new Product(){Name="可乐", Count = 80 , Price=3.5 },
            };


            var selectedProduct = from p in products where (p.Count > 60 && p.Price > 2) select p;

            foreach (Product p in selectedProduct)
            { 
            
            }
        }


    }
}
