using System;
using System.Collections.Generic;
using System.Text;
using MyFramework.Utility;

namespace MyFramework.Data.ORM
{
    /// <summary>
    /// 实体对象
    /// </summary>
    [Serializable]
    public class Entity : BaseEntity
    {
        public Entity()
        {
            ThrowException = false;
        }
        #region Property
        private EDBOperationTag _OPTag;
        /// <summary>
        /// 实体待操作标记
        /// </summary>
        public EDBOperationTag OPTag
        {
            set { _OPTag = value; }
            get { return _OPTag; }
        }

        public bool ThrowException
        { set; get; }

        #endregion

        #region 实体数据库操作方法
        /// <summary>
        /// 将实体对象插入数据库
        /// </summary>
        /// <param name="Propertys">自定义字段</param>
        public bool DB_InsertEntity(params string[] Propertys)
        {
            try
            {
                IsCreated = true;
                EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
                IsCreated = es.DoInsert(this, Propertys) > 0 ? true : false;
                return IsCreated;
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
        }
        /// <summary>
        /// 更新实体数据到数据库中
        /// </summary>       
        /// <param name="ConditionForUpdate">自定义条件</param>
        public bool DB_UpdateEntity(string ConditionForUpdate, params string[] Propertys)
        {
            try
            {

                EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
                IsCreated = es.DoUpdate(this, ConditionForUpdate, Propertys) > 0 ? true : false;
                return IsCreated;
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }

        }
        /// <summary>
        /// 更新实体数据到数据库中
        /// </summary>       
        /// <param name="ConditionForUpdate">自定义条件</param>
        public bool DB_UpdateEntity(SQLCondition ConditionForUpdate, params string[] Propertys)
        {
            try
            {

                EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
                IsCreated = es.DoUpdate(this, ConditionForUpdate, Propertys) > 0 ? true : false;
                return IsCreated; ;
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
        }
        /// <summary>
        /// 更新实体数据到数据库中
        /// </summary>
        /// <param name="Propertys">自定义字段</param>
        public bool DB_UpdateEntity()
        {
            return DB_UpdateEntity(string.Empty);
        }

        /// <summary>
        /// 保存实体,未创建为新增,创建后为更新
        /// </summary>
        /// <param name="Propertys">自定义字段</param>
        public bool DB_SaveEntity(params string[] Propertys)
        {
            bool Success = false;
            try
            {
                if (!IsCreated)
                {

                    Success = this.DB_InsertEntity(Propertys);
                }
                else
                {
                    Success = this.DB_UpdateEntity(string.Empty, Propertys);
                }
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
            return Success;
        }

        /// <summary>
        /// 保存实体,未创建为新增,创建后为更新
        /// </summary>
        /// <param name="ConditionForUpdate">自定义条件</param>
        /// <param name="Propertys">字段名1,字段名2,字段名3.....字段名n</param>
        public bool DB_SaveEntitywithCondition(string ConditionForUpdate, params string[] Propertys)
        {
            try
            {
                if (!IsCreated)
                {

                    this.IsCreated = this.DB_InsertEntity();

                }
                else
                {
                    this.IsCreated = this.DB_UpdateEntity(ConditionForUpdate, Propertys);
                }
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
            return this.IsCreated;
        }

        /// <summary>
        /// 保存实体,未创建为新增,创建后为更新
        /// </summary>
        /// <param name="ConditionForUpdate">自定义条件</param>
        /// <param name="Propertys">字段名1,字段名2,字段名3.....字段名n</param>
        public bool DB_SaveEntitywithCondition(SQLCondition ConditionForUpdate, params string[] Propertys)
        {
            try
            {
                if (!IsCreated)
                {

                    this.IsCreated = this.DB_InsertEntity();

                }
                else
                {
                    this.IsCreated = this.DB_UpdateEntity(ConditionForUpdate, Propertys);
                }
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
            return this.IsCreated;
        }
        /// <summary>
        /// 从数据库删除指定条件的实体数据
        /// </summary>
        ///  <param name="ConditionForDelete">自定义条件</param>
        public bool DB_DeleteEntity(string ConditionForDelete)
        {
            try
            {
                this.IsCreated = false;
                EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
                return es.DoDelete(this, ConditionForDelete) > 0 ? true : false;
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
        }

        /// <summary>
        /// 从数据库删除指定条件的实体数据
        /// </summary>
        ///  <param name="ConditionForDelete">自定义条件</param>
        public bool DB_DeleteEntity(SQLCondition ConditionForDelete)
        {
            try
            {
                this.IsCreated = false;
                EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
                return es.DoDelete(this, ConditionForDelete) > 0 ? true : false;
            }
            catch (Exception exp)
            {
                if (ThrowException)
                {
                    throw exp;
                }
                return false;
            }
        }
        /// <summary>
        /// 从数据库删除实体数据
        /// </summary>   
        public bool DB_DeleteEntity()
        {
            return DB_DeleteEntity(string.Empty);
        }

        /// <summary>
        /// 判断指定字段值是否存在
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="FieldValue">值</param>
        /// <param name="ConditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public bool IsExist(string FieldName, object FieldValue)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
            return es.DoCheckExist(FieldName, FieldValue) > 0 ? true : false;
        }

        /// <summary>
        /// 判断指定字段值是否存在
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="FieldValue">值</param>
        /// <param name="ConditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public bool IsExist(SQLCondition ConditionForCheckExist)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
            return es.DoCheckExist(ConditionForCheckExist) > 0 ? true : false;
        }
        /// <summary>
        /// 判断指定字段值是否存在
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="FieldValue">值</param>
        /// <param name="ConditionForCheckExist">自定义条件</param>
        /// <returns></returns>
        public bool IsExist(string ConditionForCheckExist)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());
            return es.DoCheckExist(ConditionForCheckExist) > 0 ? true : false;
        }
        /// <summary>
        /// 根据操作标记保存数据
        /// </summary>
        /// <returns></returns>
        public bool SaveByTag()
        {
            switch (this.OPTag)
            {
                case EDBOperationTag.AddNew:
                    if (this.DB_InsertEntity())
                    {
                        this._OPTag = EDBOperationTag.Ignore;
                        return true;
                    }
                    break;
                case EDBOperationTag.Delete:
                    if (this.DB_DeleteEntity())
                    {
                        this._OPTag = EDBOperationTag.Ignore;
                        return true;
                    }
                    break;
                case EDBOperationTag.Update:
                    if (this.DB_UpdateEntity())
                    {
                        this._OPTag = EDBOperationTag.Ignore;
                        return true;
                    }
                    break;
                case EDBOperationTag.Ignore: return true;
            }
            return false;
        }

        public bool FillSelf(int ID, params string[] FieldNames)
        {
            return FillSelf<int>(ID, FieldNames);
        }
        public bool FillSelf<T>(T ID, params string[] FieldNames)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());

            return es.DoFillSelf(this, ID, FieldNames);
        }
        public bool FillSelf(string Condition)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());

            return es.DoFillSelf(this, Condition);
        }
        public bool FillSelf(SQLCondition Condition)
        {
            EntityStruct es = EntityStructManager.GetEntityStruct(this.GetType());

            return es.DoFillSelf(this, Condition);
        }
        #endregion
    }
}
