using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace CurdBackend.Models.DbContact
{
    public class MY_BackendModule
    {
        protected SqlConnection DBContact = new SqlConnection(Convert.ToString(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString));
        public string thisFrom = "";
        public string thisID = "";
        public string thisAbrv = "";
        public string thisListSelect = "";
        public string thisRowSelect = "";
        public string thisOrderBy = "";
        public string thisOrderType = "";

        /**
        * 防被攻擊用
        */
        protected bool chkVariable(string sql = "")
        {
            string fileterSql = "<script,insert ,select ,delete ,drop ,update ,iframe,alert,prompt,execute,exec,create ,sysobjects,truncate,xp_cmdshell,sp_oacreate,wscript.shell,xp_regwrite";
            string oldSql = sql;
            string[] replace_sqls = fileterSql.Split(',');

            foreach (string replace_sql in replace_sqls)
            {
                sql = Regex.Replace(sql, replace_sql, "", RegexOptions.IgnoreCase);
                if (oldSql != sql)
                {
                    return false;
                }
            }
            return true;

        }


        public DataTable ReturnDataTable(string sqlCmdText, List<string> parameters, List<string> values)
        {
            string result = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdText, DBContact);
            SqlDataAdapter adapter;

            DBContact.Open();
            if (parameters.Count != 0 & values.Count != 0 & parameters.Count == values.Count)
            {
                for (byte i = 0; i <= parameters.Count - 1; i++)
                {
                    if (parameters[i] == "@offsetStart" || parameters[i] == "@perPage")
                    {
                        if (chkVariable(parameters[i]) && chkVariable(values[i]))
                        {
                            sqlCmd.Parameters.AddWithValue(parameters[i], Convert.ToInt32(values[i]));
                        }
                    }
                    else
                    {
                        if (chkVariable(parameters[i]) && chkVariable(values[i]))
                        {
                            sqlCmd.Parameters.AddWithValue(parameters[i], values[i]);
                        }
                    }
                }
            }
            try
            {
                adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(ds);
                adapter.Dispose();
            }
            catch (SqlException e)
            {
                //SetErrorsCodeData(e.Message, e.Source, e.StackTrace, Convert.ToString(e.TargetSite), "Error");
                result = Convert.ToString(e);
            }

            DBContact.Close();
            dt = ds.Tables.Count > 0 ? ds.Tables[0] : dt;
            return dt;
        }

        public string ReturnString(string sqlCmdText, List<string> parameters, List<string> values)
        {
            string result = "1";
            string insertId = "";
            string insertLastSql = "SELECT SCOPE_IDENTITY()";
            sqlCmdText = sqlCmdText + ";" + insertLastSql + ";";

            if (parameters.Count != 0 & values.Count != 0 & parameters.Count == values.Count)
            {
                DBContact.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlCmdText, DBContact))
                {
                    for (byte i = 0; i <= parameters.Count - 1; i++)
                    {
                        if (chkVariable(parameters[i]) && chkVariable(values[i]))
                        {
                            sqlCmd.Parameters.AddWithValue(parameters[i], values[i]);
                        }
                    }
                    try
                    {
                        //sqlCmd.ExecuteNonQuery();
                        insertId = Convert.ToString(sqlCmd.ExecuteScalar());
                        insertId = insertId == "" ? "1" : insertId;
                    }
                    catch (SqlException e)
                    {
                        result = Convert.ToString(e);
                    }

                }
                DBContact.Close();
            }
            return insertId;
        }

        public string addData(Dictionary<string, string> queryData)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            string sql = "INSERT INTO " + thisFrom + " ";
            string field = "(";
            string value = "(";
            string result = "";

            queryData.Add(thisAbrv + "created_user", "0");

            foreach (var val in queryData) {
                field += val.Key + ",";
                value += "@" + val.Key + ",";
                parameters.Add("@" + val.Key);
                values.Add(val.Value);
            }

            field = field +  " )";
            sql += field + "VALUES" + value;

            result = ReturnString(sql, parameters, values);
            return result;
        }

        public string updateData(Dictionary<string, string> queryData)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            string sql = "UPDATE " + thisFrom + " SET ";
            string result = "";
            string id = queryData[thisID];

           

            foreach (var val in queryData)
            {
                sql += val.Key + " = @" + val.Key + ",";
                parameters.Add("@" + val.Key);
                values.Add(val.Value);
            }
            parameters.Add("@" + thisID);
            values.Add(id);

            sql = sql.TrimEnd(',');
            sql += " WHERE 1 = 1 AND " + thisID + " = @" + thisID;

            result = ReturnString(sql, parameters, values);
            return result;
        }

        public string deleteData(Dictionary<string, string> queryData)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            string sql = "UPDATE " + thisFrom + " SET ";
            string result = "";
            string id = queryData["id"];

            

            foreach (var val in queryData)
            {
                sql += val.Key + " = @" + val.Key + ",";
                parameters.Add("@" + val.Key);
                values.Add(val.Value);
            }
            parameters.Add("@" + thisID);
            values.Add(id);

            sql = sql.TrimEnd(',');
            sql += " WHERE 1 = 1 AND " + thisID + " = @" + thisID;

            result = ReturnString(sql, parameters, values);
            return result;
        }


        public DataTable getData(string usersId)
        {
            DataTable dt = new DataTable();
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            string select = "SELECT " + thisRowSelect + " ";
            string from = "FROM " + thisFrom + " ";
            string where = "WHERE 1 = 1 AND "  + thisID + " = @" + thisID + " ";
            string sql = select + from + where;

            parameters.Add("@" + thisID + "");
            values.Add(usersId);
            dt = ReturnDataTable(sql, parameters, values);
            return dt;
        }
        
    }
}
