using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQLibrary
{
    public class XMLReader
    {
        public void ReadXMLWithLINQ()
        {
            XElement main = XElement.Load(@"XMLs\customers.xml");

            IEnumerable<XElement> searched =
                from c in main.Elements("customer")
                where (from m in c.Elements("location")
                       where (string)m.Element("city").Attribute("value") == "Los Angeles"
                           && (string)m.Element("state").Attribute("value") == "CA"
                       select m).Any()
                select c;
        
        }



    }
}
