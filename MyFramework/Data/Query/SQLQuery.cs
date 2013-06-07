using System;
using System.Data;

namespace MyFramework.Data.Query
{
    /// <summary>
    /// SQL查询对象
    /// </summary>
    public class SQLQuery
    {
        protected string _DataBaseName = string.Empty;

        public SQLQuery()
        {

        }
        public SQLQuery(string DataBaseName)
        {
            _DataBaseName = DataBaseName;
        }

        #region GetDSQuery Method
        private DataSet GetDSQuery(DataHelper dh, string TableName, string ConditionForSelect, int PageIndex, int PageSize, ref int RecordCount, params string[] FieldNames)
        {
            string sqlcmd = "select {0} from " + TableName + " {1}";

            sqlcmd = string.Format(sqlcmd, GetColumnNames(FieldNames), ConditionForSelect != string.Empty ? Checkcondition("where " + ConditionForSelect) : string.Empty);
            try
            {
                if (PageIndex == -1)
                {
                    return dh.ExecuteDataSet(sqlcmd);
                }
                else
                {
                    return dh.ExecuteDataSet(sqlcmd, PageIndex, PageSize, ref RecordCount);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>Dataset</returns>
        public DataSet GetDSQuery(string TableName, string ConditionForSelect, params string[] FieldNames)
        {
            int MaxCount = 0;
            return GetDSQuery(new DataHelper(_DataBaseName), TableName, ConditionForSelect, -1, -1, ref MaxCount, FieldNames);
        }
        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <returns>Dataset</returns>
        public DataSet GetDSQuery(string TableName, string ConditionForSelect, int PageIndex, int PageSize, ref int RecordCount, params string[] FieldNames)
        {
            return GetDSQuery(new DataHelper(_DataBaseName), TableName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);
        }
        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <returns>Dataset</returns>
        public DataSet GetDSQuery(string TableName, SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            int MaxCount = 0;
            DataHelper dh = new DataHelper(_DataBaseName);
            return GetDSQuery(dh, TableName, ConditionForSelect != null ? ConditionForSelect.ToString(dh) : string.Empty, -1, -1, ref MaxCount, FieldNames);
        }

        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="FieldNames">自定义字段</param>        
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>Dataset</returns>
        public DataSet GetDSQuery(string TableName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, ref int RecordCount, params string[] FieldNames)
        {
            DataHelper dh = new DataHelper(_DataBaseName);
            return GetDSQuery(dh, TableName, ConditionForSelect != null ? ConditionForSelect.ToString(dh) : string.Empty, PageIndex, PageSize, ref RecordCount, FieldNames);
        }


        #endregion

        #region GetDRQuery Method
        private IDataReader GetDRQuery(DataHelper dh, string TableName, string ConditionForSelect, params string[] FieldNames)
        {
            string sqlcmd = "select {0} from " + TableName + " {1}";

            sqlcmd = string.Format(sqlcmd, GetColumnNames(FieldNames), ConditionForSelect != string.Empty ? Checkcondition("where " + ConditionForSelect) : string.Empty);

            return dh.ExecuteReader(sqlcmd);
        }
        /// <summary>
        /// 执行查询返回DataReader
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>DataReader</returns>
        public IDataReader GetDRQuery(string TableName, string ConditionForSelect, params string[] FieldNames)
        {
            return GetDRQuery(new DataHelper(_DataBaseName), TableName, ConditionForSelect, FieldNames);
        }
        /// <summary>
        /// 执行查询返回DataReader
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>DataReader</returns>
        public IDataReader GetDRQuery(string TableName, SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            DataHelper dh = new DataHelper(_DataBaseName);
            return GetDRQuery(dh, TableName, ConditionForSelect != null ? ConditionForSelect.ToString(dh) : string.Empty, FieldNames);
        }

        #endregion

        #region GetDataTable Method
        /// <summary>
        ///  执行查询返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns></returns>
        public DataTable GetDTQuery(string TableName, string ConditionForSelect, params string[] FieldNames)
        {
            DataSet ds = GetDSQuery(TableName, ConditionForSelect, FieldNames);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = TableName;
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        ///  执行查询返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns></returns>
        public DataTable GetDTQuery(string TableName, SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            DataSet ds = GetDSQuery(TableName, ConditionForSelect, FieldNames);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = TableName;
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        ///  执行查询返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="FieldNames">自定义字段</param>        
        /// <returns></returns>
        public DataTable GetDTQuery(string TableName, string ConditionForSelect, int PageIndex, int PageSize, ref int RecordCount, params string[] FieldNames)
        {
            DataSet ds = GetDSQuery(TableName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = TableName;
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        ///  执行查询返回DataTable
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns></returns>
        public DataTable GetDTQuery(string TableName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, ref int RecordCount, params string[] FieldNames)
        {
            DataSet ds = GetDSQuery(TableName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = TableName;
                return ds.Tables[0];
            }
            return null;
        }
        #endregion

        #region Scalar Method
        private object ScalarQuery(DataHelper dh, string TableName, string ConditionForSelect, string FieldName)
        {
            string sqlcmd = "select {0} from " + TableName + " {1}";

            sqlcmd = string.Format(sqlcmd, GetColumnNames(FieldName), ConditionForSelect != string.Empty ? Checkcondition("where " + ConditionForSelect) : string.Empty);

            return dh.ExecuteScalar(sqlcmd);
        }
        /// <summary>
        ///  执行查询返回首字段值
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldName">首字段</param>
        /// <returns>值</returns>
        public object ScalarQuery(string TableName, string ConditionForSelect, string FieldName)
        {
            return ScalarQuery(new DataHelper(_DataBaseName), TableName, ConditionForSelect, FieldName);
        }
        /// <summary>
        ///  执行查询返回首字段值
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldName">首字段</param>
        /// <returns>值</returns>
        public object ScalarQuery(string TableName, SQLCondition ConditionForSelect, string FieldName)
        {
            DataHelper dh = new DataHelper(_DataBaseName);
            return ScalarQuery(dh, TableName, ConditionForSelect != null ? ConditionForSelect.ToString(dh) : string.Empty, FieldName);
        }

        #endregion

        #region Private Method
        /// <summary>
        /// 清除多余的操作符
        /// </summary>
        /// <param name="conditionForSelect"></param>
        /// <returns></returns>
        private string Checkcondition(string conditionForSelect)
        {

            if ((conditionForSelect != null) && (conditionForSelect != ""))
            {
                string tmp = conditionForSelect.Trim();

                if (tmp.Length > 0)
                {

                    if (tmp.Length >= 4)
                    {
                        if (tmp.Substring(0, 4).ToLower() == "and ")
                        {
                            tmp = tmp.Remove(0, 4);
                        }
                    }
                    if (tmp.Length >= 9)
                    {
                        if (tmp.Substring(0, 9).ToLower() == "order by ")
                        {
                            tmp = tmp.Insert(0, " 1=1 ");
                        }
                    }
                }
                if (tmp.Length == 0) return null;

                return tmp;
            }
            return conditionForSelect;
        }
        private string GetColumnNames(params string[] ColumnNames)
        {
            if ((ColumnNames == null) || (ColumnNames.Length == 0))
            {
                return "*";
            }
            else
            {
                string str1 = "";
                foreach (string str in ColumnNames)
                {
                    str1 += str + ",";
                }
                if (str1.Length > 0)
                {
                    str1 = str1.TrimEnd(',');
                }
                return str1;
            }
        }
        #endregion
    }
}
