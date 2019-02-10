using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace SpaxeDictionary
{
    public partial class FormStatistics : Form
    {
        public FormStatistics(int count)
        {
            InitializeComponent();

            labelCount.Text = count.ToString();
        }
    }
}