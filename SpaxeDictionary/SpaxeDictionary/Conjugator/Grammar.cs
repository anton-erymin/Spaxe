using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Morphology
{
    public class Grammar
    {
        public static String[] tenses =
        {
            "Presente",
            "Pretérito Indefinido",
            "Pretérito Imperfecto",
            "Futuro Imperfecto",
            
            "Presente",
            "Pretérito Imperfecto",
            "Futuro Imperfecto",
            
            "Imperfecto",

            "Pretérito Perfecto",
            "Pretérito Anterior",
            "Pretérito Pluscuamperfecto",
            "Futuro Perfecto",
            
            "Pretérito Perfecto",
            "Pretérito Pluscuamperfecto",
            "Futuro Perfecto",
            
            "Perfecto"
        };


        // Возвратные местоимения.
        public static String[] se = {"me", "te", "se", "nos", "os", "se"};


        // Вспомогательный глагол haber.
        public static String[,] auxiliary =
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

        
        // Окончания глаголов.
        public static String[,,] flexes =
        {{
            // Presente Indicativo
            {"o",      "as",        "a",       "amos",      "áis",       "an"},
            {"o",      "es",        "e",       "emos",      "éis",       "en"},
            {"o",      "es",        "e",       "imos",      "ís",        "en"}
         },{
            // Pretérito Indefinido Indicativo
            {"é",      "aste",      "ó",       "amos",      "asteis",    "aron"},
            {"í",      "iste",      "ió",      "imos",      "isteis",    "ieron"},
            {"í",      "iste",      "ió",      "imos",      "isteis",    "ieron"}
         },{
            // Pretérito Imperfecto Indicativo
            {"aba",    "abas",      "aba",     "ábamos",    "abais",     "aban"},
            {"ía",     "ías",       "ía",      "íamos",     "íais",      "ían"},
            {"ía",     "ías",       "ía",      "íamos",     "íais",      "ían"}
         },{
            // Futuro Imperfecto Indicativo
            {"aré",     "arás",     "ará",     "aremos",    "aréis",     "arán"},
            {"eré",     "erás",     "erá",     "eremos",    "eréis",     "erán"},
            {"iré",     "irás",     "irá",     "iremos",    "iréis",     "irán"}
         },{
            // Presente Subjuntivo
            {"e",       "es",       "e",       "emos",      "éis",       "en"},
            {"a",       "as",       "a",       "amos",      "áis",       "an"},
            {"a",       "as",       "a",       "amos",      "áis",       "an"}
         },{
            // Pretérito Imperfecto Subjuntivo
            {"ara",     "aras",     "ara",     "áramos",    "arais",     "aran"},
            {"iera",    "ieras",    "iera",    "iéramos",   "ierais",    "ieran"},
            {"iera",    "ieras",    "iera",    "iéramos",   "ierais",    "ieran"}
         },{
            // Futuro Imperfecto Subjuntivo
            {"are",     "ares",     "are",     "áremos",    "areis",     "aren"},
            {"iere",    "ieres",    "iere",    "iéremos",   "iereis",    "ieren"},
            {"iere",    "ieres",    "iere",    "iéremos",   "iereis",    "ieren"}
         },{
            // Imperfecto Potencial
            {"aría",    "arías",    "aría",    "aríamos",   "aríais",    "arían"},
            {"ería",    "erías",    "ería",    "eríamos",   "eríais",    "erían"},
            {"iría",    "irías",    "iría",    "iríamos",   "iríais",    "irían"}
        }};

        
        // Индивидуальные глаголы.
        public static String[] indVerbs = 
        { "estar", "dar", "andar", "querer", "tener", "poder", "placer", 
          "yacer", "caber", "caer", "hacer", "poner", "saber", "traer", 
          "valer", "ver", "haber", "ser", "venir", "decir", "erguir", "asir", "salir", "oír", "ir"};

        public static String[,,] individuals =
        {{
             // 0 - estar

             // Presente Indicativo
             {"estoy", "estás", "está", "estamos", "estáis", "están"},
             // Pretérito Indefinido Indicativo
             {"estuve", "estuviste", "estuvo", "estuvimos", "estuvisteis", "estuvieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"esté", "estés", "esté", "estemos", "estéis", "estén"},
             // Pretérito Imperfecto Subjuntivo
             {"estuviera", "estuvieras", "estuviera", "estuviéramos", "estuvierais", "estuvieran"},
             // Futuro Imperfecto Subjuntivo
             {"estuviere", "estuvieres", "estuviere", "estuviéramos", "estuviereis", "estuvieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 1 - dar

             // Presente Indicativo
             {"doy", "das", "da", "damos", "dais", "dan"},
             // Pretérito Indefinido Indicativo
             {"di", "diste", "dio", "dimos", "disteis", "dieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"dé", "des", "dé", "demos", "deis", "den"},
             // Pretérito Imperfecto Subjuntivo
             {"diera", "dieras", "diera", "diéramos", "dierais", "dieran"},
             // Futuro Imperfecto Subjuntivo
             {"diere", "dieres", "diere", "diéremos", "diereis", "dieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 2 - andar

             // Presente Indicativo
             {"", "", "", "", "", ""},
             // Pretérito Indefinido Indicativo
             {"anduve", "anduviste", "anduvo", "anduvimos", "anduvisteis", "anduvieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"", "", "", "", "", ""},
             // Pretérito Imperfecto Subjuntivo
             {"anduviera", "anduvieras", "anduviera", "anduviéramos", "anduvierais", "anduvieran"},
             // Futuro Imperfecto Subjuntivo
             {"anduviere", "anduvieres", "anduviere", "anduviéremos", "anduviereis", "anduvieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 3 - querer

             // Presente Indicativo
             {"quiero", "quieres", "quiere", "queremos", "queréis", "quieren"},
             // Pretérito Indefinido Indicativo
             {"quise", "quisiste", "quiso", "quisimos", "quisisteis", "quisieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"querré", "querrás", "querrá", "querremos", "querréis", "querrán"},
             // Presente Subjuntivo
             {"quiera", "quieras", "quiera", "queramos", "queráis", "quieran"},
             // Pretérito Imperfecto Subjuntivo
             {"quisiera", "quisieras", "quisiera", "quisiéramos", "quisierais", "quisieran"},
             // Futuro Imperfecto Subjuntivo
             {"quisiere", "quisieres", "quisiere", "quisiéremos", "quisiereis", "quisieren"},
             // Futuro Potencial
             {"querría", "querrías", "querría", "querríamos", "querríais", "querrían"}
         },{
             // 4 - tener

             // Presente Indicativo
             {"tengo", "tienes", "tiene", "tenemos", "tenéis", "tienen"},
             // Pretérito Indefinido Indicativo
             {"tuve", "tuviste", "tuvo", "tuvimos", "tuvisteis", "tuvieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"tendré", "tendrás", "tendrá", "tendremos", "tendréis", "tendrán"},
             // Presente Subjuntivo
             {"tenga", "tengas", "tenga", "tengamos", "tengáis", "tengan"},
             // Pretérito Imperfecto Subjuntivo
             {"tuviera", "tuvieras", "tuviera", "tuviéramos", "tuvierais", "tuvieran"},
             // Futuro Imperfecto Subjuntivo
             {"tuviere", "tuvieres", "tuviere", "tuviéremos", "tuviereis", "tuvieren"},
             // Futuro Potencial
             {"tendría", "tendrías", "tendría", "tendríamos", "tendríais", "tendrían"}
         },{
             // 5 - poder

             // Presente Indicativo
             {"puedo", "puedes", "puede", "podemos", "podéis", "pueden"},
             // Pretérito Indefinido Indicativo
             {"pude", "pudiste", "pudo", "pudimos", "pudisteis", "pudieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"podré", "podrás", "podrá", "podremos", "podréis", "podran"},
             // Presente Subjuntivo
             {"pueda", "puedas", "pueda", "podamos", "podáis", "puedan"},
             // Pretérito Imperfecto Subjuntivo
             {"pudiera", "pudieras", "pudiera", "pudiéramos", "pudierais", "pudieran"},
             // Futuro Imperfecto Subjuntivo
             {"pudiere", "pudieres", "pudiere", "pudiéremos", "pudiereis", "pudieren"},
             // Futuro Potencial
             {"podría", "podrías", "podría", "podríamos", "podríais", "podrían"}
         },{
             // 6 - placer

             // Presente Indicativo
             {"plazco", "places", "place", "placemos", "placéis", "placen"},
             // Pretérito Indefinido Indicativo
             {"plací", "placiste", "plugo", "placimos", "placisteis", "pluguieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"plazca", "plazcas", "plazca", "plazcamos", "plazcáis", "plazcan"},
             // Pretérito Imperfecto Subjuntivo
             {"placiera", "placieras", "pluguiera", "placiéramos", "placierais", "pluguieran"},
             // Futuro Imperfecto Subjuntivo
             {"placiere", "placieres", "pluguiere", "placiéremos", "placiereis", "pluguieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 7 - yacer

             // Presente Indicativo
             {"yazco", "yaces", "yace", "yacemos", "yacéis", "yacen"},
             // Pretérito Indefinido Indicativo
             {"", "", "", "", "", ""},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"yazca", "yazcas", "yazca", "yazcamos", "yazcáis", "yazcan"},
             // Pretérito Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 8 - caber

             // Presente Indicativo
             {"quepo", "cabes", "cabe", "cabemos", "cabéis", "caben"},
             // Pretérito Indefinido Indicativo
             {"cupe", "cupiste", "cupo", "cupimos", "cupisteis", "cupieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"cabré", "cabrás", "cabrá", "cabremos", "cabréis", "cabrán"},
             // Presente Subjuntivo
             {"quepa", "quepas", "quepa", "quepamos", "quepáis", "quepan"},
             // Pretérito Imperfecto Subjuntivo
             {"cupiera", "cupieras", "cupiera", "cupiéramos", "cupierais", "cupieran"},
             // Futuro Imperfecto Subjuntivo
             {"cupiere", "cupieres", "cupiere", "cupiéremos", "cupiereis", "cupieren"},
             // Futuro Potencial
             {"cabría", "cabrías", "cabría", "cabríamos", "cabríais", "cabrían"}
         },{
             // 9 - caer

             // Presente Indicativo
             {"caigo", "caes", "cae", "caemos", "caéis", "caen"},
             // Pretérito Indefinido Indicativo
             {"caí", "caiste", "cayo", "caímos", "caísteis", "cayeron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"caiga", "caigas", "caiga", "caigamos", "caigáis", "caigan"},
             // Pretérito Imperfecto Subjuntivo
             {"cayera", "cayeras", "cayera", "cayéramos", "cayerais", "cayeran"},
             // Futuro Imperfecto Subjuntivo
             {"cayere", "cayeres", "cayere", "cayéremos", "cayereis", "cayeren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 10 - hacer

             // Presente Indicativo
             {"hago", "haces", "hace", "hacemos", "hacéis", "hacen"},
             // Pretérito Indefinido Indicativo
             {"hice", "hiciste", "hizo", "hicimos", "hicisteis", "hicieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"haré", "harás", "haré", "haremos", "haréis", "harán"},
             // Presente Subjuntivo
             {"haga", "hagas", "haga", "hagamos", "hagáis", "hagan"},
             // Pretérito Imperfecto Subjuntivo
             {"hiciera", "hicieras", "hiciera", "hiciéramos", "hicierais", "hicieran"},
             // Futuro Imperfecto Subjuntivo
             {"hiciere", "hicieres", "hiciere", "hiciéremos", "hiciereis", "hicieren"},
             // Futuro Potencial
             {"haría", "harías", "haría", "haríamos", "haríais", "harían"}
         },{
             // 11 - poner

             // Presente Indicativo
             {"pongo", "pones", "pone", "ponemos", "ponéis", "ponen"},
             // Pretérito Indefinido Indicativo
             {"puse", "pusiste", "puso", "pusimos", "pusisteis", "pusieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"pondré", "pondrás", "pondrá", "pondremos", "pondréis", "pondrán"},
             // Presente Subjuntivo
             {"ponga", "pongas", "ponga", "pongamos", "pongáis", "pongan"},
             // Pretérito Imperfecto Subjuntivo
             {"pusiera", "pusieras", "pusiera", "pusiéramos", "pusierais", "pusieran"},
             // Futuro Imperfecto Subjuntivo
             {"pusiere", "pusieres", "pusiere", "pusiéremos", "pusiereis", "pusieren"},
             // Futuro Potencial
             {"pondría", "pondrías", "pondría", "pondríamos", "pondríais", "pondrían"}
         },{
             // 12 - saber

             // Presente Indicativo
             {"sé", "sabes", "sabe", "sabemos", "sabéis", "saben"},
             // Pretérito Indefinido Indicativo
             {"supe", "supiste", "supo", "supimos", "supisteis", "supieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"sabré", "sabrás", "sabrá", "sabremos", "sabréis", "sabrán"},
             // Presente Subjuntivo
             {"sepa", "sepas", "sepa", "sepamos", "sepáis", "sepan"},
             // Pretérito Imperfecto Subjuntivo
             {"supiera", "supieras", "supiera", "supiéramos", "supierais", "supieran"},
             // Futuro Imperfecto Subjuntivo
             {"supiere", "supieres", "supiere", "supiéremos", "supiereis", "supieren"},
             // Futuro Potencial
             {"sabría", "sabrías", "sabría", "sabríamos", "sabríais", "sabrían"}
         },{
             // 13 - traer

             // Presente Indicativo
             {"traigo", "traes", "trae", "traemos", "traéis", "traen"},
             // Pretérito Indefinido Indicativo
             {"traje", "trajiste", "trajo", "trajimos", "trajisteis", "trajeron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"traiga", "traigas", "traiga", "traigamos", "traigáis", "traigan"},
             // Pretérito Imperfecto Subjuntivo
             {"trajera", "trajeras", "trajera", "trajéramos", "trajerais", "trajeran"},
             // Futuro Imperfecto Subjuntivo
             {"trajere", "trajeres", "trajere", "trajéremos", "trajereis", "trajeren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 14 - valer

             // Presente Indicativo
             {"valgo", "vales", "vale", "valemos", "valéis", "valen"},
             // Pretérito Indefinido Indicativo
             {"", "", "", "", "", ""},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"valdré", "valdrás", "valdrá", "valdremos", "valdréis", "valdrán"},
             // Presente Subjuntivo
             {"valga", "valgas", "valga", "valgamos", "valgáis", "valgan"},
             // Pretérito Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Potencial
             {"valdría", "valdrías", "valdría", "valdríamos", "valdríais", "valdrían"}
         },{
             // 15 - ver

             // Presente Indicativo
             {"veo", "ves", "ve", "vemos", "veis", "ven"},
             // Pretérito Indefinido Indicativo
             {"vi", "viste", "vio", "vimos", "visteis", "vieron"},
             // Preterito Imperfecto Indicativo
             {"veía", "veías", "veía", "veíamos", "veíais", "veian"},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"vea", "veas", "vea", "veamos", "veáis", "vean"},
             // Pretérito Imperfecto Subjuntivo
             {"viera", "vieras", "viera", "viéramos", "vierais", "vieran"},
             // Futuro Imperfecto Subjuntivo
             {"viere", "vieres", "viere", "viéremos", "viereis", "vieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 16 - haber

             // Presente Indicativo
             {"he", "has", "ha", "hemos", "habéis", "han"},
             // Pretérito Indefinido Indicativo
             {"hube", "hubiste", "hubo", "hubimos", "hubisteis", "hubieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"habré", "habrás", "habrá", "habremos", "habréis", "habrán"},
             // Presente Subjuntivo
             {"haya", "hayas", "haya", "hayamos", "hayáis", "hayan"},
             // Pretérito Imperfecto Subjuntivo
             {"hubiera", "hubieras", "hubiera", "hubiéramos", "hubierais", "hubieran"},
             // Futuro Imperfecto Subjuntivo
             {"hubiere", "hubieres", "hubiere", "hubiéremos", "hubiereis", "hubieren"},
             // Futuro Potencial
             {"habría", "habrías", "habría", "habríamos", "habríais", "habrían"}
         },{
             // 17 - ser

             // Presente Indicativo
             {"soy", "eres", "es", "somos", "sois", "son"},
             // Pretérito Indefinido Indicativo
             {"fui", "fuiste", "fue", "fuimos", "fuisteis", "fueron"},
             // Preterito Imperfecto Indicativo
             {"era", "eras", "era", "éramos", "erais", "eran"},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},      
             // Presente Subjuntivo
             {"sea", "seas", "sea", "seamos", "seáis", "sean"},
             // Pretérito Imperfecto Subjuntivo
             {"fuera", "fueras", "fuera", "fuéramos", "fuerais", "fueran"},
             // Futuro Imperfecto Subjuntivo
             {"fuere", "fueres", "fuere", "fuéremos", "fuereis", "fueren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
             // Preterito Imperfecto
         },{
             // 18 - venir

             // Presente Indicativo
             {"vengo", "vienes", "viene", "venimos", "venís", "vienen"},
             // Pretérito Indefinido Indicativo
             {"vine", "viniste", "vino", "vinimos", "vinisteis", "vinieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"vendré", "vendrás", "vendrá", "vendremos", "vendréis", "vendrán"},
             // Presente Subjuntivo
             {"venga", "vengas", "venga", "vengamos", "vengáis", "vengan"},
             // Pretérito Imperfecto Subjuntivo
             {"viniera", "vinieras", "viniera", "viniéramos", "vinierais", "vinieran"},
             // Futuro Imperfecto Subjuntivo
             {"viniere", "vinieres", "viniere", "viniéremos", "viniereis", "vinieren"},
             // Futuro Potencial
             {"vendría", "vendrías", "vendría", "vendríamos", "vendríais", "vendrían"}
         },{
             // 19 - decir

             // Presente Indicativo
             {"digo", "dices", "dice", "decimos", "decís", "dicen"},
             // Pretérito Indefinido Indicativo
             {"dije", "dijiste", "dijo", "dijimos", "dijisteis", "dijeron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"diré", "dirás", "dirá", "diremos", "diréis", "dirán"},
             // Presente Subjuntivo
             {"diga", "digas", "diga", "digamos", "digáis", "digan"},
             // Pretérito Imperfecto Subjuntivo
             {"dijera", "dijeras", "dijera", "dijéramos", "dijerais", "dijeran"},
             // Futuro Imperfecto Subjuntivo
             {"dijere", "dijeres", "dijere", "dijéremos", "dijereis", "dijeren"},
             // Futuro Potencial
             {"diría", "dirías", "diría", "diríamos", "diríais", "diría"}
         },{
             // 20 - erguir

             // Presente Indicativo
             {"irgo", "irgues", "irgue", "erguimos", "erguís", "irguen"},
             // Pretérito Indefinido Indicativo
             {"erguí", "erguiste", "irguió", "erguimos", "erguisteis", "irguieron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"irga", "irgas", "irga", "irgamos", "irgáis", "irgan"},
             // Pretérito Imperfecto Subjuntivo
             {"irguiera", "irguieras", "irguiera", "irguiéramos", "irguierais", "irguieran"},
             // Futuro Imperfecto Subjuntivo
             {"irguiere", "irguieres", "irguiere", "irguiéremos", "irguiereis", "irguieren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 21 - asir

             // Presente Indicativo
             {"asgo", "ases", "ase", "asimos", "asís", "asen"},
             // Pretérito Indefinido Indicativo
             {"", "", "", "", "", ""},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"asga", "asgas", "asga", "asgamos", "asgáis", "asgan"},
             // Pretérito Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 22 - salir

             // Presente Indicativo
             {"salgo", "sales", "sale", "salimos", "salís", "salen"},
             // Pretérito Indefinido Indicativo
             {"", "", "", "", "", ""},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"saldré", "saldrás", "saldrá", "saldremos", "saldréis", "saldrán"},
             // Presente Subjuntivo
             {"salga", "salgas", "salga", "salgamos", "salgáis", "salgan"},
             // Pretérito Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Subjuntivo
             {"", "", "", "", "", ""},
             // Futuro Potencial
             {"saldría", "saldrías", "saldría", "saldríamos", "saldríais", "saldrían"}
         },{
             // 23 - oír

             // Presente Indicativo
             {"oigo", "oyes", "oye", "oímos", "oís", "oyen"},
             // Pretérito Indefinido Indicativo
             {"oí", "oíste", "oyó", "oímos", "oísteis", "oyeron"},
             // Preterito Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"oiga", "oigas", "oiga", "oigamos", "oigáis", "oigan"},
             // Pretérito Imperfecto Subjuntivo
             {"oyera", "oyeras", "oyera", "oyéramos", "oyerais", "oyeran"},
             // Futuro Imperfecto Subjuntivo
             {"oyere", "oyeres", "oyere", "oyéremos", "oyereis", "oyeren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
         },{
             // 24 - ir

             // Presente Indicativo
             {"voy", "vas", "va", "vamos", "vais", "van"},
             // Pretérito Indefinido Indicativo
             {"fui", "fuiste", "fue", "fuimos", "fuisteis", "fueron"},
             // Preterito Imperfecto Indicativo
             {"iba", "ibas", "iba", "íbamos", "ibais", "iban"},
             // Futuro Imperfecto Indicativo
             {"", "", "", "", "", ""},
             // Presente Subjuntivo
             {"vaya", "vayas", "vaya", "vayamos", "vayáis", "vayan"},
             // Pretérito Imperfecto Subjuntivo
             {"fuera", "fueras", "fuera", "fuéramos", "fuerais", "fueran"},
             // Futuro Imperfecto Subjuntivo
             {"fuere", "fueres", "fuere", "fuéremos", "fuereis", "fueren"},
             // Futuro Potencial
             {"", "", "", "", "", ""}
        }};
    }
}