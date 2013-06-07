using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.Data
{

    /// <summary>
    /// Sort by ASC or DESC
    /// </summary>
    public enum ESortType
    {
        /// <summary>
        /// 升序
        /// </summary>
        ASC,
        /// <summary>
        /// 降序
        /// </summary>
        DESC
    }

    /// <summary>
    /// 逻辑运算类型
    /// </summary>
    public enum ELogicalType
    {
        Equals,
        NotEquals,
        LessThan,
        LessThanOrEquals,
        GreaterThan,
        GreaterThanOrEquals,
        Like,
        IN,
        NotIN,
        NOT
    }

    /// <summary>
    /// 排序字段
    /// </summary>
    public class OrderByField
    {
        #region Property
        private string _FieldName;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        private ESortType _SortType;
        /// <summary>
        /// 排序方式
        /// </summary>
        public ESortType SortType
        {
            get { return _SortType; }
            set { _SortType = value; }
        }
        #endregion

        public OrderByField(string FieldName, ESortType SortType)
        {
            this.FieldName = FieldName;
            this.SortType = SortType;
        }

        public override string ToString()
        {
            return this.FieldName + " " + this.SortType.ToString();
        }
    }


    /// <summary>
    /// 条件对象
    /// </summary>
    internal class Condition
    {
        private string _FieldName;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        public virtual string ToString(DataHelper dh)
        {
            return FieldName;
        }

        public Condition()
        { }
        public Condition(string FieldName)
        { this.FieldName = FieldName; }
    }

    /// <summary>
    /// 字段条件对象
    /// </summary>
    internal class DataFieldCondition : Condition
    {

        #region Property
        private ELogicalType _LogicalType;
        /// <summary>
        /// 逻辑操作符
        /// </summary>
        public ELogicalType LogicalType
        {
            get { return _LogicalType; }
            set { _LogicalType = value; }
        }

        private object _Value;
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="dh"></param>
        /// <returns></returns>
        public override string ToString(DataHelper dh)
        {
            string sql = string.Empty;
            #region
            if (Value != null)
            {
                switch (LogicalType)
                {
                    case ELogicalType.Equals:
                        FieldName = IsString(FieldName, Value);
                        sql = FieldName + " = " + GetSpecialValue(Value, dh);
                        break;
                    case ELogicalType.NotEquals:
                        FieldName = IsString(FieldName, Value);
                        sql = FieldName + " <> " + GetSpecialValue(Value, dh);
                        break;
                    case ELogicalType.Like:
                        FieldName = IsString(FieldName, Value);
                        if (Value.ToString() != string.Empty)
                            sql = FieldName + " like upper('%" + Value.ToString() + "%')";
                        else
                        {
                            sql = "1=1";
                        }
                        break;
                    case ELogicalType.LessThan:
                        FieldName = IsString(FieldName, Value);
                        sql = FieldName + " < " + GetSpecialValue(Value, dh);
                        break;
                    case ELogicalType.GreaterThan:
                        FieldName = IsString(FieldName, Value);
                        sql = FieldName + " > " + GetSpecialValue(Value, dh);
                        break;
                    case ELogicalType.IN:
                        sql = FieldName + " in (" + Value + ")";
                        break;
                    case ELogicalType.NotIN:
                        sql = FieldName + " not  in (" + Value + ")";
                        break;
                    case ELogicalType.NOT:
                        sql = FieldName + " not " + Value;
                        break;
                    case ELogicalType.GreaterThanOrEquals:
                        sql = FieldName + " >= " + GetSpecialValue(Value, dh); ;
                        break;
                    case ELogicalType.LessThanOrEquals:
                        sql = FieldName + "<= " + GetSpecialValue(Value, dh); ;
                        break;
                }
            }
            #endregion
            return sql;
        }
        /// <summary>
        /// 判断字段是否是字符串型
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static string IsString(string FieldName, object Value)
        {
            if (Value.GetType().ToString() == "System.String")
            {
                FieldName = "upper(" + FieldName.Replace("'", "''") + ")";
            }
            return FieldName;
        }
        /// <summary>
        /// 得到特殊类型的SQL定义
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="dh"></param>
        /// <returns></returns>
        private string GetSpecialValue(object Value, DataHelper dh)
        {
            string result = string.Empty;
            Type Type = Value.GetType();

            if (Type.IsEnum)
            {
                result = Convert.ToInt32(Value).ToString();
            }
            else
            {
                switch (Type.ToString())
                {
                    case "System.String":
                        result = "upper('" + Value.ToString() + "')";
                        break;
                    case "System.DateTime":
                        result = "TO_DATE('" + ((DateTime)Value).ToShortDateString() + "','dd-mm-yyyy')";
                        break;
                    case "System.Boolean":
                        result = Convert.ToInt32(Value).ToString();
                        break;
                    default:
                        result = Value.ToString();
                        break;
                }
            }
            return result;
        }
        #endregion

        public DataFieldCondition()
        { }
        public DataFieldCondition(string FieldName, ELogicalType LogicalType, object Value)
            : base(FieldName)
        {
            this.LogicalType = LogicalType;
            this.Value = Value;
        }

    }


    /// <summary>
    /// 条件类型
    /// </summary>
    [Serializable]
    public class SQLCondition
    {
        private List<Condition> ConditionSources;
        private List<OrderByField> OrderByField = null;
        public SQLCondition()
        {
            ConditionSources = new List<Condition>();
        }
        #region Method
        /// <summary>
        /// 左括号
        /// </summary>
        public void BeginBrackets()
        {
            ConditionSources.Add(new Condition(" ("));
        }
        /// <summary>
        /// 右括号
        /// </summary>
        public void EndBrackets()
        {
            ConditionSources.Add(new Condition(" )"));
        }

        /// <summary>
        /// 并操作符
        /// </summary>
        public void AND()
        {
            ConditionSources.Add(new Condition(" and "));
        }
        /// <summary>
        /// 与操作符
        /// </summary>
        public void OR()
        {
            ConditionSources.Add(new Condition(" or "));
        }

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="LogicalType"></param>
        /// <param name="Value"></param>
        public void AddCondition(string FieldName, ELogicalType LogicalType, object Value)
        {
            ConditionSources.Add(new DataFieldCondition(FieldName, LogicalType, Value));
        }
        /// <summary>
        ///  添加OrderBy
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="SortType"></param>
        public void AddOrderBy(string FieldName, ESortType SortType)
        {
            this.OrderByField.Add(new OrderByField(FieldName, SortType));
        }
        /// <summary>
        /// 添加OrderBy
        /// </summary>
        /// <param name="OrderByFields"></param>
        public void AddOrderBy(params OrderByField[] OrderByFields)
        {
            this.OrderByField = new List<OrderByField>();
            this.OrderByField.AddRange(OrderByFields);
        }

        /// <summary>
        /// 获取SQL条件
        /// </summary>
        /// <returns></returns>
        public string ToString(DataHelper dh)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Condition str in ConditionSources)
            {
                sb.Append(str.ToString(dh));
            }
            if (this.OrderByField != null)
            {
                string str1 = "";
                foreach (OrderByField Field in this.OrderByField)
                {
                    str1 += Field.ToString() + ",";
                }
                if (str1.Length > 0)
                {
                    str1 = str1.TrimEnd(',');
                }
                sb.Append(" order by " + str1);
            }
            return sb.ToString();
        }

        public static string LikeConvert(string keyword)
        {
            return keyword.Replace("'", "['']").Replace("%", "[%]").Replace("_", "[_]");
        }

        public static string EqualConvert(string keyword)
        {
            return keyword.Replace("'", "''");
        }
        #endregion
    }
}
