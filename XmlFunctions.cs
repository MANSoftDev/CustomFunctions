using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XmlFunctions
{
    public class XmlFunctions
    {
        public XmlFunctions()
        {

        }

        public void FindAddressMethod1(string type)
        {
            XPathDocument doc = new XPathDocument("Test.xml");
            XPathNavigator nav = doc.CreateNavigator();

            // Yes, this will work but it is a bit cumbersum
            // and ineffficient.
            XPathNodeIterator nodes = nav.Select("*//Address");
            foreach(XPathNavigator node in nodes)
            {
                string attr = node.GetAttribute("type", "");
                if(attr.ToLower() == type)
                {
                    // Do something...
                }
            }
        }

        public void FindAddressForType(string type)
        {
            XPathDocument doc = new XPathDocument("Test.xml");
            XPathNavigator nav = doc.CreateNavigator();

            // Create a custom context for the XPathExpression
            CustomContext ctx = new CustomContext();
            
            // FOrmat the xpath expression for matching @type
            string XPath = string.Format("*//Address[compare(string(@type),'{0}')]", type);
            // Can also be applied to nodes
            //string XPath = string.Format("*//Address/State[compare(string(.),'pa')]");
            
            // Create an XPathExpression
            XPathExpression exp = nav.Compile(XPath);

            // Set the context to resolve the function
            // ResolveFunction is called at this point
            exp.SetContext(ctx);

            // Select nodes based on the XPathExpression
            // IXsltContextFunction.Invoke is called for each
            // node to filter the resulting nodeset
            XPathNodeIterator nodes = nav.Select(exp);

            foreach(XPathNavigator node in nodes)
            {
                // Do something...
            }
        }

        public void FindAddressForTypeWithVariable(string type)
        {
            XPathDocument doc = new XPathDocument("Test.xml");
            XPathNavigator nav = doc.CreateNavigator();

            XsltArgumentList Args = new XsltArgumentList();
            Args.AddParam("value", "", type);
            
            // Create a custom context for the XPathExpression
            CustomContext ctx = new CustomContext(new NameTable(), Args);

            // Create an XPathExpression
            XPathExpression exp = nav.Compile("*//Address[compare(string(@type))=$value]");

            // Set the context to resolve the function
            // ResolveFunction is called at this point
            exp.SetContext(ctx);

            // Select nodes based on the XPathExpression
            // IXsltContextFunction.Invoke is called for each
            // node to filter the resulting nodeset
            XPathNodeIterator nodes = nav.Select(exp);

            foreach(XPathNavigator node in nodes)
            {
                // Do something...
            }
        }
    }
}
