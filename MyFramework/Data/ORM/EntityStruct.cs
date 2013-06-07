using System;
using MyFramework.Data.ORM.Attributes;
using System.Collections.Generic;
using System.Data.Common;
using MyFramework.Utility.Serialization;
using MyFramework.Utility;
using MyFramework.Data.Transaction;
using MyFramework.Data.Query;
using System.Data;
using MySql.Data.MySqlClient;

namespace MyFramework.Data.ORM
{
    /// <summary>
    /// 实体结构
    /// </summary>
    [Serializable]
    internal class EntityStruct
    {
        #region Property

        private ORMDataTable _Table;
        /// <summary>
        /// 表特性实例
        /// </summary>
        public ORMDataTable Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        private string _TableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return Table.TableName; }

        }

        private string _DatabaseName;
        /// <summary>
        /// 数据库服务名称
        /// Configuration key for Database service
        /// </summary>
        public string DatabaseName
        {
            get { return Table.DataBaseName; }

        }

        private Dictionary<string, KeyField> _PKeyList = new Dictionary<string, KeyField>();
        /// <summary>
        /// 主键集合
        /// </summary>
        public Dictionary<string, KeyField> PKeyList
        {
            get { return _PKeyList; }
            set { _PKeyList = value; }
        }

        private Dictionary<string, KeyField> _FKeyList = new Dictionary<string, KeyField>();
        /// <summary>
        /// 外键集合
        /// </summary>
        public Dictionary<string, KeyField> FKeyList
        {
            get { return _FKeyList; }
            set { _FKeyList = value; }
        }

        private Dictionary<string, DataField> _FieldList = new Dictionary<string, DataField>();
        /// <summary>
        /// 字段集合
        /// </summary>
        public Dictionary<string, DataField> FieldList
        {
            get { return _FieldList; }
            set { _FieldList = value; }
        }

        #endregion

        #region DbCommand Generate Method
        /// <summary>
        /// 清除多余的AND
        /// </summary>
        /// <param name="conditionForSelect"></param>
        /// <returns></returns>
        private string Checkcondition(string condition)
        {
            if ((condition != null) && (condition != string.Empty))
            {
                string tmp = condition.Trim();

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
                            tmp = tmp.Insert(0, "1=1 ");
                        }
                    }
                }
                if (tmp.Length == 0) return null;
                return tmp;
            }
            return condition;
        }

        /// <summary>
        /// 生成Insert命令
        /// </summary>
        /// <param name="dh">DataHelper</param>
        /// <param name="IncremetSet">返回自动增长字段，用于Insert完毕后获取incremet的值</param>
        /// <param name="Propertys">指定特定字段插入</param>
        /// <returns>命令对象System.Data.Common.DbCommand</returns>
        private MySqlCommand GenerateInsertCommand(DataHelper dh, BaseEntity EntityInstance, ref  Dictionary<string, string> IncremetSet, params string[] Propertys)
        {
            MySqlCommand Command = null;
            #region 生成命令对象
            IncremetSet = new Dictionary<string, string>();
            string strSQL = string.Format("insert into {0}", this.TableName);
            string temp1 = "(", temp2 = " values(";

            #region 处理字段
            Dictionary<string, KeyField> m_PKeyList = null;
            Dictionary<string, KeyField> m_FKeyList = null;
            Dictionary<string, DataField> m_FieldList = null;
            if ((Propertys != null) && (Propertys.Length != 0))
            {
                #region 处理特定字段
                List<string> PropertyList = new List<string>(Propertys.Length);
                PropertyList.AddRange(Propertys);
                m_PKeyList = new Dictionary<string, KeyField>();
                m_FKeyList = new Dictionary<string, KeyField>();
                m_FieldList = new Dictionary<string, DataField>();
                foreach (KeyField Key in this.PKeyList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_PKeyList.Add(Key.FieldName, Key);
                    }
                }
                foreach (KeyField Key in this.FKeyList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_FKeyList.Add(Key.FieldName, Key);
                    }
                }
                foreach (DataField Key in this.FieldList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_FieldList.Add(Key.FieldName, Key);
                    }
                }
                #endregion
            }
            else
            {
                #region 默认全部字段
                m_PKeyList = this.PKeyList;
                m_FKeyList = this.FKeyList;
                m_FieldList = this.FieldList;
                #endregion
            }
            #endregion

            #region 生成SQL语句
            #region 处理主键
            //foreach (KeyField key in m_PKeyList.Values)
            //{
            //    if (key.IsBase) continue;
            //    if (key.Sequence != string.Empty)
            //    {
            //        temp1 += key.FieldName + ",";
            //        temp2 += key.Sequence + ".nextval,";
            //        IncremetSet.Add(key.FieldName, "select " + key.Sequence + ".currval from dual");
            //    }
            //}
            #endregion
            #region 处理其他字段
            foreach (KeyField key in m_FKeyList.Values)
            {
                if (key.IsBase) continue;
                temp1 += key.FieldName + ",";
                temp2 += dh.ParameterSeparateCode + key.FieldName + ",";
            }

            foreach (DataField key in m_FieldList.Values)
            {
                if (key.IsBase) continue;
                temp1 += key.FieldName + ",";
                temp2 += dh.ParameterSeparateCode + key.FieldName + ",";
            }
            temp1 = temp1.Substring(0, temp1.Length - 1);
            temp2 = temp2.Substring(0, temp2.Length - 1);

            strSQL += temp1 + ")" + temp2 + ")";

            #endregion
            #endregion

            Command = dh.GetSqlStringCommand(strSQL);

            #region 填充命令对象
            foreach (KeyField key in m_FKeyList.Values)
            {
                if (key.IsBase) continue;
                object Value = EntityInstance[key.FieldName];
                if (Value == null) Value = DBNull.Value;
                if (key.SerializedType != ESerializedType.NO)
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Serialization.Serialize(Value, key.SerializedType));
                }
                else
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Value);
                }
            }
            foreach (DataField key in m_FieldList.Values)
            {
                if (key.IsBase) continue;
                object Value = EntityInstance[key.FieldName];
                if (Value == null) Value = DBNull.Value;
                if (key.SerializedType != ESerializedType.NO)
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Serialization.Serialize(Value, key.SerializedType));
                }
                else
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Value);
                }
            }
            #endregion

            #endregion
            return Command;
        }

        /// <summary>
        /// 生成Update命令
        /// </summary>
        /// <param name="dh">DataHelper</param>
        /// <param name="conditionForUpdate">字定义条件</param>
        /// <param name="Propertys">自定义字段</param>
        /// <returns>命令对象System.Data.Common.DbCommand</returns>
        private MySqlCommand GenerateUpdateCommand(DataHelper dh, BaseEntity EntityInstance, string conditionForUpdate, params string[] Propertys)
        {
            MySqlCommand Command = null;
            #region 生成命令对象
            string strSQL = string.Format("update {0} ", TableName);
            strSQL += " set ";
            string strWhere = " where ";

            #region 处理字段
            Dictionary<string, KeyField> m_PKeyList = null;
            Dictionary<string, KeyField> m_FKeyList = null;
            Dictionary<string, DataField> m_FieldList = null;
            if ((Propertys != null) && (Propertys.Length != 0))
            {
                #region 处理特定字段
                List<string> PropertyList = new List<string>(Propertys.Length);
                PropertyList.AddRange(Propertys);
                m_PKeyList = new Dictionary<string, KeyField>();
                m_FKeyList = new Dictionary<string, KeyField>();
                m_FieldList = new Dictionary<string, DataField>();
                foreach (KeyField Key in this.PKeyList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_PKeyList.Add(Key.FieldName, Key);
                    }
                }
                foreach (KeyField Key in this.FKeyList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_FKeyList.Add(Key.FieldName, Key);
                    }
                }
                foreach (DataField Key in this.FieldList.Values)
                {
                    if (PropertyList.Contains(Key.FieldName))
                    {
                        m_FieldList.Add(Key.FieldName, Key);
                    }
                }
                #endregion
            }
            else
            {
                #region 默认全部字段
                m_PKeyList = this.PKeyList;
                m_FKeyList = this.FKeyList;
                m_FieldList = this.FieldList;
                #endregion
            }
            #endregion

            #region 生成SQL语句
            foreach (KeyField key in m_FKeyList.Values)
            {
                if (key.IsBase) continue;
                strSQL += key.FieldName + "=" + dh.ParameterSeparateCode + key.FieldName + ",";
            }
            foreach (DataField key in m_FieldList.Values)
            {
                if (key.IsBase) continue;
                strSQL += key.FieldName + "=" + dh.ParameterSeparateCode + key.FieldName + ",";
            }
            if ((conditionForUpdate == null) || (conditionForUpdate.Trim().Length == 0))
            {
                foreach (KeyField key in m_PKeyList.Values)
                {
                    if (key.IsBase) continue;
                    strWhere += key.FieldName + "=" + dh.ParameterSeparateCode + key.FieldName + " and ";
                }

                if (strWhere != " where ")
                {
                    strWhere = strWhere.Substring(0, strWhere.Length - 4);
                }
                else
                {
                    strWhere = string.Empty;
                }
            }
            else
            {
                conditionForUpdate = Checkcondition(conditionForUpdate);
                strWhere += conditionForUpdate;
            }

            strSQL = strSQL.Substring(0, strSQL.Length - 1);
            strSQL += strWhere;
            #endregion

            Command = dh.GetSqlStringCommand(strSQL);

            #region 填充命令对象
            if ((conditionForUpdate == null) || (conditionForUpdate.ToUpper().Length == 0))
            {
                foreach (KeyField key in m_PKeyList.Values)
                {
                    if (key.IsBase) continue;
                    object Value = EntityInstance[key.FieldName];
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Value);
                }
            }

            foreach (KeyField key in m_FKeyList.Values)
            {
                if (key.IsBase) continue;
                object Value = EntityInstance[key.FieldName];
                if (Value == null) Value = DBNull.Value;
                if (key.SerializedType != ESerializedType.NO)
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Serialization.Serialize(Value, key.SerializedType));
                }
                else
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Value);
                }
            }
            foreach (DataField key in m_FieldList.Values)
            {
                if (key.IsBase) continue;
                object Value = EntityInstance[key.FieldName];
                if (Value == null) Value = DBNull.Value;
                if (key.SerializedType != ESerializedType.NO)
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Serialization.Serialize(Value, key.SerializedType));
                }
                else
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, Value);
                }
            }
            #endregion

            #endregion
            return Command;
        }

        /// <summary>
        /// 生成Delete命令
        /// </summary>
        /// <param name="dh">DataHelper</param>
        /// <param name="conditionForDelete">字定义条件</param>
        /// <returns>命令对象System.Data.Common.DbCommand</returns>
        private MySqlCommand GenerateDeleteCommand(DataHelper dh, BaseEntity EntityInstance, string conditionForDelete)
        {
            MySqlCommand Command = null;
            #region 生成命令对象

            string strSQL = string.Format("delete from {0} ", TableName);
            #region 生成SQL语句
            if ((conditionForDelete != null) && (conditionForDelete.Trim().Length != 0))
            {
                conditionForDelete = Checkcondition(conditionForDelete);
                strSQL += " Where " + conditionForDelete;

            }
            else
            {
                strSQL += " Where ";
                foreach (KeyField key in this.PKeyList.Values)
                {
                    strSQL += key.FieldName + "=" + dh.ParameterSeparateCode + key.FieldName + " and ";
                }
                strSQL = strSQL.Substring(0, strSQL.Length - 4);
            }
            #endregion


            Command = dh.GetSqlStringCommand(strSQL);

            #region 填充Command
            if ((conditionForDelete == null) || (conditionForDelete.ToUpper().Length == 0))
            {
                foreach (KeyField key in this.PKeyList.Values)
                {
                    dh.AddInParameter(Command, dh.ParameterSeparateCode + key.FieldName, key.DBType, EntityInstance[key.FieldName]);

                }
            }

            #endregion
            #endregion
            return Command;
        }

        /// <summary>
        /// 生成CheckExist命令
        /// </summary>
        /// <param name="dh">DataHelper</param>
        /// <param name="FiledName">字段名称</param>
        /// <param name="FieldValue">值</param>
        /// <param name="conditionForExist">自定义条件</param>
        /// <returns></returns>
        public DbCommand GetCheckExistCommand(DataHelper dh, string FiledName, object FieldValue)
        {
            DbCommand Command = null;
            string P2 = "{2}";
            switch (FieldValue.GetType().ToString())
            {
                case "System.String":
                    FiledName = "upper(" + FiledName + ")";
                    P2 = "upper('{2}')";
                    FieldValue = FieldValue.ToString().Replace("'", "''");
                    break;
            }

            string strSQL = string.Format("select count(*) from {0} where {1}=" + P2, this.TableName, FiledName, FieldValue.ToString());

            Command = dh.GetSqlStringCommand(strSQL);

            return Command;
        }

        public DbCommand GetCheckExistCommand(DataHelper dh, string conditionForExist)
        {
            DbCommand Command = null;

            string strSQL = string.Format("select count(*) from {0} where {1}", this.TableName, (conditionForExist == null || conditionForExist == string.Empty) ? "1=1" : conditionForExist);

            Command = dh.GetSqlStringCommand(strSQL);

            return Command;
        }


        #endregion

        #region Do method
        /// <summary>
        /// 执行Insert命令
        /// </summary>
        /// <param name="EntityInstance">实体对象实例</param>
        /// <param name="Propertys">自定义字段</param>
        /// <returns>受影响的行数</returns>
        public int DoInsert(BaseEntity EntityInstance, params string[] Propertys)
        {
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                Dictionary<string, string> IncremetSet = null;
                MySqlCommand Command = this.GenerateInsertCommand(dh, EntityInstance, ref  IncremetSet, Propertys);

                int result = 0;
                //using (TransactionScope TS = new TransactionScope())
                //{
                    result = dh.ExecuteNonQuery(Command);

                    foreach (string key in IncremetSet.Keys)
                    {
                       EntityInstance[key] = dh.ExecuteScalar(dh.GetSqlStringCommand(IncremetSet[key]));
                    }
                    //TS.Complete();
                //}
                return result;
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        /// <summary>
        /// 执行Update命令
        /// </summary>
        /// <param name="EntityInstance">实体对象实例</param>
        /// <param name="conditionForUpdate">自定义条件</param>
        /// <param name="Propertys">自定义字段</param>
        /// <returns>受影响的行数</returns>
        public int DoUpdate(BaseEntity EntityInstance, string conditionForUpdate, params string[] Propertys)
        {
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                MySqlCommand Command = this.GenerateUpdateCommand(dh, EntityInstance, conditionForUpdate, Propertys);
                return dh.ExecuteNonQuery(Command);
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        /// <summary>
        /// 执行Update命令
        /// </summary>
        /// <param name="EntityInstance">实体对象实例</param>
        /// <param name="conditionForUpdate">SQLCondition实例</param>
        /// <param name="Propertys">自定义字段</param>
        /// <returns>受影响的行数</returns>
        public int DoUpdate(BaseEntity EntityInstance, SQLCondition conditionForUpdate, params string[] Propertys)
        {
            if (conditionForUpdate == null)
            {
                return DoUpdate(EntityInstance, string.Empty, Propertys);
            }
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                MySqlCommand Command = this.GenerateUpdateCommand(dh, EntityInstance, conditionForUpdate.ToString(dh), Propertys);
                return dh.ExecuteNonQuery(Command);
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }

        /// <summary>
        /// 执行Delete命令
        /// </summary>
        /// <param name="EntityInstance">实体对象实例</param>
        /// <param name="conditionForUpdate">自定义条件</param>
        /// <returns>受影响的行数</returns>
        public int DoDelete(BaseEntity EntityInstance, string conditionForUpdate)
        {

            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                MySqlCommand Command = this.GenerateDeleteCommand(dh, EntityInstance, conditionForUpdate);
                return dh.ExecuteNonQuery(Command);
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        /// <summary>
        /// 执行Delete命令
        /// </summary>
        /// <param name="EntityInstance">实体对象实例</param>
        /// <param name="conditionForUpdate">SQLCondition实例</param>
        /// <returns>受影响的行数</returns>
        public int DoDelete(BaseEntity EntityInstance, SQLCondition conditionForUpdate)
        {
            if (conditionForUpdate == null)
            {
                return DoDelete(EntityInstance, string.Empty);
            }
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                MySqlCommand Command = this.GenerateDeleteCommand(dh, EntityInstance, conditionForUpdate.ToString(dh));
                return dh.ExecuteNonQuery(Command);
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }

        /// <summary>
        /// 执行CheckExist命令
        /// </summary>
        /// <param name="FiledName">字段名称</param>
        /// <param name="FileValue">值</param>
        /// <param name="conditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public int DoCheckExist(string conditionForCheckExist)
        {
            try
            {
                DataHelper dh = new DataHelper();
                DbCommand Command = this.GetCheckExistCommand(dh, conditionForCheckExist);
                return Convert.ToInt32(dh.ExecuteScalar(Command.CommandText));
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        /// <summary>
        /// 执行CheckExist命令
        /// </summary>
        /// <param name="FiledName">字段名称</param>
        /// <param name="FileValue">值</param>
        /// <param name="conditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public int DoCheckExist(SQLCondition conditionForCheckExist)
        {
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                DbCommand Command = this.GetCheckExistCommand(dh, conditionForCheckExist.ToString(dh));
                return Convert.ToInt32(dh.ExecuteScalar(Command));
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        /// <summary>
        /// 执行CheckExist命令
        /// </summary>
        /// <param name="FiledName">字段名称</param>
        /// <param name="FileValue">值</param>
        /// <param name="conditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public int DoCheckExist(string FiledName, object FileValue)
        {
            try
            {
                DataHelper dh = new DataHelper(this.DatabaseName);
                DbCommand Command = this.GetCheckExistCommand(dh, FiledName, FileValue);
                return Convert.ToInt32(dh.ExecuteScalar(Command));
            }
            catch (Exception exp)
            {
                //Logging.SaveLog(ELogType.ORM, exp, string.Empty);
                throw exp;
            }
        }
        public bool DoFillSelf(BaseEntity instance, SQLCondition Condition)
        {
            SQLQuery sq = new SQLQuery(this.DatabaseName);
            DataTable dt = sq.GetDTQuery(this.TableName, Condition);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                foreach (DataColumn Column in dt.Columns)
                {
                    string Key = Column.ColumnName.ToUpper();
                    if (dr[Key] == DBNull.Value)
                    {
                        continue;
                    }
                    #region 处理字段
                    Field Field = null;
                    if (this.PKeyList.ContainsKey(Key))
                    {
                        Field = this.PKeyList[Key];
                    }
                    else if (this.FKeyList.ContainsKey(Key))
                    {
                        Field = this.FKeyList[Key];
                    }
                    else if (this.FieldList.ContainsKey(Key))
                    {
                        Field = this.FieldList[Key];
                    }
                    #endregion

                    #region 赋值
                    if (Field != null)
                    {

                        if (Field.SerializedType == ESerializedType.NO)
                        {
                            if (Field.FieldType.IsEnum)
                            {
                                instance[Field.PropertyName] = Convert.ToInt32(dr[Key]);
                            }
                            else if (Field.FieldType.ToString() == "System.Boolean")
                            {
                                instance[Field.PropertyName] = Convert.ToBoolean(dr[Key]);
                            }
                            else
                            {
                                instance[Field.PropertyName] = dr[Key];
                            }
                        }
                        else
                        {
                            instance[Field.PropertyName] = Serialization.Deserialize(dr[Key], Field.FieldType, Field.SerializedType);
                        }
                    }
                    else
                    {
                        instance[Key] = dr[Key];
                    }
                    #endregion
                }
                instance.IsCreated = true;
                return true;
            }
            return false;
        }
        public bool DoFillSelf(BaseEntity instance, string Condition, params string[] FieldNames)
        {
            if (FieldNames.Length == 0) FieldNames = null;
            SQLQuery sq = new SQLQuery(this.DatabaseName);
            DataTable dt = sq.GetDTQuery(this.TableName, Condition);
            List<string> Set = new List<string>();
            if (FieldNames != null)
            {
                Set.AddRange(FieldNames);
            }
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                foreach (DataColumn Column in dt.Columns)
                {
                    string Key = Column.ColumnName.ToUpper();
                    if (dr[Key] == DBNull.Value)
                    {
                        continue;
                    }
                    #region 处理字段
                    Field Field = null;
                    if (this.PKeyList.ContainsKey(Key))
                    {
                        Field = this.PKeyList[Key];
                    }
                    else if (this.FKeyList.ContainsKey(Key))
                    {
                        Field = this.FKeyList[Key];
                    }
                    else if (this.FieldList.ContainsKey(Key))
                    {
                        Field = this.FieldList[Key];
                    }
                    #endregion

                    #region 赋值
                    if (FieldNames != null)
                    {
                        if (!Set.Contains(Key))
                        {
                            continue;
                        }
                    }
                    if (Field != null)
                    {

                        if (Field.SerializedType == ESerializedType.NO)
                        {
                            if (Field.FieldType.IsEnum)
                            {
                                instance[Field.PropertyName] = Convert.ToInt32(dr[Key]);
                            }
                            else if (Field.FieldType.ToString() == "System.Boolean")
                            {
                                instance[Field.PropertyName] = Convert.ToBoolean(dr[Key]);
                            }
                            else if (Field.FieldType.ToString() == "System.DateTime")
                            {
                                instance[Field.PropertyName] = DateTime.Parse(dr[Key].ToString());
                            }
                            else
                            {
                                instance[Field.PropertyName] = dr[Key];
                            }
                        }
                        else
                        {
                            instance[Field.PropertyName] = Serialization.Deserialize(dr[Key], Field.FieldType, Field.SerializedType);
                        }
                    }
                    else
                    {
                        instance[Key] = dr[Key];
                    }

                    #endregion
                }
                instance.IsCreated = true;

                return true;
            }
            return false;
        }
        public bool DoFillSelf<T>(BaseEntity Entity, T ID, params string[] FieldNames)
        {
            string KeyName = string.Empty;
            foreach (string tmp in this.PKeyList.Keys)
            {
                KeyName = tmp;
                break;
            }
            return DoFillSelf(Entity, KeyName + "=" + ID.ToString(), FieldNames);
        }
        #endregion
    }
}
