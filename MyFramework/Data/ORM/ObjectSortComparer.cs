using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

namespace MyFramework.Data.ORM
{
    [Serializable]
    internal class ObjectSortComparer<T> : IComparer<T>
    {
        public static object GetPropertyValue(object obj, string prop)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            Type objtype = obj.GetType();
            PropertyInfo propinfo = objtype.GetProperty(prop);
            if (propinfo == null)
            {
                throw new Exception("not find property");
            }

            return propinfo.GetValue(obj, null);
        }

        private string PropertyName;

        private ESortType OrderMode;

        public ObjectSortComparer(string propertyname, ESortType ordermode)
        {
            this.PropertyName = propertyname;
            this.OrderMode = ordermode;
        }

        public int Compare(T x, T y)
        {
            if ((x == null) && (y == null))
            {
                return 0;
            }
            else if ((x == null) && (y != null))
            {
                return (OrderMode == ESortType.ASC) ? -1 : 1;
            }
            else if ((x != null) && (y == null))
            {
                return (OrderMode == ESortType.ASC) ? 1 : -1;
            }
            else
            {
                object propdatax = GetPropertyValue(x, PropertyName);
                object propdatay = GetPropertyValue(y, PropertyName);

                if ((propdatax == null) && (propdatay == null))
                {
                    return 0;
                }
                else if ((propdatax == null) && (propdatay != null))
                {
                    return (OrderMode == ESortType.ASC) ? -1 : 1;
                }
                else if ((propdatax != null) && (propdatay == null))
                {
                    return (OrderMode == ESortType.ASC) ? 1 : -1;
                }
                else
                {
                    if (propdatax is IComparable)
                    {
                        int result = ((IComparable)propdatax).CompareTo(propdatay);
                        return (OrderMode == ESortType.ASC) ? result : -result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
