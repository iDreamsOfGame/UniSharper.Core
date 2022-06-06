using UniSharper.Threading.Event;

namespace UniSharper.Samples
{
    /// <summary>
    /// 线程事件示例
    /// </summary>
    public class ThreadEventSample : ThreadEvent
    {
        public ThreadEventSample(Type eventType, object context = null)
            : base(eventType, context)
        {
        }

        public enum Type
        {
            ShowText
        }

        public string Content { get; set; }
    }
}