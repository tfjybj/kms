/*
 * 创建人：盖鹏军
 * 时间：2022年2月20日10点30分
 * 描述：事件策略模式父类
 */
namespace dingdingsuccess.EventStrategy
{
    /// <summary>
    /// 事件策略模式父类
    /// </summary>
    public abstract class EventType
    {
        public abstract void ActEvent(string eventContent);
    }
}