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
        private const string _bodyNodeName = "body";
        private const string _phraseNodeName = "phrase";
        private const string _tokenNodeName = "token";

        /// <summary>
        /// Full path of the file to read. Set on construction.
        /// </summary>
        private string _writeFileLocation = "C:\\Users\\Jack\\Desktop\\";

        /// <summary>
        /// List of phrases, i.e. a collection of tokens forming a sentence.
        /// </summary>
        private List<string> _phraseList = new List<string>();

        /// <summary>
        /// List of output tokens.
        /// </summary>
        static private List<string> _tokenList = new List<string>();

        private XmlDocument _tokenXMLDocument = new XmlDocument();

        private List<string> _rawLineList;

        public Tokeniser(string[] lineArray)
        {
            // :TODO: Andrew Memma 10-Dec-2019
            // Validation to be enacted on each incoming raw line?
            _rawLineList = lineArray.ToList<string>();

            // Initialise the private XML document with the structure:
            // <body>
            //     <tokens>
            //         <token>example</token>
            //         ...
            //     <tokens>
            // </body>
            XmlDeclaration declaration =
                _tokenXMLDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = _tokenXMLDocument.DocumentElement;

            _tokenXMLDocument.InsertBefore(declaration, root);

            XmlElement bodyNode = _tokenXMLDocument.CreateElement(_bodyNodeName);
            XmlElement phraseNode = _tokenXMLDocument.CreateElement(_phraseNodeName);
            XmlElement tokenNode = _tokenXMLDocument.CreateElement(_tokenNodeName);

            _tokenXMLDocument.AppendChild(bodyNode);
            bodyNode.AppendChild(phraseNode);
            phraseNode.AppendChild(tokenNode);
        }

        /// <summary>
        /// Transform raw text into a list of phrases, each of which is a list of tokens.
        /// </summary>
        public void Tokenise()
        {
            this.ParsePhrases(this._rawLineList);

            foreach (var phrase in this._phraseList)
            {
                this.ParseIntoTokens(phrase);
            }
        }

        private void ParsePhrases(List<string> rawLineList)
        {
            string currentPhrase = string.Empty;
            foreach (var rawLine in rawLineList)
            {
                var currentLine = rawLine;

                // Loop: for every complete phrase encapsulated in this single line:
                while (SentenceMarkerIndex(currentLine) != -1)
                {
                    int phraseMarkerIndex = SentenceMarkerIndex(currentLine);

                    currentPhrase += currentLine.Substring(0, phraseMarkerIndex);

                    int cutoffIndex = phraseMarkerIndex + 1;
                    currentLine = currentLine.Substring(cutoffIndex,
                                                        currentLine.Length - cutoffIndex);

                    this._phraseList.Add(currentPhrase);

                    currentPhrase = string.Empty;
                }

                currentPhrase += currentLine;
            }
        }

        private void ParseIntoTokens(string fileLine)
        {
            XmlNode nodes = _tokenXMLDocument.SelectSingleNode("//" + _phraseNodeName);

            foreach (string token in fileLine.Split(' '))
            {
                string tokenLowercase = token.ToLower();

                int periodIndex = token.IndexOf('.');

                if (periodIndex != -1)
                {
                    string periodToken = token.Substring(periodIndex);

                    XmlNode periodTokenNode = _tokenXMLDocument.CreateNode(
                        "element", _tokenNodeName, string.Empty);
                    periodTokenNode.InnerText = periodToken;
                    //_tokenList.Add(token);
                    nodes.AppendChild(periodTokenNode);

                    tokenLowercase = token.Substring(0, periodIndex);
                }

                XmlNode newTokenNode = _tokenXMLDocument.CreateNode("element", "token", string.Empty);
                newTokenNode.InnerText = tokenLowercase;
                //_tokenList.Add(token);
                nodes.AppendChild(newTokenNode);
            }
        }

        private int SentenceMarkerIndex(string Line)
        {
            return Line.IndexOf('.');
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
