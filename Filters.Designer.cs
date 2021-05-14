namespace StockCounter
{
    partial class Filters
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
            this.textBoxItmref = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxItmdes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTsi3Start = new System.Windows.Forms.TextBox();
            this.textBoxTsi3End = new System.Windows.Forms.TextBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCategory = new System.Windows.Forms.TextBox();
            this.checkBoxNoCounts = new System.Windows.Forms.CheckBox();
            this.checkBoxInventory = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Artigo";
            // 
            // textBoxItmref
            // 
            this.textBoxItmref.Location = new System.Drawing.Point(81, 42);
            this.textBoxItmref.Name = "textBoxItmref";
            this.textBoxItmref.Size = new System.Drawing.Size(221, 20);
            this.textBoxItmref.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(146, 183);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(227, 183);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descrição";
            // 
            // textBoxItmdes
            // 
            this.textBoxItmdes.Location = new System.Drawing.Point(81, 68);
            this.textBoxItmdes.Name = "textBoxItmdes";
            this.textBoxItmdes.Size = new System.Drawing.Size(221, 20);
            this.textBoxItmdes.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fam. Est. 3";
            // 
            // textBoxTsi3Start
            // 
            this.textBoxTsi3Start.Location = new System.Drawing.Point(81, 115);
            this.textBoxTsi3Start.Name = "textBoxTsi3Start";
            this.textBoxTsi3Start.Size = new System.Drawing.Size(100, 20);
            this.textBoxTsi3Start.TabIndex = 7;
            // 
            // textBoxTsi3End
            // 
            this.textBoxTsi3End.Location = new System.Drawing.Point(202, 115);
            this.textBoxTsi3End.Name = "textBoxTsi3End";
            this.textBoxTsi3End.Size = new System.Drawing.Size(100, 20);
            this.textBoxTsi3End.TabIndex = 8;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Location = new System.Drawing.Point(81, 94);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(50, 17);
            this.checkBoxActive.TabIndex = 10;
            this.checkBoxActive.Text = "Ativo";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Categoria";
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Location = new System.Drawing.Point(81, 141);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.Size = new System.Drawing.Size(221, 20);
            this.textBoxCategory.TabIndex = 12;
            // 
            // checkBoxNoCounts
            // 
            this.checkBoxNoCounts.AutoSize = true;
            this.checkBoxNoCounts.Location = new System.Drawing.Point(202, 94);
            this.checkBoxNoCounts.Name = "checkBoxNoCounts";
            this.checkBoxNoCounts.Size = new System.Drawing.Size(100, 17);
            this.checkBoxNoCounts.TabIndex = 13;
            this.checkBoxNoCounts.Text = "Sem contagens";
            this.checkBoxNoCounts.UseVisualStyleBackColor = true;
            // 
            // checkBoxInventory
            // 
            this.checkBoxInventory.AutoSize = true;
            this.checkBoxInventory.Location = new System.Drawing.Point(81, 19);
            this.checkBoxInventory.Name = "checkBoxInventory";
            this.checkBoxInventory.Size = new System.Drawing.Size(141, 17);
            this.checkBoxInventory.TabIndex = 14;
            this.checkBoxInventory.Text = "Em sessão de inventário";
            this.checkBoxInventory.UseVisualStyleBackColor = true;
            // 
            // Filters
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(314, 218);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxInventory);
            this.Controls.Add(this.checkBoxNoCounts);
            this.Controls.Add(this.textBoxCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.textBoxTsi3End);
            this.Controls.Add(this.textBoxTsi3Start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxItmdes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxItmref);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Filters";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filtros";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxItmref;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxItmdes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTsi3Start;
        private System.Windows.Forms.TextBox textBoxTsi3End;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCategory;
        private System.Windows.Forms.CheckBox checkBoxNoCounts;
        private System.Windows.Forms.CheckBox checkBoxInventory;
    }
}