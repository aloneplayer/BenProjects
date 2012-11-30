using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternDemo.Structural.Flyweight
{
    class BookDemo
    {
        class Book
        {
            public string Publish { get; set; }
            public string Writer { get; set; }
            public int BookID { get; set; }
            public int Price { get; set; }
            public string Name { get; set; }
        }

        class PublishFlyweight
        {
            private string name;

            public PublishFlyweight(string s)
            {
                name = s;
            }
            public string GetName()
            {
                return name;
            }
        }

        class PublishFlyweightFactory
        {
            static Dictionary<string, PublishFlyweight> it = new Dictionary<string, PublishFlyweight>();

            public PublishFlyweight GetPublish(string key)
            {
                PublishFlyweight p;

                // 存在这个出版社
                if (it.ContainsKey(key))
                {
                    p = it[key];
                }
                else
                {// 插入这个PublishFlyweight

                    p = new PublishFlyweight(key);
                    it[key] = p;
                }

                return p;
            }
        };

        class WriterFlyweight
        {
            private string m_name;
            public WriterFlyweight(string s)
            {
                m_name = s;
            }
            public string GetName()
            {
                return m_name;
            }
        };

        class WriterFlyweightFactory
        {
            private static Dictionary<string, WriterFlyweight> mapWriter = new Dictionary<string, WriterFlyweight>();

            public WriterFlyweight GetWriter(string key)
            {
                WriterFlyweight p;

                // 存在这个Writer
                if (mapWriter.ContainsKey(key))
                {
                    p = mapWriter[key];
                }
                else
                {// 插入这个PublishFlyweight
                    p = new WriterFlyweight(key);
                    mapWriter[key] = p;
                }

                return p;
            }
        };

        static void ShowBookInfo(Book book)
        {
            Console.WriteLine("");
            Console.WriteLine(book.Name);
            Console.WriteLine(book.BookID);
            Console.WriteLine(book.Price);
            Console.WriteLine(book.Publish);
            Console.WriteLine(book.Writer);
        }

        public static void Demo()
        {
            PublishFlyweightFactory pff = new PublishFlyweightFactory();
            WriterFlyweightFactory wff = new WriterFlyweightFactory();
            Book book1 = new Book();
            Book book2 = new Book();
            Book book3 = new Book();

            book1.Publish = pff.GetPublish("机械工业出版社").GetName();
            book1.Writer = wff.GetWriter("候捷").GetName();
            book1.BookID = 0;
            book1.Price = 20;
            book1.Name = "C++";

            ShowBookInfo(book1);

            book2.Publish = pff.GetPublish("人民邮电出版社").GetName();
            book2.Writer = wff.GetWriter("候捷").GetName();
            book2.BookID = 1;
            book2.Price = 30;
            book2.Name = "Java";

            ShowBookInfo(book2);

            book3.Publish = pff.GetPublish("机械工业出版社").GetName();
            book3.Writer = wff.GetWriter("Huang").GetName();
            book3.BookID = 2;
            book3.Price = 50;
            book3.Name = "Debug";

            ShowBookInfo(book3);
        }
    }
}