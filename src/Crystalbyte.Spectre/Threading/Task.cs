#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading{
    internal sealed class Task : OwnedRefCountedNativeObject{
        private readonly Action _action;
        private readonly ExecuteCallback _executeCallback;

        public Task(Action action)
            : base(typeof (CefTask)){
            _action = action;
            _executeCallback = OnExecute;
            MarshalToNative(new CefTask{
                                           Base = DedicatedBase,
                                           Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
                                       });
        }

        private void OnExecute(IntPtr self, CefThreadId threadid){
            if (_action != null){
                _action();
            }
        }
    }
}