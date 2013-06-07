using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyFramework.Data;
using MyFramework.Data.ORM;
using MyFramework.Data.ORM.Attributes;

namespace Entities.Users
{
    /// <summary>
    /// MF_GroupMember 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_GroupMember")]
    public class MF_GroupMember : Entity
    {
        #region Property
        private int _GroupMemberID;
        /// <summary>
        /// 字段名称 GroupMemberID
        /// </summary>
        public static string M_GroupMemberID = "GroupMemberID";

        private int _GroupID;
        /// <summary>
        /// 字段名称 GroupID
        /// </summary>
        public static string M_GroupID = "GroupID";

        private int _UserID;
        /// <summary>
        /// 字段名称 UserID
        /// </summary>
        public static string M_UserID = "UserID";

        private string _UserName;
        /// <summary>
        /// 字段名称 UserName
        /// </summary>
        public static string M_UserName = "UserName";

        private int _MyID;
        /// <summary>
        /// 字段名称 MyID
        /// </summary>
        public static string M_MyID = "MyID";

        private string _MyName;
        /// <summary>
        /// 字段名称 MyName
        /// </summary>
        public static string M_MyName = "MyName";

        private int _Status;
        /// <summary>
        /// 字段名称 Status
        /// 1,正式好友
        /// 2,黑名单
        /// 3,已经邀请
        /// </summary>
        public static string M_Status = "Status";

        /// <summary>
        /// GroupMemberID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_GroupMember_PK")]
        public int GroupMemberID
        {
            get
            {
                return this._GroupMemberID;
            }
            set
            {
                this._GroupMemberID = value;
            }
        }

        /// <summary>
        /// GroupID
        /// </summary>
        [DataField(DbType.Int32)]
        public int GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                this._GroupID = value;
            }
        }

        /// <summary>
        /// UserID
        /// </summary>
        [DataField(DbType.Int32)]
        public int UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }

        /// <summary>
        /// UserName
        /// </summary>
        [DataField(DbType.String)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        /// <summary>
        /// MyID
        /// </summary>
        [DataField(DbType.Int32)]
        public int MyID
        {
            get
            {
                return this._MyID;
            }
            set
            {
                this._MyID = value;
            }
        }

        /// <summary>
        /// MyName
        /// </summary>
        [DataField(DbType.String)]
        public string MyName
        {
            get
            {
                return this._MyName;
            }
            set
            {
                this._MyName = value;
            }
        }

        /// <summary>
        /// Status
        /// </summary>
        [DataField(DbType.Int32)]
        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// MF_GroupMember 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_GroupMemberSet : EntitySet<MF_GroupMember>
    {
    }
}
