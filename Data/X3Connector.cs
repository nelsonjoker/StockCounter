using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace StockCounter.Data
{
    class X3Connector
    {
        

        public int CommandTimeout { get; set; }

        public string ConnectionString { get; set; }

        public X3Connector(string connectionString)
        {
            ConnectionString = connectionString;
            CommandTimeout = 60;
        }
        

        private static X3Connector theX3Connector;
        public static X3Connector X3
        {
            get
            {
                if (theX3Connector == null)
                    theX3Connector = new X3Connector("Server=192.168.0.7\\SAGEX3;Database=x3v11prod;User Id=reader;Password=xxxxxx;");
                return theX3Connector;
            }
        }

        private static X3Connector theX3TestConnector;
        public static X3Connector X3Test
        {
            get
            {
                if (theX3TestConnector == null)
                    theX3TestConnector = new X3Connector("Server=192.168.10.7\\SAGEX3;Database=x3v11prod;User Id=sa;Password=xxxxxx;");
                return theX3TestConnector;
            }
        }

        public SqlConnection Connect()
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            sqlConnection1.Open();
            return sqlConnection1;
        }

        public SqlDataReader Query(SqlConnection con, string query)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandTimeout = this.CommandTimeout;


            reader = cmd.ExecuteReader();
            return reader;
        }

        public string WhereIN(string fieldname, List<string> values)
        {
            return WhereIN(fieldname, values.ToArray());
        }


        public string WhereIN(string fieldname, string[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fieldname);
            sb.Append(" IN (");

            for(int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append("'");
                sb.Append(values[i]);
                sb.Append("'");
            }


            sb.Append(")");

            return sb.ToString();
        }

        public string WhereIN(string fieldname, List<long> values)
        {
            return WhereIN(fieldname, values.ToArray());
        }


        public string WhereIN(string fieldname, long[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fieldname);
            sb.Append(" IN (");

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(values[i]);
            }


            sb.Append(")");

            return sb.ToString();
        }

        public long CountItems(SqlConnection con)
        {
            using (SqlCommand select = con.CreateCommand())
            {
                select.CommandText = "SELECT count(*) as c FROM PRDSOT.ITMMASTER ITM WHERE ITM.STOMGTCOD_0 = 2";
                object c = select.ExecuteScalar();
                return (int)c;
            }
        }


        public SqlDataReader GetItems(SqlConnection con)
        {
            using (SqlCommand select = con.CreateCommand())
            {
                string sql = @"
                    SELECT
                    ITM.ITMREF_0,
                    ITM.ITMDES1_0,
                    ITM.ITMSTA_0,
                    ITM.TSICOD_0,
                    ITM.TSICOD_1,
                    ITM.TSICOD_2,
                    ITM.TSICOD_3,
                    ITM.TSICOD_4,
                    ITM.TCLCOD_0,
                    ITM.STU_0,
                    ITV.LASCUNDAT_0,
                    ITV.PHYSTO_0,
                    MFM.MFMQTY,
                    ITV.ITMREF_0 AS HASMVT,
                    ITF.CUNFLG_0,
                    ITF.CUNLISNUM_0
                    FROM
                    PRDSOT.ITMMASTER ITM
                    JOIN PRDSOT.ITMFACILIT ITF ON ITF.ITMREF_0 = ITM.ITMREF_0 AND ITF.STOFCY_0 = '001'
                    LEFT JOIN (
                        SELECT
                        ITV.ITMREF_0,
                        MAX(ITV.LASCUNDAT_0) AS LASCUNDAT_0,
                        SUM(ITV.PHYSTO_0) AS PHYSTO_0
                        FROM
                        PRDSOT.ITMMVT ITV
                        GROUP BY ITV.ITMREF_0
                    )AS ITV ON ITV.ITMREF_0 = ITM.ITMREF_0
                    LEFT JOIN(
                        SELECT
                        MFM.ITMREF_0,
                        SUM(MFM.RETQTY_0) AS MFMQTY
                        FROM
                        PRDSOT.MFGMAT MFM
                        WHERE
                        MFM.MATSTA_0 < 3
                        AND MFM.MFGSTA_0 < 4
                        AND MFM.MFMTRKFLG_0 < 5
                        GROUP BY MFM.ITMREF_0
                    )AS MFM ON MFM.ITMREF_0 = ITM.ITMREF_0
                    WHERE
                    ITM.STOMGTCOD_0 = 2
                    ORDER BY ITM.ITMREF_0";

                select.CommandText = sql;
                select.CommandTimeout = 10 * 60;
                SqlDataReader rd = select.ExecuteReader();
                return rd;

            }
        }

    }
}
