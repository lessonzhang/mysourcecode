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
    /// MF_User 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("MF_User")]
    public class MF_User : Entity
    {
        #region Property
        private int _UserID;

        /// <summary>
        /// 字段名称 UserID
        /// </summary>
        public static string M_UserID = "UserID";

        private string _Username;

        /// <summary>
        /// 字段名称 Username
        /// </summary>
        public static string M_Username = "Username";

        private string _Password;

        /// <summary>
        /// 字段名称 Password
        /// </summary>
        public static string M_Password = "Password";

        private string _Name;

        /// <summary>
        /// 字段名称 Name
        /// </summary>
        public static string M_Name = "Name";

        private string _Email = string.Empty;

        /// <summary>
        /// 字段名称 Email
        /// </summary>
        public static string M_Email = "Email";

        private string _Note = string.Empty;

        /// <summary>
        /// 字段名称 Note
        /// </summary>
        public static string M_Note = "Note";

        private int _Grade;

        /// <summary>
        /// 字段名称 Grade
        /// </summary>
        public static string M_Grade = "Grade";

        private bool _Stauts = true;

        /// <summary>
        /// 字段名称 Stauts
        /// </summary>
        public static string M_Stauts = "Stauts";

        private DateTime _LastUpdate;

        /// <summary>
        /// 字段名称 LastUpdate
        /// </summary>
        public static string M_LastUpdate = "LastUpdate";

        /// <summary>
        /// UserID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_User_PK")]
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
        /// Username
        /// </summary>
        [DataField(DbType.String)]
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        [DataField(DbType.String)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        [DataField(DbType.String)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        [DataField(DbType.String)]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        /// <summary>
        /// Note
        /// </summary>
        [DataField(DbType.String)]
        public string Note
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
        /// Grade
        /// </summary>
        [DataField(DbType.Int32)]
        public int Grade
        {
            get
            {
                return this._Grade;
            }
            set
            {
                this._Grade = value;
            }
        }

        /// <summary>
        /// Stauts
        /// </summary>
        [DataField(DbType.Int32)]
        public bool Stauts
        {
            get
            {
                return this._Stauts;
            }
            set
            {
                this._Stauts = value;
            }
        }

        /// <summary>
        /// LastUpdate
        /// </summary>
        [DataField(DbType.DateTime)]
        public DateTime LastUpdate
        {
            get
            {
                return this._LastUpdate;
            }
            set
            {
                this._LastUpdate = value;
            }
        }

        #endregion

        public bool Login(string UserName, string Password)
        {
            bool IsAllow = this.FillSelf(MF_User.M_Username + "='" + UserName + "' and " + MF_User.M_Password + "='" + Password + "'");
            return IsAllow;
        }
    }

    /// <summary>
    /// MF_User 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_UserSet : EntitySet<MF_User>
    {
    }
}
