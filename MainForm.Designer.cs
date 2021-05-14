namespace StockCounter
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.backgroundWorkerX3Importer = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarBottom = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelBottom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripBottom = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelPager = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.CountedQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialogCSV = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorkerSync = new System.ComponentModel.BackgroundWorker();
            this.toolStripTextBoxFilter = new System.Windows.Forms.ToolStripTextBox();
            this.itmrefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itmdesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tsicod2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tclcodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mvtDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mfmDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.stoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.articleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripButtonPrinter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportX3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCounter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLabels = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawing = new System.Windows.Forms.ToolStripButton();
            this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.toolStripBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorkerX3Importer
            // 
            this.backgroundWorkerX3Importer.WorkerReportsProgress = true;
            this.backgroundWorkerX3Importer.WorkerSupportsCancellation = true;
            this.backgroundWorkerX3Importer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerX3Importer_DoWork);
            this.backgroundWorkerX3Importer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerX3Importer_ProgressChanged);
            this.backgroundWorkerX3Importer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerX3Importer_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBarBottom,
            this.toolStripStatusLabelBottom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1113, 31);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBarBottom
            // 
            this.toolStripProgressBarBottom.Name = "toolStripProgressBarBottom";
            this.toolStripProgressBarBottom.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripStatusLabelBottom
            // 
            this.toolStripStatusLabelBottom.Name = "toolStripStatusLabelBottom";
            this.toolStripStatusLabelBottom.Size = new System.Drawing.Size(16, 26);
            this.toolStripStatusLabelBottom.Text = "...";
            // 
            // toolStripBottom
            // 
            this.toolStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrinter,
            this.toolStripButtonImportX3,
            this.toolStripButtonImport,
            this.toolStripButtonExport,
            this.toolStripButtonReset,
            this.toolStripSeparator1,
            this.toolStripTextBoxFilter,
            this.toolStripButtonFilter,
            this.toolStripButtonFirst,
            this.toolStripButtonPrevious,
            this.toolStripLabelPager,
            this.toolStripButtonNext,
            this.toolStripButtonLast,
            this.toolStripSeparator2,
            this.toolStripButtonCounter,
            this.toolStripButtonLabels,
            this.toolStripButtonDrawing});
            this.toolStripBottom.Location = new System.Drawing.Point(0, 0);
            this.toolStripBottom.Name = "toolStripBottom";
            this.toolStripBottom.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripBottom.Size = new System.Drawing.Size(1113, 39);
            this.toolStripBottom.TabIndex = 2;
            this.toolStripBottom.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabelPager
            // 
            this.toolStripLabelPager.Name = "toolStripLabelPager";
            this.toolStripLabelPager.Size = new System.Drawing.Size(24, 36);
            this.toolStripLabelPager.Text = "0/0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMain.AutoGenerateColumns = false;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itmrefDataGridViewTextBoxColumn,
            this.itmdesDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.tsicod2DataGridViewTextBoxColumn,
            this.tclcodDataGridViewTextBoxColumn,
            this.stuDataGridViewTextBoxColumn,
            this.mvtDataGridViewCheckBoxColumn,
            this.mfmDataGridViewCheckBoxColumn,
            this.stoDataGridViewCheckBoxColumn,
            this.CountedQuantity,
            this.LabelCount});
            this.dataGridViewMain.DataSource = this.articleBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewMain.Location = new System.Drawing.Point(18, 65);
            this.dataGridViewMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewMain.MultiSelect = false;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewMain.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewMain.RowTemplate.Height = 32;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMain.ShowEditingIcon = false;
            this.dataGridViewMain.Size = new System.Drawing.Size(1077, 458);
            this.dataGridViewMain.TabIndex = 3;
            this.dataGridViewMain.SelectionChanged += new System.EventHandler(this.dataGridViewMain_SelectionChanged);
            this.dataGridViewMain.DoubleClick += new System.EventHandler(this.dataGridViewMain_DoubleClick);
            // 
            // CountedQuantity
            // 
            this.CountedQuantity.DataPropertyName = "CountedQuantity";
            this.CountedQuantity.HeaderText = "Contado";
            this.CountedQuantity.Name = "CountedQuantity";
            this.CountedQuantity.ReadOnly = true;
            // 
            // LabelCount
            // 
            this.LabelCount.DataPropertyName = "LabelCount";
            this.LabelCount.HeaderText = "Etiquetas";
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.ReadOnly = true;
            // 
            // saveFileDialogCSV
            // 
            this.saveFileDialogCSV.DefaultExt = "csv";
            this.saveFileDialogCSV.Filter = "Ficheiro csv|*.csv";
            // 
            // backgroundWorkerSync
            // 
            this.backgroundWorkerSync.WorkerReportsProgress = true;
            this.backgroundWorkerSync.WorkerSupportsCancellation = true;
            this.backgroundWorkerSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSync_DoWork);
            // 
            // toolStripTextBoxFilter
            // 
            this.toolStripTextBoxFilter.Name = "toolStripTextBoxFilter";
            this.toolStripTextBoxFilter.Size = new System.Drawing.Size(100, 39);
            this.toolStripTextBoxFilter.Leave += new System.EventHandler(this.toolStripTextBoxFilter_Leave);
            // 
            // itmrefDataGridViewTextBoxColumn
            // 
            this.itmrefDataGridViewTextBoxColumn.DataPropertyName = "Itmref";
            this.itmrefDataGridViewTextBoxColumn.HeaderText = "Artigo";
            this.itmrefDataGridViewTextBoxColumn.Name = "itmrefDataGridViewTextBoxColumn";
            this.itmrefDataGridViewTextBoxColumn.ReadOnly = true;
            this.itmrefDataGridViewTextBoxColumn.Width = 210;
            // 
            // itmdesDataGridViewTextBoxColumn
            // 
            this.itmdesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.itmdesDataGridViewTextBoxColumn.DataPropertyName = "Itmdes";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itmdesDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.itmdesDataGridViewTextBoxColumn.HeaderText = "Descrição";
            this.itmdesDataGridViewTextBoxColumn.Name = "itmdesDataGridViewTextBoxColumn";
            this.itmdesDataGridViewTextBoxColumn.ReadOnly = true;
            this.itmdesDataGridViewTextBoxColumn.Width = 105;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Ativo";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isActiveDataGridViewCheckBoxColumn.Width = 50;
            // 
            // tsicod2DataGridViewTextBoxColumn
            // 
            this.tsicod2DataGridViewTextBoxColumn.DataPropertyName = "Tsicod2";
            this.tsicod2DataGridViewTextBoxColumn.HeaderText = "Est 3";
            this.tsicod2DataGridViewTextBoxColumn.Name = "tsicod2DataGridViewTextBoxColumn";
            this.tsicod2DataGridViewTextBoxColumn.ReadOnly = true;
            this.tsicod2DataGridViewTextBoxColumn.Width = 80;
            // 
            // tclcodDataGridViewTextBoxColumn
            // 
            this.tclcodDataGridViewTextBoxColumn.DataPropertyName = "Tclcod";
            this.tclcodDataGridViewTextBoxColumn.HeaderText = "Categoria";
            this.tclcodDataGridViewTextBoxColumn.Name = "tclcodDataGridViewTextBoxColumn";
            this.tclcodDataGridViewTextBoxColumn.ReadOnly = true;
            this.tclcodDataGridViewTextBoxColumn.Width = 80;
            // 
            // stuDataGridViewTextBoxColumn
            // 
            this.stuDataGridViewTextBoxColumn.DataPropertyName = "Stu";
            this.stuDataGridViewTextBoxColumn.HeaderText = "Un.";
            this.stuDataGridViewTextBoxColumn.Name = "stuDataGridViewTextBoxColumn";
            this.stuDataGridViewTextBoxColumn.ReadOnly = true;
            this.stuDataGridViewTextBoxColumn.Width = 50;
            // 
            // mvtDataGridViewCheckBoxColumn
            // 
            this.mvtDataGridViewCheckBoxColumn.DataPropertyName = "Mvt";
            this.mvtDataGridViewCheckBoxColumn.HeaderText = "Movimentos";
            this.mvtDataGridViewCheckBoxColumn.Name = "mvtDataGridViewCheckBoxColumn";
            this.mvtDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // mfmDataGridViewCheckBoxColumn
            // 
            this.mfmDataGridViewCheckBoxColumn.DataPropertyName = "Mfm";
            this.mfmDataGridViewCheckBoxColumn.HeaderText = "OFs";
            this.mfmDataGridViewCheckBoxColumn.Name = "mfmDataGridViewCheckBoxColumn";
            this.mfmDataGridViewCheckBoxColumn.ReadOnly = true;
            this.mfmDataGridViewCheckBoxColumn.Width = 50;
            // 
            // stoDataGridViewCheckBoxColumn
            // 
            this.stoDataGridViewCheckBoxColumn.DataPropertyName = "Sto";
            this.stoDataGridViewCheckBoxColumn.HeaderText = "Stock";
            this.stoDataGridViewCheckBoxColumn.Name = "stoDataGridViewCheckBoxColumn";
            this.stoDataGridViewCheckBoxColumn.ReadOnly = true;
            this.stoDataGridViewCheckBoxColumn.Width = 50;
            // 
            // articleBindingSource
            // 
            this.articleBindingSource.DataSource = typeof(StockCounter.Data.Article);
            // 
            // toolStripButtonPrinter
            // 
            this.toolStripButtonPrinter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrinter.Image = global::StockCounter.Properties.Resources.printer;
            this.toolStripButtonPrinter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPrinter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrinter.Name = "toolStripButtonPrinter";
            this.toolStripButtonPrinter.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPrinter.Text = "Impressora...";
            this.toolStripButtonPrinter.Click += new System.EventHandler(this.toolStripButtonPrinter_Click);
            // 
            // toolStripButtonImportX3
            // 
            this.toolStripButtonImportX3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripButtonImportX3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportX3.Image = global::StockCounter.Properties.Resources.safex3;
            this.toolStripButtonImportX3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonImportX3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportX3.Name = "toolStripButtonImportX3";
            this.toolStripButtonImportX3.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonImportX3.Text = "Importar artigos do X3";
            this.toolStripButtonImportX3.Click += new System.EventHandler(this.toolStripButtonImportX3_Click);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImport.Image = global::StockCounter.Properties.Resources.folder_sent_mail_256;
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(23, 36);
            this.toolStripButtonImport.Text = "Importar registos...";
            this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.Image = global::StockCounter.Properties.Resources.filesave_256;
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.toolStripButtonExport.Size = new System.Drawing.Size(32, 36);
            this.toolStripButtonExport.Text = "Exportar csv";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripButtonReset
            // 
            this.toolStripButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReset.Image = global::StockCounter.Properties.Resources.fileclose_256;
            this.toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReset.Name = "toolStripButtonReset";
            this.toolStripButtonReset.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.toolStripButtonReset.Size = new System.Drawing.Size(32, 36);
            this.toolStripButtonReset.Text = "Limpar todos os dados de contagem";
            this.toolStripButtonReset.Click += new System.EventHandler(this.toolStripButtonReset_Click);
            // 
            // toolStripButtonFilter
            // 
            this.toolStripButtonFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFilter.Image = global::StockCounter.Properties.Resources.filter;
            this.toolStripButtonFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFilter.Name = "toolStripButtonFilter";
            this.toolStripButtonFilter.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonFilter.Text = "Filtros";
            this.toolStripButtonFilter.Click += new System.EventHandler(this.toolStripButtonFilter_Click);
            // 
            // toolStripButtonFirst
            // 
            this.toolStripButtonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirst.Enabled = false;
            this.toolStripButtonFirst.Image = global::StockCounter.Properties.Resources.fast_backward;
            this.toolStripButtonFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirst.Name = "toolStripButtonFirst";
            this.toolStripButtonFirst.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonFirst.Text = "Início";
            this.toolStripButtonFirst.ToolTipText = "Início";
            this.toolStripButtonFirst.Click += new System.EventHandler(this.toolStripButtonFirst_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Enabled = false;
            this.toolStripButtonPrevious.Image = global::StockCounter.Properties.Resources.back;
            this.toolStripButtonPrevious.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPrevious.Text = "Anterior...";
            this.toolStripButtonPrevious.ToolTipText = "Anterior...";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Enabled = false;
            this.toolStripButtonNext.Image = global::StockCounter.Properties.Resources.play;
            this.toolStripButtonNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonNext.Text = "Seguinte...";
            this.toolStripButtonNext.ToolTipText = "Seguinte...";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonLast
            // 
            this.toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLast.Enabled = false;
            this.toolStripButtonLast.Image = global::StockCounter.Properties.Resources.fast_forward;
            this.toolStripButtonLast.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLast.Name = "toolStripButtonLast";
            this.toolStripButtonLast.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonLast.Text = "Última";
            this.toolStripButtonLast.ToolTipText = "Última...";
            this.toolStripButtonLast.Click += new System.EventHandler(this.toolStripButtonLast_Click);
            // 
            // toolStripButtonCounter
            // 
            this.toolStripButtonCounter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCounter.Enabled = false;
            this.toolStripButtonCounter.Image = global::StockCounter.Properties.Resources.counter_reset;
            this.toolStripButtonCounter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonCounter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCounter.Name = "toolStripButtonCounter";
            this.toolStripButtonCounter.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonCounter.Text = "Contagem";
            this.toolStripButtonCounter.Click += new System.EventHandler(this.toolStripButtonCounter_Click);
            // 
            // toolStripButtonLabels
            // 
            this.toolStripButtonLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLabels.Enabled = false;
            this.toolStripButtonLabels.Image = global::StockCounter.Properties.Resources.label;
            this.toolStripButtonLabels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLabels.Name = "toolStripButtonLabels";
            this.toolStripButtonLabels.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonLabels.Text = "Etiquetas";
            this.toolStripButtonLabels.Click += new System.EventHandler(this.toolStripButtonLabels_Click);
            // 
            // toolStripButtonDrawing
            // 
            this.toolStripButtonDrawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDrawing.Enabled = false;
            this.toolStripButtonDrawing.Image = global::StockCounter.Properties.Resources.drawing;
            this.toolStripButtonDrawing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawing.Name = "toolStripButtonDrawing";
            this.toolStripButtonDrawing.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonDrawing.Text = "Desenho";
            this.toolStripButtonDrawing.Click += new System.EventHandler(this.toolStripButtonDrawing_Click);
            // 
            // openFileDialogImport
            // 
            this.openFileDialogImport.FileName = "data.csv";
            this.openFileDialogImport.Filter = "Ficheiro csv|*.csv";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 561);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.toolStripBottom);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1100, 600);
            this.Name = "MainForm";
            this.Text = "Contagem de stock";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripBottom.ResumeLayout(false);
            this.toolStripBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerX3Importer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarBottom;
        private System.Windows.Forms.ToolStrip toolStripBottom;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportX3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBottom;
        private System.Windows.Forms.BindingSource articleBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonFilter;
        private System.Windows.Forms.ToolStripButton toolStripButtonFirst;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrevious;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPager;
        private System.Windows.Forms.ToolStripButton toolStripButtonNext;
        private System.Windows.Forms.ToolStripButton toolStripButtonLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonCounter;
        private System.Windows.Forms.ToolStripButton toolStripButtonLabels;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrinter;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawing;
        private System.Windows.Forms.DataGridViewTextBoxColumn itmrefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itmdesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tsicod2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tclcodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mvtDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mfmDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn stoDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountedQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelCount;
        private System.Windows.Forms.ToolStripButton toolStripButtonReset;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialogCSV;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSync;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFilter;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private System.Windows.Forms.OpenFileDialog openFileDialogImport;
    }
}

