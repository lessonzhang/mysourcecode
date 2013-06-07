using System;
using System.Data;


namespace MyFramework.Data.ORM.Attributes
{
    public enum EKeyType
    {
        /// <summary>
        /// 主键
        /// </summary>
        PRIMARY,
        /// <summary>
        /// 外键
        /// </summary>
        FOREIGN
    }

    /// <summary>
    /// 键字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Serializable]
    public class KeyField : Field
    {
        #region Property
        private EKeyType _KeyType;
        /// <summary>
        /// 键类型
        /// </summary>
        public EKeyType KeyType
        {
            get { return _KeyType; }
            set { _KeyType = value; }
        }

        private bool _IsIncrement;
        /// <summary>
        /// 是否自动增长字段
        /// </summary>
        public bool IsIncrement
        {
            get { return _IsIncrement; }
            set { _IsIncrement = value; }
        }

        private string _Sequence = string.Empty;
        /// <summary>
        /// 序列名
        /// </summary>
        public string Sequence
        {
            get { return _Sequence; }
            set { _Sequence = value; }
        }

        private string _ForeignMappingEntity = string.Empty;
        /// <summary>
        /// 外键隐射的实体类
        /// </summary>
        public string ForeignMappingEntity
        {
            get
            {
                return _ForeignMappingEntity;
            }
            set { _ForeignMappingEntity = value; }
        }
        #endregion

        #region Constructor
        public KeyField() { }


        public KeyField(string FieldName, DbType DBType, bool IsIncrement, string Sequence, string ForeignMappingEntity, EKeyType KeyType)
            : base(FieldName, DBType, ESerializedType.NO)
        {
            this.IsIncrement = IsIncrement;
            this.Sequence = Sequence;
            this.ForeignMappingEntity = ForeignMappingEntity;
            this.KeyType = KeyType;
        }

        public KeyField(DbType DBType, bool IsIncrement, string Sequence, EKeyType KeyType)
            : base(string.Empty, DBType, ESerializedType.NO)
        {
            this.IsIncrement = IsIncrement;
            this.Sequence = Sequence;
            this.KeyType = KeyType;
        }
        public KeyField(DbType DBType, string Sequence)
            : base(string.Empty, DBType, ESerializedType.NO)
        {
            this.IsIncrement = true;
            this.Sequence = Sequence;
            this.KeyType = EKeyType.PRIMARY;
        }

        public KeyField(DbType DBType)
            : base(string.Empty, DBType, ESerializedType.NO)
        {
            this.IsIncrement = true;
            this.Sequence = string.Empty;
            this.KeyType = EKeyType.PRIMARY;
        }


        public KeyField(string ForeignMappingEntity, DbType DBType)
            : base(string.Empty, DBType, ESerializedType.NO)
        {
            this.IsIncrement = false;
            this.KeyType = EKeyType.FOREIGN;
            this.ForeignMappingEntity = ForeignMappingEntity;
        }

        #endregion
    }
}
