using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DesignPatternDemo.Behavioral.Visitor
{
    class VisitorStructuralExample
    {
        // "Element"
        abstract class Element
        {
            // Methods
            abstract public void Accept(Visitor visitor);
        }

        // "Visitor"
        abstract class Visitor
        {
            // Methods
            abstract public void VisitConcreteElementA(ConcreteElementA concreteElementA);
            abstract public void VisitConcreteElementB(ConcreteElementB concreteElementB);
        }

        // "ConcreteVisitor1"
        class ConcreteVisitor1 : Visitor
        {
            // Methods
            override public void VisitConcreteElementA(ConcreteElementA concreteElementA)
            {
                Console.WriteLine("{0} visited by {1}", concreteElementA, this);
            }

            override public void VisitConcreteElementB(ConcreteElementB concreteElementB)
            {
                Console.WriteLine("{0} visited by {1}", concreteElementB, this);
            }
        }

        // "ConcreteVisitor2"
        class ConcreteVisitor2 : Visitor
        {
            // Methods
            override public void VisitConcreteElementA(ConcreteElementA concreteElementA)
            {
                Console.WriteLine("{0} visited by {1}", concreteElementA, this);
            }
            override public void VisitConcreteElementB(ConcreteElementB concreteElementB)
            {
                Console.WriteLine("{0} visited by {1}", concreteElementB, this);
            }
        }

        // "ConcreteElementA"
        class ConcreteElementA : Element
        {
            // Methods
            override public void Accept(Visitor visitor)
            {
                visitor.VisitConcreteElementA(this);
            }

            public void OperationA()
            {
            }
        }

        // "ConcreteElementB"
        class ConcreteElementB : Element
        {
            // Methods
            override public void Accept(Visitor visitor)
            {
                visitor.VisitConcreteElementB(this);
            }

            public void OperationB()
            {
            }
        }

        // "ObjectStructure"
        class ObjectStructure
        {
            // Fields
            private ArrayList elements = new ArrayList();

            // Methods
            public void Attach(Element element)
            {
                elements.Add(element);
            }

            public void Detach(Element element)
            {
                elements.Remove(element);
            }

            public void Accept(Visitor visitor)
            {
                foreach (Element e in elements) e.Accept(visitor);
            }
        }

        public static void Demo(string[] args)
        {
            // Setup structure
            ObjectStructure o = new ObjectStructure();
            o.Attach(new ConcreteElementA());
            o.Attach(new ConcreteElementB());

            // Create visitor objects
            ConcreteVisitor1 v1 = new ConcreteVisitor1();
            ConcreteVisitor2 v2 = new ConcreteVisitor2();

            // Structure accepting visitors
            o.Accept(v1);
            o.Accept(v2);
        }
    }
}
