using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;

namespace Curd.Models.Function
{
    public class DataConversionModule
    {
        public List<DataRow> toLists(DataTable dt)
        {
            List<DataRow> result = new List<DataRow>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(row);
                }
            }
            return result;
        }
                
        public string toJson(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<DataRow> result = new List<DataRow>();
            Dictionary<string, object> row;
            var jsonSerialiser = new JavaScriptSerializer();
            string json = "";

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            json = jsonSerialiser.Serialize(rows);
            return json;
        }

        public Dictionary<string, string> toKeyValueDictionary(DataTable dt, string key, string value)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(Convert.ToString(row[key]), Convert.ToString(row[value]));
                }
            }
            return result;
        }

        public string ArrayToString(string[] array)
        {
            string result = "";
            int arrayLength = array.Length;
            result = array[0];
            for (int i = 1; i < arrayLength; i++)
            {
                result += "," + array[i];
            }
            return result;
        }

        public Dictionary<string, string> PostToKeyValue()
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            foreach (var postKey in HttpContext.Current.Request.Unvalidated.Form)
            {
                if (HttpContext.Current.Request.Unvalidated.Form[Convert.ToString(postKey)] != null)
                {
                    postData.Add(Convert.ToString(postKey), Convert.ToString(HttpContext.Current.Request.Unvalidated.Form[Convert.ToString(postKey)]));
                }
            }
            return postData;
        }

        public Dictionary<string, string> GetToKeyValue()
        {
            Dictionary<string, string> getData = new Dictionary<string, string>();
            foreach (var getKey in HttpContext.Current.Request.Unvalidated.QueryString)
            {
                if (HttpContext.Current.Request.Unvalidated.QueryString[Convert.ToString(getKey)] != null)
                {
                    getData.Add(Convert.ToString(getKey), Convert.ToString(HttpContext.Current.Request.Unvalidated.QueryString[Convert.ToString(getKey)]));
                }
            }
            return getData;
        }

       

        
    }
}
