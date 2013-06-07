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
    /// MF_Action 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_Action")]
    public class MF_Action : Entity
    {
        #region Property
        private int _ActionID;

        /// <summary>
        /// 字段名称 ActionID
        /// </summary>
        public static string M_ActionID = "ActionID";

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

        private DateTime _ActionDate;

        /// <summary>
        /// 字段名称 ActionDate
        /// </summary>
        public static string M_ActionDate = "ActionDate";

        private DateTime _ActionTime;

        /// <summary>
        /// 字段名称 ActionTime
        /// </summary>
        public static string M_ActionTime = "ActionTime";

        private int _ActionType;

        /// <summary>
        /// 字段名称 ActionType
        /// </summary>
        public static string M_ActionType = "ActionType";

        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "KnowledgeID";

        private string _ActionContent;

        /// <summary>
        /// 字段名称 ActionContent
        /// </summary>
        public static string M_ActionContent = "ActionContent";

        private string _ActionContentID;

        /// <summary>
        /// 字段名称 ActionContentID
        /// </summary>
        public static string M_ActionContentID = "ActionContentID";

        /// <summary>
        /// ActionID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Action_PK")]
        public int ActionID
        {
            get
            {
                return this._ActionID;
            }
            set
            {
                this._ActionID = value;
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
        /// ActionDate
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime ActionDate
        {
            get
            {
                return this._ActionDate;
            }
            set
            {
                this._ActionDate = value;
            }
        }

        /// <summary>
        /// ActionTime
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime ActionTime
        {
            get
            {
                return this._ActionTime;
            }
            set
            {
                this._ActionTime = value;
            }
        }

        /// <summary>
        /// ActionType
        /// </summary>
        [DataField(DbType.Int32)]
        public int ActionType
        {
            get
            {
                return this._ActionType;
            }
            set
            {
                this._ActionType = value;
            }
        }

        /// <summary>
        /// KnowledgeID
        /// </summary>
        [DataField(DbType.Int32)]
        public int KnowledgeID
        {
            get
            {
                return this._KnowledgeID;
            }
            set
            {
                this._KnowledgeID = value;
            }
        }

        /// <summary>
        /// ActionContent
        /// </summary>
        [DataField(DbType.String)]
        public string ActionContent
        {
            get
            {
                return this._ActionContent;
            }
            set
            {
                this._ActionContent = value;
            }
        }

        /// <summary>
        /// ActionContentID
        /// </summary>
        [DataField(DbType.String)]
        public string ActionContentID
        {
            get
            {
                return this._ActionContentID;
            }
            set
            {
                this._ActionContentID = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// MF_Action 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_ActionSet : EntitySet<MF_Action>
    {
    }
}
