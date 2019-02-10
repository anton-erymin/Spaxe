using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Morphology;


            //StreamReader reader = new StreamReader(File.Open(@"Dict\Article.txt", FileMode.Open));
            //String article = reader.ReadToEnd();
            //reader.Close();

            //Regex regex = new Regex(@"((?<word>.+)\r\n)(?<items>(\t(.+)\r\n)+)(\t\r\n)");
            //Match match = regex.Match(article);

            //regex = new Regex(@"\t(?<item>.+)\r\n");
            //MatchCollection matches = regex.Matches(match.Groups["items"].Value);


            //StreamWriter writer = new StreamWriter(File.Create(@"Dict\Article_Parsed.txt"));
            //foreach (Match item in matches)
            //{
            //    String line = item.Groups["item"].Value;

            //    regex = new Regex(@"\[p\].+\[/p\]");
            //    match = regex.Match(line);

            //    writer.WriteLine(match.Value);
            //}
            //writer.Close();


namespace Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = @"Dict\UniversalEsRu";
            List<String> result = Import(fileName);


            // Фильтрация.
            char[] symbols = { ' ', '\\', '(', '!', '?', '/', '№', '-', '$', '\'', '.', ':' };
            var strings = from item in result
                          where item.IndexOfAny(symbols) == -1 && item.Length >= 2
                          select item;


            List<String> filteredResult = new List<string>();
            
            foreach (String item in strings)
            {
                bool flag = true;
                foreach (Char symbol in item)
                {
                    if (65 <= (byte)symbol && (byte)symbol <= 90)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    filteredResult.Add(item);
                }
            }

            
            List<String> list1 = new List<string>();
            List<String> list2 = new List<string>();
            List<String> list3 = new List<string>();
            List<String> other = new List<string>();

            foreach (var item in filteredResult)
            {
                string suffix = item.Substring(item.Length - 2);

                switch (suffix)
                {
                    case "ar":
                        {
                            list1.Add(item);
                            break;
                        }
                    case "er":
                        {
                            list1.Add(item);
                            break;
                        }
                    case "ir":
                        {
                            list1.Add(item);
                            break;
                        }
                    default:
                        {
                            other.Add(item);
                            break;
                        }
                }
            }


            List<DictionaryArticle> articles = new List<DictionaryArticle>();
            foreach (String item in list1)
            {
                byte group = VerbTypeRecognizer.Recognize(item);
                DictionaryArticle article = new DictionaryArticle();
                article.word = item;
                article.Group = group;

                articles.Add(article);
            }

            
            StreamWriter writer = new StreamWriter(File.Create(fileName + "_Filtered.log"));


            var items = from item in articles
                        orderby item.Group
                        select item;

            int count = 0;
            foreach (DictionaryArticle item in items)
            {
                writer.WriteLine(count.ToString() + "\t" + item.Group + "\t" + item.word);
                count++;
            }
            //writer.Write("\n\n\n");


            //foreach (String item in list2)
            //{
            //    writer.WriteLine(count.ToString() + "\t" + item);
            //    count++;
            //}
            //writer.Write("\n\n\n");

            //foreach (String item in list3)
            //{
            //    writer.WriteLine(count.ToString() + "\t" + item);
            //    count++;
            //}
            //writer.Write("\n\n\n");

            //foreach (String item in other)
            //{
            //    writer.WriteLine(count.ToString() + "\t" + item);
            //    count++;
            //}

            writer.Close();
        }


        static List<String> Import(String fileName)
        {
            // Загружаем словарь из файла.
            StreamReader reader = new StreamReader(File.Open(fileName + ".dsl", FileMode.Open));
            String dictionary = reader.ReadToEnd();
            reader.Close();


            // Парсим заголовок словаря.
            Regex regex = new Regex(@"(#NAME\t(?<name>.+)\n)(#INDEX_LANGUAGE\t(?<index_language>.+)\n)(#CONTENTS_LANGUAGE\t(?<contents_language>.+)\n)");
            Match match = regex.Match(dictionary);

            Console.WriteLine("Name:\t\t\t" + match.Groups["name"].Value);
            Console.WriteLine("Index language:\t\t" + match.Groups["index_language"].Value);
            Console.WriteLine("Contents language:\t" + match.Groups["contents_language"].Value);

            dictionary = dictionary.Replace(match.Value, String.Empty);


            // Парсим данные словаря.
            regex = new Regex(@"((?<word>.+)\r\n)(\t(?<article>.+)\r\n)+(\t\r\n)");
            MatchCollection matches = regex.Matches(dictionary);


            // Сохроняем результат парсинга.
            StreamWriter writer = new StreamWriter(File.Create(fileName + ".log"));

            List<String> result = new List<String>();
            int count = 0;

            foreach (Match item in matches)
            {
                writer.WriteLine(count.ToString() + "\t" + item.Groups["word"].Value);

                result.Add(item.Groups["word"].Value);
                count++;
            }

            writer.Close();

            
            return result;
        }
    }
}
