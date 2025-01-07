namespace Horizontal_Dot_pattern_Generater
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PatternLabel = new System.Windows.Forms.Label();
            this.AddHex = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Reset_Hex = new System.Windows.Forms.Button();
            this.PatternLabel2 = new System.Windows.Forms.Label();
            this.PatternLabel3 = new System.Windows.Forms.Label();
            this.overfilled = new System.Windows.Forms.Label();
            this.OutputFile = new System.Windows.Forms.Button();
            this.OutputFileName = new System.Windows.Forms.TextBox();
            this.OutputFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output:";
            // 
            // PatternLabel
            // 
            this.PatternLabel.Location = new System.Drawing.Point(105, 9);
            this.PatternLabel.Name = "PatternLabel";
            this.PatternLabel.Size = new System.Drawing.Size(461, 1157);
            this.PatternLabel.TabIndex = 1;
            this.PatternLabel.Text = "insert_Patterns";
            // 
            // AddHex
            // 
            this.AddHex.Location = new System.Drawing.Point(2007, 180);
            this.AddHex.Name = "AddHex";
            this.AddHex.Size = new System.Drawing.Size(115, 84);
            this.AddHex.TabIndex = 3;
            this.AddHex.Text = "Add Hex String";
            this.AddHex.UseVisualStyleBackColor = true;
            this.AddHex.Click += new System.EventHandler(this.AddHex_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2007, 618);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 65);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reset_Hex
            // 
            this.Reset_Hex.Location = new System.Drawing.Point(2007, 270);
            this.Reset_Hex.Name = "Reset_Hex";
            this.Reset_Hex.Size = new System.Drawing.Size(115, 65);
            this.Reset_Hex.TabIndex = 4;
            this.Reset_Hex.Text = "Reset";
            this.Reset_Hex.UseVisualStyleBackColor = true;
            this.Reset_Hex.Click += new System.EventHandler(this.Reset_Hex_Click);
            // 
            // PatternLabel2
            // 
            this.PatternLabel2.Location = new System.Drawing.Point(572, 9);
            this.PatternLabel2.Name = "PatternLabel2";
            this.PatternLabel2.Size = new System.Drawing.Size(461, 1157);
            this.PatternLabel2.TabIndex = 5;
            this.PatternLabel2.Text = "insert_Patterns";
            // 
            // PatternLabel3
            // 
            this.PatternLabel3.Location = new System.Drawing.Point(1039, 9);
            this.PatternLabel3.Name = "PatternLabel3";
            this.PatternLabel3.Size = new System.Drawing.Size(461, 1157);
            this.PatternLabel3.TabIndex = 6;
            this.PatternLabel3.Text = "insert_Patterns";
            // 
            // overfilled
            // 
            this.overfilled.Location = new System.Drawing.Point(1542, 1088);
            this.overfilled.Name = "overfilled";
            this.overfilled.Size = new System.Drawing.Size(121, 67);
            this.overfilled.TabIndex = 7;
            this.overfilled.Text = "FULL";
            this.overfilled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OutputFile
            // 
            this.OutputFile.Location = new System.Drawing.Point(1865, 455);
            this.OutputFile.Name = "OutputFile";
            this.OutputFile.Size = new System.Drawing.Size(115, 66);
            this.OutputFile.TabIndex = 8;
            this.OutputFile.Text = "Output to file";
            this.OutputFile.UseVisualStyleBackColor = true;
            this.OutputFile.Click += new System.EventHandler(this.OutputFile_Click);
            // 
            // OutputFileName
            // 
            this.OutputFileName.Location = new System.Drawing.Point(1741, 420);
            this.OutputFileName.Name = "OutputFileName";
            this.OutputFileName.Size = new System.Drawing.Size(381, 29);
            this.OutputFileName.TabIndex = 9;
            // 
            // OutputFileLabel
            // 
            this.OutputFileLabel.AutoSize = true;
            this.OutputFileLabel.Location = new System.Drawing.Point(1860, 338);
            this.OutputFileLabel.Name = "OutputFileLabel";
            this.OutputFileLabel.Size = new System.Drawing.Size(161, 25);
            this.OutputFileLabel.TabIndex = 10;
            this.OutputFileLabel.Text = "Output File name";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2134, 1164);
            this.Controls.Add(this.OutputFileLabel);
            this.Controls.Add(this.OutputFileName);
            this.Controls.Add(this.OutputFile);
            this.Controls.Add(this.overfilled);
            this.Controls.Add(this.PatternLabel3);
            this.Controls.Add(this.PatternLabel2);
            this.Controls.Add(this.Reset_Hex);
            this.Controls.Add(this.AddHex);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PatternLabel);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PatternLabel;
        private System.Windows.Forms.Button AddHex;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Reset_Hex;
        private System.Windows.Forms.Label PatternLabel2;
        private System.Windows.Forms.Label PatternLabel3;
        private System.Windows.Forms.Label overfilled;
        private System.Windows.Forms.Button OutputFile;
        private System.Windows.Forms.TextBox OutputFileName;
        private System.Windows.Forms.Label OutputFileLabel;
    }
}