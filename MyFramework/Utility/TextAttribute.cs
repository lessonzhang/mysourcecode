using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace MyFramework.Utility
{
    public class TextAttribute : Attribute
    {
        string _Text;
        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
            }
        }


        string _Url;
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        /// <summary>
        /// 赋值text
        /// </summary>
        /// <param name="text"></param>
        public TextAttribute(string text)
        {
            this._Text = text;
        }

        /// <summary>
        /// 赋值text，url
        /// </summary>
        /// <param name="text"></param>      
        /// <param name="url"></param>
        public TextAttribute(string text, string url)
        {
            this._Text = text;
            this._Url = url;
        }

        /// <summary>
        /// Get Enum's TextAttribute
        /// </summary>
        /// <param name="enumConst"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetEnumTextVal(int enumConst, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            string textVal = "";

            Type typeDescription = typeof(TextAttribute);
            FieldInfo fieldInfo = enumType.GetField(Enum.GetName(enumType, enumConst).ToString());

            if (fieldInfo != null)
            {
                object[] arr = fieldInfo.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    TextAttribute textAttribute = (TextAttribute)arr[0];
                    textVal = textAttribute.Text;
                }
            }

            return textVal;
        }

        public static string GetEnumTextVal(string enumName, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            string textVal = "";

            Type typeDescription = typeof(TextAttribute);
            FieldInfo fieldInfo = enumType.GetField(enumName);

            if (fieldInfo != null)
            {
                object[] arr = fieldInfo.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    TextAttribute textAttribute = (TextAttribute)arr[0];
                    textVal = textAttribute.Text;
                }
            }

            return textVal;
        }

        /// <summary>
        /// Get a table by Enum,the table has Text and Value columns
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static DataTable GetEnumTable(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(System.String));
            dt.Columns.Add("Value", typeof(System.String));

            Type typeDescription = typeof(TextAttribute);

            FieldInfo[] fields = enumType.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    DataRow dr = dt.NewRow();

                    dr["Value"] = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();

                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        TextAttribute aa = (TextAttribute)arr[0];
                        dr["Text"] = aa.Text;
                    }
                    else
                    {
                        dr["Text"] = field.Name;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        /// <summary>
        /// Get a table by Enum,the table has Text and Value columns
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static DataTable GetEnumTableForText(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(System.String));
            dt.Columns.Add("Value", typeof(System.String));

            FieldInfo[] fields = enumType.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    DataRow dr = dt.NewRow();

                    dr["Value"] = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    dr["Text"] = enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null).ToString();
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

    }
}
