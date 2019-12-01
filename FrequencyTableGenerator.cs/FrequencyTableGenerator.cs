using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyTableGenerator
{
    class FrequencyTableGenerator
    {
        static private string _filePath = "C:\\Users\\Jack\\Desktop\\";
        static private string _fileName = "test.txt";

        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting program");

            TextFileReader.ReadFile(_filePath + _fileName);

            Tokeniser.Tokenise(TextFileReader._fileTextLines);
            Tokeniser.PrintTokens();

            // Pauses the console so console-printed messages are perceptible.
            Console.ReadKey();
        }
    }
}
