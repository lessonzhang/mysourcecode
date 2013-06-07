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
    /// mf_BlackList 实体
    /// </summary>
    [Serializable()]
    [ORMDataTable("mf_BlackList")]
    public class MF_BlackList:Entity
    {
        #region Property
        private int _BLID;

        /// <summary>
        /// 字段名称 BLID
        /// </summary>
        public static string M_BLID = "BLID";

        private string _URL;

        /// <summary>
        /// 字段名称 URL
        /// </summary>
        public static string M_URL = "URL";

        /// <summary>
        /// BLID
        /// </summary>
        [KeyField(DbType.Int32, "SEQ_mf_BlackList_PK")]
        public int BLID
        {
            get
            {
                return this._BLID;
            }
            set
            {
                this._BLID = value;
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
        #endregion
    }

    /// <summary>
    /// mf_BlackList 实体集合类
    /// </summary>
    [Serializable()]
    public class mf_BlackListSet : EntitySet<MF_BlackList>
    {
    }
}
