using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockCounter.Data;

namespace StockCounter
{
    public partial class LabelTemplate : UserControl
    {
        public LabelTemplate(Article art, CountEntry en)
        {
            InitializeComponent();
            labelBarcode.Text = art.Itmref;
            labelBarcodeItmref.Text = art.Itmref;
            labelItmdes.Text = art.Itmdes;

            labelNumber.Text = en.LabelNumber.ToString();
            labelDate.Text = en.Time.ToLocalTime().ToShortDateString();

        }
    }
}
