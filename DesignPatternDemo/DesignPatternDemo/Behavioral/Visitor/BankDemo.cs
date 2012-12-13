using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Behavioral.Visitor
{
    class BankDemo
    {
        public class Visitor
        {
            public void Process(IService service)
            {
                // 基本业务  
                Console.WriteLine("基本业务");
            }
            public void Process(Saving service)
            {
                // 存款  
                Console.WriteLine("存款");
            }
            public void Process(Draw service)
            {
                // 提款  
                Console.WriteLine("提款");
            }
            public void Process(Fund service)
            {
                Console.WriteLine("基金");
                // 基金  
            }
        }
        public interface IService
        {
            void Accept(Visitor visitor);
        }

        public class Saving : IService
        {
            public void Accept(Visitor visitor)
            {
                visitor.Process(this);
            }
        }

        public class Draw : IService
        {
            public void Accept(Visitor visitor)
            {

                visitor.Process(this);
            }
        }

        public class Fund : IService
        {
            public void Accept(Visitor visitor)
            {

                visitor.Process(this);
            }
        }

        public static void Demo(String[] args)
        {
            IService s1 = new Saving();
            IService s2 = new Draw();
            IService s3 = new Fund();

            Visitor visitor = new Visitor();

            s1.Accept(visitor);
            s2.Accept(visitor);
            s3.Accept(visitor);
        }

        //普通实现方法  
        public static void Demo2()
        {
            IService s1 = new Saving();
            IService s2 = new Draw();
            IService s3 = new Fund();

            //要办理业务的三个客户队列  
            List<IService> list = new List<IService>();
            list.Add(s1);
            list.Add(s2);
            list.Add(s3);

            foreach (IService item in list)
            {
                if (item is Saving)
                {
                    Console.WriteLine("存款");

                }
                else if (item is Draw)
                {
                    Console.WriteLine("取款");
                }
                else if (item is Fund)
                {
                    Console.WriteLine("基金");
                }
            }
        }
    }
}
