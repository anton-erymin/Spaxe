package laxe;

import java.util.Vector;

class Conjugator
{
	public static final byte CONJUGATION_I    = 0;
    public static final byte CONJUGATION_II   = 1;
    public static final byte CONJUGATION_III  = 2;
              
    public static final byte GROUP_IRREGULAR_INDIVIDUAL   = 0;
    public static final byte GROUP_IRREGULAR_I            = 1;
    public static final byte GROUP_IRREGULAR_II           = 2;
    public static final byte GROUP_IRREGULAR_III          = 3;
    public static final byte GROUP_IRREGULAR_IV           = 4;
    public static final byte GROUP_IRREGULAR_V            = 5;
    public static final byte GROUP_IRREGULAR_VI           = 6;
    public static final byte GROUP_IRREGULAR_VII          = 7;
    public static final byte GROUP_IRREGULAR_VIII         = 8;
    public static final byte GROUP_IRREGULAR_IX           = 9;
    public static final byte GROUP_REGULAR                = 10;


    public static String[] tenses =
    {
    	"Presente Indicativo",
        "Pretérito Indefinido Indicativo",
        "Pretérito Imperfecto Indicativo",
        "Futuro Imperfecto Indicativo",
        "Presente Subjuntivo",
        "Pretérito Imperfecto Subjuntivo",
        "Futuro Imperfecto Subjuntivo",
        "Imperfecto Potencial",
        "Pretérito Perfecto Indicativo",
        "Pretérito Anterior Indicativo",
        "Pretérito Pluscuamperfecto Indicativo",
        "Futuro Perfecto Indicativo",
        "Pretérito Perfecto Subjuntivo",
        "Pretérito Pluscuamperfecto Subjuntivo",
        "Futuro Perfecto Subjuntivo",
        "Perfecto Potencial"
    };


    public static final byte TENSE_INDICATIVO_PRESENTE                     = 0;
    public static final byte TENSE_INDICATIVO_PRETERITO_INDEFINIDO         = 1;
    public static final byte TENSE_INDICATIVO_PRETERITO_IMPERFECTO         = 2;
    public static final byte TENSE_INDICATIVO_FUTURO_IMPERFECTO            = 3;
    public static final byte TENSE_SUBJUNTIVO_PRESENTE                     = 4;
    public static final byte TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO         = 5;
    public static final byte TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO            = 6;
    public static final byte TENSE_POTENCIAL_IMPERFECTO                    = 7;

    public static final byte TENSE_INDICATIVO_PRETERITO_PERFECTO           = 8;
    public static final byte TENSE_INDICATIVO_PRETERITO_ANTERIOR           = 9;
    public static final byte TENSE_INDICATIVO_PRETERITO_PLUSCUAMPERFECTO   = 10;
    public static final byte TENSE_INDICATIVO_FUTURO_PERFECTO              = 11;
    public static final byte TENSE_SUBJUNTIVO_PRETERITO_PERFECTO           = 12;
    public static final byte TENSE_SUBJUNTIVO_PRETERITO_PLUSCUAMPERFECTO   = 13;
    public static final byte TENSE_SUBJUNTIVO_FUTURO_PERFECTO              = 14;
    public static final byte TENSE_POTENCIAL_PERFECTO                      = 15;
        

        
    private static String[][] haber =
    {
        {"he",       "has",       "ha",       "hemos",       "habéis",     "han"},
        {"hube",     "hubiste",   "hubo",     "hubimos",     "hubisteis",  "hubieron"},
        {"había",    "habías",    "había",    "habíamos",    "habíais",    "habían"},
        {"habré",    "habrás",    "habrá",    "habremos",    "habréis",    "habrán"},
        {"haya",     "hayas",     "haya",     "hayamos",     "hayáis",     "hayan"},
        {"hubiera",  "hubieras",  "hubiera",  "hubiéramos",  "hubierais",  "hubieran"},
        {"hubiere",  "hubieres",  "hubiere",  "hubiéremos",  "hubiereis",  "hubieren"},
        {"habría",   "habrías",   "habría",   "habríamos",   "habríais",   "habrían"}
    };

            
    private static String[][][] flexes =
    {{
        {"o",      "as",        "a",       "amos",      "áis",       "an"},
        {"o",      "es",        "e",       "emos",      "éis",       "en"},
        {"o",      "es",        "e",       "imos",      "ís",        "en"}
     },{
        {"é",      "aste",      "ó",       "amos",      "asteis",    "aron"},
        {"í",      "iste",      "ió",      "imos",      "isteis",    "ieron"},
        {"í",      "iste",      "ió",      "imos",      "isteis",    "ieron"}
     },{
        {"aba",    "abas",      "aba",     "ábamos",    "abais",     "aban"},
        {"ía",     "ías",       "ía",      "íamos",     "íais",      "ían"},
        {"ía",     "ías",       "ía",      "íamos",     "íais",      "ían"}
     },{
        {"aré",     "arás",     "ará",     "aremos",    "aréis",     "arán"},
        {"eré",     "erás",     "erá",     "eremos",    "eréis",     "erán"},
        {"iré",     "irás",     "irá",     "iremos",    "iréis",     "irán"}
     },{
        {"e",       "es",       "e",       "emos",      "éis",       "en"},
        {"a",       "as",       "a",       "amos",      "áis",       "an"},
        {"a",       "as",       "a",       "amos",      "áis",       "an"}
     },{
        {"ara",     "aras",     "ara",     "áramos",    "arais",     "aran"},
        {"iera",    "ieras",    "iera",    "iéramos",   "ierais",    "ieran"},
        {"iera",    "ieras",    "iera",    "iéramos",   "ierais",    "ieran"}
     },{
        {"are",     "ares",     "are",     "áremos",    "areis",     "aren"},
        {"iere",    "ieres",    "iere",    "iéremos",   "iereis",    "ieren"},
        {"iere",    "ieres",    "iere",    "iéremos",   "iereis",    "ieren"}
     },{
        {"aría",    "arías",    "aría",    "aríamos",   "aríais",    "arían"},
        {"ería",    "erías",    "ería",    "eríamos",   "eríais",    "erían"},
        {"iría",    "irías",    "iría",    "iríamos",   "iríais",    "irían"}
       }};
    
    
    
    public static Vector Conjugate(DictArticle article, byte tense)
    {
        Vector result = new Vector();
        String infinitive = article.word.substring(0, article.word.length() - 2);

        for (int i = 0; i <= 5; i++)
        {
            String root = infinitive;


            switch (tense)
            {
                //
                // Простые времена.
                //
                case TENSE_INDICATIVO_PRESENTE:
                {
                    switch (article.group)
                    {
                        case GROUP_IRREGULAR_I:
                        {
                            if (i == 0 || i == 1 || i == 2 || i == 5)
                                root = replaceLetter(infinitive, 'e', "ie");
                            break;
                        }
                        case GROUP_IRREGULAR_IV:
                        {
                            if (i == 0 || i == 1 || i == 2 || i == 5)
                                root = replaceLetter(infinitive, 'e', "ie");
                            break;
                        }
                        case GROUP_IRREGULAR_VI:
                        {
                            if (i == 0)
                                root = replaceLetter(infinitive, 'c', "zc");
                            break;
                        }
                    }

                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_INDICATIVO_PRETERITO_INDEFINIDO:
                {
                    switch (article.group)
                    {
                        case GROUP_IRREGULAR_IV:
                        {
                            if (i == 2 || i == 5)
                                root = replaceLetter(infinitive, 'e', "i");
                            break;
                        }
                    }

                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_INDICATIVO_PRETERITO_IMPERFECTO:
                {
                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_INDICATIVO_FUTURO_IMPERFECTO:
                {
                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_SUBJUNTIVO_PRESENTE:
                {
                    switch (article.group)
                    {
                        case GROUP_IRREGULAR_IV:
                        {
                            if (i == 0 || i == 1 || i == 2 || i == 5)
                                root = replaceLetter(infinitive, 'e', "ie");
                            if (i == 3 || i == 4)
                                root = replaceLetter(infinitive, 'e', "i");
                            break;
                        }
                        case GROUP_IRREGULAR_VI:
                        {
                            root = replaceLetter(infinitive, 'c', "zc");
                            break;
                        }
                    }

                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO:
                {
                    switch (article.group)
                    {
                        case GROUP_IRREGULAR_IV:
                        {
                            root = replaceLetter(infinitive, 'e', "i");
                            break;
                        }
                    }

                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO:
                {
                    switch (article.group)
                    {
                        case GROUP_IRREGULAR_IV:
                        {
                            root = replaceLetter(infinitive, 'e', "i");
                            break;
                        }
                    }

                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                case TENSE_POTENCIAL_IMPERFECTO:
                {
                    root = root + flexes[tense][article.conjugation][i];
                    break;
                }
                //
                // Сложные времена.
                //
                case TENSE_INDICATIVO_PRETERITO_PERFECTO:
                {
                    root = haber[TENSE_INDICATIVO_PRESENTE][i] + " " + root;
                    break;
                }
                case TENSE_INDICATIVO_PRETERITO_ANTERIOR:
                {
                    root = haber[TENSE_INDICATIVO_PRETERITO_INDEFINIDO][i] + " " + root;
                    break;
                }
                case TENSE_INDICATIVO_PRETERITO_PLUSCUAMPERFECTO:
                {
                    root = haber[TENSE_INDICATIVO_PRETERITO_IMPERFECTO][i] + " " + root;
                    break;
                }
                case TENSE_INDICATIVO_FUTURO_PERFECTO:
                {
                    root = haber[TENSE_INDICATIVO_FUTURO_IMPERFECTO][i] + " " + root;
                    break;
                }
                case TENSE_SUBJUNTIVO_PRETERITO_PERFECTO:
                {
                    root = haber[TENSE_SUBJUNTIVO_PRESENTE][i] + " " + root;
                    break;
                }
                case TENSE_SUBJUNTIVO_PRETERITO_PLUSCUAMPERFECTO:
                {
                    root = haber[TENSE_SUBJUNTIVO_PRETERITO_IMPERFECTO][i] + " " + root;
                    break;
                }
                case TENSE_SUBJUNTIVO_FUTURO_PERFECTO:
                {
                    root = haber[TENSE_SUBJUNTIVO_FUTURO_IMPERFECTO][i] + " " + root;
                    break;
                }
                case TENSE_POTENCIAL_PERFECTO:
                {
                    root = haber[TENSE_POTENCIAL_IMPERFECTO][i] + " " + root;
                    break;
                }
            }
            
            result.addElement(root);
        }


        return result;
    }


    public static String replaceLetter(String infinitive, char c, String str)
    {
    	StringBuffer sbuf = new StringBuffer(infinitive);
        int index = infinitive.lastIndexOf(c);
        sbuf = sbuf.delete(index, index + 1);
        sbuf = sbuf.insert(index, str);
        return sbuf.toString();
    }
}
