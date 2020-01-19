using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyDistribution
{
    class Program
    {
        static private string _fileInputPath = "C:\\Users\\Jack\\Desktop\\";
        static private string _fileOutputPath = "C:\\Users\\Jack\\Desktop\\out.xml";
        static private string _fileName = "test.txt";

        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting program");

            TextFileReader.ReadFile(_fileInputPath + _fileName);

            Tokeniser myTokeniser = new Tokeniser(TextFileReader._fileTextLines);

            myTokeniser.Tokenise();
            myTokeniser.ExportTokensInXml(_fileOutputPath);

            // Pauses the console so console-printed messages are perceptible.
            Console.ReadKey();
        }
    }
}
