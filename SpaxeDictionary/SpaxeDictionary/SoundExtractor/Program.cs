using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Converter;
using Morphology;



namespace SoundExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                String fileName = args[0];
                Dictionary<String, DictionaryArticle> input = Import.ImportFromLingvo(Properties.Settings.Default.DictionaryPath + fileName);

                
                Dictionary<String, DictionaryArticle> dictionary = null;

                if (Boolean.Parse(args[2]))
                {
                    Random random = new Random();

                    dictionary = new Dictionary<String, DictionaryArticle>();

                    do
                    {
                        String key = input.Keys.ElementAt(random.Next(input.Count));
                        dictionary.Add(key, input[key]);
                        input.Remove(key);
                    }
                    while (input.Count != 0);
                }
                else
                {
                    dictionary = input;
                }


                FileStream file = null;
                Dictionary<String, Entity> entities = null;


                List<uint> data = new List<uint>();
                ulong total = 0;


                foreach (String word in dictionary.Keys)
                {
                    if (dictionary[word].soundfile != null && dictionary[word].sound != null)
                    {
                        if (entities == null)
                        {
                            file = File.Open(Properties.Settings.Default.SoundPath + dictionary[word].soundfile + ".lsa", FileMode.Open, FileAccess.Read, FileShare.Read);
                            entities = GetEntities(file);
                        }
                        
                        
                        String sound = dictionary[word].sound;

                        if (entities.Keys.Contains(sound))
                        {
                            data.Add(entities[sound].position);
                            data.Add(entities[sound].length);

                            total += entities[sound].length;
                        }
                    }
                }


                Console.WriteLine("Count: " + data.Count / 2);


                IntPtr handle = file.SafeFileHandle.DangerousGetHandle();
                int pause = Int32.Parse(args[1]);

                Extractor.Process(handle, pause, data.ToArray(), data.Count, total);


                file.Close();
            }
        }


        private static Dictionary<String, Entity> GetEntities(FileStream file)
        {
            BinaryReader reader = new BinaryReader(file);
            Dictionary<String, Entity> entities = new Dictionary<String, Entity>();
            
            
            String signature = Encoding.Unicode.GetString(reader.ReadBytes(8));
            byte stop = reader.ReadByte();
            uint count = reader.ReadUInt32();


            for (int i = 0; i < count; i++)
            {
                Entity entity = new Entity();

                List<byte> buffer = new List<byte>();
                byte value = 0;

                do
                {
                    value = reader.ReadByte();
                    buffer.Add(value);
                }
                while (value != 255);


                int offset = i == 0 ? 6 : 5;

                entity.title = Encoding.Unicode.GetString(buffer.ToArray(), 0, buffer.Count - offset);
                entity.prefix = BitConverter.ToUInt32(buffer.ToArray(), buffer.Count - offset);


                if (i != 0)
                {
                    entity.position = reader.ReadUInt32();
                    stop = reader.ReadByte();
                }


                entity.length = reader.ReadUInt32();

                entities.Add(entity.title, entity);
            }

            
            return entities;
        }
    }
}
