using System.Reflection.Metadata;
using System.Xml;
using System;
using System.Xml.Linq;


namespace LinqXMLApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
           XDocument xmlDocument = new XDocument(
           new XElement("root",
           new XElement("person",
           new XElement("name", "John"),
           new XElement("age", "30")
           ),
          new XElement("person",
          new XElement("name", "Alice"),
          new XElement("age", "25")
           )
           )
           );

           Console.WriteLine(xmlDocument);

           XElement newPerson = new XElement("person",
           new XElement("name", "Bob"),
           new XElement("age", "35")
           );

           xmlDocument.Root.Add(newPerson);

           Console.WriteLine(xmlDocument);

            var query = from person in xmlDocument.Descendants("person")
                        where (string)person.Element("age") == "30"
                        select person.Element("name").Value;

            foreach (var name in query)
            {
                Console.WriteLine("Name: " + name);
            }

            var personToModify = xmlDocument.Descendants("person").FirstOrDefault(p => (string)p.Element("name") == "John");
            if (personToModify != null)
            {
                personToModify.Element("age").Value = "32";
            }

            Console.WriteLine(xmlDocument);

            XElement newPerson2 = new XElement("person",
            new XElement("name", "Eve"),
            new XElement("age", "28")
            );

            xmlDocument.Root.Add(newPerson2);

            Console.WriteLine(xmlDocument);

            var personToDelete = xmlDocument.Descendants("person").FirstOrDefault(p => (string)p.Element("name") == "Alice");
            if (personToDelete != null)
            {
                personToDelete.Remove();
            }

            Console.WriteLine(xmlDocument);
        }
    }
}