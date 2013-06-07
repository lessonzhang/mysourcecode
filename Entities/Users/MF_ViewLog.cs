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
    /// MF_ViewLog 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_ViewLog")]
    public class MF_ViewLog : Entity
    {
        #region Property
        private int _ViewLogID;

        /// <summary>
        /// 字段名称 ViewLogID
        /// </summary>
        public static string M_ViewLogID = "ViewLogID";

        private int _UserID;

        /// <summary>
        /// 字段名称 UserID
        /// </summary>
        public static string M_UserID = "UserID";

        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "KnowledgeID";

        private string _SessionID;

        /// <summary>
        /// 字段名称 SessionID
        /// </summary>
        public static string M_SessionID = "SessionID";

        private int _URLID;

        /// <summary>
        /// 字段名称 URLID
        /// </summary>
        public static string M_URLID = "URLID";

        private string _URL;

        /// <summary>
        /// 字段名称 URL
        /// </summary>
        public static string M_URL = "URL";

        private DateTime _ViewDate;

        /// <summary>
        /// 字段名称 ViewDate
        /// </summary>
        public static string M_ViewDate = "ViewDate";

        private DateTime _ViewTime;

        /// <summary>
        /// 字段名称 ViewTime
        /// </summary>
        public static string M_ViewTime = "ViewTime";

        private int _TotalTime;

        /// <summary>
        /// 字段名称 TotalTime
        /// </summary>
        public static string M_TotalTime = "TotalTime";

        /// <summary>
        /// ViewLogID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_ViewLog_PK")]
        public int ViewLogID
        {
            get
            {
                return this._ViewLogID;
            }
            set
            {
                this._ViewLogID = value;
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
        /// SessionID
        /// </summary>
        [DataField(DbType.String)]
        public string SessionID
        {
            get
            {
                return this._SessionID;
            }
            set
            {
                this._SessionID = value;
            }
        }

        /// <summary>
        /// URLID
        /// </summary>
        [DataField(DbType.Int32)]
        public int URLID
        {
            get
            {
                return this._URLID;
            }
            set
            {
                this._URLID = value;
            }
        }

        /// <summary>
        /// URL
        /// </summary>
        [DataField(DbType.String)]
        public string URL
        {
            get
            {
                return this._URL;
            }
            set
            {
                this._URL = value;
            }
        }

        /// <summary>
        /// ViewDate
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime ViewDate
        {
            get
            {
                return this._ViewDate;
            }
            set
            {
                this._ViewDate = value;
            }
        }

        /// <summary>
        /// ViewTime
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime ViewTime
        {
            get
            {
                return this._ViewTime;
            }
            set
            {
                this._ViewTime = value;
            }
        }

        /// <summary>
        /// TotalTime
        /// </summary>
        [DataField(DbType.Int32)]
        public int TotalTime
        {
            get
            {
                return this._TotalTime;
            }
            set
            {
                this._TotalTime = value;
            }
        }
    }
    #endregion
    /// <summary>
    /// MF_ViewLog 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_ViewLogSet : EntitySet<MF_ViewLog>
    {
    }
}