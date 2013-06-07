using System;
using System.Collections.Generic;
using MyFramework.Data.Transaction;
using MyFramework.Utility;

namespace MyFramework.Data.ORM
{
    /// <summary>
    /// 实体实例集
    /// </summary>
    /// <typeparam name="T">Entity泛型</typeparam>
    [Serializable]
    public class EntitySet<T> : List<object> where T : Entity
    {
        private int _TotalCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        #region Method
        /// <summary>
        /// 在内存中查找匹配的实体
        /// </summary>
        /// <param name="PropertySet">属性值集合</param>
        /// <returns></returns>
        public EntitySet<T> Find(Dictionary<string, object> PropertySet)
        {
            EntitySet<T> es = new EntitySet<T>();
            if (this.Count > 0)
            {
                bool blresult = false;
                foreach (Entity entity in this)
                {
                    blresult = true;
                    foreach (string Key in PropertySet.Keys)
                    {
                        blresult = (entity[Key].Equals(PropertySet[Key]));
                        if (!blresult) break;
                    }
                    if (blresult)
                    {
                        es.Add(entity);
                    }

                }
            }
            return es;
        }

        /// <summary>
        /// 在内存中查找匹配的实体
        /// </summary>
        /// <param name="PropertyName">属性名</param>
        /// <param name="Value">字段值</param>
        /// <returns></returns>
        public EntitySet<T> Find(string PropertyName, object Value)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();
            tmp.Add(PropertyName, Value);
            return Find(tmp);
        }

        /// <summary>
        /// 返回指定索引的Entity实例
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public T GetEntity(int index)
        {
            return (T)base[index];
        }
        /// <summary>
        /// 排序方法（内存中）
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="ordermode">排序方式</param>
        public void Sort(string propertyName, ESortType ordermode)
        {
            this.Sort(new ObjectSortComparer<object>(propertyName, ordermode));
        }

        /// <summary>
        /// 返回有效实体的数据集
        /// </summary>
        /// <returns></returns>
        public EntitySet<T> GetAvailEntitySet()
        {
            EntitySet<T> result = new EntitySet<T>();
            foreach (T obj in this)
            {
                if (obj.OPTag != EDBOperationTag.Delete)
                {
                    result.Add(obj);
                }
            }
            return result;
        }

        /// <summary>
        /// 在内存中分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public EntitySet<T> FindPageIndex(int PageIndex, int PageSize)
        {
            EntitySet<T> result = new EntitySet<T>();
            int Index = (PageIndex - 1) * PageSize;
            if (this.Count > PageSize)
            {
                int cnt = 0;
                for (int i = Index; i < this.Count; i++)
                {
                    result.Add(this[i]);
                    cnt++;
                    if (cnt == PageSize) break;
                }
            }
            else
            {
                result.AddRange(this);
            }
            return result;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        public void BatDelete()
        {
            try
            {
                using (TransactionScope Tran = new TransactionScope())
                {
                    foreach (Entity entity in this)
                    {
                        entity.DB_DeleteEntity();
                    }
                    Tran.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        public bool BatInsert()
        {
            bool result = true;

            using (TransactionScope Tran = new TransactionScope())
            {
                foreach (Entity entity in this)
                {
                    result = entity.DB_InsertEntity();
                    if (!result)
                    {
                        break;
                    }
                }
                if (result)
                {
                    Tran.Complete();
                }

            }
            return result;
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        public bool BatUpdate()
        {
            bool result = true;
            using (TransactionScope Tran = new TransactionScope())
            {
                foreach (Entity entity in this)
                {
                    result = entity.DB_UpdateEntity();
                    if (!result)
                    {
                        break;
                    }
                }
                if (result)
                {
                    Tran.Complete();
                }

            }
            return result;
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        public bool BatUpdate(params string[] FiledNams)
        {

            bool result = true;
            using (TransactionScope Tran = new TransactionScope())
            {
                foreach (Entity entity in this)
                {
                    result = entity.DB_UpdateEntity(string.Empty, FiledNams);
                    if (!result)
                    {
                        break;
                    }
                }
                if (result)
                {
                    Tran.Complete();
                }

            }
            return result;
        }
        /// <summary>
        /// 批量保存
        /// </summary>
        public void BatSaveByTag()
        {
            try
            {
                using (TransactionScope Tran = new TransactionScope())
                {
                    foreach (Entity entity in this)
                    {
                        entity.SaveByTag();
                    }
                    Tran.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public List<T> ToList()
        {
            object[] obj = this.ToArray();

            List<T> result = new List<T>();
            foreach (T tmp in obj)
            {
                result.Add(tmp);
            }
            return result;
        }
        #endregion
    }
}
