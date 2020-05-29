using System.Threading;
using UniSharper.Threading.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Examples.Threading
{
    public class ThreadEventDispatcherExample : MonoBehaviour
    {
        [SerializeField]
        private Text textComponent = null;

        private IThreadEventDispatcher dispatcher;

        private void Start()
        {
            dispatcher = new ThreadEventDispatcher();
            dispatcher.AddEventListener(ThreadEventExample.Type.ShowText, OnShowText);
            ThreadPool.QueueUserWorkItem(state => ShowTextContent());
        }

        private void ShowTextContent() 
        {
            Thread.Sleep(1000);
            dispatcher.DispatchEvent(new ThreadEventExample(ThreadEventExample.Type.ShowText) { Content = $"Message come from another thread!"});
        }
        
        private void OnShowText(ThreadEvent e)
        {
            var evt = e as ThreadEventExample;
            textComponent.text = evt.Content;
            dispatcher.RemoveAllEventListeners();
        }
    }
}