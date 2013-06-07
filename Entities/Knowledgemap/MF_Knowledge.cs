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
    [ORMDataTable("MF_Knowledge")]
    public class MF_Knowledge : Entity
    {
        #region Property
        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "knowledgeID";

        private string _KnowledgeName;

        /// <summary>
        /// 字段名称 KnowledgeName
        /// </summary>
        public static string M_KnowledgeName = "KnowledgeName";

        private int _Course;

        /// <summary>
        /// 字段名称 Course
        /// </summary>
        public static string M_Course = "Course";

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


        /// <summary>
        /// KnowledgeID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_Knowledge_PK")]
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
        /// KnowledgeName
        /// </summary>
        [DataField(DbType.String)]
        public string KnowledgeName
        {
            get
            {
                return this._KnowledgeName;
            }
            set
            {
                this._KnowledgeName = value;
            }
        }

        /// <summary>
        /// Course
        /// </summary>
        [DataField(DbType.Int32)]
        public int Course
        {
            get
            {
                return this._Course;
            }
            set
            {
                this._Course = value;
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

        #endregion
    }

    /// <summary>
    /// MF_Knowledge 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_KnowledgeSet : EntitySet<MF_Knowledge>
    {
    }
}
