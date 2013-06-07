using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using MyFramework.Data.ORM.Attributes;
using MyFramework.Data.ORM;

namespace MyFramework.Data.ORM
{
    /// <summary>
    /// 实体基类类
    /// </summary>
    [Serializable]
    public abstract class BaseEntity : ICustomTypeDescriptor
    {
        #region BaseEntity Members
        private Dictionary<string, Field> innerProperty = new Dictionary<string, Field>();
        #region ICustomTypeDescriptor Members



        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this, true);

            PropertyDescriptorCollection newprops = new PropertyDescriptorCollection(null, false);
            foreach (PropertyDescriptor prop in props)
            {
                newprops.Add(prop);
            }

            foreach (Field field in this.innerProperty.Values)
            {
                FieldDescriptor fielddescriptor = new FieldDescriptor(field);
                newprops.Add(fielddescriptor);
            }

            return newprops;
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        #region Custom Members
        private bool _IsCreated = false;
        /// <summary>
        /// 已经创建
        /// </summary>
        public bool IsCreated
        {
            get { return _IsCreated; }
            set { _IsCreated = value; }
        }
        /// <summary>
        /// 得到字段的值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public object GetField(string PropertyName)
        {
            if (innerProperty.ContainsKey(PropertyName.ToUpper()))
                return this.innerProperty[PropertyName.ToUpper()].Value;
            else
                return null;
        }
        /// <summary>
        /// 设置字段的值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="Value"></param>
        public void SetField(string PropertyName, object Value)
        {
            this.innerProperty[PropertyName.ToUpper()] = new Field(PropertyName, Value);
        }


        /// <summary>
        /// 属性索引器
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public object this[string PropertyName]
        {
            get
            {
                try
                {
                    Type objtype = this.GetType();
                    PropertyInfo prop = objtype.GetProperty(PropertyName);
                    if (prop != null)
                    {
                        return prop.GetValue(this, null);
                    }
                    else
                    {
                        return GetField(PropertyName);
                    }
                }
                catch
                {
                    return DBNull.Value;
                }

            }
            set
            {
                Type objtype = this.GetType();
                PropertyInfo prop = objtype.GetProperty(PropertyName);
                if (prop != null)
                {
                    prop.SetValue(this, value, null);
                }
                else
                {
                    SetField(PropertyName, value);
                }
            }
        }

        #region Method
        /// <summary>
        /// 验证实体对象是否包含指定的属性
        /// </summary>
        /// <param name="PropertyName">属性名称</param>
        /// <returns></returns>
        public bool ContainProperty(string PropertyName)
        {
            bool result = false;
            Type objtype = this.GetType();
            PropertyInfo prop = objtype.GetProperty(PropertyName);
            if (prop != null)
            {
                result = true;
            }
            else
            {
                result = innerProperty.ContainsKey(PropertyName.ToUpper());
            }
            return result;
        }


        /// <summary>
        /// 验证实体中是否有实体对象包含指定属性的值
        /// </summary>
        /// <param name="PropertyName">属性名称</param>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public bool ContainEntitybyProperty(string PropertyName, object Value)
        {
            bool result = false;

            if (this.ContainProperty(PropertyName))
            {
                result = this[PropertyName].Equals(Value);
            }

            return result;
        }


        /// <summary>
        /// 实体克隆
        /// </summary>
        /// <param name="entity"></param>
        public void Clone(Entity entity)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(entity, true);
            foreach (PropertyDescriptor prop in props)
            {
                this[prop.Name] = entity[prop.Name];
            }
            foreach (DataField field in entity.innerProperty.Values)
            {
                this[field.FieldName] = entity[field.FieldName];
            }

        }
        #endregion
        #endregion



        #region Constructor
        public BaseEntity()
        {
            //EntityStructManager.RegisterEntityStruct(this.GetType());
        }
        #endregion
        #endregion
    }
}
