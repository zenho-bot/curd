using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Curd.Models.DbContact
{
    public class MessageModule : MY_BackendModule
    {
        public MessageModule()
        {
            thisFrom = "message";
            thisID = "message_id";
            thisAbrv = "message_";
            thisListSelect = "message_id, message_title, message_content";
            thisRowSelect = "*";
            thisOrderBy = "message_id";
            thisOrderType = "DESC";
            
        }

        public DataTable getList()
        {
            
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            DataTable dt;
            string select = "SELECT " + thisListSelect + " ";
            string from = "FROM " + thisFrom + " ";
            string where = " ";
            string order = "ORDER BY " + thisOrderBy + " " + thisOrderType + " ";
            string offset = "";
            string sql = "";

           

            sql = select + from + where + order + offset;
            dt = ReturnDataTable(sql, parameters, values);
            return dt;
        }

        public int getListTotal(Dictionary<string, string> queryData)
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            DataTable dt;
            int result = 0;
            string select = "SELECT count(" + thisID + ") total ";
            string from = "FROM " + thisFrom + " ";
            string where = " ";
            string sql = "";


            sql = select + from + where;
            dt = ReturnDataTable(sql, parameters, values);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = Convert.ToInt32(dt.Rows[0]["total"]);
            }
            return result;
        }

    }
}
