using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyDistribution
{
    class Tokeniser
    {
        static private List<string> _tokenArray;

        static public void Tokenise(string[] lineArray)
        {
            _tokenArray = new List<string>();

            foreach (string line in lineArray)
            {
                Tokenise(line);
            }
        }

        static private void Tokenise(string fileLine)
        {
            foreach (string token in fileLine.Split(' '))
            {
                _tokenArray.Add(token);
            }
        }

        static public void PrintTokens()
        {
            UInt16 indentationLevel = 0;

            WriteXMLNode("tokens", indentationLevel++, true);
            Console.Write("\n");

            for (int i = 0; i < _tokenArray.Count; i++)
            {
                WriteXMLNode("token", indentationLevel, true);
                Console.Write(_tokenArray[i]);
                WriteXMLNode("token", 0, false);
                Console.Write("\n");
            }
            WriteXMLNode("tokens", --indentationLevel, false);

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
