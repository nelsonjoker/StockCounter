using StockCounter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockCounter
{
    public partial class CountList : Form
    {
        
        public Article SelectedArticle { get; set; }
        private List<CountEntry> CountEntries { get; set; }

        private List<CountEntry> ModifiedEntries;

        public CountList(Article art)
        {
            InitializeComponent();
            SelectedArticle = art;
            CountEntries = null;
            ModifiedEntries = new List<CountEntry>(10);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBind();
        }


        protected void DataBind()
        {
            SqliteConnector db = SqliteConnector.SqliteDB;
            using (SQLiteConnection con = db.Connect())
            {
                CountEntries = db.GetCountEntries(con, SelectedArticle.Itmref);
                dataGridViewCounts.DataSource = CountEntries;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
            if (ModifiedEntries.Count > 0)
            {
                SqliteConnector db = SqliteConnector.SqliteDB;
                using (SQLiteConnection con = db.Connect())
                {
                    foreach (CountEntry en in ModifiedEntries)
                    {
                        en.Sync = CountEntry.SyncStatus.NEW;
                        db.Update(con, en);
                    }
                }
            }

            double acc = 0;
            foreach (CountEntry en in CountEntries)
            {
                acc += en.Value;
            }

            SelectedArticle.CountedQuantity = acc;
            SelectedArticle.LabelCount = CountEntries.Count;
            

            DialogResult = DialogResult.OK;
            Close();
        }

        private void dataGridViewCounts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView g = sender as DataGridView;
            if(e.RowIndex >= 0 && g != null && g.Rows.Count >= e.RowIndex)
            {
                CountEntry en = g.Rows[e.RowIndex].DataBoundItem as CountEntry;
                if(en != null && !ModifiedEntries.Contains(en))
                {
                    ModifiedEntries.Add(en);
                }
            }
        }
    }
}
