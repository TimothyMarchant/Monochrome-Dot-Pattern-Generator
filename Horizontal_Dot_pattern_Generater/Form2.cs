using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horizontal_Dot_pattern_Generater
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        private List<string> HexString;
        
        public Form2(Form1 form)
        {
            form1= form;
            InitializeComponent();
            HexString = new List<string>();
            SetupOutput();
        }
        private void SetupOutput()
        {
            PatternLabel.Text= "struct CharPattern[" + HexString.Count() + "]= {\n";
        }
        private void PrintDotPatterns()
        {
            string entirestring;
            entirestring = "struct CharPattern InsertName[" + HexString.Count() + "]= {\n";
            uint i = 0;
            foreach (string s in HexString)
            {
                string[] t = s.Split(',');
                entirestring += "{";
                for (uint j = 0; j < t.Length; j++)
                {
                    entirestring += t[j];
                    if (i != t.Length - 1)
                    {
                        entirestring += ",";
                    }
                }
                if (i != HexString.Count() - 1)
                {
                    entirestring += "}, //" + i+"\n";
                }
                else
                {
                    entirestring += "} //" + i;
                }
                i++;
            }
            PatternLabel.Text = entirestring;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddHex_Click(object sender, EventArgs e)
        {
            HexString.Add(form1.GetHex());
            PrintDotPatterns();
        }

        private void Reset_Hex_Click(object sender, EventArgs e)
        {
            HexString.Clear();
            SetupOutput();
        }
    }
}
