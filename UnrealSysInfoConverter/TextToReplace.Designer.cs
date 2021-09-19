namespace UnrealSysInfoConverter
{
    partial class TextToReplace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.TextReplacerData1 = new System.Windows.Forms.ListBox();
            this.TextInputForReplace = new System.Windows.Forms.TextBox();
            this.GoToMainMenu = new System.Windows.Forms.Button();
            this.TextOutputForReplace = new System.Windows.Forms.TextBox();
            this.AddToReplaceList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoveSelectedFromList = new System.Windows.Forms.Button();
            this.TextReplacerData2 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextReplacerData1
            // 
            this.TextReplacerData1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextReplacerData1.FormattingEnabled = true;
            this.TextReplacerData1.Location = new System.Drawing.Point(13, 88);
            this.TextReplacerData1.Name = "TextReplacerData1";
            this.TextReplacerData1.Size = new System.Drawing.Size(111, 147);
            this.TextReplacerData1.TabIndex = 0;
            // 
            // TextInputForReplace
            // 
            this.TextInputForReplace.Location = new System.Drawing.Point(24, 38);
            this.TextInputForReplace.Name = "TextInputForReplace";
            this.TextInputForReplace.Size = new System.Drawing.Size(100, 20);
            this.TextInputForReplace.TabIndex = 1;
            this.TextInputForReplace.Text = "Storage Card";
            // 
            // GoToMainMenu
            // 
            this.GoToMainMenu.Location = new System.Drawing.Point(96, 280);
            this.GoToMainMenu.Name = "GoToMainMenu";
            this.GoToMainMenu.Size = new System.Drawing.Size(75, 23);
            this.GoToMainMenu.TabIndex = 2;
            this.GoToMainMenu.Text = "Закрыть";
            this.GoToMainMenu.UseVisualStyleBackColor = true;
            this.GoToMainMenu.Click += new System.EventHandler(this.GoToMainMenu_Click);
            this.GoToMainMenu.MouseEnter += new System.EventHandler(this.GoToMainMenu_MouseEnter);
            this.GoToMainMenu.MouseLeave += new System.EventHandler(this.GoToMainMenu_MouseLeave);
            // 
            // TextOutputForReplace
            // 
            this.TextOutputForReplace.Location = new System.Drawing.Point(160, 38);
            this.TextOutputForReplace.Name = "TextOutputForReplace";
            this.TextOutputForReplace.Size = new System.Drawing.Size(100, 20);
            this.TextOutputForReplace.TabIndex = 1;
            this.TextOutputForReplace.Text = "SDMMC";
            // 
            // AddToReplaceList
            // 
            this.AddToReplaceList.Location = new System.Drawing.Point(46, 64);
            this.AddToReplaceList.Name = "AddToReplaceList";
            this.AddToReplaceList.Size = new System.Drawing.Size(182, 23);
            this.AddToReplaceList.TabIndex = 3;
            this.AddToReplaceList.Text = "Добавить в список для замены.";
            this.AddToReplaceList.UseVisualStyleBackColor = true;
            this.AddToReplaceList.Click += new System.EventHandler(this.AddToReplaceList_Click);
            this.AddToReplaceList.MouseEnter += new System.EventHandler(this.AddToReplaceList_MouseEnter);
            this.AddToReplaceList.MouseLeave += new System.EventHandler(this.AddToReplaceList_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите текс и нажмите \"Добавить\":";
            this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // RemoveSelectedFromList
            // 
            this.RemoveSelectedFromList.Location = new System.Drawing.Point(66, 241);
            this.RemoveSelectedFromList.Name = "RemoveSelectedFromList";
            this.RemoveSelectedFromList.Size = new System.Drawing.Size(144, 24);
            this.RemoveSelectedFromList.TabIndex = 5;
            this.RemoveSelectedFromList.Text = "Удалить из списка.";
            this.RemoveSelectedFromList.UseVisualStyleBackColor = true;
            this.RemoveSelectedFromList.Click += new System.EventHandler(this.RemoveSelectedFromList_Click);
            this.RemoveSelectedFromList.MouseEnter += new System.EventHandler(this.RemoveSelectedFromList_MouseEnter);
            this.RemoveSelectedFromList.MouseLeave += new System.EventHandler(this.RemoveSelectedFromList_MouseLeave);
            // 
            // TextReplacerData2
            // 
            this.TextReplacerData2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextReplacerData2.FormattingEnabled = true;
            this.TextReplacerData2.Location = new System.Drawing.Point(160, 88);
            this.TextReplacerData2.Name = "TextReplacerData2";
            this.TextReplacerData2.Size = new System.Drawing.Size(111, 147);
            this.TextReplacerData2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(125, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "В:";
            this.label2.MouseEnter += new System.EventHandler(this.label2_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.label2_MouseLeave);
            // 
            // TextToReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UnrealSysInfoConverter.Properties.Resources.backgroundforSysInfoResizer;
            this.ClientSize = new System.Drawing.Size(284, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RemoveSelectedFromList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddToReplaceList);
            this.Controls.Add(this.GoToMainMenu);
            this.Controls.Add(this.TextOutputForReplace);
            this.Controls.Add(this.TextInputForReplace);
            this.Controls.Add(this.TextReplacerData2);
            this.Controls.Add(this.TextReplacerData1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TextToReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TextToReplace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox TextReplacerData1;
        private System.Windows.Forms.TextBox TextInputForReplace;
        private System.Windows.Forms.Button GoToMainMenu;
        private System.Windows.Forms.TextBox TextOutputForReplace;
        private System.Windows.Forms.Button AddToReplaceList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RemoveSelectedFromList;
        private System.Windows.Forms.ListBox TextReplacerData2;
        private System.Windows.Forms.Label label2;
    }
}