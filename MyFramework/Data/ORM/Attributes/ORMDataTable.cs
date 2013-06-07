using System;

namespace MyFramework.Data.ORM.Attributes
{
    /// <summary>
    /// 数据库表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    [Serializable]
    public class ORMDataTable : Attribute
    {
        #region Property
        private string _TableName;
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        private string _DataBaseName = "mfdatabase";
        /// <summary>
        /// 数据库服务名称
        /// Configuration key for Database service
        /// </summary>
        public string DataBaseName
        {
            get { return _DataBaseName; }
            set { _DataBaseName = value; }
        }

        private Type _BaseType = null;
        /// <summary>
        /// 基类类型
        /// </summary>
        public Type BaseType
        {
            get { return _BaseType; }
            set { _BaseType = value; }
        }

        #endregion

        #region Constructor
        public ORMDataTable(string TableName)
        {
            this.TableName = TableName;
        }
        public ORMDataTable(string TableName, Type BaseType)
        {
            this.TableName = TableName;
            this.BaseType = BaseType;
        }
        public ORMDataTable(string TableName, string DataBaseName)
        {
            this.DataBaseName = DataBaseName;
            this.TableName = TableName;
        }
        public ORMDataTable(string TableName, string DataBaseName, Type BaseType)
        {
            this.DataBaseName = DataBaseName;
            this.TableName = TableName;
            this.BaseType = BaseType;
        }

        #endregion
    }
}
