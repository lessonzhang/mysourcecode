using System;
using MyFramework.Data.ORM;
using MyFramework.Utility;
using System.Data;
using System.Collections.Generic;
using MyFramework.Data.ORM.Attributes;
using MyFramework.Utility.Serialization;


namespace MyFramework.Data.Query
{
    public enum EResultPolicy
    {
        /// <summary>
        /// 可多次使用
        /// </summary>
        Reusable,
        /// <summary>
        /// 使用一次后删除
        /// </summary>
        UseOnce
    }
    /// <summary>
    /// ORM 查询器
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    [Serializable]
    public class ORMQuery<T> : SQLQuery where T : Entity
    {
        #region Private Property
        private SQLCondition _ConditionForSelect;
        private string _strConditionForSelect;
        private int _PageIndex, _PageSize;
        private string[] _FieldNames;
        private string _ViewName;
        private EntitySet<T> _LastResult = null;
        /// <summary>
        /// 最后一次查询的结果
        /// </summary>
        public EntitySet<T> LastResult
        {
            get { return _LastResult; }
            set { _LastResult = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        public ORMQuery()
        {
            this._DataBaseName = EntityStructManager.GetEntityStruct(typeof(T)).DatabaseName;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        public ORMQuery(string DataBaseName) : base(DataBaseName) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="ViewName">视图名称</param>
        ///// <param name="ConditionForSelect">查询条件</param>
        ///// <param name="PageIndex">页索引号</param>
        ///// <param name="PageSize">页行数</param>
        ///// <param name="FieldNames">自定义字段</param>
        //public ORMQuery(string ViewName,SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        //{
        //    this._ViewName = ViewName;
        //    this._ConditionForSelect = ConditionForSelect;
        //    this._strConditionForSelect = string.Empty;
        //    this._FieldNames = FieldNames;
        //    this._PageIndex = PageIndex;
        //    this._PageSize = PageSize;
        //}
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="ConditionForSelect">查询条件</param>      
        ///// <param name="ViewName">视图名称</param>  
        ///// <param name="PageIndex">页索引号</param>
        ///// <param name="PageSize">页行数</param>
        ///// <param name="FieldNames">自定义字段</param>
        //public ORMQuery(string ConditionForSelect, string ViewName, int PageIndex, int PageSize, params string[] FieldNames)
        //{
        //    this._ViewName = ViewName;
        //    this._strConditionForSelect = ConditionForSelect;
        //    this._ConditionForSelect = null;
        //    this._FieldNames = FieldNames;
        //    this._PageIndex = PageIndex;
        //    this._PageSize = PageSize;
        //}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this._strConditionForSelect = ConditionForSelect;
            this._ConditionForSelect = null;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>        
        /// <param name="ViewName">视图名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ViewName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ViewName = ViewName;
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._strConditionForSelect = ConditionForSelect;
            this._ConditionForSelect = null;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>  
        ///  <param name="ViewName">视图名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ViewName, string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ViewName = ViewName;
            this._strConditionForSelect = ConditionForSelect;
            this._ConditionForSelect = null;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ConditionForSelect, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ConditionForSelect = null;
            this._strConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        ///  <param name="ViewName">视图名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ViewName, string ConditionForSelect, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ViewName = ViewName;
            this._ConditionForSelect = null;
            this._strConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, SQLCondition ConditionForSelect, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DataBaseName">数据库服务名称</param>
        ///  <param name="ViewName">视图名称</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string DataBaseName, string ViewName, SQLCondition ConditionForSelect, params string[] FieldNames)
            : base(DataBaseName)
        {
            this._ViewName = ViewName;
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>     
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(string ConditionForSelect, params string[] FieldNames)
        {
            this._ConditionForSelect = null;
            this._strConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="ConditionForSelect">查询条件</param>       
        /////  <param name="ViewName">视图名称</param>
        ///// <param name="FieldNames">自定义字段</param>
        //public ORMQuery(string ConditionForSelect, string ViewName, params string[] FieldNames)
        //{
        //    this._ViewName = ViewName;
        //    this._ConditionForSelect = null;
        //    this._strConditionForSelect = ConditionForSelect;
        //    this._FieldNames = FieldNames;
        //    this._PageIndex = -1;
        //    this._PageSize = -1;
        //}
        /// <summary>
        /// 构造函数
        /// </summary>     
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }
        /// <summary>
        /// 构造函数
        /// </summary> 
        /// <param name="ConditionForSelect">查询条件</param>       
        /// <param name="ViewName">视图名称</param>
        /// <param name="FieldNames">自定义字段</param>
        public ORMQuery(SQLCondition ConditionForSelect, string ViewName, params string[] FieldNames)
        {
            this._ViewName = ViewName;
            this._ConditionForSelect = ConditionForSelect;
            this._strConditionForSelect = string.Empty;
            this._FieldNames = FieldNames;
            this._PageIndex = -1;
            this._PageSize = -1;
        }
        #endregion

        #region Query Method
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> Query(string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            try
            {
                EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));
                int RecordCount = 0;
                DataSet ds = this.GetDSQuery(es.TableName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);

                if (ds != null)
                {
                    EntitySet<T> result = FillSetForDS(ds, es);
                    result.RecordCount = RecordCount;
                    return result;
                }
            }
            catch (Exception ex)
            {
            }
            return new EntitySet<T>();
        }
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> Query(SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));
            int RecordCount = 0;
            DataSet ds = this.GetDSQuery(es.TableName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);

            if (ds != null)
            {
                EntitySet<T> result = FillSetForDS(ds, es);
                result.RecordCount = RecordCount;
                return result;
            }
            return new EntitySet<T>();
        }
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> Query(SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));

            DataSet ds = this.GetDSQuery(es.TableName, ConditionForSelect, FieldNames);

            if (ds != null)
            {
                EntitySet<T> result = FillSetForDS(ds, es);
                result.RecordCount = result.Count;
                return result;
            }
            return new EntitySet<T>();
        }
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> Query(string ConditionForSelect, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));

            DataSet ds = this.GetDSQuery(es.TableName, ConditionForSelect, FieldNames);

            if (ds != null)
            {
                EntitySet<T> result = FillSetForDS(ds, es);
                result.RecordCount = result.Count;
                return result;
            }
            return new EntitySet<T>();
        }

        /// <summary>
        /// 通过视图查询
        /// </summary>
        /// <param name="ViewName">视图名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="RecordCount">总记录数量</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> QueryView(string ViewName, string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));
            int RecordCount = 0;
            ViewName = "(" + ViewName + ") a";
            DataSet ds = this.GetDSQuery(ViewName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);

            if (ds != null)
            {
                EntitySet<T> result = FillSetForDS(ds, es);
                result.RecordCount = RecordCount;
                return result;
            }
            return new EntitySet<T>();
        }
        /// <summary>
        /// 通过视图查询
        /// </summary>
        /// <param name="ViewName">视图名</param>
        /// <param name="ConditionForSelect">自定义条件</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="RecordCount">总记录数量</param>
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> QueryView(string ViewName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(typeof(T));
            int RecordCount = 0;
            ViewName = "(" + ViewName + ") a";
            DataSet ds = this.GetDSQuery(ViewName, ConditionForSelect, PageIndex, PageSize, ref RecordCount, FieldNames);

            if (ds != null)
            {
                EntitySet<T> result = FillSetForDS(ds, es);
                result.RecordCount = RecordCount;
                return result;
            }
            return new EntitySet<T>();
        }
        /// <summary>
        /// 通过视图查询
        /// </summary>
        /// <param name="ViewName">视图名</param>
        /// <param name="ConditionForSelect">自定义条件</param>    
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> QueryView(string ViewName, string ConditionForSelect, params string[] FieldNames)
        {
            return QueryView(ViewName, ConditionForSelect, -1, -1, FieldNames);
        }
        /// <summary>
        /// 通过视图查询
        /// </summary>
        /// <param name="ViewName">视图名</param>
        /// <param name="ConditionForSelect">自定义条件</param> 
        /// <param name="FieldNames">自定义字段</param>
        /// <returns>实体集合</returns>
        public EntitySet<T> QueryView(string ViewName, SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            return QueryView(ViewName, ConditionForSelect, -1, -1, FieldNames);
        }
        #endregion

        #region Cache Query Method

        #region ORM Method
        /// <summary>
        /// 清除当前查询的条件和结果
        /// </summary>
        public void Clear()
        {
            this._ViewName = null;
            this._FieldNames = null;
            this._PageIndex = -1;
            this._PageSize = -1;
            this._strConditionForSelect = string.Empty;
            this._ConditionForSelect = null;
            this._LastResult = null;
        }

        #region Add OrderBy Method
        /// <summary>
        ///  添加OrderBy
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="SortType"></param>
        public void AddOrderBy(string FieldName, ESortType SortType)
        {
            AddOrderBy(new OrderByField(FieldName, SortType));

        }
        /// <summary>
        /// 添加OrderBy
        /// </summary>
        /// <param name="OrderByFields"></param>
        public void AddOrderBy(params OrderByField[] OrderByFields)
        {
            if (this._ConditionForSelect != null)
                this._ConditionForSelect.AddOrderBy(OrderByFields);
        }

        #endregion

        #region Add Condition Method
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this.Clear();
            this._ConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="ViewName">视图</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(string ViewName, SQLCondition ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this.Clear();
            this._ViewName = ViewName;
            this._ConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this.Clear();
            this._strConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>        
        /// <param name="ViewName">视图</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(string ViewName, string ConditionForSelect, int PageIndex, int PageSize, params string[] FieldNames)
        {
            this.Clear();
            this._ViewName = ViewName;
            this._strConditionForSelect = ConditionForSelect;
            this._FieldNames = FieldNames;
            this._PageIndex = PageIndex;
            this._PageSize = PageSize;
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>   
        public void AddCondition(SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            AddCondition(ConditionForSelect, -1, -1, FieldNames);
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>   
        /// <param name="ViewName">视图</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>   
        public void AddCondition(string ViewName, SQLCondition ConditionForSelect, params string[] FieldNames)
        {
            AddCondition(ViewName, ConditionForSelect, -1, -1, FieldNames);
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>      

        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(string ConditionForSelect, params string[] FieldNames)
        {
            AddCondition(ConditionForSelect, -1, -1, FieldNames);
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>   
        ///  <param name="ViewName">视图</param>
        /// <param name="ConditionForSelect">查询条件</param>
        /// <param name="FieldNames">自定义字段</param>
        public void AddCondition(string ViewName, string ConditionForSelect, params string[] FieldNames)
        {
            AddCondition(ViewName, ConditionForSelect, -1, -1, FieldNames);
        }

        #endregion

        #region 获取结果方法

        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <param name="ResultPolicy">结果读取策略，1.讲结果放入LastResult属性，2.读取一次，不缓存</param>
        /// <returns></returns>
        public EntitySet<T> GetResult(EResultPolicy ResultPolicy)
        {
            EntitySet<T> Result = null;
            if (this._ConditionForSelect != null)
            {
                if (this._ViewName == null)
                {
                    Result = this.Query(this._ConditionForSelect, this._PageIndex, this._PageSize, _FieldNames);
                }
                else
                {
                    Result = this.QueryView(this._ViewName, this._ConditionForSelect, this._PageIndex, this._PageSize, this._FieldNames);
                }
            }
            else if (this._strConditionForSelect != null)
            {
                if (this._ViewName == null)
                {
                    Result = this.Query(this._strConditionForSelect, this._PageIndex, this._PageSize, _FieldNames);
                }
                else
                {
                    Result = this.QueryView(this._ViewName, this._strConditionForSelect, this._PageIndex, this._PageSize, this._FieldNames);
                }
            }
            if (ResultPolicy == EResultPolicy.Reusable)
            {
                this.LastResult = Result;
                return LastResult;
            }
            else
            {
                this.LastResult = null;
                return Result;
            }
        }
        /// <summary>
        /// 下一页数据
        /// </summary>
        /// <param name="ResultPolicy">结果读取策略，1.讲结果放入LastResult属性，2.读取一次，不缓存</param>
        /// <returns></returns>
        public EntitySet<T> NextPage(EResultPolicy ResultPolicy)
        {
            if (this._PageIndex != -1)
            {
                this._PageIndex += 1;
            }
            return GetResult(ResultPolicy);
        }
        /// <summary>
        /// 下一页数据
        /// </summary>
        /// <param name="ResultPolicy">结果读取策略，1.讲结果放入LastResult属性，2.读取一次，不缓存</param>
        /// <returns></returns>
        public EntitySet<T> LastPage(EResultPolicy ResultPolicy)
        {
            if (this._PageIndex != -1)
            {
                this._PageIndex -= 1;
            }
            return GetResult(ResultPolicy);
        }
        /// <summary>
        /// 指定页数据
        /// </summary>
        /// <param name="PageIndex">页索引号</param>
        /// <param name="ResultPolicy">结果读取策略，1.讲结果放入LastResult属性，2.读取一次，不缓存</param>
        /// <returns></returns>
        public EntitySet<T> GotoPage(int PageIndex, int PageSize, EResultPolicy ResultPolicy)
        {
            if (this._PageIndex != -1)
            {
                this._PageIndex = PageIndex;
            }
            this._PageSize = PageSize;
            return GetResult(ResultPolicy);
        }
        /// <summary>
        /// 在内存中读取当前结果的分页数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public EntitySet<T> FindPageIndex(int PageIndex, int PageSize)
        {
            return LastResult != null ? LastResult.FindPageIndex(PageIndex, PageSize) : null;
        }

        #endregion


        #endregion


        #endregion

        #region Private Method
        /// <summary>
        /// 使用DataReader填充集合
        /// </summary>
        /// <param name="type">实体类类型</param>
        /// <param name="dr">DataReader</param>
        /// <param name="ColumnNames"></param>
        private EntitySet<T> FillSetForDS(DataSet ds, EntityStruct es)
        {
            Type type = typeof(T);

            EntitySet<T> result = new EntitySet<T>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                T instance = Activator.CreateInstance<T>();// (T)type.Assembly.CreateInstance(type.ToString());
                foreach (DataColumn Column in ds.Tables[0].Columns)
                {
                    try
                    {
                        string Key = Column.ColumnName.ToUpper();
                        if (dr[Key] == DBNull.Value)
                        {
                            continue;
                        }
                        #region 处理字段
                        Field Field = null;
                        if (es.PKeyList.ContainsKey(Key))
                        {
                            Field = es.PKeyList[Key];
                        }
                        else if (es.FKeyList.ContainsKey(Key))
                        {
                            Field = es.FKeyList[Key];
                        }
                        else if (es.FieldList.ContainsKey(Key))
                        {
                            Field = es.FieldList[Key];
                        }
                        #endregion

                        #region 赋值
                        if (Field != null)
                        {

                            if (Field.SerializedType == ESerializedType.NO)
                            {
                                if (Field.FieldType.IsEnum)
                                {
                                    instance[Field.PropertyName] = Convert.ToInt32(dr[Key]);
                                }
                                else if (Field.FieldType.ToString() == "System.Boolean")
                                {
                                    instance[Field.PropertyName] = Convert.ToBoolean(dr[Key]);
                                }
                                else
                                {
                                    instance[Field.PropertyName] = dr[Key];
                                }
                            }
                            else
                            {
                                instance[Field.PropertyName] = Serialization.Deserialize(dr[Key], Field.FieldType, Field.SerializedType);
                            }
                        }
                        else
                        {
                            instance[Key] = dr[Key];
                        }
                        #endregion
                    }
                    catch
                    {

                    }
                }
                instance.IsCreated = true;
                instance.OPTag = EDBOperationTag.Ignore;
                result.Add(instance);
            }
            return result;
        }
        #endregion
    }
}
