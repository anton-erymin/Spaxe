using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morphology;



namespace Converter
{
    public static class Export
    {
        public static String ExportToText(Dictionary<String, DictionaryArticle> dictionary)
        {
            String text = String.Empty;

            foreach (String word in dictionary.Keys)
            {
                String separator = String.Empty;

                if (word.Length < 8)
                {
                    separator = "\t\t";
                }
                else
                {
                    separator = "\t";
                }

                text += word + separator + "\t" +
                    dictionary[word].sound + "\t" +
                    dictionary[word].soundfile + "\t" +
                    dictionary[word].translation + "\n";
            }

            return text;
        }
    }
}
