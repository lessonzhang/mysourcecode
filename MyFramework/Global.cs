using System;
using System.Reflection;
using MyFramework.Utility;


namespace MyFramework
{
    /// <summary>
    /// 序列化类型
    /// </summary>
    public enum ESerializedType
    {
        NO,
        XML,
        JOSN,
        BINARY
    }

    /// <summary>
    /// 日志记录的源
    /// </summary>
    public enum ELogType
    {
        /// <summary>
        /// ORM层
        /// </summary>
        [TextAttribute("ORM Layer")]
        ORM,
        /// <summary>
        /// Entity层
        /// </summary>
        [TextAttribute("Entity Layer")]
        Entity,
        /// <summary>
        /// UI层
        /// </summary>
        [TextAttribute("UI Layer")]
        UI,
        /// <summary>
        /// 底层框架层
        /// </summary>
        [TextAttribute("Framework Layer")]
        Framework
    }

    /// <summary>
    /// 优先级
    /// </summary>
    public enum EPriority
    {
        /// <summary>
        /// 高
        /// </summary>
        High = 16,
        /// <summary>
        /// 普通
        /// </summary>
        Normal = 10,
        /// <summary>
        /// 低
        /// </summary>
        Low = 1,
    }

    /// <summary>
    /// 数据库操作标记
    /// </summary>
    public enum EDBOperationTag
    {
        /// <summary>
        /// 忽略
        /// </summary>
        Ignore,
        /// <summary>
        /// 新添加
        /// </summary>
        AddNew,
        /// <summary>
        /// 待更新
        /// </summary>
        Update,
        /// <summary>
        /// 待删除
        /// </summary>
        Delete

    }

    /// <summary>
    /// 控件属性名称
    /// </summary>
    public enum EControlProperts
    {
        Text,
        Checked,
        Enabled,
        DataSource,
        ImageUrl,
    }

}
