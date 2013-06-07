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
    /// MF_Group 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_Group")]
    public class MF_Group : Entity
    {
        #region Property
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

        private string _GroupName;

        /// <summary>
        /// 字段名称 GroupName
        /// </summary>
        public static string M_GroupName = "GroupName";

        /// <summary>
        /// Group
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Group_PK")]
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
        /// GroupName
        /// </summary>
        [DataField(DbType.String)]
        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                this._GroupName = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// MF_Group 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_GroupSet : EntitySet<MF_Group>
    {
    }
}
