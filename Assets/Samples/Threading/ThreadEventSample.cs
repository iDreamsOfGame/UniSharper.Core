using UniSharper.Threading.Event;

namespace UniSharper.Samples
{
    /// <summary>
    /// 线程事件示例
    /// </summary>
    public class ThreadEventSample : ThreadEvent
    {
        #region Constructors

        public ThreadEventSample(Type eventType, object context = null)
            : base(eventType, context)
        {
        }

        #endregion Constructors

        #region Enums

        public enum Type
        {
            ShowText
        }

        #endregion Enums

        #region Properties

        public string Content { get; set; }

        #endregion Properties
    }
}