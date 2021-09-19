namespace UnrealSysInfoConverter
{
    partial class SysMenuConverter
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.InputXSize = new System.Windows.Forms.TextBox();
            this.InputYSize = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AutoDetectSize = new System.Windows.Forms.Button();
            this.SelectIniDir = new System.Windows.Forms.Button();
            this.InputIniDir = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OutputYSize = new System.Windows.Forms.TextBox();
            this.OutputXSize = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.ResizePercentage = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ReplaceIniText = new System.Windows.Forms.CheckBox();
            this.CreateReplaceList = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputXSize
            // 
            this.InputXSize.Location = new System.Drawing.Point(30, 36);
            this.InputXSize.Name = "InputXSize";
            this.InputXSize.Size = new System.Drawing.Size(54, 20);
            this.InputXSize.TabIndex = 0;
            this.InputXSize.Text = "320";
            this.InputXSize.TextChanged += new System.EventHandler(this.InputXSize_TextChanged);
            // 
            // InputYSize
            // 
            this.InputYSize.Location = new System.Drawing.Point(136, 36);
            this.InputYSize.Name = "InputYSize";
            this.InputYSize.Size = new System.Drawing.Size(54, 20);
            this.InputYSize.TabIndex = 0;
            this.InputYSize.Text = "240";
            this.InputYSize.TextChanged += new System.EventHandler(this.InputYSize_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.AutoDetectSize);
            this.panel1.Controls.Add(this.InputYSize);
            this.panel1.Controls.Add(this.InputXSize);
            this.panel1.Location = new System.Drawing.Point(24, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 92);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(-2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Исходное разрешение:";
            this.label2.MouseEnter += new System.EventHandler(this.label2_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.label2_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(99, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "X";
            // 
            // AutoDetectSize
            // 
            this.AutoDetectSize.Location = new System.Drawing.Point(62, 62);
            this.AutoDetectSize.Name = "AutoDetectSize";
            this.AutoDetectSize.Size = new System.Drawing.Size(97, 23);
            this.AutoDetectSize.TabIndex = 2;
            this.AutoDetectSize.Text = "Auto detect size";
            this.AutoDetectSize.UseVisualStyleBackColor = true;
            this.AutoDetectSize.Click += new System.EventHandler(this.AutoDetectSize_Click);
            // 
            // SelectIniDir
            // 
            this.SelectIniDir.Location = new System.Drawing.Point(63, 38);
            this.SelectIniDir.Name = "SelectIniDir";
            this.SelectIniDir.Size = new System.Drawing.Size(97, 23);
            this.SelectIniDir.TabIndex = 3;
            this.SelectIniDir.Text = "Обзор...";
            this.SelectIniDir.UseVisualStyleBackColor = true;
            this.SelectIniDir.Click += new System.EventHandler(this.SelectIniDir_Click);
            this.SelectIniDir.MouseEnter += new System.EventHandler(this.SelectIniDir_MouseEnter);
            this.SelectIniDir.MouseLeave += new System.EventHandler(this.SelectIniDir_MouseLeave);
            // 
            // InputIniDir
            // 
            this.InputIniDir.Location = new System.Drawing.Point(39, 13);
            this.InputIniDir.Name = "InputIniDir";
            this.InputIniDir.Size = new System.Drawing.Size(160, 20);
            this.InputIniDir.TabIndex = 1;
            this.InputIniDir.Text = "Путь к папке с меню..";
            this.InputIniDir.TextChanged += new System.EventHandler(this.InputIniDir_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.OutputYSize);
            this.panel2.Controls.Add(this.OutputXSize);
            this.panel2.Location = new System.Drawing.Point(24, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 73);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(-2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Выберите разрешение:";
            this.label3.MouseEnter += new System.EventHandler(this.label3_MouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.label3_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(99, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "X";
            // 
            // OutputYSize
            // 
            this.OutputYSize.Location = new System.Drawing.Point(136, 36);
            this.OutputYSize.Name = "OutputYSize";
            this.OutputYSize.Size = new System.Drawing.Size(54, 20);
            this.OutputYSize.TabIndex = 0;
            this.OutputYSize.Text = "240";
            this.OutputYSize.TextChanged += new System.EventHandler(this.OutputYSize_TextChanged);
            // 
            // OutputXSize
            // 
            this.OutputXSize.Location = new System.Drawing.Point(30, 36);
            this.OutputXSize.Name = "OutputXSize";
            this.OutputXSize.Size = new System.Drawing.Size(54, 20);
            this.OutputXSize.TabIndex = 0;
            this.OutputXSize.Text = "320";
            this.OutputXSize.TextChanged += new System.EventHandler(this.OutputXSize_TextChanged);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(67, 383);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "Выполнить!";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            // 
            // ResizePercentage
            // 
            this.ResizePercentage.AutoSize = true;
            this.ResizePercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResizePercentage.Location = new System.Drawing.Point(60, 189);
            this.ResizePercentage.Name = "ResizePercentage";
            this.ResizePercentage.Size = new System.Drawing.Size(146, 22);
            this.ResizePercentage.TabIndex = 3;
            this.ResizePercentage.Text = "X: 0%        Y: 0%";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.SelectIniDir);
            this.panel3.Controls.Add(this.InputIniDir);
            this.panel3.Location = new System.Drawing.Point(24, 214);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 74);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.CreateReplaceList);
            this.panel4.Controls.Add(this.ReplaceIniText);
            this.panel4.Location = new System.Drawing.Point(24, 303);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(229, 74);
            this.panel4.TabIndex = 5;
            // 
            // ReplaceIniText
            // 
            this.ReplaceIniText.AutoSize = true;
            this.ReplaceIniText.Location = new System.Drawing.Point(39, 7);
            this.ReplaceIniText.Name = "ReplaceIniText";
            this.ReplaceIniText.Size = new System.Drawing.Size(133, 17);
            this.ReplaceIniText.TabIndex = 1;
            this.ReplaceIniText.Text = "Заменить текст в INI";
            this.ReplaceIniText.UseVisualStyleBackColor = true;
            this.ReplaceIniText.MouseEnter += new System.EventHandler(this.ReplaceIniText_MouseEnter);
            this.ReplaceIniText.MouseLeave += new System.EventHandler(this.ReplaceIniText_DragLeave);
            // 
            // CreateReplaceList
            // 
            this.CreateReplaceList.Location = new System.Drawing.Point(15, 35);
            this.CreateReplaceList.Name = "CreateReplaceList";
            this.CreateReplaceList.Size = new System.Drawing.Size(200, 23);
            this.CreateReplaceList.TabIndex = 2;
            this.CreateReplaceList.Text = "Создать список строк для замены.";
            this.CreateReplaceList.UseVisualStyleBackColor = true;
            this.CreateReplaceList.Click += new System.EventHandler(this.CreateReplaceList_Click);
            this.CreateReplaceList.MouseEnter += new System.EventHandler(this.CreateReplaceList_MouseEnter);
            this.CreateReplaceList.MouseLeave += new System.EventHandler(this.CreateReplaceList_MouseLeave);
            // 
            // SysMenuConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 429);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ResizePercentage);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysMenuConverter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysInfo Menu Resizer  (2015.12.24)";
            this.Load += new System.EventHandler(this.SysMenuConverter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputXSize;
        private System.Windows.Forms.TextBox InputYSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectIniDir;
        private System.Windows.Forms.Button AutoDetectSize;
        private System.Windows.Forms.TextBox InputIniDir;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OutputYSize;
        private System.Windows.Forms.TextBox OutputXSize;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label ResizePercentage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ReplaceIniText;
        private System.Windows.Forms.Button CreateReplaceList;
    }
}

