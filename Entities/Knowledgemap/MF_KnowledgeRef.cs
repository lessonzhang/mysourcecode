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
    [ORMDataTable("MF_KnowledgeRef")]
    public class MF_KnowledgeRef : Entity
    {
        #region Property
        private int _RefID;

        /// <summary>
        /// 字段名称 RefID
        /// </summary>
        public static string M_RefID = "RefID";

        private int _KnowledgeID;

        /// <summary>
        /// 字段名称 KnowledgeID
        /// </summary>
        public static string M_KnowledgeID = "KnowledgeID";

        private int _PreKnowledgeID;

        /// <summary>
        /// 字段名称 PreKnowledgeID
        /// </summary>
        public static string M_PreKnowledgeID = "PreKnowledgeID";

        private int _Course;

        /// <summary>
        /// 字段名称 Course
        /// </summary>
        public static string M_Course = "Course";
        /// <summary>
        /// RefID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_MF_KnowledgeRef_PK")]
        public int RefID
        {
            get
            {
                return this._RefID;
            }
            set
            {
                this._RefID = value;
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
        /// PreKnowledgeID
        /// </summary>
        [DataField(DbType.Int32)]
        public int PreKnowledgeID
        {
            get
            {
                return this._PreKnowledgeID;
            }
            set
            {
                this._PreKnowledgeID = value;
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
        #endregion
    }

    /// <summary>
    /// MF_KnowledgeRef 实体集合类
    /// </summary>
    [Serializable()]
    public class MF_KnowledgeRefSet : EntitySet<MF_KnowledgeRef>
    {
    }
}
