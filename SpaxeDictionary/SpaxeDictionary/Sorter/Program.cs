using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;



namespace Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(AssemblyProduct + " " + AssemblyTitle + ", " + AssemblyVersion);
            Console.WriteLine(AssemblyCopyright);
            Console.WriteLine();


            if (args.Length < 2)
            {
                Console.WriteLine("Error: Invalid arguments.");
                return;
            }

            Dictionary<String, Entry> dict = new Dictionary<String, Entry>();


            // Загружаем словарь в память из файла.
            FileStream file = null;
            try
            {
                file = File.Open(args[0], FileMode.Open);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return;
            }
            StreamReader reader = new StreamReader(file);
            Regex regex = new Regex(@"(?<key>.+)\t+(?<signature>.+)\t+(?<translation>.+)");


            while (!reader.EndOfStream)
            {
                String line = reader.ReadLine();
                Match match = regex.Match(line);

                if (match.Success)
                {
                    Entry entry = new Entry();
                    entry.signature = match.Groups["signature"].Value;
                    entry.translation = match.Groups["translation"].Value;

                    try
                    {
                        dict.Add(match.Groups["key"].Value, entry);
                    }
                    catch
                    {
                        Console.WriteLine("Entrie has not been added to dictionary. Key: " + match.Groups["key"].Value);
                    }
                }
            }

            reader.Close();


            // Сортируем словарь в памяти.
            var x = from k in dict.Keys
                    orderby k ascending
                    select k;


            // Выгружаем словарь из памяти в файл.
            try
            {
                file = File.Create(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return;
            }
            StreamWriter writer = new StreamWriter(file, Encoding.BigEndianUnicode);

            writer.WriteLine(dict.Count.ToString());

            foreach (String s in x)
                writer.WriteLine(s + '\t' + dict[s].signature + '\t' + dict[s].translation);

            writer.Close();


            Console.WriteLine("Dictionary was sorted successfully.");
            Console.WriteLine("Entries: " + dict.Count.ToString());
        }


        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion
    }
}