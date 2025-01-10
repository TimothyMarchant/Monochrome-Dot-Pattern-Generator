/*
 * Made by Timothy Marchant
 * For the person reading this, this project exists so I could write characters to a display without saving a full buffer for a display.
 * In short we can make a table of dot patterns (in particular 8x8 pixel squares, where characters only occupy a 5x7 pixel area).
 * For example in the common SSD1306 display (128x64), normally you could save an entire buffer using 1024 bytes of ram.
 * But with this trick we can index a table for saved patterns and display each pattern for each 8x8 pixel square
 * So instead of a 128x64 pixel display you have a 16x8 character display and your buffer can be an array of characters.  In this particular case this takes up 128 bytes of ram.
 * This is clearly a good solution if your project only need to display text and you have plenty of programmable storage on your microcontroller 
 * (I think when I did this last with a PIC, it took about 1000 bytes of programmable storage with the drivers needed for I2C and controlling the SSD1306).
 * The current project I'm working on is for an epaper screen from Pervaise Displays.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horizontal_Dot_pattern_Generater
{
    //First window.  Lets you make the dot pattern.  There is another window for actually saving them.  Or you can just make one pattern and copy and paste it into something else.
    public partial class Form1 : Form
    {
        private bool InvertedColor = false;
        private Button[] ButtonArray;
        private Label[] LabelArray;
        private Label[] HorizontalBytesLabels;
        private Label[] VerticalBytesLabels;
        private int HexStringLength=7;
        private int HexStringWidth = 5;
        private string hexstring;
        //most significant byte which is used in most SPI transfers.  Some SPI registers can be configured for least significant byte (LSB) transfers.
        private bool MSB;
        //Depending on the display one byte may be a line of 8 pixels either horizontally or vertically.  For example on the SSD1306 it's vertical by default.  For the Pervaise Display epaper screen I'm using it's horizontal.
        private bool VerticalPixels;
        //dotpattern
        private uint[] pixels;
        public Form1()
        {
            InitializeComponent();
            //fill arrays.
            ButtonArray =new [] { button1,button2, button3,button4,button5,button6,button7,button8,button9,button10,button11,button12,button13,button14,button15,
            button16,button17,button18,button19,button20,button21,button22,button23,button24,button25,button26,button27,button28,button29,button30,
            button31,button32,button33,button34,button35};
            LabelArray = new[] {label1,label2,label3,label4,label5,label6,label7,label8,label9,label10,label11,label12,label13,label14,label15,
            label16,label17,label18,label19,label20,label21,label22,label23,label24,label25,label26,label27,label28,label29,label30,
            label31,label32,label33,label34,label35};
            pixels = new uint[7];
            HorizontalBytesLabels = new[] { label36, label37, label38, label39, label40, label41, label42 };
            VerticalBytesLabels = new[] { label45, label46, label47, label48,label49 };
            //remove text on top with this for loop on startup.  Default is horizontal mode.
            for (int i = 0; i < VerticalBytesLabels.Length; i++)
            {
                VerticalBytesLabels[i].Text = "";
            }
            Reset_Form();
            //give a default string.
            hexstring = "0x00,0x00,0x00,0x00,0x00,0x00,0x00";
            //start in most significant byte mode and horizontal mode.
            MSB = true;
            VerticalPixels = false;
        }
        //get function for the current hex string.
        public string GetHex()
        {
            return hexstring;
        }
        //general button click method.  This avoids having 35 different methods for buttons.
        private void GeneralButtonClick(object sender, EventArgs e)
        {
            for (int i = 0; i < ButtonArray.Length; i++)
            {
                if (ButtonArray[i] == (Button) sender)
                {
                    uint temp = uint.Parse(ButtonArray[i].Text);
                    temp = (temp + 1) % 2;
                    ButtonArray[i].Text = temp.ToString();
                    //do this math to get which bit we need to modify.
                    int row = i / HexStringWidth;
                    int col=i % HexStringWidth;
                    if (!InvertedColor)
                    {
                        if (temp == 1)
                        {
                            LabelArray[i].Text = "*";
                            
                            bitwrite(row, col);
                        }
                        else
                        {
                            LabelArray[i].Text = "";
                            bitdelete(row, col);
                        }
                    }
                    else
                    {
                        if (temp == 1)
                        {
                            LabelArray[i].Text = "";
                            bitwrite(row, col);
                        }
                        else
                        {
                            LabelArray[i].Text = "*";
                            bitdelete(row, col);
                        }
                    }
                    
                    break;
                }
            }
            UpdateHex();
        }
        //starting bit position, depends on whether or not we are concerned with most significant bit or least significant bit.
        private const uint MSBmode= 0x80;
        private const uint LSBmode = 0x01;
        private void bitwrite(int row, int col)
        {
            //swap row and column.  I did this to avoid writing another method.
            if (VerticalPixels)
            {
                int temp = row;
                row = col;
                col= temp;
            }
            if (MSB)
            {
                if (!VerticalPixels)
                {
                    pixels[row] = pixels[row] | (MSBmode >> col);
                }
                else
                {
                    pixels[col] = pixels[col] | (MSBmode >> row);
                }
            }
            else
            {
                if (!VerticalPixels)
                {
                    pixels[row] = pixels[row] | (LSBmode << col);
                }
                else
                {
                    pixels[col] = pixels[col] | (LSBmode << row);
                }
            }

        }
        private void bitdelete(int row,int col)
        {
            if (VerticalPixels)
            {
                int temp = row;
                row = col;
                col = temp;
            }
            if (MSB)
            {
                if (!VerticalPixels)
                {
                    pixels[row] = pixels[row] & ~(MSBmode >> col);
                }
                else
                {
                    pixels[col] = pixels[col] & ~(MSBmode >> row);
                }

            }
            else
            {
                if (!VerticalPixels)
                {
                    pixels[row] = pixels[row] & ~(LSBmode << col);
                }
                else
                {
                    pixels[col] = pixels[col] & ~(LSBmode << row);
                }
            }
        }
        //reset the form and make it ready for inputting the next pattern.
        private void Reset_Form()
        {
            for (int i = 0; i < ButtonArray.Length; i++)
            {
                //setup text
                if (!InvertedColor)
                {
                    ButtonArray[i].Text = "0";
                }
                else
                {
                    ButtonArray[i].Text = "1";
                }
                LabelArray[i].Text = "";

            }
            for (int i = 0; i < HexStringLength; i++)
            {
                pixels[i] = 0;
                if (InvertedColor)
                {
                    pixels[i] = 0xFF;
                }
            }
            UpdateHex();
        }
        private void Reset_Click(object sender, EventArgs e)
        {
            Reset_Form();
        }
        //updates hex string
        private void UpdateHex()
        {
            
            string Output = "";
            //to make copying the hex values easier for another IDE to read.
            for (int i = 0; i < HexStringLength; i++)
            {
                Output = Output + "0x"+pixels[i].ToString("X");
                if (i != HexStringLength-1)
                {
                    Output += ",";
                }
            }
            HexOutput.Text = Output;
            hexstring = Output;
        }
        //black and white only (or for other displays 0 or 1; for example the SSD1306 ON and OFF)
        private void InvertColorButton_Click(object sender, EventArgs e)
        {
            if (!InvertedColor)
            {
                InvertedColor=true;
                InvertColorButton.BackColor = Color.White;
            }
            else
            {
                InvertedColor=false;
                InvertColorButton.BackColor = Color.Gray;
            }

            Reset_Form();
        }
        //load form2
        private void button36_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(this);
            form.Show();
        }

        private void MSBButton_Click(object sender, EventArgs e)
        {
            if (MSB)
            {
                //LSB mode
                MSB = false;
                MSBButton.Text = "LSB";
            }
            else
            {
                MSB = true;
                MSBButton.Text = "MSB";
            }
            Reset_Form();
        }

        private void VerticalHortMode_Click(object sender, EventArgs e)
        {
            if (!VerticalPixels)
            {
                VerticalPixels = true;
                VerticalHortMode.Text = "Vertical Mode";
                //To avoid creating another set of methods for vertical mode we need to change the order of elements in the button and label array.  This order works for the buttons vertically.
                ButtonArray = new[] { button1,button6, button11,button16,button21,button26,button31,button2,button7,button12,button17,button22,button27,button32,button3,
            button8,button13,button18,button23,button28,button33,button4,button9,button14,button19,button24,button29,button34,button5,button10,
            button15,button20,button25,button30,button35};
                //same thing as before but with labels.
                LabelArray = new[] {label1,label6,label11,label16,label21,label26,label31,label2,label7,label12,label17,label22,label27,label32,label3,
            label8,label13,label18,label23,label28,label33,label4,label9,label14,label19,label24,label29,label34,label5,label10,
            label15,label20,label25,label30,label35};
                //display the bytes that we will be editing.
                for (int i = 0; i < VerticalBytesLabels.Length; i++)
                {
                    VerticalBytesLabels[i].Text = "Byte " + (i + 1);
                }
                for (int i=0;i<HorizontalBytesLabels.Length; i++)
                {
                    HorizontalBytesLabels[i].Text = "";
                }
            }
            else 
            { 
                VerticalPixels = false;
                VerticalHortMode.Text = "Horizontal Mode";
                //To avoid creating another set of methods for vertical mode we need to change the order of elements in the button and label array.  This order works for the buttons horizontally.
                ButtonArray = new[] { button1,button2, button3,button4,button5,button6,button7,button8,button9,button10,button11,button12,button13,button14,button15,
            button16,button17,button18,button19,button20,button21,button22,button23,button24,button25,button26,button27,button28,button29,button30,
            button31,button32,button33,button34,button35};
                //same thing as before but with labels.
                LabelArray = new[] {label1,label2,label3,label4,label5,label6,label7,label8,label9,label10,label11,label12,label13,label14,label15,
            label16,label17,label18,label19,label20,label21,label22,label23,label24,label25,label26,label27,label28,label29,label30,
            label31,label32,label33,label34,label35};
                //display the bytes that we will be editing.
                for (int i = 0; i < VerticalBytesLabels.Length; i++)
                {
                    VerticalBytesLabels[i].Text = "";
                }
                for (int i = 0; i < HorizontalBytesLabels.Length; i++)
                {
                    HorizontalBytesLabels[i].Text = "Byte " + (i + 1);
                }
            }
            //convert the lengths to avoid having extra cases.
            int temp = HexStringWidth;
            HexStringWidth = HexStringLength;
            HexStringLength = temp;
            Reset_Form();
        }
    }
}
