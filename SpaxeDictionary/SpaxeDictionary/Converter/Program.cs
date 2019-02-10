using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Morphology;



namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = @"_My(Es-Ru).xml";

            
            Dictionary<String, DictionaryArticle> dictionary = Import.ImportFromLingvo(fileName);
            String text = Export.ExportToText(dictionary);

            
            StreamWriter writer = new StreamWriter(File.Create(@"_My(Es-Ru).txt"));
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
