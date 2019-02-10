using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morphology
{
    public class VerbTypeRecognizer
    {
        // 1 - pensar, comenzar, perder, recomendar, atender (1, 2, 3)
        // 2 - soler, tronar, torcer, cocer, sonar, almorzar, costar (1, 2)
        // 3 - pedir, reír, vestir, seguir, elegir, rendir (3)
        // 4 - sentir, advertir, preferir, convertir (3)
        // 5 - dormir, morir (3)
        // 6 - conocer, pertenecer, carecer, parecer, ofrecer, apetecer (2, 3, -ocer, -ecer, -ucir, -acer) искл.: mecer, remecer, cocer, escocer
        // 7 - traducir, introducir, producir, aducir, deducir (3, -ducir)
        // 8 - construir, destruir, incluir, constituir (3, -uir)
        // 9 - gruñir, bruñir, zambullir, tañer (2, 3)

        public static byte Recognize(String infinitive)
        {
            for (int i = 0; i < Grammar.indVerbs.Length; i++)
            {
                if (Grammar.indVerbs[i] == infinitive)
                {
                    return Conjugator.GROUP_IRREGULAR_INDIVIDUAL;
                }
            }


            String conj = infinitive.Substring(infinitive.Length - 2);
            String root = infinitive.Substring(0, infinitive.Length - 2);


            switch (conj)
            {
                case "ár":
                case "ar":
                    {
                        // Группа 1. 
                        String vowels = GetLastRootVowels(root);
                        if (vowels == "e")
                            return Conjugator.GROUP_IRREGULAR_1;

                        // Группа 2. 
                        vowels = GetLastRootVowels(root);
                        if (vowels == "o")
                            return Conjugator.GROUP_IRREGULAR_2;

                        break;
                    }
                case "ér":
                case "er":
                    {
                        char c1 = root[root.Length - 1];
                        char c2 = root[root.Length - 2];

                        // Группа 6.
                        if (c1 == 'c')
                        {
                            if (infinitive == "mecer" || infinitive == "remecer")
                                return Conjugator.GROUP_IRREGULAR_1;
                            if (infinitive == "cocer" || infinitive == "escocer")
                                return Conjugator.GROUP_IRREGULAR_2;

                            if (IsVowel(c2))
                                return Conjugator.GROUP_IRREGULAR_6;
                        }

                        // Группа 9.                 
                        if (c1 == 'ñ' || (c1 == 'l' && c2 == 'l'))
                            return Conjugator.GROUP_IRREGULAR_9;

                        // Группа 2. 
                        String vowels = GetLastRootVowels(root);
                        if (vowels == "o")
                            return Conjugator.GROUP_IRREGULAR_2;

                        // Группа 1. 
                        vowels = GetLastRootVowels(root);
                        if (vowels == "e")
                            return Conjugator.GROUP_IRREGULAR_1;


                        break;
                    }
                case "ír":
                case "ir":
                    {
                        char c1 = root[root.Length - 1];
                        char c2 = root[root.Length - 2];

                        String last3 = "";
                        if (infinitive.Length >= 3) last3 = infinitive.Substring(infinitive.Length - 3);
                        String last4 = "";
                        if (infinitive.Length >= 4) last4 = infinitive.Substring(infinitive.Length - 4);
                        String last5 = "";
                        if (infinitive.Length >= 5) last5 = infinitive.Substring(infinitive.Length - 5);

                        // Группа 3.
                        if (last3 == "eír" || last4 == "ebir" || last4 == "emir" || last4 == "etir" || last4 == "eñir" || last4 == "edir" ||
                            last4 == "egir" || last5 == "enchir" || last5 == "endir" || last5 == "estir" || last5 == "eguir" ||
                            infinitive == "servir")
                            return Conjugator.GROUP_IRREGULAR_3;

                        // Группа 4.
                        if (last4 == "erir" || last5 == "entir" || last5 == "ertir" || last5 == "ervir")
                            return Conjugator.GROUP_IRREGULAR_4;

                        // Группа 9.
                        if (c1 == 'ñ' || (c1 == 'l' && c2 == 'l'))
                            return Conjugator.GROUP_IRREGULAR_9;

                        // Группа 8.
                        if ((c1 == 'u' && c2 != 'g') || c1 == 'ü')
                            return Conjugator.GROUP_IRREGULAR_8;

                        // Группа 6, 7.
                        if (c1 == 'c')
                        {
                            String s = root.Substring(root.Length - 3, 2);
                            if (s == "du")
                                return Conjugator.GROUP_IRREGULAR_7;
                            else if (s[1] == 'u') return Conjugator.GROUP_IRREGULAR_6;
                        }

                        // Группа 5.
                        if (infinitive == "dormir" || infinitive == "morir")
                            return Conjugator.GROUP_IRREGULAR_5;

                        break;
                    }
            }

            return Conjugator.GROUP_REGULAR;
        }


        private static String GetLastRootVowels(String root)
        {
            for (int i = root.Length - 1; i >= 0; i--)
            {
                // Если буква согласная, идем дальше.
                if (!IsVowel(root[i])) continue;

                if (i > 0)
                {
                    if (IsVowel(root[i - 1]))
                    {
                        // Дифтонг.
                        return root.Substring(i - 1, 2);
                    }
                }

                return root[i].ToString();
            }
            return "";
        }


        private static bool IsVowel(char letter)
        {
            if (letter == 'a' || letter == 'o' || letter == 'u' || letter == 'e' || letter == 'i' ||
                letter == 'á' || letter == 'ó' || letter == 'ú' || letter == 'é' || letter == 'í')
                return true;
            else return false;
        }
    }
}
