using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using MyFramework.Data.ORM.Attributes;

namespace MyFramework.Data.ORM
{
    [Serializable]
    internal class FieldDescriptor : PropertyDescriptor
    {
        private Field _Field;

        public FieldDescriptor(Field field)
            : base(field.PropertyName, null)
        {
            this._Field = field;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }

        public override object GetValue(object component)
        {
            BaseEntity obj = (BaseEntity)component;
            return obj.GetField(_Field.PropertyName);
        }

        public override void SetValue(object component, object value)
        {
            BaseEntity obj = (BaseEntity)component;
            obj.SetField(_Field.PropertyName, value);
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return _Field.FieldType;
            }
        }

        public override void ResetValue(object component)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
