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
    /// MF_Note 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_Note")]
    public class MF_Note : Entity
    {
        #region Property
        private int _NoteID;

        /// <summary>
        /// 字段名称 NoteID
        /// </summary>
        public static string M_NoteID = "NoteID";

        private int _UserID;

        /// <summary>
        /// 字段名称 UserID
        /// </summary>
        public static string M_UserID = "UserID";

        private int _Type;

        /// <summary>
        /// 字段名称 Type
        /// </summary>
        public static string M_Type = "Type";

        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "KnowledgeID";

        private string _Title;

        /// <summary>
        /// 字段名称 Title
        /// </summary>
        public static string M_Title = "Title";

        private byte[] _Note;

        /// <summary>
        /// 字段名称 Note
        /// </summary>
        public static string M_Note = "Note";

        private byte[] _Text;

        /// <summary>
        /// 字段名称 Text
        /// </summary>
        public static string M_Text = "Text";

        private DateTime _NoteDate;

        /// <summary>
        /// 字段名称 NoteDate
        /// </summary>
        public static string M_NoteDate = "NoteDate";

        private DateTime _NoteTime;

        /// <summary>
        /// 字段名称 NoteTime
        /// </summary>
        public static string M_NoteTime = "NoteTime";

        /// <summary>
        /// NoteID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Note_PK")]
        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
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
        /// Type
        /// </summary>
        [DataField(DbType.Int32)]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
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
        /// Title
        /// </summary>
        [DataField(DbType.String)]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        /// <summary>
        /// NoteDate
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime NoteDate
        {
            get
            {
                return this._NoteDate;
            }
            set
            {
                this._NoteDate = value;
            }
        }

        /// <summary>
        /// NoteTime
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime NoteTime
        {
            get
            {
                return this._NoteTime;
            }
            set
            {
                this._NoteTime = value;
            }
        }

        /// <summary>
        /// Note
        /// </summary>
        [DataField(DbType.Object)]
        public byte[] Note
        {
            get
            {
                return this._Note;
            }
            set
            {
                this._Note = value;
            }
        }

        /// <summary>
        /// Text
        /// </summary>
        [DataField(DbType.Object)]
        public byte[] Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// MF_Note 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_NoteSet : EntitySet<MF_Note>
    {
    }
}
