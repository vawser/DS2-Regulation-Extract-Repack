namespace DS2_Param_Extract
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.t_ModPath = new System.Windows.Forms.TextBox();
            this.b_Extract = new System.Windows.Forms.Button();
            this.b_SelectMod = new System.Windows.Forms.Button();
            this.l_status = new System.Windows.Forms.Label();
            this.c_IncludeEMEVD = new System.Windows.Forms.CheckBox();
            this.c_IncludeFMG = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.b_Repack = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // t_ModPath
            // 
            this.t_ModPath.Location = new System.Drawing.Point(12, 27);
            this.t_ModPath.Name = "t_ModPath";
            this.t_ModPath.Size = new System.Drawing.Size(403, 23);
            this.t_ModPath.TabIndex = 0;
            // 
            // b_Extract
            // 
            this.b_Extract.Location = new System.Drawing.Point(6, 91);
            this.b_Extract.Name = "b_Extract";
            this.b_Extract.Size = new System.Drawing.Size(286, 23);
            this.b_Extract.TabIndex = 2;
            this.b_Extract.Text = "Extract";
            this.b_Extract.UseVisualStyleBackColor = true;
            this.b_Extract.Click += new System.EventHandler(this.b_Extract_Click);
            // 
            // b_SelectMod
            // 
            this.b_SelectMod.Location = new System.Drawing.Point(421, 27);
            this.b_SelectMod.Name = "b_SelectMod";
            this.b_SelectMod.Size = new System.Drawing.Size(197, 23);
            this.b_SelectMod.TabIndex = 3;
            this.b_SelectMod.Text = "Select Mod";
            this.b_SelectMod.UseVisualStyleBackColor = true;
            this.b_SelectMod.Click += new System.EventHandler(this.b_SelectMod_Click);
            // 
            // l_status
            // 
            this.l_status.AutoSize = true;
            this.l_status.Location = new System.Drawing.Point(421, 9);
            this.l_status.Name = "l_status";
            this.l_status.Size = new System.Drawing.Size(27, 15);
            this.l_status.TabIndex = 4;
            this.l_status.Text = "Test";
            this.l_status.Click += new System.EventHandler(this.l_status_Click);
            // 
            // c_IncludeEMEVD
            // 
            this.c_IncludeEMEVD.AutoSize = true;
            this.c_IncludeEMEVD.Location = new System.Drawing.Point(160, 66);
            this.c_IncludeEMEVD.Name = "c_IncludeEMEVD";
            this.c_IncludeEMEVD.Size = new System.Drawing.Size(132, 19);
            this.c_IncludeEMEVD.TabIndex = 5;
            this.c_IncludeEMEVD.Text = "Include EMEVD Files";
            this.c_IncludeEMEVD.UseVisualStyleBackColor = true;
            // 
            // c_IncludeFMG
            // 
            this.c_IncludeFMG.AutoSize = true;
            this.c_IncludeFMG.Location = new System.Drawing.Point(6, 66);
            this.c_IncludeFMG.Name = "c_IncludeFMG";
            this.c_IncludeFMG.Size = new System.Drawing.Size(119, 19);
            this.c_IncludeFMG.TabIndex = 6;
            this.c_IncludeFMG.Text = "Include FMG Files";
            this.c_IncludeFMG.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.c_IncludeEMEVD);
            this.groupBox1.Controls.Add(this.b_Extract);
            this.groupBox1.Controls.Add(this.c_IncludeFMG);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 120);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Extacts the PARAM, EMEVD and FMG files into \r\ntheir loose forms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Path to Mod";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.b_Repack);
            this.groupBox2.Location = new System.Drawing.Point(318, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 120);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Repack";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Repacks the EMEVD and FMG files back into \r\nthe enc_regulation.bnd.dcx.\r\n";
            // 
            // b_Repack
            // 
            this.b_Repack.Location = new System.Drawing.Point(6, 91);
            this.b_Repack.Name = "b_Repack";
            this.b_Repack.Size = new System.Drawing.Size(288, 23);
            this.b_Repack.TabIndex = 2;
            this.b_Repack.Text = "Repack";
            this.b_Repack.UseVisualStyleBackColor = true;
            this.b_Repack.Click += new System.EventHandler(this.b_Repack_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 188);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.l_status);
            this.Controls.Add(this.b_SelectMod);
            this.Controls.Add(this.t_ModPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Regulation Extract/Repack";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox t_ModPath;
        private Button b_Extract;
        private Button b_SelectMod;
        private Label l_status;
        private CheckBox c_IncludeEMEVD;
        private CheckBox c_IncludeFMG;
        private GroupBox groupBox1;
        private Label label1;
        private GroupBox groupBox2;
        private Button b_Repack;
        private Label label3;
        private Label label2;
    }
}