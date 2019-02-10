using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Morphology;



namespace SpaxeDictionary
{
    public partial class FormForms : Form
    {
        private const int LEFT          = 0;
        private const int TOP           = 100;
        private const int LEFT_OFFSET   = 50;
        private const int TOP_OFFSET    = 7;
        private const int WIDTH         = 200;
        private const int HEIGHT        = 200;




        public FormForms(DictionaryArticle article)
        {
            InitializeComponent();
            this.Text = article.word;


            Label labelWord = new Label();
            labelWord.Text = article.word;
            labelWord.Width = 200;
            labelWord.Left = 10;
            labelWord.Top = 10;
            this.Controls.Add(labelWord);

            String[] rim = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"};
            String signature = "conj. " + rim[article.conjugation] + ", ";

            if (article.Group == 10)
            {
                signature += "reg.";
            }
            else
            {
                if (article.Group == 0)
                {
                    signature += "ind.";
                }
                else
                {
                    signature += "irreg. " + rim[article.Group - 1];
                }
            }

            Label labelSignature = new Label();
            labelSignature.Text = signature;
            labelSignature.Width = 200;
            labelSignature.Left = 10;
            labelSignature.Top = 35;
            this.Controls.Add(labelSignature);

            Label labelTranslation = new Label();
            labelTranslation.Text = article.translation;
            labelTranslation.Width = 500;
            labelTranslation.Left = 10;
            labelTranslation.Top = 60;
            this.Controls.Add(labelTranslation);



            int left = LEFT + LEFT_OFFSET;
            int top = TOP + TOP_OFFSET;
            for (byte tense = 0; tense <= 3; tense++)
            {
                DisplayForms(article, tense, left, top);
                left += WIDTH;
            }

            left = LEFT + LEFT_OFFSET;
            top += HEIGHT;
            for (byte tense = 8; tense <= 11; tense++)
            {
                DisplayForms(article, tense, left, top);
                left += WIDTH;
            }

            left = LEFT + LEFT_OFFSET;
            top += HEIGHT;
            for (byte tense = 4; tense <= 6; tense++)
            {
                DisplayForms(article, tense, left, top);
                left += WIDTH;
            }

            left = LEFT + LEFT_OFFSET;
            top += HEIGHT;
            for (byte tense = 12; tense <= 14; tense++)
            {
                DisplayForms(article, tense, left, top);
                left += WIDTH;
            }

            left = LEFT + LEFT_OFFSET;
            top += HEIGHT;
            DisplayForms(article, 7, left, top);

            left += WIDTH;
            DisplayForms(article, 15, left, top);


            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = global::SpaxeDictionary.Properties.Resources.FrameForms;
            pictureBox.Left = LEFT;
            pictureBox.Top = TOP;
            pictureBox.Width = LEFT_OFFSET + 4 * WIDTH;
            pictureBox.Height = 5 * HEIGHT;
            this.Controls.Add(pictureBox);
        }


        private void DisplayForms(DictionaryArticle article, byte tense, int left, int top)
        {
            List<string> forms = Conjugator.Conjugate(article, tense);

            Label labelTense = new Label();
            labelTense.Text = Grammar.tenses[tense];
            labelTense.Width = 190;
            labelTense.BackColor = Color.FromArgb(230, 230, 230);
            labelTense.Left = left;
            labelTense.Top = top;
            this.Controls.Add(labelTense);

            top += 37;

            foreach (String form in forms)
            {
                Label labelForm = new Label();
                labelForm.Text = form;
                labelForm.Width = 180;
                labelForm.Left = left;
                labelForm.Top = top;
                this.Controls.Add(labelForm);

                top += 25;
            }
        }
    }
}