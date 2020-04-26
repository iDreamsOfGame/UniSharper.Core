// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Concurrent;
using UniSharper.Patterns;

namespace UniSharper.Threading
{
    /// <summary>
    /// A <see cref="UnityCrossThreadInvoker"/> representing a <see cref="UnityEngine.MonoBehaviour"/> to
    /// invoke methods in other threads from unity main thread.
    /// Implements the <see cref="UnityCrossThreadInvoker"/>
    /// </summary>
    /// <seealso cref="UnityCrossThreadInvoker"/>
    public sealed class UnityCrossThreadInvoker : SingletonMonoBehaviour<UnityCrossThreadInvoker>
    {
        private ConcurrentQueue<Action> executionQueue;
        
        private ConcurrentQueue<Action<object[]>> executionWithParametersQueue;

        private ConcurrentDictionary<Action<object[]>, object[]> methodParameterMap;

        /// <summary>
        /// Initialize.
        /// </summary>
        public static void Initialize()
        {
            Instance.InternalInitialize();
        }

        /// <summary>
        /// Invoke method without parameters in other thread.
        /// </summary>
        /// <param name="method">The method without parameters in other thread to be executed. </param>
        public static void Invoke(Action method)
        {
            Instance.executionQueue.Enqueue(method);
        }

        /// <summary>
        /// Invoke method with parameters in other thread.
        /// </summary>
        /// <param name="method">The method with parameters in other thread to be executed. </param>
        /// <param name="parameters">The parameters of method. </param>
        public static void Invoke(Action<object[]> method, params object[] parameters)
        {
            Instance.executionWithParametersQueue.Enqueue(method);

            if (parameters != null)
            {
                Instance.methodParameterMap.TryAdd(method, parameters);
            }
        }

        private void InternalInitialize()
        {
            // Do nothing, just for instantiating GameObject and add component.
        }

        private void Awake()
        {
            executionQueue = new ConcurrentQueue<Action>();
            executionWithParametersQueue = new ConcurrentQueue<Action<object[]>>();
            methodParameterMap = new ConcurrentDictionary<Action<object[]>, object[]>();
        }

        private void Update()
        {
            InvokeMethod();
            InvokeMethodWithParameters();
        }

        private void InvokeMethod()
        {
            var result = executionQueue.TryDequeue(out var method);
            if (!result) 
                return;
            
            method.Invoke();
        }

        private void InvokeMethodWithParameters()
        {
            var result = executionWithParametersQueue.TryDequeue(out var method);
            if (!result) 
                return;
            
            var hasParameters = methodParameterMap.TryGetValue(method, out var parameters);
            method.Invoke(parameters);

            if (hasParameters)
            {
                methodParameterMap.TryRemove(method, out parameters);
            }
        }
    }
}