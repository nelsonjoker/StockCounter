namespace StockCounter
{
    partial class CountForm
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
            this.labelItmref = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxAcc = new System.Windows.Forms.GroupBox();
            this.labelAccLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAccCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUn = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.comboBoxLocations = new System.Windows.Forms.ComboBox();
            this.groupBoxAcc.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelItmref
            // 
            this.labelItmref.AutoSize = true;
            this.labelItmref.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItmref.Location = new System.Drawing.Point(12, 54);
            this.labelItmref.Name = "labelItmref";
            this.labelItmref.Size = new System.Drawing.Size(237, 33);
            this.labelItmref.TabIndex = 0;
            this.labelItmref.Text = "M3CPOXXXXXXXXXX";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(15, 87);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(106, 13);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "descrição do artigo...";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(380, 163);
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
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(461, 163);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancelar...";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxAcc
            // 
            this.groupBoxAcc.Controls.Add(this.labelAccLabel);
            this.groupBoxAcc.Controls.Add(this.label2);
            this.groupBoxAcc.Controls.Add(this.labelAccCount);
            this.groupBoxAcc.Controls.Add(this.label1);
            this.groupBoxAcc.Location = new System.Drawing.Point(18, 121);
            this.groupBoxAcc.Name = "groupBoxAcc";
            this.groupBoxAcc.Size = new System.Drawing.Size(200, 65);
            this.groupBoxAcc.TabIndex = 5;
            this.groupBoxAcc.TabStop = false;
            this.groupBoxAcc.Text = "Acumulados";
            // 
            // labelAccLabel
            // 
            this.labelAccLabel.AutoSize = true;
            this.labelAccLabel.Location = new System.Drawing.Point(119, 40);
            this.labelAccLabel.Name = "labelAccLabel";
            this.labelAccLabel.Size = new System.Drawing.Size(13, 13);
            this.labelAccLabel.TabIndex = 3;
            this.labelAccLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Etiquetas:";
            // 
            // labelAccCount
            // 
            this.labelAccCount.AutoSize = true;
            this.labelAccCount.Location = new System.Drawing.Point(119, 27);
            this.labelAccCount.Name = "labelAccCount";
            this.labelAccCount.Size = new System.Drawing.Size(28, 13);
            this.labelAccCount.TabIndex = 1;
            this.labelAccCount.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quantidade contada:";
            // 
            // labelUn
            // 
            this.labelUn.AutoSize = true;
            this.labelUn.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUn.Location = new System.Drawing.Point(480, 54);
            this.labelUn.Name = "labelUn";
            this.labelUn.Size = new System.Drawing.Size(56, 33);
            this.labelUn.TabIndex = 6;
            this.labelUn.Text = "UN.";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCount.Location = new System.Drawing.Point(308, 51);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(166, 38);
            this.textBoxCount.TabIndex = 7;
            this.textBoxCount.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCount_Validating);
            // 
            // comboBoxLocations
            // 
            this.comboBoxLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLocations.FormattingEnabled = true;
            this.comboBoxLocations.Location = new System.Drawing.Point(12, 12);
            this.comboBoxLocations.Name = "comboBoxLocations";
            this.comboBoxLocations.Size = new System.Drawing.Size(524, 33);
            this.comboBoxLocations.TabIndex = 8;
            // 
            // CountForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(548, 198);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxLocations);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelUn);
            this.Controls.Add(this.groupBoxAcc);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelItmref);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Contagem";
            this.TopMost = true;
            this.groupBoxAcc.ResumeLayout(false);
            this.groupBoxAcc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelItmref;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxAcc;
        private System.Windows.Forms.Label labelAccLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAccCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUn;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.ComboBox comboBoxLocations;
    }
}