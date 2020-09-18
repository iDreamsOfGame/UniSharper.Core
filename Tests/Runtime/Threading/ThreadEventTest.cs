using UniSharper.Threading.Events;

namespace UniSharper.Tests.Threading
{
    /// <summary>
    /// 线程事件示例
    /// </summary>
    public class ThreadEventTest : ThreadEvent
    {
        public enum Type
        {
            ShowText
        }
        
        public ThreadEventTest(Type eventType, object context = null) 
            : base(eventType, context)
        {
        }

        public string Content { get; set; }
    }
}