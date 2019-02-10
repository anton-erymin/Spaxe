using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Morphology;



namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Заполняем запись.
            DictionaryArticle article = new DictionaryArticle();
            article.word = "estar";
            article.translation = null;
            article.signature = null;
            article.conjugation = Conjugator.CONJUGATION_1;
            article.Group = Conjugator.GROUP_IRREGULAR_INDIVIDUAL;
            article.index = 0;


            FileStream file = File.Create(@"conjugation.txt");
            StreamWriter writer = new StreamWriter(file);


            for (byte t = 0; t <= 15; t++)
            {
                List<string> result = Conjugator.Conjugate(article, t);

                writer.WriteLine(Grammar.tenses[t]);
                writer.WriteLine();

                foreach (string item in result)
                    writer.WriteLine(item);

                writer.WriteLine();
                writer.WriteLine();
            }

            writer.Close();
        }
    }
}
