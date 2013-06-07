using System;
using System.Data;


namespace MyFramework.Data.ORM.Attributes
{


    /// <summary>
    /// 字段特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Serializable]
    public class Field : Attribute
    {
        #region Property
        private string _FieldName = string.Empty;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        private DbType _DBType;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DBType
        {
            get { return _DBType; }
            set { _DBType = value; }
        }

        private Type _FieldType;
        /// <summary>
        /// 系统类型
        /// </summary>
        public Type FieldType
        {
            get { return _FieldType; }
            set { _FieldType = value; }
        }

        private ESerializedType _SerializedType;
        /// <summary>
        /// 序列化类型
        /// </summary>
        public ESerializedType SerializedType
        {
            get { return _SerializedType; }
            set { _SerializedType = value; }
        }

        private string _PropertyName;
        /// <summary>
        /// 隐射属性名称
        /// </summary>
        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }


        private bool _IsBase = false;
        /// <summary>
        /// 是否是继承字段
        /// </summary>
        public bool IsBase
        {
            get { return _IsBase; }
            set { _IsBase = value; }
        }

        private object _Value;
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        #endregion

        #region Constructor
        public Field() { }
        public Field(string PropertyName, object Value)
        {
            this.PropertyName = PropertyName;
            this.Value = Value;
            this.FieldName = PropertyName;
        }
        public Field(string FieldName, DbType DBType, ESerializedType SerializedType)
        {
            this.FieldName = FieldName;
            this.DBType = DBType;
            this.SerializedType = SerializedType;
        }
        #endregion
    }
}
