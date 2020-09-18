using System.Threading;
using UniSharper.Threading.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Tests.Threading
{
    public class ThreadEventDispatcherTest : MonoBehaviour
    {
        [SerializeField]
        private Text textComponent = null;

        private IThreadEventDispatcher dispatcher;

        private void Start()
        {
            dispatcher = new ThreadEventDispatcher();
            dispatcher.AddEventListener(ThreadEventTest.Type.ShowText, OnShowText);
            ThreadPool.QueueUserWorkItem(state => ShowTextContent());
        }

        private void ShowTextContent() 
        {
            Thread.Sleep(1000);
            dispatcher.DispatchEvent(new ThreadEventTest(ThreadEventTest.Type.ShowText) { Content = $"Message come from another thread!"});
        }
        
        private void OnShowText(ThreadEvent e)
        {
            var evt = e as ThreadEventTest;
            textComponent.text = evt.Content;
            dispatcher.RemoveAllEventListeners();
        }
    }
}