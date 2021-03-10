using System.Threading;
using UniSharper.Threading.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Samples
{
    public class ThreadEventDispatcherSample : MonoBehaviour
    {
        #region Fields

        private IThreadEventDispatcher dispatcher;

        [SerializeField]
        private Text textComponent = null;

        #endregion Fields

        #region Methods

        private void OnShowText(ThreadEvent e)
        {
            var evt = e as ThreadEventSample;
            textComponent.text = evt.Content;
            dispatcher.RemoveAllEventListeners();
        }

        private void ShowTextContent()
        {
            Thread.Sleep(1000);
            dispatcher.DispatchEvent(new ThreadEventSample(ThreadEventSample.Type.ShowText) { Content = $"Message come from another thread!" });
        }

        private void Start()
        {
            dispatcher = new ThreadEventDispatcher();
            dispatcher.AddEventListener(ThreadEventSample.Type.ShowText, OnShowText);
            ThreadPool.QueueUserWorkItem(state => ShowTextContent());
        }

        #endregion Methods
    }
}