using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using StockCounter.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StockCounter
{
    public partial class MainForm : Form
    {
        private SqliteConnector DbConnection { get; set; }

        protected long Offset { get; set; }
        protected long Limit { get; set; }
        protected long RecordsCount { get; set; }

        protected Filters FilterForm { get; private set; }
        protected SerialPort BarcodeScanner;

        public MainForm()
        {

            InitializeComponent();
            dataGridViewMain.AllowUserToAddRows = false;
            Offset = 0;
            Limit = 100;
            RecordsCount = 0;

            DbConnection = SqliteConnector.SqliteDB;
            FilterForm = new Filters();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FilterForm.Counting = Properties.Settings.Default.CountSessionMode;

            DataBind();
            dataGridViewMain.ClearSelection();
            SetupBarcodeScanner();
            backgroundWorkerSync.RunWorkerAsync();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (BarcodeScanner != null)
            {
                BarcodeScanner.Close();
                BarcodeScanner = null;
            }
            backgroundWorkerX3Importer.CancelAsync();
            backgroundWorkerSync.CancelAsync();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            while (backgroundWorkerX3Importer.IsBusy)
            {
                Application.DoEvents();
            }

        }

        protected void updateToolStrip()
        {

            if (Offset == 0)
            {
                toolStripButtonFirst.Enabled = false;
                toolStripButtonPrevious.Enabled = false;
            }
            else
            {
                toolStripButtonFirst.Enabled = true;
                toolStripButtonPrevious.Enabled = true;
            }

            if(Offset + Limit > RecordsCount)
            {
                toolStripButtonNext.Enabled = false;
                toolStripButtonLast.Enabled = false;
            }
            else
            {
                toolStripButtonNext.Enabled = true;
                toolStripButtonLast.Enabled = true;
            }

            long pages = (long)Math.Ceiling((double)RecordsCount / Limit);
            long page = (Offset / Limit)+1;
            if (pages > 0)
            {
                toolStripLabelPager.Text = String.Format("{0}/{1}", page, pages);
            }
            else
            {
                toolStripLabelPager.Text = "0/0";
            }

        }


        private void toolStripButtonImportX3_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerX3Importer.IsBusy)
                ShowError("Já existe uma operação em curso");

            toolStripButtonImportX3.Enabled = false;
            backgroundWorkerX3Importer.RunWorkerAsync();

        }

        private void ShowError(string v)
        {
            MessageBox.Show(v, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void backgroundWorkerX3Importer_DoWork(object sender, DoWorkEventArgs e)
        {

            backgroundWorkerX3Importer.ReportProgress(1, "A ligar ao servidor X3...");

            X3Connector x3 = X3Connector.X3;
            SqliteConnector sqlite = DbConnection;
            long inserted = 0;
            long updated = 0;

            using (SqlConnection con = x3.Connect())
            {
                
                long count = x3.CountItems(con);
                long done = 0;

                using (SqlDataReader rd = x3.GetItems(con))
                {

                    using (SQLiteConnection sqliteCon = sqlite.Connect())
                    {

                        while (!backgroundWorkerX3Importer.CancellationPending && rd.Read())
                        {
                            Article articleX3 = new Article();

                            articleX3.Itmref = rd.GetString(0);
                            articleX3.Itmdes = rd.GetString(1);
                            articleX3.Itmsta = (int)rd.GetByte(2);
                            articleX3.Tsicod0 = rd.GetString(3);
                            articleX3.Tsicod1 = rd.GetString(4);
                            articleX3.Tsicod2 = rd.GetString(5);
                            articleX3.Tsicod3 = rd.GetString(6);
                            articleX3.Tsicod4 = rd.GetString(7);
                            articleX3.Tclcod = rd.GetString(8);
                            articleX3.Stu = rd.GetString(9);
                            articleX3.Lascundat = rd.IsDBNull(10) ? DateTime.MinValue : rd.GetDateTime(10);
                            articleX3.Physto = rd.IsDBNull(11) ? -1 : (double)rd.GetDecimal(11);
                            articleX3.Mfmqty = rd.IsDBNull(12) ? -1 : (double)rd.GetDecimal(12);

                            articleX3.Mvt = !rd.IsDBNull(13);
                            articleX3.Cunflg = rd.GetByte(14) == 2;
                            articleX3.Cunlisnum = rd.GetString(15);

                            articleX3.Cun = articleX3.Lascundat.Year > 2000;
                            articleX3.Mfm = articleX3.Mfmqty > 0;
                            articleX3.Sto = articleX3.Physto > 0;


                            Article ex = sqlite.GetItem(sqliteCon, articleX3.Itmref);

                            if (ex != null)
                            {
                                if (!ex.Equals(articleX3))
                                    updated += sqlite.Update(sqliteCon, articleX3);
                            }
                            else
                            {
                                inserted += sqlite.Insert(sqliteCon, articleX3);
                            }


                            done++;
                            if(done % 10 == 0)
                            {
                                int p = (int)((100 * done) / count);
                                backgroundWorkerX3Importer.ReportProgress(p, String.Format("a importar registo {0} ({1} de {2})", articleX3.Itmref, done, count));
                            }

                        }


                    }


                }
            }

            backgroundWorkerX3Importer.ReportProgress(100, string.Format("importação concluída, {0} registo(s) inseridos, {1} modificado(s)", inserted, updated));

        }

        private void backgroundWorkerX3Importer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string msg = "...";
            if(e.UserState != null)
            {
                msg = e.UserState.ToString();
            }
            ReportProgress(e.ProgressPercentage, msg);
        }

        private void backgroundWorkerX3Importer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                toolStripStatusLabelBottom.Text = e.Error.Message;
            }

            toolStripButtonImportX3.Enabled = true;

        }

        private void ReportProgress(int percentage, string msg)
        {
            toolStripProgressBarBottom.Value = percentage;
            toolStripStatusLabelBottom.Text = string.IsNullOrEmpty(msg) ? string.Empty : msg;
        }

        private void DataBind()
        {

            

            long offset = this.Offset;
            long limit = this.Limit;

            Dictionary<string, object> filters = new Dictionary<string, object>();
            
            filters.Add("counting", FilterForm.Counting);
            
            if (!string.IsNullOrEmpty(FilterForm.Itmref))
            {
                filters.Add("itmref", FilterForm.Itmref);
            }
            if (!string.IsNullOrEmpty(FilterForm.Itmdes))
            {
                filters.Add("itmdes", FilterForm.Itmdes);
            }
            if (FilterForm.IsActive)
            {
                filters.Add("active", true);
            }
            if (FilterForm.NoCounts)
            {
                filters.Add("nocount", true);
            }
            if (!string.IsNullOrEmpty(FilterForm.TSI3Start) && !string.IsNullOrEmpty(FilterForm.TSI3End))
            {
                string[] v = new string[] { FilterForm.TSI3Start, FilterForm.TSI3End };
                filters.Add("tsicod3", v);
            }
            else if (!string.IsNullOrEmpty(FilterForm.TSI3Start))
            {
                filters.Add("tsicod3", FilterForm.TSI3Start);
            }
            else if (!string.IsNullOrEmpty(FilterForm.TSI3End))
            {
                filters.Add("tsicod3", FilterForm.TSI3End);
            }
            if (!string.IsNullOrEmpty(FilterForm.Category))
            {
                filters.Add("tclcod", FilterForm.Category);
            }
            long oldRecordsCount = RecordsCount;

            //TODO: make this asynchronous
            using (SQLiteConnection con = DbConnection.Connect())
            {
                
                RecordsCount = DbConnection.CountItems(con, filters);

                List<Article> articles = DbConnection.GetItems(con, offset, limit, filters);

                dataGridViewMain.DataSource = articles;

            }
            if (oldRecordsCount != RecordsCount)
            {
                Offset = 0;
            }
            updateToolStrip();
        }


        private void SetupBarcodeScanner()
        {
            if(BarcodeScanner != null)
            {
                BarcodeScanner.Close();
                BarcodeScanner = null;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ScannerPort))
            {
                try
                {

                    mBarcodeRxTime = DateTime.Now;
                    mBarcodeRxBuffer = new StringBuilder();
                    BarcodeScanner = new SerialPort(Properties.Settings.Default.ScannerPort, Properties.Settings.Default.ScannerBaudRate);
                    BarcodeScanner.DataReceived += BarcodeScanner_DataReceived;
                    BarcodeScanner.Open();

                }catch(Exception e)
                {
                    ShowError(e.Message);
                }

            }


        }

        private DateTime mBarcodeRxTime;
        private StringBuilder mBarcodeRxBuffer;

        private void BarcodeScanner_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - mBarcodeRxTime).TotalSeconds > 0.5)
                mBarcodeRxBuffer.Clear();

            mBarcodeRxTime = now;
            mBarcodeRxBuffer.Append(BarcodeScanner.ReadExisting());

            DataGridViewRow sel = null;
            foreach (DataGridViewRow r in dataGridViewMain.Rows)
            {
                Article a = r.DataBoundItem as Article;
                if(a.Itmref.Equals(mBarcodeRxBuffer.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    sel = r;
                    break;
                }
            }

            if (sel != null)
            {
                mBarcodeRxBuffer.Clear();
                //emulate double click
                dataGridViewMain.ClearSelection();
                sel.Selected = true;
                dataGridViewMain_SelectionChanged(dataGridViewMain, new EventArgs());
                dataGridViewMain_DoubleClick(dataGridViewMain, new EventArgs());
            }else
            {
                Article a = null;
                using (SQLiteConnection con = DbConnection.Connect())
                {

                    a = DbConnection.GetItem(con, mBarcodeRxBuffer.ToString());
                }

                if(a != null)
                {
                    mBarcodeRxBuffer.Clear();
                    DisplayCountForm(a);
                }

                
            }


        }

        private void dataGridViewMain_FilterStringChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonFilter_Click(object sender, EventArgs e)
        {
            //Filters form = new Filters();

            DialogResult res = FilterForm.ShowDialog();

            FilterForm.Itmref = toolStripTextBoxFilter.Text;

            if (res == DialogResult.OK)
            {
                toolStripTextBoxFilter.Text = FilterForm.Itmref;
                DataBind();
                Properties.Settings.Default.CountSessionMode = FilterForm.Counting;
                Properties.Settings.Default.Save();
            }
            

        }



        private void toolStripButtonFirst_Click(object sender, EventArgs e)
        {
            Offset = 0;
            DataBind();
        }

       
        private void toolStripButtonPrevious_Click(object sender, EventArgs e)
        {
            Offset -= Limit;
            if (Offset < 0)
                Offset = 0;
            DataBind();

        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            Offset += Limit;
            DataBind();
        }

        private void toolStripButtonLast_Click(object sender, EventArgs e)
        {
            Offset = (RecordsCount / Limit) * Limit;
            DataBind();
        }

        private void dataGridViewMain_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView g = sender as DataGridView;
            if (g != null && g.SelectedRows.Count > 0)
            {
                Article a = g.SelectedRows[0].DataBoundItem as Article;
                toolStripButtonCounter.Enabled = true;
                toolStripButtonLabels.Enabled = a.LabelCount > 0;
                toolStripButtonDrawing.Enabled = true;
            }
            else
            {
                toolStripButtonCounter.Enabled = false;
                toolStripButtonLabels.Enabled = false;
                toolStripButtonDrawing.Enabled = false;
            }
        }

        private void dataGridViewMain_DoubleClick(object sender, EventArgs e)
        {
            DataGridView g = sender as DataGridView;
            if (g != null && g.SelectedRows.Count > 0)
            {
                Article a = g.SelectedRows[0].DataBoundItem as Article;
                if (a != null)
                {
                    DisplayCountForm(a);
                }

            }
        }





        private void toolStripButtonCounter_Click(object sender, EventArgs e)
        {
            DataGridView g = dataGridViewMain;
            if (g != null && g.SelectedRows.Count > 0)
            {
                Article a = g.SelectedRows[0].DataBoundItem as Article;
                if (a != null)
                {
                    DisplayCountForm(a);
                }

            }
        }


        private DialogResult DisplayCountForm(Article a)
        {
            DataGridView g = dataGridViewMain;
            CountForm form = new CountForm(a);
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {

                articleBindingSource.ResetCurrentItem();
                g.Refresh();
                //DataBind();
            }
            return res;
        }

        private void toolStripButtonLabels_Click(object sender, EventArgs e)
        {
            DataGridView g = dataGridViewMain;
            if (g != null && g.SelectedRows.Count > 0)
            {
                Article a = g.SelectedRows[0].DataBoundItem as Article;
                if (a != null)
                {
                    CountList form = new CountList(a);
                    DialogResult res = form.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        articleBindingSource.ResetCurrentItem();
                        g.Refresh();
                        //DataBind();
                    }
                }

            }
        }

        private void toolStripButtonPrinter_Click(object sender, EventArgs e)
        {
            PrinterSettings form = new PrinterSettings();
            if(form.ShowDialog() == DialogResult.OK)
            {
                SetupBarcodeScanner();
            }
        }

        private void toolStripButtonDrawing_Click(object sender, EventArgs e)
        {
            DataGridView g = dataGridViewMain;
            if (g != null && g.SelectedRows.Count > 0)
            {
                Article a = g.SelectedRows[0].DataBoundItem as Article;
                if (a != null)
                {

                    Process p = new Process();
                    p.StartInfo.FileName = "http://api.sotubo.pt/audros/thumbnail/" + a.Itmref;
                    p.Start();


                }

            }
        }

        private void toolStripButtonReset_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Eliminar dados de contagem?", "Tem a certeza que deseja remover todos os dados da contagem?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Absoluta?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqliteConnector sqlite = DbConnection;
                    using (SQLiteConnection sqliteCon = sqlite.Connect())
                    {
                        sqlite.DeleteAllCountEntries(sqliteCon);
                    }
                    DataBind();
                }
            }
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saveFileDialogCSV.FileName))
            {
                saveFileDialogCSV.FileName = DateTime.Now.ToString("yyyyMMdd.csv");
            }

            if(saveFileDialogCSV.ShowDialog() == DialogResult.OK)
            {

                List<CountEntry> entries = null;
                SqliteConnector sqlite = DbConnection;
                using (SQLiteConnection sqliteCon = sqlite.Connect())
                {
                    entries = sqlite.GetCountEntries(sqliteCon);
                }

                if (entries == null || entries.Count == 0)
                {
                    ShowError("Sem registos para exportar...");
                    return;
                }

                using (Stream st = saveFileDialogCSV.OpenFile())
                {
                    using (StreamWriter wr = new StreamWriter(st))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (CountEntry en in entries)
                        {
                            sb.Clear();
                            sb.Append(en.Itmref);
                            sb.Append(";");
                            sb.Append(Properties.Settings.Default.DefaultFCY);
                            sb.Append(";");
                            sb.Append(en.Value.ToString().Replace('.', ','));
                            sb.Append(";");
                            sb.Append(en.Unit);
                            wr.WriteLine(sb);
                        }
                    }
                }



            }
        }

        private void backgroundWorkerSync_DoWork(object sender, DoWorkEventArgs e)
        {
#if DEBUG
            /*
            string username = "ncs";
            string key = "7a8d5b8e34fc8339f58cc2c8e8353e7f";
            string baseURL = "http://192.168.0.77:8080/api.dev";
            */
            string username = "sig";
            string key = "a2292cc4096d84751539965522655cff";
            string baseURL = "http://api.sotubo.pt";


#else
            string username = "sig";
            string key = "a2292cc4096d84751539965522655cff";
            string baseURL = "http://api.sotubo.pt";
#endif            
            WebClient cl = new WebClient();
            cl.Credentials = new NetworkCredential(username, key);
            cl.Encoding = System.Text.Encoding.UTF8;
            cl.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}:{1}", username, key);
            cl.Headers[HttpRequestHeader.ContentType] = "application/json;charset=utf-8";
            cl.Headers[HttpRequestHeader.Accept] = "application / json, text / plain, */*";
            cl.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";


            //RestClient client = new RestClient();
            //client.BaseUrl = new Uri("http://api.sotubo.pt");
            //client.Authenticator = new HttpBasicAuthenticator("sig", "a2292cc4096d84751539965522655cff");
            //client.BaseUrl = new Uri("http://192.168.0.77:8080/api.dev");
            //client.Authenticator = new HttpBasicAuthenticator("ncs", "7a8d5b8e34fc8339f58cc2c8e8353e7f");


            int campaign_id = -1;
            bool dirty = false;
            while (!backgroundWorkerSync.CancellationPending && campaign_id < 0)
            {

                try
                {
                    
                    string response = cl.DownloadString(baseURL + "/mrp/count");
                    
                    //RestRequest request = new RestRequest(Method.POST);
                    //request.Resource = "mrp/count";
                    //IRestResponse response = client.Execute(request);
                    if (!string.IsNullOrEmpty(response))
                    {
                        JObject r = JObject.Parse(response);
                        if (r.ContainsKey("result") && r["result"].Value<Int32>() == 1)
                        {
                            JToken jdata = r["jdata"];
                            if (jdata != null)
                            {
                                campaign_id = jdata["id"].Value<Int32>();
                                break;
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


                int t = 60;
                while (t-- > 0 || backgroundWorkerSync.CancellationPending)
                {
                    Thread.Sleep(1000);
                }
            }


            SqliteConnector sqlite = DbConnection;

            if (!backgroundWorkerSync.CancellationPending)
            {
                try
                {

                    string response = cl.DownloadString(baseURL + "/mrp/count/locations");

                    //RestRequest request = new RestRequest(Method.POST);
                    //request.Resource = "mrp/count";
                    //IRestResponse response = client.Execute(request);
                    if (!string.IsNullOrEmpty(response))
                    {
                        JObject r = JObject.Parse(response);
                        if (r.ContainsKey("result") && r["result"].Value<Int32>() == 1)
                        {
                            JArray jdata = r["jdata"] as JArray;
                            if (jdata != null && jdata.Count > 0)
                            {

                                using (SQLiteConnection sqliteCon = sqlite.Connect())
                                {

                                    List<Location> existent = sqlite.GetLocations(sqliteCon);
                                    
                                    foreach (JObject loc in jdata)
                                    {
                                        string code = loc["code"].Value<string>();
                                        string description = loc["description"].Value<string>();
                                        Location db = null;
                                        for(int i = 0; i < existent.Count; i++)
                                        {
                                            if (existent[i].Code.Equals(code))
                                            {
                                                db = existent[i];
                                                break;
                                            }
                                        }

                                        if (db == null)
                                        {
                                            //new location record
                                            db = new Data.Location();
                                            db.Code = code;
                                            db.Description = description;
                                            sqlite.Insert(sqliteCon, db);
                                        }
                                        else
                                        {
                                            //exists, update it and remove it from existent list so we can later delete it
                                            bool changes = false;
                                            if (!db.Description.Equals(description))
                                            {
                                                db.Description = description;
                                                changes = true;

                                            }

                                            if (changes)
                                            {
                                                sqlite.Update(sqliteCon, db);
                                            }

                                            existent.Remove(db);

                                        }
                                    }
                                    //the ones that remain in existent list are to be deleted
                                    foreach(Location l in existent)
                                    {
                                        sqlite.Delete(sqliteCon, l);
                                    }
                                }
                            }

                        }
                    }



                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            
            while (!backgroundWorkerSync.CancellationPending)
            {
                dirty = false;
                try
                {

                    //sync upstream changes
                    long stamp = Properties.Settings.Default.SyncLastUpdate;
                    //RestRequest request = new RestRequest(Method.POST);
                    //request.Resource = string.Format("mrp/count/sync/{0}/{1}", campaign_id, stamp);

                    //IRestResponse response = client.Execute(request);
                    string response = cl.DownloadString(baseURL + string.Format("/mrp/count/sync/{0}/{1}", campaign_id, stamp));
                    if (!string.IsNullOrEmpty(response))
                    {
                        JObject r = JObject.Parse(response);
                        if (r.ContainsKey("result") && r["result"].Value<Int32>() == 1)
                        {
                            JArray jdata = r["jdata"] as JArray;
                            if (jdata != null && jdata.Count > 0)
                            {

                                using (SQLiteConnection sqliteCon = sqlite.Connect())
                                {
                                    foreach (JObject it in jdata)
                                    {
                                        string hash = it["hash"].Value<string>();
                                        long update_stamp = it["update"].Value<long>();
                                        if (update_stamp > stamp)
                                            stamp = update_stamp;

                                        CountEntry en = new CountEntry();
                                        en.Itmref = it["itmref"].Value<string>();
                                        //en.LabelNumber = nextNumber;
                                        en.Value = it["value"].Value<double>(); 
                                        en.Unit = it["stu"].Value<string>();
                                        en.Hash = it["hash"].Value<string>();

                                        long unixTime = it["time"].Value<long>();
                                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                        en.Time = epoch.AddSeconds(unixTime);
                                        en.IsDeleted = it["deleted"].Value<int>() == 1;
                                        if (en.IsDeleted)
                                        {
                                            en.DeleteKey = DateTime.UtcNow.ToFileTimeUtc();
                                        }
                                        en.SyncCode = it["id"].Value<int>();
                                        en.Sync = CountEntry.SyncStatus.POSTED;
                                        en.Location = it["location"].Value<string>();

                                        CountEntry ex = sqlite.GetCountEntryByHash(sqliteCon, hash);
                                        if (ex == null)
                                        {
                                            int nextNumber = 0;
                                            
                                            List<CountEntry> CountEntries = sqlite.GetCountEntries(sqliteCon, en.Itmref);
                                            nextNumber = CountEntries.Count + 1;
                                            foreach (CountEntry entry in CountEntries)
                                            {
                                                nextNumber = Math.Max(nextNumber, entry.LabelNumber + 1);
                                            }


                                            en.LabelNumber = nextNumber;
                                            sqlite.Insert(sqliteCon, en);
                                            dirty = true;
                                        }
                                        else
                                        {
                                            bool changes = false;
                                            if (!en.Itmref.Equals(ex.Itmref))
                                            {
                                                ex.Itmref = en.Itmref;
                                                changes = true;
                                            }
                                            if (Math.Abs(en.Value - ex.Value) > 0.001)
                                            {
                                                ex.Value = en.Value;
                                                changes = true;
                                            }
                                            if (!en.Unit.Equals(ex.Unit))
                                            {
                                                ex.Unit = en.Unit;
                                                changes = true;
                                            }
                                            if (!en.Hash.Equals(ex.Hash))
                                            {
                                                ex.Hash = en.Hash;
                                                changes = true;
                                            }
                                            if (Math.Abs((en.Time - ex.Time).TotalSeconds) > 1 )
                                            {
                                                ex.Time = en.Time;
                                                changes = true;
                                            }
                                            if (!en.IsDeleted.Equals(ex.IsDeleted))
                                            {
                                                ex.IsDeleted = en.IsDeleted;
                                                changes = true;
                                            }
                                            if (!en.SyncCode.Equals(ex.SyncCode))
                                            {
                                                ex.SyncCode = en.SyncCode;
                                                changes = true;
                                            }
                                            if (!en.Location.Equals(ex.Location))
                                            {
                                                ex.Location = en.Location;
                                                changes = true;
                                            }

                                            if (changes)
                                            {
                                                sqlite.Update(sqliteCon, ex);
                                                dirty = true;
                                            }

                                        }

                                    }
                                }
                            }
                        }
                    }
                    if(stamp > Properties.Settings.Default.SyncLastUpdate)
                    {
                        Properties.Settings.Default.SyncLastUpdate = stamp;
                        Properties.Settings.Default.Save();
                    }




                    //check for dirty records
                    List<CountEntry> hoes = null;
                    
                    using (SQLiteConnection sqliteCon = sqlite.Connect())
                    {
                        hoes = sqlite.GetDirtyCountEntries(sqliteCon);
                    }
                    if (hoes != null && hoes.Count > 0)
                    {

                        //get current active session if any
                        //request = new RestRequest(Method.POST);
                        //request.Resource = "mrp/count";

                        //response = client.Execute(request);
                        response = cl.DownloadString(baseURL + "/mrp/count");

                        if (!string.IsNullOrEmpty(response))
                        {
                            JObject r = JObject.Parse(response);
                            if (r.ContainsKey("result") && r["result"].Value<Int32>() == 1)
                            {
                                JToken jdata = r["jdata"];
                                if (jdata != null)
                                {
                                    int campaing_id = jdata["id"].Value<Int32>();

                                    //commit changes

                                    foreach (CountEntry en in hoes)
                                    {
                                        Debug.Assert(en.Sync == CountEntry.SyncStatus.NEW);
                                        //we are assuming new to know wich ones to update on local DB

                                        if (en.IsDeleted && en.SyncCode > 0)
                                        {
                                            //delete operation
                                            //request = new RestRequest(Method.POST);
                                            //request.Resource = string.Format("mrp/count/delete/{0}/{1}", campaing_id, en.SyncCode);
                                            //response = client.Execute(request);
                                            response = cl.DownloadString(baseURL + string.Format("/mrp/count/delete/{0}/{1}", campaing_id, en.SyncCode));
                                            r = JObject.Parse(response);
                                            if (r.ContainsKey("result") && r["result"].Value<Int32>() == 1)
                                            {
                                                en.Sync = CountEntry.SyncStatus.POSTED;
                                            }
                                        }
                                        if (!en.IsDeleted)
                                        {
                                            string url;
                                            //request = new RestRequest(Method.POST);
                                            if (en.SyncCode > 0)
                                            {
                                                //update
                                                //request.Resource = string.Format("mrp/count/update/{0}/{1}", campaing_id, en.SyncCode);
                                                url = baseURL + string.Format("/mrp/count/update/{0}/{1}", campaing_id, en.SyncCode);
                                            }
                                            else
                                            {
                                                //insert
                                                //request.Resource = string.Format("mrp/count/create/{0}/", campaing_id);
                                                url = baseURL + string.Format("/mrp/count/create/{0}", campaing_id);
                                            }

                                            Article a = null;
                                            using (SQLiteConnection sqliteCon = sqlite.Connect())
                                            {
                                                a = sqlite.GetItem(sqliteCon, en.Itmref);
                                            }

                                            JObject parameters = new JObject();
                                            parameters.Add("itmref", en.Itmref);
                                            parameters.Add("itmdes", a == null ? string.Empty : a.Itmdes);
                                            //parameters.Add("itmdes", string.Empty);
                                            parameters.Add("value", en.Value);
                                            parameters.Add("stu", en.Unit);
                                            parameters.Add("operator", "000");
                                            parameters.Add("hash", en.Hash);
                                            parameters.Add("location", en.Location);
                                            parameters.Add("location_data", string.Empty);

                                            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                                            parameters.Add("time", Convert.ToInt64((en.Time - epoch).TotalSeconds));

                                            //response = client.Execute(request);
                                            response = cl.UploadString(url, parameters.ToString());
                                            r = JObject.Parse(response);
                                            //ignore result because we want to update local record anyway
                                            if (r.ContainsKey("result") )
                                            {
                                                jdata = r["jdata"];
                                                en.Sync = CountEntry.SyncStatus.POSTED;
                                                en.SyncCode = jdata["id"].Value<Int32>();
                                            }

                                        }
                                        else
                                        {
                                            en.Sync = CountEntry.SyncStatus.POSTED;
                                        }



                                    }

                                    //update local DB
                                    using (SQLiteConnection sqliteCon = sqlite.Connect())
                                    {
                                        foreach (CountEntry en in hoes)
                                        {
                                            if (en.Sync == CountEntry.SyncStatus.POSTED)
                                            {
                                                sqlite.Update(sqliteCon, en);
                                                dirty = true;
                                            }
                                        }

                                    }


                                }

                            }

                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


                int t = dirty ? 10 : 60;
                while (t-- > 0 || backgroundWorkerSync.CancellationPending)
                {
                    Thread.Sleep(1000);
                }




            }

        }

        private void toolStripTextBoxFilter_Leave(object sender, EventArgs e)
        {
            FilterForm.Itmref = toolStripTextBoxFilter.Text;
            DataBind();
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            if(openFileDialogImport.ShowDialog(this) == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialogImport.FileName);
                SqliteConnector sqlite = DbConnection;
                SQLiteTransaction tr = null;
                int done = 0, total = lines.Length;
                ReportProgress(0, "A importar ficheiro de dados");

                using (SQLiteConnection sqliteCon = sqlite.Connect())
                {
                    try
                    {
                        tr = sqliteCon.BeginTransaction();
                        foreach (string l in lines)
                        {
                            string[] els = l.Split('\t', ';');
                            Debug.Assert(els.Length == 5);
                            if (els.Length != 5)
                            {
                                throw new ArgumentException("Formato de ficheiro invalido");
                            }
                            string itmref = els[0];
                            string itmdes = els[1];
                            double qty = double.Parse(els[2].Replace('.', ','));
                            string stu = els[3];
                            DateTime date = DateTime.Parse(els[4]);



                            CountEntry en = new CountEntry();
                            en.Itmref = itmref;
                            en.Value = qty;
                            en.Unit = stu;
                            en.Time = date;
                            en.Sync = CountEntry.SyncStatus.NEW;

                            int nextNumber = 0;

                            List<CountEntry> CountEntries = sqlite.GetCountEntries(sqliteCon, en.Itmref);
                            nextNumber = CountEntries.Count + 1;
                            foreach (CountEntry entry in CountEntries)
                            {
                                nextNumber = Math.Max(nextNumber, entry.LabelNumber + 1);
                            }


                            en.LabelNumber = nextNumber;
                            sqlite.Insert(sqliteCon, en, tr);

                            done++;
                            int progress =(int)( 100.0 * done / total);
                            ReportProgress(progress, string.Format("A inserir registo {0} de {1}", done, total));

                        }
                        tr.Commit();
                        ReportProgress(100, "Importacao concluida");
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        ReportProgress(0, string.IsNullOrEmpty(ex.Message) ? "Erro ao importar ficheiro" : ex.Message);
                    }
                }



            }
        }
    }

            
}
