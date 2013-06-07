using System;
using System.Reflection;
using MyFramework.Data.ORM.Attributes;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace MyFramework.Data.ORM
{
    internal class EntityStructManager
    {
        const string ConstCacheManagerName = "{8EB89CCB-C0F7-4bcc-8A1E-9944CAD6C60F}";
        private static ICacheManager innerCacheManager
        {
            get
            {
                return CacheFactory.GetCacheManager("EntityStructCacheManager");
            }
        }

        private EntityStructManager() { }

        /// <summary>
        /// 注册实体结构到缓存管理器
        /// </summary>
        /// <param name="EntityType"></param>
        public static void RegisterEntityStruct(Type EntityType)
        {
            if (!CheckExist(EntityType))
            {
                EntityStruct Struct = new EntityStruct();

                #region Fill DataTable
                ORMDataTable[] dbTable = (ORMDataTable[])EntityType.GetCustomAttributes(typeof(ORMDataTable), true);


                if (dbTable.Length > 0)
                {
                    Struct.Table = dbTable[0];

                }
                #endregion

                #region Fill Field

                #region 获取当前类的隐射信息

                #region 取字段信息
                foreach (FieldInfo fi in EntityType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    #region 获取主键信息
                    KeyField[] dbKeyFieldSet = (KeyField[])fi.GetCustomAttributes(typeof(KeyField), true);
                    foreach (KeyField key in dbKeyFieldSet)
                    {
                        key.FieldType = fi.FieldType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (key.KeyType == EKeyType.PRIMARY)
                        {
                            if (!Struct.PKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                Struct.PKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                        else
                        {
                            if (!Struct.FKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                    }
                    #endregion

                    #region 获取其他字段信息
                    DataField[] dbDataFieldSet = (DataField[])fi.GetCustomAttributes(typeof(DataField), true);
                    foreach (DataField key in dbDataFieldSet)
                    {
                        key.FieldType = fi.FieldType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (!Struct.FKeyList.ContainsKey(key.FieldName.ToUpper()))
                        {
                            key.FieldType = fi.FieldType;
                            key.PropertyName = fi.Name;

                            if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                            Struct.FieldList.Add(key.FieldName.ToUpper(), key);
                        }

                    }
                    #endregion
                }
                #endregion

                #region 取属性信息
                foreach (PropertyInfo fi in EntityType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    #region 获取主键信息
                    KeyField[] dbKeyFieldSet = (KeyField[])fi.GetCustomAttributes(typeof(KeyField), true);
                    foreach (KeyField key in dbKeyFieldSet)
                    {
                        key.FieldType = fi.PropertyType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (key.KeyType == EKeyType.PRIMARY)
                        {
                            if (!Struct.PKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                Struct.PKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                        else
                        {
                            if (!Struct.FKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                    }
                    #endregion

                    #region 获取其他字段信息
                    DataField[] dbDataFieldSet = (DataField[])fi.GetCustomAttributes(typeof(DataField), true);
                    foreach (DataField key in dbDataFieldSet)
                    {
                        key.FieldType = fi.PropertyType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称
                        if (!Struct.FieldList.ContainsKey(key.FieldName.ToUpper()))
                        {
                            key.FieldType = fi.PropertyType;
                            key.PropertyName = fi.Name;

                            if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                            Struct.FieldList.Add(key.FieldName.ToUpper(), key);
                        }

                    }
                    #endregion
                }
                #endregion
                #endregion


                #region 获取继承的隐射信息

                #region 取字段信息
                foreach (FieldInfo fi in EntityType.GetFields())
                {
                    #region 获取主键信息
                    KeyField[] dbKeyFieldSet = (KeyField[])fi.GetCustomAttributes(typeof(KeyField), true);
                    foreach (KeyField key in dbKeyFieldSet)
                    {
                        key.FieldType = fi.FieldType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (key.KeyType == EKeyType.PRIMARY)
                        {
                            if (!Struct.PKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                key.KeyType = EKeyType.FOREIGN;
                                key.Sequence = string.Empty;
                                key.IsBase = true;
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                        else
                        {
                            if (!Struct.FKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                key.IsBase = true;
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                    }
                    #endregion

                    #region 获取其他字段信息
                    DataField[] dbDataFieldSet = (DataField[])fi.GetCustomAttributes(typeof(DataField), true);
                    foreach (DataField key in dbDataFieldSet)
                    {
                        key.FieldType = fi.FieldType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (!Struct.FieldList.ContainsKey(key.FieldName.ToUpper()))
                        {
                            key.FieldType = fi.FieldType;
                            key.PropertyName = fi.Name;
                            key.IsBase = true;
                            if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                            Struct.FieldList.Add(key.FieldName.ToUpper(), key);
                        }

                    }
                    #endregion
                }
                #endregion

                #region 取属性信息
                foreach (PropertyInfo fi in EntityType.GetProperties())
                {
                    #region 获取主键信息
                    KeyField[] dbKeyFieldSet = (KeyField[])fi.GetCustomAttributes(typeof(KeyField), true);
                    foreach (KeyField key in dbKeyFieldSet)
                    {
                        key.FieldType = fi.PropertyType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (key.KeyType == EKeyType.PRIMARY)
                        {
                            if (!Struct.PKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                key.KeyType = EKeyType.FOREIGN;
                                key.Sequence = string.Empty;
                                key.IsBase = true;
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                        else
                        {
                            if (!Struct.FKeyList.ContainsKey(key.FieldName.ToUpper()))
                            {
                                key.IsBase = true;
                                Struct.FKeyList.Add(key.FieldName.ToUpper(), key);
                            }
                        }
                    }
                    #endregion

                    #region 获取其他字段信息
                    DataField[] dbDataFieldSet = (DataField[])fi.GetCustomAttributes(typeof(DataField), true);
                    foreach (DataField key in dbDataFieldSet)
                    {
                        key.FieldType = fi.PropertyType;
                        key.PropertyName = fi.Name;

                        if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                        if (!Struct.FieldList.ContainsKey(key.FieldName.ToUpper()))
                        {
                            key.FieldType = fi.PropertyType;
                            key.PropertyName = fi.Name;
                            key.IsBase = true;
                            if (key.FieldName == string.Empty) key.FieldName = key.PropertyName;//如果字段隐射为空就等于属性名称

                            Struct.FieldList.Add(key.FieldName.ToUpper().ToUpper(), key);
                        }

                    }
                    #endregion
                }
                #endregion
                #endregion

                #endregion

                innerCacheManager.Add(EntityType.ToString(), Struct);
            }

        }
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        public static bool CheckExist(Type EntityType)
        {
            return innerCacheManager.Contains(EntityType.ToString());
        }

        /// <summary>
        /// 获取实体结构实例
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        public static EntityStruct GetEntityStruct(Type EntityType)
        {
            if (CheckExist(EntityType))
            {
                return (EntityStruct)innerCacheManager.GetData(EntityType.ToString());
            }
            RegisterEntityStruct(EntityType);
            return GetEntityStruct(EntityType);
        }
    }
}
