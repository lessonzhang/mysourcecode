using System;
using System.Data;


namespace MyFramework.Data.ORM.Attributes
{
    /// <summary>
    /// 数据字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Serializable]
    public class DataField : Field
    {
        #region Constructor
        public DataField() { }

        public DataField(string FieldName, DbType DBType, ESerializedType SerializedType) : base(FieldName, DBType, SerializedType) { }

        public DataField(DbType DBType, ESerializedType SerializedType) : base(string.Empty, DBType, SerializedType) { }

        public DataField(DbType DBType) : base(string.Empty, DBType, ESerializedType.NO) { }
        #endregion
    }
}
