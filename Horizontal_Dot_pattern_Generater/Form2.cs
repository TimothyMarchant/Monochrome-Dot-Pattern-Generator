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
        private const int maxstringheight = 46;
        public Form2(Form1 form)
        {
            form1= form;
            InitializeComponent();
            HexString = new List<string>();
            SetupOutput();
            //TestPrintDotPatterns();
        }
        private void TestPrintDotPatterns()
        {
            for (int i = 0; i < 3 * maxstringheight; i++)
            {
                HexString.Add(form1.GetHex());
            }
            PrintDotPatterns();
        }
        private void SetupOutput()
        {
            PatternLabel.Text= "struct CharPattern[" + HexString.Count() + "]= {\n";
            PatternLabel2.Text = "";
            PatternLabel3.Text = "";
            overfilled.Text = "";
        }
        //adjust the labels to print the saved hex strings into one convient place.
        //These patterns are printed for use in the following struct in C.
        /*
         * typedef struct CharPattern{
         * unsigned char line0;
         * unsigned char line1;
         * unsigned char line2;
         * unsigned char line3;
         * unsigned char line4;
         * unsigned char line5;
         * unsigned char line6;
         * }CharPattern;
         */
        //in the case of vertical patterns omit line5 and line6.
        private void PrintDotPatterns()
        {
            //we can only fit so many hex strings onto one label, so we have a maximum of 3 with height 46 strings.
            string[] entirestring =new string[3];
            int Stringindex = 0;
            //This is how an array of struts are meant to be declared in the 99 standard of C.
            entirestring[Stringindex] = "CharPattern InsertName[" + HexString.Count() + "]= {\n";
            uint i = 0;
            //do a lot of catanations in this loop.
            foreach (string s in HexString)
            {
                string[] t = s.Split(',');
                entirestring[Stringindex] += "{";
                for (uint j = 0; j < t.Length; j++)
                {
                    entirestring[Stringindex] += t[j];
                    if (i != t.Length - 1)
                    {
                        entirestring[Stringindex] += ",";
                    }
                }
                if (i != HexString.Count() - 1)
                {
                    entirestring[Stringindex] += "}, //" + i+"\n";
                }
                else
                {
                    entirestring[Stringindex] += "} //" + i;
                }
                i++;
                //increases if and only if divisible by
                if (i % maxstringheight == 0&&Stringindex!=2)
                {
                    Stringindex++;
                }
                //no more entries may be added at this point.
                else if (i % maxstringheight == 0 && Stringindex == 2)
                {
                    overfilled.Text = "FULL no more space";
                    break;
                }
            }
            //display the new strings.
            PatternLabel.Text = entirestring[0];
            PatternLabel2.Text = entirestring[1];
            PatternLabel3.Text = entirestring[2];
        }
        //close this window
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //add new hex string to the list.
        private void AddHex_Click(object sender, EventArgs e)
        {
            HexString.Add(form1.GetHex());
            PrintDotPatterns();
        }
        //Reset this window.
        private void Reset_Hex_Click(object sender, EventArgs e)
        {
            HexString.Clear();
            SetupOutput();
        }
    }
}
