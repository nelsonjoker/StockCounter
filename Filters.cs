using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockCounter
{
    public partial class Filters : Form
    {
        public Filters()
        {
            InitializeComponent();
        }

        public bool Counting { get { return checkBoxInventory.Checked; } set { checkBoxInventory.Checked = value; } }
        public string Itmref { get { return textBoxItmref.Text; } set { textBoxItmref.Text = value; } }
        public string Itmdes { get { return textBoxItmdes.Text; } }
        public bool IsActive { get { return checkBoxActive.Checked; } }
        public bool NoCounts { get { return checkBoxNoCounts.Checked; } }
        public string TSI3Start { get { return textBoxTsi3Start.Text; } }
        public string TSI3End { get { return textBoxTsi3End.Text; } }
        public string Category { get { return textBoxCategory.Text; } }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
