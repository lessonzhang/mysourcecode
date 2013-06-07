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
    /// MF_Favorite 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_Favorite")]
    public class MF_Favorite : Entity
    {
        #region Property
        private int _FavoriteID;

        /// <summary>
        /// 字段名称 FavoriteID
        /// </summary>
        public static string M_FavoriteID = "FavoriteID";

        private string _URL;

        /// <summary>
        /// 字段名称 URL
        /// </summary>
        public static string M_URL = "URL";

        private string _Title;

        /// <summary>
        /// 字段名称 Title
        /// </summary>
        public static string M_Title = "Title";

        private string _Description;

        /// <summary>
        /// 字段名称 Name
        /// </summary>
        public static string M_Description = "Description";

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

        /// <summary>
        /// UserID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Favorite_PK")]
        public int FavoriteID
        {
            get
            {
                return this._FavoriteID;
            }
            set
            {
                this._FavoriteID = value;
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
        /// Note
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

        #endregion
    }

    /// <summary>
    /// MF_Favorite 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_FavoriteSet : EntitySet<MF_Favorite>
    {
    }
}
