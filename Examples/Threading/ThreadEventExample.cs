using UniSharper.Threading.Events;

namespace UniSharper.Examples.Threading
{
    /// <summary>
    /// 线程事件示例
    /// </summary>
    public class ThreadEventExample : ThreadEvent
    {
        public enum Type
        {
            ShowText
        }
        
        public ThreadEventExample(Type eventType, object context = null) 
            : base(eventType, context)
        {
        }

        public string Content { get; set; }
    }
}