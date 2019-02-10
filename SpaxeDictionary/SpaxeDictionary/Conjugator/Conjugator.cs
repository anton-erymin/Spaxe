using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Morphology
{
    public class Conjugator
    {
        public const byte CONJUGATION_1 = 0;
        public const byte CONJUGATION_2 = 1;
        public const byte CONJUGATION_3 = 2;


        public const byte GROUP_IRREGULAR_INDIVIDUAL = 0;
        public const byte GROUP_IRREGULAR_1 = 1;
        public const byte GROUP_IRREGULAR_2 = 2;
        public const byte GROUP_IRREGULAR_3 = 3;
        public const byte GROUP_IRREGULAR_4 = 4;
        public const byte GROUP_IRREGULAR_5 = 5;
        public const byte GROUP_IRREGULAR_6 = 6;
        public const byte GROUP_IRREGULAR_7 = 7;
        public const byte GROUP_IRREGULAR_8 = 8;
        public const byte GROUP_IRREGULAR_9 = 9;
        public const byte GROUP_REGULAR = 10;


        public const byte TENSE_INDICATIVO_PRESENTE = 0;
        public const byte TENSE_INDICATIVO_PRETERITO_INDEFINIDO = 1;
        public const byte TENSE_INDICATIVO_PRETERITO_IMPERFECTO = 2;
        public const byte TENSE_INDICATIVO_FUTURO_IMPERFECTO = 3;
        public const byte TENSE_SUBJUNTIVO_PRESENTE = 4;
        public const byte TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO = 5;
        public const byte TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO = 6;
        public const byte TENSE_POTENCIAL_IMPERFECTO = 7;

        public const byte TENSE_INDICATIVO_PRETERITO_PERFECTO = 8;
        public const byte TENSE_INDICATIVO_PRETERITO_ANTERIOR = 9;
        public const byte TENSE_INDICATIVO_PRETERITO_PLUSCUAMPERFECTO = 10;
        public const byte TENSE_INDICATIVO_FUTURO_PERFECTO = 11;
        public const byte TENSE_SUBJUNTIVO_PRETERITO_PERFECTO = 12;
        public const byte TENSE_SUBJUNTIVO_PRETERITO_PLUSCUAMPERFECTO = 13;
        public const byte TENSE_SUBJUNTIVO_FUTURO_PERFECTO = 14;
        public const byte TENSE_POTENCIAL_PERFECTO = 15;


        private static bool isSeVerb;



        public static List<String> Conjugate(DictionaryArticle article, byte tense)
        {
            String infinitive = article.word;
            byte conjugation = article.conjugation;
            byte group = article.Group;

            List<String> forms = new List<String>();

            // Выделяем корень глагола.
            String savedRoot = GetRoot(infinitive);
            String root = "";
            String flex = "";
            String form = "";

            // Цикл по всем лицам.
            for (int i = 0, person = 1; i < 6; i++, person++)
            {
                root = savedRoot;


                if (tense < 8)
                {
                    // Получаем окончание.
                    flex = Grammar.flexes[tense, conjugation, i];
                }

                switch (tense)
                {
                    //
                    // Простые времена.
                    //
                    case TENSE_INDICATIVO_PRESENTE:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (Grammar.individuals[article.index, 0, i] != "")
                                        {
                                            root = Grammar.individuals[article.index, 0, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                                case GROUP_IRREGULAR_1:
                                    {
                                        // Изменяем e > ie.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "e", "ie");

                                        break;
                                    }
                                case GROUP_IRREGULAR_2:
                                    {
                                        // Изменяем o > ue.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "o", "ue");

                                        break;
                                    }
                                case GROUP_IRREGULAR_3:
                                    {
                                        // Изменяем e > i.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_4:
                                    {
                                        // Изменяем e > ie.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "e", "ie");

                                        break;
                                    }
                                case GROUP_IRREGULAR_5:
                                    {
                                        // Изменяем o > ue.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "o", "ue");

                                        break;
                                    }
                                case GROUP_IRREGULAR_6:
                                    {
                                        // Изменяем c > zc перед o.
                                        if (person == 1)
                                            root = root.Remove(root.Length - 1) + "zc";

                                        break;
                                    }
                                case GROUP_IRREGULAR_7:
                                    {
                                        // Изменяем c > zc перед o.
                                        if (person == 1)
                                            root = root.Remove(root.Length - 1) + "zc";

                                        break;
                                    }
                                case GROUP_IRREGULAR_8:
                                    {
                                        // Изменяем u > uy перед a, o, e.
                                        if (person == 1 || person == 2 || person == 3 || person == 6)
                                            root = root + "y";

                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_PRETERITO_INDEFINIDO:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (Grammar.individuals[article.index, 1, i] != "")
                                        {
                                            root = Grammar.individuals[article.index, 1, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                                case GROUP_IRREGULAR_3:
                                    {
                                        // Изменяем e > i.
                                        if (person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_4:
                                    {
                                        // Изменяем e > i.
                                        if (person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_5:
                                    {
                                        // Изменяем o > u.
                                        if (person == 3 || person == 6)
                                            root = ChangeLastVowel(root, "o", "u");

                                        break;
                                    }
                                case GROUP_IRREGULAR_7:
                                    {
                                        // Изменяем c > j.
                                        root = root.Remove(root.Length - 1) + "j";

                                        if (person == 1) flex = "e";
                                        if (person == 3) flex = "o";
                                        if (person == 6) flex = "eron";

                                        // Изменяем ie, io > e, o после j.
                                        //if (flex.Length > 1)
                                        //    if (flex.Substring(0, 2) == "ie" ||
                                        //        flex.Substring(0, 2) == "ié" ||
                                        //        flex.Substring(0, 2) == "io" ||
                                        //        flex.Substring(0, 2) == "ió")
                                        //        flex = flex.Substring(1);

                                        break;
                                    }
                                case GROUP_IRREGULAR_8:
                                    {
                                        if (person == 3)
                                        {
                                            root += "y";
                                            flex = "ó";
                                        }
                                        if (person == 6)
                                        {
                                            root += "y";
                                            flex = "eron";
                                        }

                                        break;
                                    }
                                case GROUP_IRREGULAR_9:
                                    {
                                        if (person == 3)
                                        {
                                            flex = "ó";
                                        }
                                        if (person == 6)
                                        {
                                            flex = "eron";
                                        }

                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_PRETERITO_IMPERFECTO:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (infinitive == "ser" || infinitive == "ver" || infinitive == "ir")
                                        {
                                            root = Grammar.individuals[article.index, 5, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_FUTURO_IMPERFECTO:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (Grammar.individuals[article.index, 2, i] != "")
                                        {
                                            root = Grammar.individuals[article.index, 2, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_PRESENTE:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (Grammar.individuals[article.index, 3, i] != "")
                                        {
                                            root = Grammar.individuals[article.index, 3, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                                case GROUP_IRREGULAR_1:
                                    {
                                        // Изменяем e > ie.
                                        if (person != 4 && person != 5)
                                            root = ChangeLastVowel(root, "e", "ie");

                                        break;
                                    }
                                case GROUP_IRREGULAR_2:
                                    {
                                        // Изменяем o > ue.
                                        if (person != 4 && person != 5)
                                            root = ChangeLastVowel(root, "o", "ue");

                                        break;
                                    }
                                case GROUP_IRREGULAR_3:
                                    {
                                        // Изменяем e > i.
                                        root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_4:
                                    {
                                        // Изменяем e > ie, i.
                                        if (person != 4 && person != 5)
                                            root = ChangeLastVowel(root, "e", "ie");
                                        else
                                            root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_5:
                                    {
                                        // Изменяем o > ue, u.
                                        if (person != 4 && person != 5)
                                            root = ChangeLastVowel(root, "o", "ue");
                                        else
                                            root = ChangeLastVowel(root, "o", "u");

                                        break;
                                    }
                                case GROUP_IRREGULAR_6:
                                    {
                                        // Изменяем c > zc.
                                        root = root.Remove(root.Length - 1) + "zc";

                                        break;
                                    }
                                case GROUP_IRREGULAR_7:
                                    {
                                        // Изменяем c > zc.
                                        root = root.Remove(root.Length - 1) + "zc";

                                        break;
                                    }
                                case GROUP_IRREGULAR_8:
                                    {
                                        // Добавляем y.
                                        root += "y";

                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_INDIVIDUAL:
                                    {
                                        if (Grammar.individuals[article.index, 4, i] != "")
                                        {
                                            root = Grammar.individuals[article.index, 4, i];
                                            flex = "";
                                        }
                                        break;
                                    }
                                case GROUP_IRREGULAR_3:
                                    {
                                        // Изменяем e > i.
                                        root = ChangeLastVowel(root, "e", "i");
                                        break;
                                    }
                                case GROUP_IRREGULAR_4:
                                    {
                                        // Изменяем e > i.
                                        root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_5:
                                    {
                                        // Изменяем o > u.
                                        root = ChangeLastVowel(root, "o", "u");

                                        break;
                                    }
                                case GROUP_IRREGULAR_7:
                                    {
                                        // Изменяем c > j.
                                        root = root.Remove(root.Length - 1) + "j";
                                        flex = flex.Substring(1);

                                        break;
                                    }
                                case GROUP_IRREGULAR_8:
                                    {
                                        // Добавляем y.
                                        root += "y";
                                        flex = flex.Substring(1);

                                        break;
                                    }
                                case GROUP_IRREGULAR_9:
                                    {
                                        flex = flex.Substring(1);

                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO:
                        {
                            switch (group)
                            {
                                case GROUP_IRREGULAR_3:
                                    {
                                        // Изменяем e > i.
                                        root = ChangeLastVowel(root, "e", "i");
                                        break;
                                    }
                                case GROUP_IRREGULAR_4:
                                    {
                                        // Изменяем e > i.
                                        root = ChangeLastVowel(root, "e", "i");

                                        break;
                                    }
                                case GROUP_IRREGULAR_5:
                                    {
                                        // Изменяем o > u.
                                        root = ChangeLastVowel(root, "o", "u");

                                        break;
                                    }
                                case GROUP_IRREGULAR_7:
                                    {
                                        // Изменяем c > j.
                                        root = root.Remove(root.Length - 1) + "j";
                                        flex = flex.Substring(1);

                                        break;
                                    }
                                case GROUP_IRREGULAR_8:
                                    {
                                        // Добавляем y.
                                        root += "y";
                                        flex = flex.Substring(1);

                                        break;
                                    }
                                case GROUP_IRREGULAR_9:
                                    {
                                        flex = flex.Substring(1);

                                        break;
                                    }
                            }

                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_POTENCIAL_IMPERFECTO:
                        {
                            // Образуем форму глагола.
                            form = root + flex;
                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }


                    //
                    // Сложные времена.
                    //
                    case TENSE_INDICATIVO_PRETERITO_PERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_INDICATIVO_PRESENTE, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_PRETERITO_ANTERIOR:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_INDICATIVO_PRETERITO_INDEFINIDO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_PRETERITO_PLUSCUAMPERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_INDICATIVO_PRETERITO_IMPERFECTO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_INDICATIVO_FUTURO_PERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_INDICATIVO_FUTURO_IMPERFECTO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_PRETERITO_PERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_SUBJUNTIVO_PRESENTE, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_PRETERITO_PLUSCUAMPERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_SUBJUNTIVO_FUTURO_PERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }

                    case TENSE_POTENCIAL_PERFECTO:
                        {
                            // Получаем причастие глагола.
                            String participio = GetParticipio(root, conjugation);

                            // Образуем форму глагола.
                            form = Grammar.auxiliary[TENSE_POTENCIAL_IMPERFECTO, i]
                                        + " " + participio;

                            // Если глагол - возвратный, добавляем возвратное местоимение.
                            if (isSeVerb) form = Grammar.se[i] + " " + form;

                            break;
                        }
                }



                //
                // Учитываем орфографические изменения.
                //
                if (flex != "")
                {
                    String last = infinitive.Substring(infinitive.Length - 3);

                    switch (last)
                    {
                        case "car":
                            {
                                // c > qu перед e.
                                if (flex[0] == 'e' || flex[0] == 'é')
                                    form = ChangeLastVowel(form, "c", "qu");

                                break;
                            }
                        case "zar":
                            {
                                // z > c перед e.
                                if (flex[0] == 'e' || flex[0] == 'é')
                                    form = ChangeLastVowel(form, "z", "c");

                                break;
                            }
                        case "gar":
                            {
                                // g > gu перед e.
                                if (flex[0] == 'e' || flex[0] == 'é')
                                    form = ChangeLastVowel(form, "g", "gu");

                                break;
                            }
                        case "cer":
                        case "cir":
                            {
                                if (group != GROUP_IRREGULAR_6)
                                {
                                    // c > z перед a, o.
                                    if (flex[0] == 'a' || flex[0] == 'o' || flex[0] == 'á' || flex[0] == 'ó')
                                        form = ChangeLastVowel(form, "c", "z");
                                }

                                break;
                            }
                        case "ger":
                        case "gir":
                            {
                                // g > j перед a, o.
                                if (flex[0] == 'a' || flex[0] == 'o' || flex[0] == 'á' || flex[0] == 'ó')
                                    form = ChangeLastVowel(form, "g", "j");

                                break;
                            }
                        case "iar":
                            {
                                // i > í
                                form = ChangeLastVowel(form, "i", "í");


                                break;
                            }
                        case "uar":
                            {
                                // u > ú 
                                form = ChangeLastVowel(form, "u", "ú");

                                break;
                            }
                    }


                    last = infinitive.Substring(infinitive.Length - 4);

                    switch (last)
                    {
                        case "quer":
                        case "quir":
                            {
                                // qu > c перед a, o.
                                if (flex[0] == 'a' || flex[0] == 'o' || flex[0] == 'á' || flex[0] == 'ó')
                                    form = ChangeLastVowel(form, "qu", "c");

                                break;
                            }
                        case "guer":
                        case "guir":
                            {
                                // gu > g перед a, o.
                                if (flex[0] == 'a' || flex[0] == 'o' || flex[0] == 'á' || flex[0] == 'ó')
                                    form = ChangeLastVowel(form, "gu", "g");

                                break;
                            }
                        case "guar":
                            {
                                // gu > gü перед e.
                                if (flex[0] == 'e' || flex[0] == 'é')
                                    form = ChangeLastVowel(form, "gu", "gü");

                                break;
                            }
                    }
                }


                forms.Add(form);

            }

            return forms;
        }


        public static String GetRoot(String infinitive)
        {
            String root = "";

            if (infinitive.Substring(infinitive.Length - 2) == "se")
            {
                root = infinitive.Remove(infinitive.Length - 4);
                isSeVerb = true;
            }
            else
            {
                root = infinitive.Remove(infinitive.Length - 2);
                isSeVerb = false;
            }

            return root;
        }


        public static String GetParticipio(String root, byte conjugation)
        {
            String participio = root;
            if (conjugation == CONJUGATION_1)
                participio += "ado";
            else if (conjugation == CONJUGATION_2 || conjugation == CONJUGATION_3)
                participio += "ido";
            return participio;
        }


        public static String ChangeLastVowel(String root, String vowel, String sound)
        {
            int index = root.LastIndexOf(vowel);
            String newRoot = root.Remove(index, vowel.Length);
            newRoot = newRoot.Insert(index, sound);
            return newRoot;
        }
    }
}