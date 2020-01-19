using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;


namespace WordFrequencyDistribution
{
    /// <summary>
    /// Contains behaviour for converting plaintext to structured tokens.
    /// </summary>
    class Tokeniser
    {
        private string _writeFileLocation = "C:\\Users\\Jack\\Desktop\\";

        private List<string> _rawLineList;
        static private List<string> _tokenList;

        private XmlDocument _tokenXMLDocument;

        public Tokeniser(string[] lineArray)
        {

            _tokenXMLDocument = new XmlDocument();
            _tokenList = new List<string>();

            XmlDeclaration declaration =
                _tokenXMLDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = _tokenXMLDocument.DocumentElement;

            _tokenXMLDocument.InsertBefore(declaration, root);

            XmlElement bodyNode = _tokenXMLDocument.CreateElement("body");
            XmlElement tokensNode = _tokenXMLDocument.CreateElement("tokens");
            XmlElement tokenNode = _tokenXMLDocument.CreateElement("token");

            _tokenXMLDocument.AppendChild(bodyNode);
            bodyNode.AppendChild(tokensNode);
            tokensNode.AppendChild(tokenNode);

            _rawLineList = lineArray.ToList<string>();

            foreach (string line in lineArray)
            {
                // :TODO: Andrew Memma 10-Dec-2019
                // Validation to be enacted on each incoming raw line?

                _rawLineList.Add(line);
            }
        }

        public void Tokenise()
        {
            foreach (string line in _rawLineList)
            {
                Tokenise(line);
            }
        }

        private void Tokenise(string fileLine)
        {
            XmlNode nodes = _tokenXMLDocument.SelectSingleNode("//tokens");

            foreach (string token in fileLine.Split(' '))
            {
                XmlNode newTokenNode = _tokenXMLDocument.CreateNode("element", "token", string.Empty);
                newTokenNode.InnerText = token;
                //_tokenList.Add(token);
                nodes.AppendChild(newTokenNode);
            }
        }

        public void PrintTokens()
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                _tokenXMLDocument.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                Console.Write(stringWriter.GetStringBuilder().ToString());
            }
        }

        public void ExportTokensInXml(string writeFileLocation)
        {
            using (StreamWriter output = File.CreateText(writeFileLocation))
            {
                output.WriteLine(_tokenXMLDocument.OuterXml);
            }
        }

        static private void WriteXMLNode(string element, UInt16 indentationLevel, bool nodeStart)
        {
            string line = Indent(indentationLevel) + WriteXMLElement(element, nodeStart);

            Console.Write(line);
        }

        static private string WriteXMLElement(string element, bool start)
        {
            return (start ? "<" : "</") + element + ">";
        }

        static private string Indent(UInt16 indendationLevel)
        {
            string indent = string.Empty;
            for (int i = 0; i < indendationLevel; i++)
            {
                indent += "   ";
            }

            return indent;
        }
    }
}
