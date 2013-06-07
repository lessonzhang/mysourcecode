using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Threading;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MyFramework.Data.Transaction
{
    /// <summary>
    /// 隐式事务管理类
    /// </summary>
    [Serializable]
    public class TransactionScope : IDisposable
    {
        #region 私有字段
        public const string ConstTransactionName = "{6280226B-16FA-4822-BD9E-0AAD9FD35D98}";
        //事务对象缓存
        private static ICacheManager _DatabaseChache = null;
        //提交标志
        private bool UnCommit = false;
        //当前事务对象使用的链接
        private DbConnection ConnectionInstance = null;
        //当前事务对象
        private DbTransaction Transaction = null;

        private string DatabaseName = null;

        private bool IsMerge = false;

        /// <summary>
        /// 当前事务对象缓存名称
        /// </summary>
        private string TransactionName
        {
            get
            {
                return TransactionScope.ConstTransactionName + DatabaseName + Thread.CurrentThread.ManagedThreadId.ToString();
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 得到当前线程事务对象
        /// </summary>
        /// <returns></returns>
        public static DbTransaction GetDbTransactionInstance(string CacheName)
        {
            CacheName = TransactionScope.ConstTransactionName + CacheName + Thread.CurrentThread.ManagedThreadId.ToString();
            if (_DatabaseChache == null)
            {
                _DatabaseChache = CacheFactory.GetCacheManager("DBTransactionCacheManager");
            }

            return (DbTransaction)TransactionScope._DatabaseChache.GetData(CacheName);
        }
        /// <summary>
        /// 清除事务对象缓存
        /// </summary>
        public static void RemoveDbTransactionInstance(string CacheName)
        {
            CacheName = TransactionScope.ConstTransactionName + CacheName + Thread.CurrentThread.ManagedThreadId.ToString();

            if (TransactionScope._DatabaseChache.Contains(CacheName))
                TransactionScope._DatabaseChache.Remove(CacheName);
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        static TransactionScope()
        {
            if (_DatabaseChache == null)
            {
                _DatabaseChache = CacheFactory.GetCacheManager("DBTransactionCacheManager");
            }

        }
        #endregion

        #region 公共方法
        /// <summary>
        ///  构造事务对象
        /// </summary>
        /// <param name="IsolationLevel">事务级别</param>
        public TransactionScope(IsolationLevel IsolationLevel)
        {
            InitTransactionScope(IsolationLevel, string.Empty);
        }
        /// <summary>
        /// 构造事务对象
        /// </summary>
        public TransactionScope()
        {
            InitTransactionScope(IsolationLevel.ReadCommitted, string.Empty);
        }

        /// <summary>
        /// 构造事务对象
        /// </summary>
        /// <param name="DataBaseName">Configuration key for Database service</param>
        public TransactionScope(string DataBaseName)
        {
            InitTransactionScope(IsolationLevel.ReadCommitted, DataBaseName);
        }
        /// <summary>
        /// 构造事务对象
        /// </summary>
        /// <param name="IsolationLevel">事务级别</param>
        /// <param name="DataBaseName">Configuration key for Database service</param>
        public TransactionScope(IsolationLevel IsolationLevel, string DataBaseName)
        {
            InitTransactionScope(IsolationLevel, DataBaseName);
        }

        /// <summary>
        /// 初始化事务对象
        /// </summary>
        /// <param name="IsolationLevel"></param>
        private void InitTransactionScope(IsolationLevel IsolationLevel, string Name)
        {
            string Key = TransactionScope.ConstTransactionName + Name + Thread.CurrentThread.ManagedThreadId.ToString();
            if (TransactionScope.GetDbTransactionInstance(Name) == null)
            {
                //创建数据库对象
                Database db = null;

                DatabaseName = Name;

                //创建数据库连接
                ConnectionInstance = null;
                //开启连接
                ConnectionInstance.Open();
                //打开事务
                Transaction = ConnectionInstance.BeginTransaction(IsolationLevel);

                //将事务对象填充缓存池
                TransactionScope._DatabaseChache.Add(TransactionName, Transaction);
                //设置是否提交标记
                UnCommit = true;
                IsMerge = false;
            }
            else
            {
                IsMerge = true;
            }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Complete()
        {
            if (!IsMerge)
            {
                if (Transaction.Connection != null)
                {
                    try
                    {
                        Transaction.Commit();
                    }
                    catch (Exception exp)
                    {
                        Transaction.Rollback();
                        throw exp;
                    }
                    finally
                    {
                        ConnectionInstance.Close();

                        TransactionScope._DatabaseChache.Remove(TransactionName);
                        UnCommit = false;
                    }
                }
            }
        }
        #endregion


        #region IDisposable 成员
        /// <summary>
        /// 事务对象销毁方法
        /// </summary>
        public void Dispose()
        {

            if (!IsMerge)
            {
                //如果发生异常后进入事务对象销毁处理，需要通过判断提交标志进行回滚和关闭数据库连接
                if ((UnCommit) && (Transaction != null) && (Transaction.Connection != null)) Transaction.Rollback();
                if ((ConnectionInstance != null) && (ConnectionInstance.State == System.Data.ConnectionState.Open)) ConnectionInstance.Close();
                if (TransactionScope._DatabaseChache.Contains(TransactionName))
                    TransactionScope._DatabaseChache.Remove(TransactionName);
            }
        }

        #endregion
    }
}
