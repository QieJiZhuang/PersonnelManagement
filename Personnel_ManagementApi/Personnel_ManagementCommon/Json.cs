using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Data.Common;

namespace DTcms.Common
{
    public class CommonJsonModelAnalyzer
    {
        protected string _GetKey(string rawjson)
        {
            if (string.IsNullOrEmpty(rawjson))
                return rawjson;
            rawjson = rawjson.Trim();
            string[] jsons = rawjson.Split(new char[] { ':' });
            if (jsons.Length < 2)
                return rawjson;
            return jsons[0].Replace("\"", "").Trim();
        }
        protected string _GetValue(string rawjson)
        {
            if (string.IsNullOrEmpty(rawjson))
                return rawjson;
            rawjson = rawjson.Trim();
            string[] jsons = rawjson.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (jsons.Length < 2)
                return rawjson;
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < jsons.Length; i++)
            {
                builder.Append(jsons[i]);
                builder.Append(":");
            }
            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);
            string value = builder.ToString();
            if (value.StartsWith("\""))
                value = value.Substring(1);
            if (value.EndsWith("\""))
                value = value.Substring(0, value.Length - 1);
            return value;
        }
        protected List<string> _GetCollection(string rawjson)
        {
            //[{},{}]
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(rawjson))
                return list;
            rawjson = rawjson.Trim();
            StringBuilder builder = new StringBuilder();
            int nestlevel = -1;
            int mnestlevel = -1;
            for (int i = 0; i < rawjson.Length; i++)
            {
                if (i == 0)
                    continue;
                else if (i == rawjson.Length - 1)
                    continue;
                char jsonchar = rawjson[i];
                if (jsonchar == '{')
                {
                    nestlevel++;
                }
                if (jsonchar == '}')
                {
                    nestlevel--;
                }
                if (jsonchar == '[')
                {
                    mnestlevel++;
                }
                if (jsonchar == ']')
                {
                    mnestlevel--;
                }
                if (jsonchar == ',' && nestlevel == -1 && mnestlevel == -1)
                {
                    list.Add(builder.ToString());
                    builder = new StringBuilder();
                }
                else
                {
                    builder.Append(jsonchar);
                }
            }
            if (builder.Length > 0)
                list.Add(builder.ToString());
            return list;
        }
    }


    public class CommonJsonModel : CommonJsonModelAnalyzer
    {
        private string rawjson;
        private bool isValue = false;
        private bool isModel = false;
        private bool isCollection = false;

        public static CommonJsonModel DeSerialize(string json)
        {
            return new CommonJsonModel(json);
        }
        public CommonJsonModel(string rawjson)//internal  wx 12-9
        {
            this.rawjson = rawjson;
            if (string.IsNullOrEmpty(rawjson))
                throw new Exception("missing rawjson");
            rawjson = rawjson.Trim().Replace(": ", ":").Replace(" :", ":");
            if (rawjson.StartsWith("{"))
            {
                isModel = true;
            }
            else if (rawjson.StartsWith("["))
            {
                isCollection = true;
            }
            else
            {
                isValue = true;
            }
        }
        public string Rawjson
        {
            get { return rawjson; }
        }
        public bool IsValue()
        {
            return isValue;
        }
        public bool IsValue(string key)
        {
            if (!isModel)
                return false;
            if (string.IsNullOrEmpty(key))
                return false;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                {
                    if (string.IsNullOrEmpty(model.Value))
                    {
                        return false;
                    }
                    CommonJsonModel submodel = new CommonJsonModel(model.Value);
                    return submodel.IsValue();
                }
            }
            return false;
        }
        public bool IsModel()
        {
            return isModel;
        }
        public bool IsModel(string key)
        {
            if (!isModel)
                return false;
            if (string.IsNullOrEmpty(key))
                return false;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                {
                    CommonJsonModel submodel = new CommonJsonModel(model.Value);
                    return submodel.IsModel();
                }
            }
            return false;
        }
        public bool IsCollection()
        {
            return isCollection;
        }
        public bool IsCollection(string key)
        {
            if (!isModel)
                return false;
            if (string.IsNullOrEmpty(key))
                return false;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                {
                    CommonJsonModel submodel = new CommonJsonModel(model.Value);
                    return submodel.IsCollection();
                }
            }
            return false;
        }

        /// <summary>
        /// 当模型是对象，返回拥有的key
        /// </summary>
        /// <returns></returns>
        public List<string> GetKeys()
        {
            if (!isModel)
                return null;
            List<string> list = new List<string>();
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                string key = new CommonJsonModel(subjson).Key;
                if (!string.IsNullOrEmpty(key))
                    list.Add(key);
            }
            return list;
        }
        /// <summary>
        /// 当模型是对象，key对应是值，则返回key对应的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            if (!isModel)
                return null;
            if (string.IsNullOrEmpty(key))
                return null;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                    return model.Value;
            }
            return null;
        }
        /// <summary>
        /// 模型是对象，key对应是对象，返回key对应的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CommonJsonModel GetModel(string key)
        {
            if (!isModel)
                return null;
            if (string.IsNullOrEmpty(key))
                return null;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                {
                    CommonJsonModel submodel = new CommonJsonModel(model.Value);
                    if (!submodel.IsModel())
                        return null;
                    else
                        return submodel;
                }
            }
            return null;
        }
        /// <summary>
        /// 模型是对象，key对应是集合，返回集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CommonJsonModel GetCollection(string key)
        {
            if (!isModel)
                return null;
            if (string.IsNullOrEmpty(key))
                return null;
            foreach (string subjson in base._GetCollection(this.rawjson))
            {
                CommonJsonModel model = new CommonJsonModel(subjson);
                if (!model.IsValue())
                    continue;
                if (model.Key == key)
                {
                    CommonJsonModel submodel = new CommonJsonModel(model.Value);
                    if (!submodel.IsCollection())
                        return null;
                    else
                        return submodel;
                }
            }
            return null;
        }
        /// <summary>
        /// 模型是集合，返回自身
        /// </summary>
        /// <returns></returns>
        public List<CommonJsonModel> GetCollection()
        {
            List<CommonJsonModel> list = new List<CommonJsonModel>();
            if (IsValue())
                return list;
            foreach (string subjson in base._GetCollection(rawjson))
            {
                list.Add(new CommonJsonModel(subjson));
            }
            return list;
        }


        /// <summary>
        /// 当模型是值对象，返回key
        /// </summary>
        private string Key
        {
            get
            {
                if (IsValue())
                    return base._GetKey(rawjson);
                return null;
            }
        }
        /// <summary>
        /// 当模型是值对象，返回value
        /// </summary>
        private string Value
        {
            get
            {
                if (!IsValue())
                    return null;
                return base._GetValue(rawjson);
            }
        }
    }



    public class ConvertJson
    {
        #region 私有方法
        /// <summary> 
        /// 过滤特殊字符 
        /// </summary> 
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary> 
        /// 格式化字符型、日期型、布尔型 
        /// </summary> 
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        #endregion

        #region List转换成Json
        /// <summary> 
        /// List转换成Json 
        /// </summary> 
        public static string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        /// <summary> 
        /// List转换成Json 
        /// </summary> 
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = list[0].GetType().Name;
            Json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        #region 对象转换为Json
        /// <summary> 
        /// 对象转换为Json 
        /// </summary> 
        /// <param name="jsonObject">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }
        #endregion

        #region 对象集合转换Json
        /// <summary> 
        /// 对象集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }
        #endregion

        #region 普通集合转换Json
        /// <summary> 
        /// 普通集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }
        #endregion

        #region DataSet转换为Json
        /// <summary> 
        /// DataSet转换为Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        #endregion

        #region Datatable转换为Json
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary> 
        /// DataTable转换为Json 
        /// </summary> 
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName)) jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        #region DataReader转换为Json
        /// <summary> 
        /// DataReader转换为Json 
        /// </summary> 
        /// <param name="dataReader">DataReader对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            while (dataReader.Read())
            {
                jsonString.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type type = dataReader.GetFieldType(i);
                    string strKey = dataReader.GetName(i);
                    string strValue = dataReader[i].ToString();
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            dataReader.Close();
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion


        public static string EncodeJsonString(string _s)
        {
            ////Convert.ToBase64String(Encoding.UTF8.GetBytes(_s));
            //if (_s == null) return "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder(_s);
            //sb.Replace(@"\", @"\\");
            //sb.Replace(@"'", @"\'");
            //sb.Replace(@"""", @"\""");
            //sb.Replace(Environment.NewLine, "");    //替换连在一起的\r\n
            //sb.Replace("\n", @"\n");                //单个替换
            //sb.Replace("\r", @"\n");
            //string html = sb.ToString();
            //html = Regex.Replace(html, "[\f\n\r\t\v]", "");
            //html = Regex.Replace(html, " {2,}", " ");
            //html = Regex.Replace(html, ">[ ]{1}", ">");
            //return html;
            return Utils.HtmlEncode(Utils.ToHtml(_s));
        }

        public static string EncodeJsonStringForTextArea(string _s)
        {
            return Utils.ToHtml(_s);
        }


    }

}
