namespace StockCounter
{
    partial class LabelTemplate
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBarcodeItmref = new System.Windows.Forms.Label();
            this.labelBarcode = new System.Windows.Forms.Label();
            this.labelItmdes = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelBarcodeItmref
            // 
            this.labelBarcodeItmref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBarcodeItmref.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarcodeItmref.Location = new System.Drawing.Point(3, 64);
            this.labelBarcodeItmref.Name = "labelBarcodeItmref";
            this.labelBarcodeItmref.Size = new System.Drawing.Size(397, 26);
            this.labelBarcodeItmref.TabIndex = 3;
            this.labelBarcodeItmref.Text = "M3CPOXXXXXXXXXX";
            this.labelBarcodeItmref.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBarcode
            // 
            this.labelBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBarcode.Font = new System.Drawing.Font("Codigo 128", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarcode.Location = new System.Drawing.Point(3, 17);
            this.labelBarcode.Name = "labelBarcode";
            this.labelBarcode.Size = new System.Drawing.Size(394, 52);
            this.labelBarcode.TabIndex = 2;
            this.labelBarcode.Text = "M3CPOXXXXXXXXXX";
            this.labelBarcode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelItmdes
            // 
            this.labelItmdes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItmdes.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItmdes.Location = new System.Drawing.Point(3, 90);
            this.labelItmdes.Name = "labelItmdes";
            this.labelItmdes.Size = new System.Drawing.Size(394, 80);
            this.labelItmdes.TabIndex = 4;
            this.labelItmdes.Text = "Descrição do artigo ...";
            // 
            // labelNumber
            // 
            this.labelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNumber.AutoSize = true;
            this.labelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumber.Location = new System.Drawing.Point(230, 170);
            this.labelNumber.MinimumSize = new System.Drawing.Size(50, 0);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(50, 20);
            this.labelNumber.TabIndex = 6;
            this.labelNumber.Text = "1";
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(286, 170);
            this.labelDate.MinimumSize = new System.Drawing.Size(50, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(89, 20);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "01/01/2016";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.labelItmdes);
            this.Controls.Add(this.labelBarcodeItmref);
            this.Controls.Add(this.labelBarcode);
            this.Name = "LabelTemplate";
            this.Size = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBarcodeItmref;
        private System.Windows.Forms.Label labelBarcode;
        private System.Windows.Forms.Label labelItmdes;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Label labelDate;
    }
}
