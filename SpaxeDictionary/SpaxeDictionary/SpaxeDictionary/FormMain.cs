using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Morphology;
using Converter;



namespace SpaxeDictionary
{
    public partial class FormMain : Form
    {
        private Dictionary<String, DictionaryArticle> dictionary;



        public FormMain()
        {
            InitializeComponent();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            //Open(@"dictionary.sdf");
            //tableLayoutPanel.Visible = true;
        }


        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (dictionary == null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Spaxe Dictionary Format (*.sdf)|*.sdf | Lingvo Tutor Format (*.xml)|*.xml";
                dialog.ShowDialog();
                String fileName = dialog.FileName;

                if (fileName != "")
                {
                    Open(fileName);
                    tableLayoutPanel.Visible = true;
                }
            }
        }
        
        
        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            if (dictionary != null)
            {
                tableLayoutPanel.Visible = false;
                listBox.Items.Clear();
                dictionary = null;
            }
        }

        
        private void toolStripMenuItemStatistics_Click(object sender, EventArgs e)
        {
            if (dictionary != null)
            {
                FormStatistics form = new FormStatistics(dictionary.Keys.Count);
                form.ShowDialog();
            }
        }


        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (listBox.IndexFromPoint(e.Location) == listBox.SelectedIndex)
                {
                    String word = listBox.SelectedItem.ToString();
                    DictionaryArticle article = dictionary[word];

                    if (article.type == 'V')
                    {
                        FormForms form = new FormForms(article);
                        form.Show();
                    }
                }
            }
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            //textBox.Text = listBox.SelectedItem.ToString();
        }



        private void listBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (listBox.SelectedIndex != -1)
                {
                    String word = listBox.SelectedItem.ToString();
                    DictionaryArticle article = dictionary[word];

                    if (article.type == 'V')
                    {
                        FormForms form = new FormForms(article);
                        form.Show();
                    }
                }
            }
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            int index = listBox.FindString(textBox.Text);

            if (index != -1)
            {
                listBox.SelectedIndex = index;
                listBox.TopIndex = index;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (listBox.SelectedIndex != -1)
                {
                    String word = listBox.SelectedItem.ToString();
                    DictionaryArticle article = dictionary[word];

                    if (article.type == 'V')
                    {
                        FormForms form = new FormForms(article);
                        form.Show();
                    }
                }
            }
        }

        
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Open(String fileName)
        {
            String extention = fileName.Substring(fileName.LastIndexOf('.'));


            dictionary = Import.ImportFromSpaxe(fileName);



            var sortedKeys = from key in dictionary.Keys
                             orderby key ascending
                             select key;


            foreach (String key in sortedKeys)
            {
                listBox.Items.Add(key);
            }

            listBox.SelectedIndex = 0;
        }
    }
}