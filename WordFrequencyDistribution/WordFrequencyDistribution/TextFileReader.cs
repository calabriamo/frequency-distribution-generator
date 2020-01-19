using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyDistribution
{
    /// <summary>
    /// Contains methods for accessing external files.
    /// </summary>
    class TextFileReader
    {
        static public string[] _fileTextLines { get; private set; }

        static public void ReadFile(string inputFilePath)
        {
            if (System.IO.File.Exists(inputFilePath))
            {
                System.Console.WriteLine("Found file");

                // Process file
                _fileTextLines = System.IO.File.ReadAllLines(inputFilePath);
            }
            else
            {
                System.Console.WriteLine("File does not exist: " + inputFilePath);
            }
        }

        static public void PrintFileLines()
        {
            for (int i = 0; i < _fileTextLines.Length; i++)
            {
                Console.WriteLine(_fileTextLines[i]);
            }
        }
    }
}
