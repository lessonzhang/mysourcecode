using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyFramework.Data;
using MyFramework.Data.ORM;
using MyFramework.Data.ORM.Attributes;

namespace Entities.Knowledgemap
{
    /// <summary>
    /// MF_User 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_Video")]
    public class MF_Video : Entity
    {
        #region Property
        private int _VideoID;

        /// <summary>
        /// 字段名称 VideoID
        /// </summary>
        public static string M_VideoID = "VideoID";

        private string _URL;

        /// <summary>
        /// 字段名称 URL
        /// </summary>
        public static string M_URL = "URL";

        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "KnowledgeID";

        private int _Count;

        /// <summary>
        /// 字段名称 Count
        /// </summary>
        public static string M_Count = "Count";

        private string _Title;

        /// <summary>
        /// 字段名称 Title
        /// </summary>
        public static string M_Title = "Title";

        private string _Description;

        /// <summary>
        /// 字段名称 Description
        /// </summary>
        public static string M_Description = "Description";

        /// <summary>
        /// VideoID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Video_PK")]
        public int VideoID
        {
            get
            {
                return this._VideoID;
            }
            set
            {
                this._VideoID = value;
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
        /// Count
        /// </summary>
        [DataField(DbType.Int32)]
        public int Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
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
        /// Description
        /// </summary>
        [DataField(DbType.String)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// MF_Video 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_VideoSet : EntitySet<MF_Video>
    {
    }
}
