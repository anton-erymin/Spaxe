using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Morphology;



namespace Converter
{
    public static class Import
    {
        public static Dictionary<String, DictionaryArticle> ImportFromSpaxe(String fileName)
        {
            FileStream file = File.Open(fileName, FileMode.Open);

            StreamReader reader = new StreamReader(file);
            Regex regex = new Regex(@"(?<word>.+?)\t+(?<signature>.+?)\t+(?<translation>.+)");
            Dictionary<String, DictionaryArticle> dictionary = new Dictionary<String, DictionaryArticle>();

            while (!reader.EndOfStream)
            {
                String line = reader.ReadLine();
                Match match = regex.Match(line);

                if (match.Success)
                {
                    DictionaryArticle article = new DictionaryArticle();
                    article.word = match.Groups["word"].Value;
                    article.signature = match.Groups["signature"].Value;
                    article.translation = match.Groups["translation"].Value;


                    article.type = article.signature[0];

                    switch (article.type)
                    {
                        case 'V':
                            {
                                article.conjugation = Byte.Parse(article.signature[1].ToString());

                                if (article.signature[2] == 'R')
                                    article.Group = Conjugator.GROUP_REGULAR;
                                else
                                    article.Group = Byte.Parse(article.signature[2].ToString());

                                if (article.Group == 0)
                                    article.index = Byte.Parse(article.signature.Substring(3, 2));

                                break;
                            }
                    }


                    dictionary.Add(article.word, article);
                }
            }

            reader.Close();

            return dictionary;
        }


        public static Dictionary<String, DictionaryArticle> ImportFromLingvo(String fileName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(fileName);

            Console.WriteLine("formatVersion:\t\t"          + document.DocumentElement.Attributes["formatVersion"].InnerText);
            Console.WriteLine("title:\t\t\t"                + document.DocumentElement.Attributes["title"].InnerText);
            Console.WriteLine("sourceLanguageId:\t"         + document.DocumentElement.Attributes["sourceLanguageId"].InnerText);
            Console.WriteLine("destinationLanguageId:\t"    + document.DocumentElement.Attributes["destinationLanguageId"].InnerText);
            Console.WriteLine("nextWordId:\t\t"             + document.DocumentElement.Attributes["nextWordId"].InnerText);
            Console.WriteLine();
                 

            XmlNodeList cards = document.DocumentElement.SelectNodes("card");
            Dictionary<String, DictionaryArticle> dictionary = new Dictionary<String, DictionaryArticle>();

            foreach (XmlNode card in cards)
            {
                XmlNode word        = card.SelectSingleNode("word");
                XmlNode translation = card.SelectSingleNode("meanings/meaning/translations/word");
                XmlNode meaning     = card.SelectSingleNode("meanings/meaning");


                DictionaryArticle article = new DictionaryArticle();
                article.word            = word.InnerText;
                article.translation     = translation.InnerText;


                if (meaning.Attributes.GetNamedItem("sound") != null && meaning.Attributes.GetNamedItem("soundfile") != null)
                {
                    article.sound       = meaning.Attributes["sound"].Value.ToLower();
                    article.soundfile   = meaning.Attributes["soundfile"].Value;
                }


                dictionary.Add(article.word, article);
            }

            
            return dictionary;
        }
    }
}
