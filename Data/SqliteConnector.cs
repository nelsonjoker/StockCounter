using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace StockCounter.Data
{
    class SqliteConnector
    {

        private static SqliteConnector theConnector;
        public static SqliteConnector SqliteDB
        {
            get
            {
                if (theConnector == null)
                    theConnector = new SqliteConnector(Properties.Settings.Default.DbPath);
                //theConnector = new SqliteConnector("C:\\data\\stock.sqlite");
                return theConnector;
            }
        }

        public string ConnectionString { get; set; }

        public SqliteConnector(string filename)
        {
            ConnectionString = String.Format("Data Source={0};Version=3;", filename);
        }

        public SQLiteConnection Connect()
        {
            SQLiteConnection sqlConnection1 = new SQLiteConnection(ConnectionString);
            sqlConnection1.Open();
            return sqlConnection1;
        }

        public Article GetItem(SQLiteConnection con, string itmref)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {
                    cmd.CommandText = @"SELECT 
                    ITM.ITMREF,
                    ITM.ITMDES1,
                    ITM.ITMSTA,
                    ITM.TSICOD0,
                    ITM.TSICOD1,
                    ITM.TSICOD2,
                    ITM.TSICOD3,
                    ITM.TSICOD4,
                    ITM.TCLCOD,
                    ITM.STU,
                    ITM.LASCUNDAT,
                    ITM.PHYSTO,
                    ITM.MFMQTY,
                    ITM.FLGMVT,
                    ITM.FLGCUN,
                    ITM.FLGMFM,
                    ITM.FLGSTO,
                    ITM.CUNFLG,
                    ITM.CUNLISNUM
                    FROM ITMMASTER ITM 
                    WHERE ITM.ITMREF=@ITMREF";
                cmd.Parameters.AddWithValue("@ITMREF", itmref);

                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        Article res = new Article();
                        res.Itmref = rd.GetString(0);
                        res.Itmdes = rd.GetString(1);
                        res.Itmsta = (int)rd.GetByte(2);
                        res.Tsicod0 = rd.GetString(3);
                        res.Tsicod1 = rd.GetString(4);
                        res.Tsicod2 = rd.GetString(5);
                        res.Tsicod3 = rd.GetString(6);
                        res.Tsicod4 = rd.GetString(7);
                        res.Tclcod = rd.GetString(8);
                        res.Stu = rd.GetString(9);
                        res.Lascundat = rd.IsDBNull(10) ? DateTime.MinValue : rd.GetDateTime(10);
                        res.Physto = rd.IsDBNull(11) ? -1 : (double)rd.GetDecimal(11);
                        res.Mfmqty = rd.IsDBNull(12) ? -1 : (double)rd.GetDecimal(12);

                        res.Mvt = rd.GetByte(13) != 0;
                        res.Cun = rd.GetByte(14) != 0;
                        res.Mfm = rd.GetByte(15) != 0;
                        res.Sto = rd.GetByte(16) != 0;

                        res.Cunflg = rd.GetByte(17) != 0;
                        res.Cunlisnum = rd.IsDBNull(18) ? string.Empty : rd.GetString(18);

                        return res;
                    }
                }
            }

            return null;
        }



        

        public long CountItems(SQLiteConnection con, Dictionary<string, object> filters)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {

                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT count(*) as c FROM ITMMASTER ITM LEFT OUTER JOIN CNT ON ITM.ITMREF = CNT.ITMREF AND CNT.DELETED = 0 ");
                

                string prefix = "WHERE ";

                if (filters != null && filters.Count > 0)
                {
                    if (filters.ContainsKey("counting") && (bool)filters["counting"])
                    {
                        sql.Append(prefix + " ITM.CUNFLG = 1 ");
                        prefix = " AND ";
                    }
                    if (filters.ContainsKey("itmref"))
                    {
                        sql.Append(prefix + " ITM.ITMREF LIKE @litmref ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@litmref", Like(filters["itmref"].ToString()));
                    }
                    if (filters.ContainsKey("itmdes"))
                    {
                        sql.Append(prefix + " ITM.ITMDES1 LIKE @litmdes ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@litmdes", Like(filters["itmdes"].ToString()));
                    }
                    if (filters.ContainsKey("active"))
                    {
                        if ((bool)filters["active"])
                        {
                            sql.Append(prefix + " ITM.ITMSTA = 1 ");
                        }
                        else
                        {
                            sql.Append(prefix + " ITM.ITMSTA <> 1 ");
                        }
                        prefix = " AND ";
                    }
                    if (filters.ContainsKey("nocount"))
                    {
                        if ((bool)filters["nocount"])
                        {
                            sql.Append(prefix + " CNT.ITMREF IS NULL ");
                            prefix = " AND ";
                        }

                    }

                    if (filters.ContainsKey("tsicod3"))
                    {
                        object v = filters["tsicod3"];
                        string[] cods = v as string[];
                        if (cods != null)
                        {
                            sql.Append(prefix + " ITM.TSICOD2 >= @ltsicod3s AND ITM.TSICOD2 <= @ltsicod3e ");
                            prefix = " AND ";
                            cmd.Parameters.AddWithValue("@ltsicod3s", cods[0]);
                            cmd.Parameters.AddWithValue("@ltsicod3e", cods[1]);
                        }
                        else
                        {
                            sql.Append(prefix + " ITM.TSICOD2 = @ltsicod3 ");
                            prefix = " AND ";
                            cmd.Parameters.AddWithValue("@ltsicod3", v.ToString());
                        }
                    }
                    if (filters.ContainsKey("tclcod"))
                    {
                        sql.Append(prefix + " ITM.TCLCOD LIKE @ltclcod ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@ltclcod", filters["tclcod"].ToString());
                    }



                }

                cmd.CommandText = sql.ToString();

                object c = cmd.ExecuteScalar();
                return (long)c;
            }
        }

        public List<Article> GetItems(SQLiteConnection con, long offset, long limit)
        {
            return GetItems(con, offset, limit, null);
        }

        private string Like(string raw)
        {
            string r = raw.Replace('?', '_');
            r = r.Replace('*', '%');
            return r;
        }

        public List<Article> GetItems(SQLiteConnection con, long offset, long limit, Dictionary<string, object> filters)
        {
            List<Article> articles = new List<Article>((int)limit);

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT 
                    ITM.ITMREF,
                    ITM.ITMDES1,
                    ITM.ITMSTA,
                    ITM.TSICOD0,
                    ITM.TSICOD1,
                    ITM.TSICOD2,
                    ITM.TSICOD3,
                    ITM.TSICOD4,
                    ITM.TCLCOD,
                    ITM.STU,
                    ITM.LASCUNDAT,
                    ITM.PHYSTO,
                    ITM.MFMQTY,
                    ITM.FLGMVT,
                    ITM.FLGCUN,
                    ITM.FLGMFM,
                    ITM.FLGSTO,
                    count(CNT.ITMREF) as LABELS,
                    sum(CNT.VALUE) as COUNTED,
                    ITM.CUNFLG,
                    ITM.CUNLISNUM
                    FROM ITMMASTER ITM LEFT OUTER JOIN CNT ON ITM.ITMREF = CNT.ITMREF AND CNT.DELETED = 0 
                ");

                string prefix = "WHERE ";

                if(filters !=null && filters.Count > 0)
                {
                    if (filters.ContainsKey("counting") && (bool)filters["counting"])
                    {
                        sql.Append(prefix + " ITM.CUNFLG = 1 ");
                        prefix = " AND ";
                    }

                    if (filters.ContainsKey("itmref"))
                    {
                        sql.Append(prefix + " ITM.ITMREF LIKE @litmref ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@litmref", Like(filters["itmref"].ToString()));
                    }
                    if (filters.ContainsKey("itmdes"))
                    {
                        sql.Append(prefix + " ITM.ITMDES1 LIKE @litmdes ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@litmdes", Like(filters["itmdes"].ToString()));
                    }
                    if (filters.ContainsKey("active"))
                    {
                        if((bool)filters["active"])
                        {
                            sql.Append(prefix + " ITM.ITMSTA = 1 ");
                        }
                        else
                        {
                            sql.Append(prefix + " ITM.ITMSTA <> 1 ");
                        }
                        prefix = " AND ";
                    }
                    if (filters.ContainsKey("nocount"))
                    {
                        if ((bool)filters["nocount"])
                        {
                            sql.Append(prefix + " CNT.ITMREF IS NULL ");
                            prefix = " AND ";
                        }

                    }
                    if (filters.ContainsKey("tsicod3"))
                    {
                        object v = filters["tsicod3"];
                        string[] cods = v as string[];
                        if(cods != null)
                        {
                            sql.Append(prefix + " ITM.TSICOD2 >= @ltsicod3s AND ITM.TSICOD2 <= @ltsicod3e ");
                            prefix = " AND ";
                            cmd.Parameters.AddWithValue("@ltsicod3s", cods[0]);
                            cmd.Parameters.AddWithValue("@ltsicod3e", cods[1]);
                        }
                        else
                        {
                            sql.Append(prefix + " ITM.TSICOD2 = @ltsicod3 ");
                            prefix = " AND ";
                            cmd.Parameters.AddWithValue("@ltsicod3", v.ToString());
                        }
                    }
                    if (filters.ContainsKey("tclcod"))
                    {
                        sql.Append(prefix + " ITM.TCLCOD LIKE @ltclcod ");
                        prefix = " AND ";
                        cmd.Parameters.AddWithValue("@ltclcod", filters["tclcod"].ToString());
                    }



                }

                sql.Append("GROUP BY ITM.ITMREF LIMIT @limit OFFSET @offset");

                cmd.CommandText = sql.ToString();

                cmd.Parameters.AddWithValue("@limit", limit);
                cmd.Parameters.AddWithValue("@offset", offset);

                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Article res = new Article();
                        res.Itmref = rd.GetString(0);
                        res.Itmdes = rd.GetString(1);
                        res.Itmsta = (int)rd.GetByte(2);
                        res.Tsicod0 = rd.GetString(3);
                        res.Tsicod1 = rd.GetString(4);
                        res.Tsicod2 = rd.GetString(5);
                        res.Tsicod3 = rd.GetString(6);
                        res.Tsicod4 = rd.GetString(7);
                        res.Tclcod = rd.GetString(8);
                        res.Stu = rd.GetString(9);
                        res.Lascundat = rd.IsDBNull(10) ? DateTime.MinValue : rd.GetDateTime(10);
                        res.Physto = rd.IsDBNull(11) ? -1 : (double)rd.GetDecimal(11);
                        res.Mfmqty = rd.IsDBNull(12) ? -1 : (double)rd.GetDecimal(12);

                        res.Mvt = rd.GetByte(13) != 0;
                        res.Cun = rd.GetByte(14) != 0;
                        res.Mfm = rd.GetByte(15) != 0;
                        res.Sto = rd.GetByte(16) != 0;

                        res.LabelCount = rd.IsDBNull(17) ? 0 : rd.GetInt32(17);
                        res.CountedQuantity = rd.IsDBNull(18) ? 0 : (double)rd.GetDecimal(18);

                        res.Cunflg = rd.GetByte(19) != 0;
                        res.Cunlisnum = rd.IsDBNull(20) ? string.Empty : rd.GetString(20);


                        articles.Add(res);
                    }
                }
            }

            return articles;
        }

       


        internal int Update(SQLiteConnection con, Article art)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"UPDATE ITMMASTER SET 
                    ITMREF = @ITMREF,
                    ITMDES1 = @ITMDES1,
                    ITMSTA = @ITMSTA,
                    TSICOD0 = @TSICOD0,
                    TSICOD1 = @TSICOD1,
                    TSICOD2 = @TSICOD2,
                    TSICOD3 = @TSICOD3,
                    TSICOD4 = @TSICOD4,
                    TCLCOD = @TCLCOD,
                    STU = @STU,
                    LASCUNDAT = @LASCUNDAT,
                    PHYSTO = @PHYSTO,
                    MFMQTY = @MFMQTY,
                    FLGMVT = @FLGMVT,
                    FLGCUN = @FLGCUN,
                    FLGMFM = @FLGMFM,
                    FLGSTO = @FLGSTO,
                    CUNFLG = @CUNFLG,
                    CUNLISNUM = @CUNLISNUM
                    WHERE ITMMASTER.ITMREF=@WITMREF";
                cmd.Parameters.AddWithValue("@ITMREF", art.Itmref);
                cmd.Parameters.AddWithValue("@ITMDES1", art.Itmdes);
                cmd.Parameters.AddWithValue("@ITMSTA", art.Itmsta);
                cmd.Parameters.AddWithValue("@TSICOD0", art.Tsicod0);
                cmd.Parameters.AddWithValue("@TSICOD1", art.Tsicod1);
                cmd.Parameters.AddWithValue("@TSICOD2", art.Tsicod2);
                cmd.Parameters.AddWithValue("@TSICOD3", art.Tsicod3);
                cmd.Parameters.AddWithValue("@TSICOD4", art.Tsicod4);
                cmd.Parameters.AddWithValue("@TCLCOD", art.Tclcod);
                cmd.Parameters.AddWithValue("@STU", art.Stu);
                cmd.Parameters.AddWithValue("@LASCUNDAT", art.Lascundat);
                cmd.Parameters.AddWithValue("@PHYSTO", art.Physto);
                cmd.Parameters.AddWithValue("@MFMQTY", art.Mfmqty);
                cmd.Parameters.AddWithValue("@FLGMVT", art.Mvt);
                cmd.Parameters.AddWithValue("@FLGCUN", art.Cun);
                cmd.Parameters.AddWithValue("@FLGMFM", art.Mfm);
                cmd.Parameters.AddWithValue("@FLGSTO", art.Sto);
                cmd.Parameters.AddWithValue("@WITMREF", art.Itmref);
                cmd.Parameters.AddWithValue("@CUNFLG", art.Cunflg);
                cmd.Parameters.AddWithValue("@CUNLISNUM", art.Cunlisnum);

                return cmd.ExecuteNonQuery();
            }
        }

        internal int Insert(SQLiteConnection con, Article art)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO ITMMASTER 
                    (ITMREF, ITMDES1, ITMSTA, TSICOD0, TSICOD1, TSICOD2, TSICOD3, TSICOD4, TCLCOD, STU, LASCUNDAT, PHYSTO, MFMQTY, FLGMVT, FLGCUN, FLGMFM, FLGSTO, CUNFLG, CUNLISNUM) 
                    VALUES
                    (@ITMREF, @ITMDES1, @ITMSTA, @TSICOD0, @TSICOD1, @TSICOD2, @TSICOD3, @TSICOD4, @TCLCOD, @STU, @LASCUNDAT, @PHYSTO, @MFMQTY, @FLGMVT, @FLGCUN, @FLGMFM, @FLGSTO, @CUNFLG, @CUNLISNUM)";
                cmd.Parameters.AddWithValue("@ITMREF", art.Itmref);
                cmd.Parameters.AddWithValue("@ITMDES1", art.Itmdes);
                cmd.Parameters.AddWithValue("@ITMSTA", art.Itmsta);
                cmd.Parameters.AddWithValue("@TSICOD0", art.Tsicod0);
                cmd.Parameters.AddWithValue("@TSICOD1", art.Tsicod1);
                cmd.Parameters.AddWithValue("@TSICOD2", art.Tsicod2);
                cmd.Parameters.AddWithValue("@TSICOD3", art.Tsicod3);
                cmd.Parameters.AddWithValue("@TSICOD4", art.Tsicod4);
                cmd.Parameters.AddWithValue("@TCLCOD", art.Tclcod);
                cmd.Parameters.AddWithValue("@STU", art.Stu);
                cmd.Parameters.AddWithValue("@LASCUNDAT", art.Lascundat);
                cmd.Parameters.AddWithValue("@PHYSTO", art.Physto);
                cmd.Parameters.AddWithValue("@MFMQTY", art.Mfmqty);
                cmd.Parameters.AddWithValue("@FLGMVT", art.Mvt);
                cmd.Parameters.AddWithValue("@FLGCUN", art.Cun);
                cmd.Parameters.AddWithValue("@FLGMFM", art.Mfm);
                cmd.Parameters.AddWithValue("@FLGSTO", art.Sto);
                cmd.Parameters.AddWithValue("@CUNFLG", art.Cunflg);
                cmd.Parameters.AddWithValue("@CUNLISNUM", art.Cunlisnum);


                return cmd.ExecuteNonQuery();
            }
        }


        public List<CountEntry> GetCountEntries(SQLiteConnection con, string itmref)
        {
            List<CountEntry> entries = new List<CountEntry>(10);

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = "SELECT ITMREF, LABEL, CREATED, VALUE, STU, DELETED, SYNC, HASH, SYNC_CODE, LOCATION FROM CNT WHERE DELETED = 0 AND ITMREF=@itmref ";
                cmd.Parameters.AddWithValue("@itmref", itmref);
                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CountEntry entry = ParseCountEntry(rd);
                        entries.Add(entry);

                    }
                }
            }

            return entries;
        }


        public CountEntry GetCountEntryByHash(SQLiteConnection con, string hash)
        {
            CountEntry entry = null;

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = "SELECT ITMREF, LABEL, CREATED, VALUE, STU, DELETED, SYNC, HASH, SYNC_CODE, LOCATION FROM CNT WHERE HASH=@h ";
                cmd.Parameters.AddWithValue("@h", hash);
                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        entry = ParseCountEntry(rd);
                    }
                }
            }

            return entry;
        }

        public List<CountEntry> GetCountEntries(SQLiteConnection con)
        {
            List<CountEntry> entries = new List<CountEntry>(10);

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = "SELECT ITMREF, LABEL, CREATED, VALUE, STU, DELETED, SYNC, HASH, SYNC_CODE, LOCATION FROM CNT WHERE DELETED = 0 ";
                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        CountEntry entry = ParseCountEntry(rd);
                        entries.Add(entry);

                    }
                }
            }

            return entries;
        }

        public List<CountEntry> GetDirtyCountEntries(SQLiteConnection con)
        {
            List<CountEntry> entries = new List<CountEntry>(10);

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = "SELECT ITMREF, LABEL, CREATED, VALUE, STU, DELETED, SYNC, HASH, SYNC_CODE, LOCATION FROM CNT WHERE SYNC = 0";
                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        CountEntry entry = ParseCountEntry(rd);
                        entries.Add(entry);

                    }
                }
            }

            return entries;
        }

        private CountEntry ParseCountEntry(SQLiteDataReader rd)
        {
            CountEntry entry = new CountEntry();
            entry.Itmref = rd.GetString(0);
            entry.LabelNumber = rd.GetInt32(1);
            entry.Time = rd.GetDateTime(2);
            entry.Value = (double)rd.GetDecimal(3);
            entry.Unit = rd.GetString(4);
            entry.IsDeleted = rd.GetByte(5) != 0;
            entry.Sync = (CountEntry.SyncStatus)rd.GetInt32(6);
            entry.Hash = rd.GetString(7);
            entry.SyncCode = rd.GetInt32(8);
            entry.Location = rd.IsDBNull(9) ? string.Empty : rd.GetString(9);
            return entry;
        }


        internal int DeleteAllCountEntries(SQLiteConnection con)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"UPDATE CNT SET DELETED = 1, SYNC=0, DELETE_KEY=@K WHERE DELETED = 0";
                cmd.Parameters.AddWithValue("@K", DateTime.UtcNow.ToFileTimeUtc());
                return cmd.ExecuteNonQuery();
            }
        }

        internal int Insert(SQLiteConnection con, CountEntry en)
        {
            return Insert(con, en, null);
        }
        internal int Insert(SQLiteConnection con, CountEntry en, SQLiteTransaction trans)
        {
            //en.Sync = CountEntry.SyncStatus.NEW;

            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.Transaction = trans;

                cmd.CommandText = @"INSERT INTO CNT 
                    (ITMREF, LABEL, CREATED, VALUE, STU, LOCATION, DELETED, SYNC, HASH, SYNC_CODE, DELETE_KEY ) 
                    VALUES
                    (@ITMREF, @LABEL, @CREATED, @VALUE, @STU, @LOCATION, @DELETED, @SYNC, @HASH, @SYNC_CODE, @DELETE_KEY)";
                cmd.Parameters.AddWithValue("@ITMREF", en.Itmref);
                cmd.Parameters.AddWithValue("@LABEL", en.LabelNumber);
                cmd.Parameters.AddWithValue("@CREATED", en.Time);
                cmd.Parameters.AddWithValue("@VALUE", en.Value);
                cmd.Parameters.AddWithValue("@STU", en.Unit);
                cmd.Parameters.AddWithValue("@LOCATION", en.Location);
                cmd.Parameters.AddWithValue("@DELETED", en.IsDeleted ? 1 : 0);
                cmd.Parameters.AddWithValue("@SYNC", en.Sync);
                cmd.Parameters.AddWithValue("@HASH", en.Hash);
                cmd.Parameters.AddWithValue("@SYNC_CODE", en.SyncCode);
                cmd.Parameters.AddWithValue("@DELETE_KEY", en.DeleteKey);

                return cmd.ExecuteNonQuery();
            }
        }
        internal int Update(SQLiteConnection con, CountEntry en)
        {
            //en.Sync = CountEntry.SyncStatus.NEW;

            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"UPDATE CNT SET 
                    VALUE = @VALUE, LOCATION = @LOCATION,
                    DELETED = @DELETED, SYNC=@SYNC, HASH=@HASH, SYNC_CODE=@SYNC_CODE, DELETE_KEY=@DELETE_KEY
                    WHERE CNT.ITMREF=@WITMREF AND LABEL=@WLABEL AND HASH=@HASH";
                cmd.Parameters.AddWithValue("@VALUE", en.Value);
                cmd.Parameters.AddWithValue("@LOCATION", en.Location);
                cmd.Parameters.AddWithValue("@DELETED", en.IsDeleted);
                cmd.Parameters.AddWithValue("@SYNC", en.Sync);
                cmd.Parameters.AddWithValue("@HASH", en.Hash);
                cmd.Parameters.AddWithValue("@SYNC_CODE", en.SyncCode);
                cmd.Parameters.AddWithValue("@DELETE_KEY", en.DeleteKey);

                cmd.Parameters.AddWithValue("@WITMREF", en.Itmref);
                cmd.Parameters.AddWithValue("@WLABEL", en.LabelNumber);
                cmd.Parameters.AddWithValue("@HASH", en.Hash);


                return cmd.ExecuteNonQuery();
            }
        }


        public List<Location> GetLocations(SQLiteConnection con)
        {
            List<Location> entries = new List<Location>(10);

            using (SQLiteCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = "SELECT CODE, DESCRIPTION FROM LOCATION";
                using (SQLiteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        Location entry = new Location();
                        entry.Code = rd.GetString(0);
                        entry.Description = rd.GetString(1);

                        entries.Add(entry);
                    }
                }
            }

            return entries;
        }

        internal int Delete(SQLiteConnection con, Location l)
        {
            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM LOCATION WHERE CODE=@C";
                cmd.Parameters.AddWithValue("@C", l.Code);
                return cmd.ExecuteNonQuery();
            }
        }

        internal int Insert(SQLiteConnection con, Location en)
        {
            return Insert(con, en, null);
        }
        internal int Insert(SQLiteConnection con, Location en, SQLiteTransaction trans)
        {
            //en.Sync = CountEntry.SyncStatus.NEW;

            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.Transaction = trans;

                cmd.CommandText = @"INSERT INTO LOCATION (CODE, DESCRIPTION ) VALUES (@CODE, @DESCRIPTION)";
                cmd.Parameters.AddWithValue("@CODE", en.Code);
                cmd.Parameters.AddWithValue("@DESCRIPTION", en.Description);

                return cmd.ExecuteNonQuery();
            }
        }

        internal int Update(SQLiteConnection con, Location en)
        {
            //en.Sync = CountEntry.SyncStatus.NEW;

            using (SQLiteCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"UPDATE LOCATION SET DESCRIPTION = @DESCRIPTION WHERE CODE=@CODE";
                cmd.Parameters.AddWithValue("@DESCRIPTION", en.Description);
                cmd.Parameters.AddWithValue("@CODE", en.Code);

                return cmd.ExecuteNonQuery();
            }
        }


    }
}
