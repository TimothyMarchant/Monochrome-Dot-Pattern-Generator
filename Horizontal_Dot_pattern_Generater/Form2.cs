using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Horizontal_Dot_pattern_Generater
{
    //general output window.  Allows multiple patterns to be saved.
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
        //for testing purposes will be removed in a final version.
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
            PatternLabel.Text= "CharPattern[" + HexString.Count() + "]= {\n";
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
            string structname = StructName.Text;
            if (structname.Length == 0)
            {
                structname = "DefaultName";
            }
            //This is how an array of struts are meant to be declared in the 99 standard of C.
            entirestring[Stringindex] = "CharPattern "+structname+"[" + HexString.Count() + "]= {\n";
            uint i = 0;
            //do a lot of catanations in this loop.
            foreach (string s in HexString)
            {
                string[] t = s.Split(',');
                entirestring[Stringindex] += "{";
                for (uint j = 0; j < t.Length; j++)
                {
                    entirestring[Stringindex] += t[j];
                    if (j != t.Length - 1)
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
                    entirestring[Stringindex] += "\n}";
                    break;
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
        const int FileExtensionindex = 1;
        //output an actual file with the array saved.
        private void OutputFile_Click(object sender, EventArgs e)
        {

            string outputfile = OutputFileName.Text;
            if (outputfile.Length == 0)
            {
                //default output file name.
                outputfile = "ASCII_Table.h";
            }
            string[] CheckIfFileType = outputfile.Split('.');
            //invalid file type, and I don't feel like dealing with these cases considering the main user (probably the ONLY user) of this application won't make such mistakes unintentionally.
            if (CheckIfFileType.Length > 2)
            {
                //the sleep is a delay so that the text doesn't change back too fast.  There's definitely a better way to do this than using a blocking delay. but it is what it is.
                OutputFile.Text = "INVALID FILE NAME";
                Thread.Sleep(1000);
                OutputFile.Text = "Output to file";
                return;
            }
            //default file type
            else if (CheckIfFileType.Length == 1)
            {
                outputfile += ".h";
            }
            //only supports 3 formats, because I don't want to deal with weird file extensions.
            else if (CheckIfFileType.Length == 2)
            {
                if (CheckIfFileType[FileExtensionindex]!="txt"&& CheckIfFileType[FileExtensionindex] != "c"
                    && CheckIfFileType[FileExtensionindex] != "h")
                {
                    //if not a supported file type print this.
                    OutputFile.Text = "Unsupported file extension";
                    Thread.Sleep(1000);
                    OutputFile.Text = "Output to file";
                    return;
                }
            }
            //write to the file.
            StreamWriter writer = new StreamWriter(outputfile);
            writer.Write(PatternLabel.Text);
            writer.Write(PatternLabel2.Text);
            writer.Write(PatternLabel3.Text);
            writer.Close(); 
        }
    }
}
