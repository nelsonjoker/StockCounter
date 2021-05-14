
using StockCounter.Data;
using StockCounter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockCounter
{
    public partial class CountForm : Form
    {
        public Article SelectedArticle { get; set; }
        private List<CountEntry> CountEntries { get; set; }
        private List<Location> Locations { get; set; }

        public double AccCount { get { return Double.Parse(labelAccCount.Text); } set { labelAccCount.Text = value.ToString(); } }
        public int LabelCount { get { return Int16.Parse(labelAccLabel.Text); } set { labelAccLabel.Text = value.ToString(); } }
        public string StockUnit { get { return labelUn.Text; } set { labelUn.Text = value; } }


        public CountForm(Article art)
        {
            InitializeComponent();
            SelectedArticle = art;
            StockUnit = SelectedArticle.Stu;
            CountEntries = null;
            Locations = null;
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBind();
            labelItmref.Text = SelectedArticle.Itmref;
            labelDescription.Text = SelectedArticle.Itmdes;
            

        }

      


        protected void DataBind()
        {
            SqliteConnector db = SqliteConnector.SqliteDB;
            using (SQLiteConnection con = db.Connect())
            {
                CountEntries = db.GetCountEntries(con, SelectedArticle.Itmref);
                Locations = db.GetLocations(con);
            }

            double accCount = 0;
            int labels = CountEntries.Count;
            foreach(CountEntry e in CountEntries)
            {
                accCount += e.Value;
            }

            AccCount = accCount;
            LabelCount = labels;

            if (Locations.Count > 0)
            {
                comboBoxLocations.Items.AddRange(Locations.ToArray());
                int selectedIndex = 0;
                string l = Properties.Settings.Default.DefaultLocation;
                if (!string.IsNullOrEmpty(l))
                {
                    for(int i = 0; i < Locations.Count; i++)
                    {
                        if (Locations[i].Code.Equals(l))
                        {
                            selectedIndex = i;
                            break;
                        }
                    }
                }
                comboBoxLocations.SelectedIndex = selectedIndex;



            }





        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            double v = -1;
            if (!Double.TryParse(textBoxCount.Text, out v))
            {
                MessageBox.Show("Introduza uma quantidade válida", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int nextNumber = 0;
            foreach(CountEntry entry in CountEntries)
            {
                nextNumber = Math.Max(nextNumber, entry.LabelNumber);
            }
            nextNumber++;

            Location l = comboBoxLocations.SelectedItem as Location;


            CountEntry en = new CountEntry();
            en.Itmref = SelectedArticle.Itmref;
            en.LabelNumber = nextNumber;
            en.Value = v;
            en.Unit = StockUnit;
            en.Time = DateTime.UtcNow;
            en.IsDeleted = false;

            if(l != null)
            {
                en.Location = l.Code;
                if (!Properties.Settings.Default.DefaultLocation.Equals(l.Code))
                {
                    Properties.Settings.Default.DefaultLocation = l.Code;
                    Properties.Settings.Default.Save();
                }
            }


            SqliteConnector db = SqliteConnector.SqliteDB;
            using (SQLiteConnection con = db.Connect())
            {

                bool retry = false;
                do
                {
                    retry = false;
                    SQLiteTransaction tr = null;
                    try
                    {
                        tr = con.BeginTransaction();
                        db.Insert(con, en, tr);
                        PrintLabel(SelectedArticle, en);
                        tr.Commit();
                        CountEntries.Add(en);
                    }
                    catch (Exception ex)
                    {
                        DialogResult res = MessageBox.Show(ex.Message, "Ocorreu um erro ao imprimir a etiqueta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
                        if(res == DialogResult.Ignore)
                        {
                            tr.Commit();
                        }else
                        {
                            if (tr != null)
                                tr.Rollback();
                        }
                        if (res == DialogResult.Retry)
                            retry = true;
                        if (res == DialogResult.Abort)
                            return;
                    }

                } while (retry);

            }

            

            double acc = 0;
            foreach (CountEntry entry in CountEntries)
            {
                acc += entry.Value;
            }

            SelectedArticle.CountedQuantity = acc;
            SelectedArticle.LabelCount = CountEntries.Count;

            DialogResult = DialogResult.OK;
            Close();
        }

        private int TSCLIBResult
        {
            set
            {
                if (value == 0)
                    throw new Exception("Ocorreu um erro ao tentar imprimir...");
            }
        }

        protected void PrintLabel(Article art, CountEntry en)
        {

            if (Settings.Default.PrinterNull.Equals(Settings.Default.PrinterName))
            {
                return;
            }

            string size = String.Format("{0}x{1}(mm)", Settings.Default.PageWidth, Settings.Default.PageHeight);
            //in hundredths of an inch.
            int w = (int)(Settings.Default.PageWidth / 0.254);
            int h = (int)(Settings.Default.PageHeight / 0.254);



            using (PrintDocument doc = new PrintDocument())
            {
                doc.PrinterSettings.PrinterName = Settings.Default.PrinterName;
                doc.DefaultPageSettings.Landscape = false;
                doc.DefaultPageSettings.Color = false;
                doc.DefaultPageSettings.PaperSize = new PaperSize(size, w, h);
                doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
                foreach(PrinterResolution r in doc.PrinterSettings.PrinterResolutions)
                {
                    if(r.Kind == PrinterResolutionKind.High)
                    {
                        doc.DefaultPageSettings.PrinterResolution = r;
                        break;
                    }
                }
                
                //make it a multiple of bitmap width

                doc.PrintPage += delegate (object sender, PrintPageEventArgs e)
                {
                    //Font font = new Font("Codigo 128", 30, FontStyle.Regular, GraphicsUnit.Point);
                    Brush solid = Brushes.Black;
                    Graphics g = e.Graphics;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    g.Clear(Color.White);
                    

                    float dphiX = g.DpiX / 100;
                    float dphiY = g.DpiY / 100;
                    g.PageScale = 1 / (2*dphiX);

                    //Rectangle printArea = new Rectangle((int)(e.MarginBounds.Left * dphiX), (int)(e.MarginBounds.Top * dphiX), (int)(e.MarginBounds.Width * dphiX), (int)(30 * dphiY));
                    Rectangle printArea = new Rectangle((int)(e.MarginBounds.Left / g.PageScale), (int)(e.MarginBounds.Top / g.PageScale), (int)(e.MarginBounds.Width / g.PageScale), (int)(30 / g.PageScale));

                    switch (Settings.Default.Barcode)
                    {
                        case "Code39":
                            Barcodes.Barcode39 c39 = new Barcodes.Barcode39();
                            c39.MakeBarcodeImage(art.Itmref, g, printArea);
                            break;
                        default:
                            GenCode128.Code128Rendering.MakeBarcodeImage(art.Itmref, g, printArea);
                            break;
                    }

                    g.PageScale = 1;

                    Font font = new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                    SizeF dataSize = g.MeasureString(art.Itmref, font);
                    Rectangle area = new Rectangle((int)(e.MarginBounds.Left + ((e.MarginBounds.Width - dataSize.Width) / 2.0)), e.MarginBounds.Top + 28, e.MarginBounds.Width, 12);
                    g.DrawString(art.Itmref, font, solid, area);

                    font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                    area = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top + 28 + 12 + 3, e.MarginBounds.Width, 24);
                    g.DrawString(art.Itmdes, font, solid, area);

                    font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Pixel);
                    String footer = String.Format("{0}  {1}", en.LabelNumber, en.Time.ToShortDateString());
                    dataSize = g.MeasureString(footer, font);
                    area = new Rectangle((int)(e.MarginBounds.Left + e.MarginBounds.Width - dataSize.Width*1.2 + 0.5), e.MarginBounds.Top + e.MarginBounds.Bottom - 12 - 5, (int)(dataSize.Width*1.2 + 0.5), 10);
                    g.DrawString(footer, font, solid, area);


                    g.Flush();

                    font.Dispose();
                    //g.Dispose();

                };

                doc.Print();
            }


        }

        private Bitmap LabelBitmap { get; set; }

        protected void zPrintLabel(Article art, CountEntry en)
        {
            /*
            string printerName = "";
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (!string.IsNullOrEmpty(printer) && printer.Contains("TSC"))
                    printerName = printer;
            }
            */
            LabelTemplate tpl = new LabelTemplate(art, en);

            string size = String.Format("{0}x{1}(mm)", Settings.Default.PageWidth, Settings.Default.PageHeight);
            //in hundredths of an inch.
            int w = (int)(Settings.Default.PageWidth / 0.254);
            int h = (int)(Settings.Default.PageHeight / 0.254);
            int dpi = 203;

            tpl.Width = (int)(Settings.Default.PageWidth * dpi / 25.4);
            tpl.Height = (int)(Settings.Default.PageHeight * dpi / 25.4);



            using (Bitmap scaled = new Bitmap(w * dpi / 100, h * dpi / 100))
            {
                using (Bitmap b = new Bitmap(tpl.Width, tpl.Height))
                {
                    
                    b.SetResolution(dpi, dpi);
                    //tpl.Width = w;
                    //tpl.Height = h;
                    tpl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
                    //tpl.CreateGraphics()
                    Graphics graph = Graphics.FromImage(scaled);
                    graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    graph.SmoothingMode = SmoothingMode.AntiAlias;

                    graph.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graph.CompositingQuality = CompositingQuality.HighQuality;
                    //graph.SmoothingMode = SmoothingMode.None;
                    graph.DrawImage(b, new Rectangle(0, 0, scaled.Width, scaled.Height));


                    b.Save("LBL.bmp");
                }



                LabelBitmap = scaled;


                using (PrintDocument doc = new PrintDocument())
                {
                    doc.PrinterSettings.PrinterName = Settings.Default.PrinterName;
                    doc.DefaultPageSettings.Landscape = false;
                    doc.DefaultPageSettings.Color = false;
                    doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
                    //make it a multiple of bitmap width

                    doc.DefaultPageSettings.PaperSize = new PaperSize(size, w, h);

                    //doc.PrintPage += Doc_PrintPage;

                    doc.PrintPage += delegate (object sender, PrintPageEventArgs e)
                    {
                        Font font = new Font("Codigo 128", 30, FontStyle.Regular, GraphicsUnit.Point);
                        Brush solid = Brushes.Black;
                        Graphics g = e.Graphics;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                        g.Clear(Color.White);

                        SizeF dataSize = g.MeasureString(art.Itmref, font);
                        Rectangle area = new Rectangle((int)(e.MarginBounds.Left + ((e.MarginBounds.Width - dataSize.Width) / 2.0)), e.MarginBounds.Top, e.MarginBounds.Width, 30);

                        g.DrawString(art.Itmref, font, solid, area);


                        font = new Font("Calibri", 14, FontStyle.Regular, GraphicsUnit.Pixel);
                        dataSize = g.MeasureString(art.Itmref, font);
                        area = new Rectangle((int)(e.MarginBounds.Left + ((e.MarginBounds.Width - dataSize.Width) / 2.0)), e.MarginBounds.Top + 30, e.MarginBounds.Width, 14);
                        g.DrawString(art.Itmref, font, solid, area);

                        font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                        area = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top + 30 + 14 + 5, e.MarginBounds.Width, 22);
                        g.DrawString(art.Itmdes, font, solid, area);

                        font = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Pixel);
                        String footer = String.Format("{0}  {1}", en.LabelNumber, en.Time.ToShortDateString());
                        dataSize = g.MeasureString(footer, font);
                        area = new Rectangle((int)(e.MarginBounds.Left + e.MarginBounds.Width - dataSize.Width + 0.5), e.MarginBounds.Top + e.MarginBounds.Bottom - 12 - 5, (int)(dataSize.Width+0.5), 10);
                        g.DrawString(footer, font, solid, area);


                        g.Flush();

                        font.Dispose();
                        //g.Dispose();

                    };

                    doc.Print();
                }




                //b.Save("LBL.BMP");
                /*
                using (MagickImage image = new MagickImage(scaled))
                {
                    image.Format = MagickFormat.Pcx;
                    image.ColorType = ColorType.Bilevel;
                    //image.Write("LBL.PCX");

                    using(Bitmap t = image.ToBitmap())
                    {
                        t.Save("pcb.bmp");
                    }

                }
                */
            }

            /*
            TSCLIBResult = TSCLIB.openport(printerName);                                           //Open specified printer driver

            try
            {
                TSCLIBResult = TSCLIB.setup("50", "25", "1", "12", "0", "3", "0");                      //Setup the media size and sensor type info
                //TSCLIBResult = TSCLIB.formfeed();
                TSCLIBResult = TSCLIB.clearbuffer();                                                    //Clear image buffer

                //TSCLIBResult = TSCLIB.barcode("8", "8", "128", "48", "1", "0", "2", "2", en.Itmref);   //Drawing barcode
                //TSCLIBResult = TSCLIB.sendcommand("BARCODE 8,8,\"128M\",48,2,0,1,2,2,\""+en.Itmref+"\"");

                //TSCLIBResult = TSCLIB.windowsfont(8, 80, 10, 0, 0, 0, "Verdana", art.Itmdes);           //Drawing printer font

                TSCLIBResult = TSCLIB.downloadpcx("LBL.PCX", "LBL.PCX");                                         //Download PCX file into printer
                TSCLIBResult = TSCLIB.sendcommand("PUTPCX 0,0,\"LBL.PCX\"");                                //Drawing PCX graphic

                TSCLIBResult = TSCLIB.printlabel("1", "1");                                                    //Print labels
            }
            finally
            {
                TSCLIB.closeport();
            }

            */

        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            Graphics graphics = e.Graphics;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawImage(LabelBitmap, e.MarginBounds);
            
            //graphics.DrawImage(LabelBitmap, 0, 0);
            e.HasMorePages = false;
        }

        private void textBoxCount_Validating(object sender, CancelEventArgs e)
        {
            double v = -1;
            if (!Double.TryParse(textBoxCount.Text, out v))
            {
                e.Cancel = true;
                
                MessageBox.Show("Introduza uma quantidade válida", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxCount.Select(0, textBoxCount.Text.Length);
            }

        }
    }
}
