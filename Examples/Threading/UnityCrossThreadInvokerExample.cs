﻿using System.Threading;
using UniSharper.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace UniSharper.Examples.Threading
{
    internal class UnityCrossThreadInvokerExample : MonoBehaviour
    {
        [SerializeField]
        private Text text1 = null;

        [SerializeField]
        private Text text2 = null;

        [SerializeField]
        private Text text3 = null;
        
        private void Start()
        {
            UnityCrossThreadInvoker.Initialize();
            InvokeWithParametersExample();
            InvokeWithoutParametersExample();
            InvokeAnonymousMethodExample();
        }

        private void InvokeAnonymousMethodExample()
        {
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(0);
                UnityCrossThreadInvoker.Invoke(
                    () => { text1.text = "I am anonymous method coming from other thread!"; });
            })).Start();
        }

        private void InvokeWithoutParametersExample()
        {
            new Thread(() =>
            {
                Thread.Sleep(2000);
                UnityCrossThreadInvoker.Invoke(InternalShowText);
            }).Start();
        }

        private void InternalShowText()
        {
            text2.text = "I am private method coming from other thread!";
        }

        private void InvokeWithParametersExample()
        {
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(3000);
                UnityCrossThreadInvoker.Invoke(InternalShowTextWithParameters, "I am method with parameters coming from other thread!");
            })).Start();
        }

        private void InternalShowTextWithParameters(params object[] parameters)
        {
            if (parameters == null) 
                return;
            
            var content = parameters[0] as string;
            text3.text = content;
        }
    }
}