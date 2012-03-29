using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Crystalbyte.Chocolate.TestSuite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Size of int: {0}", Marshal.SizeOf(typeof(int))));
            Console.WriteLine(string.Format("Size of string: {0}", Marshal.SizeOf(typeof(Str))));
            Console.WriteLine(string.Format("Size of IntPtr: {0}", Marshal.SizeOf(typeof(IntPtr))));
            Console.WriteLine(string.Format("Size of bool: {0}", Marshal.SizeOf(typeof(bool))));
            Console.WriteLine(string.Format("Size of uint: {0}", Marshal.SizeOf(typeof(uint))));
            Console.WriteLine(string.Format("Size of CefStringUtf16: {0}", Marshal.SizeOf(typeof(CefStringUtf16))));
            Console.Read();
        }

        struct Str {
            [MarshalAs(UnmanagedType.LPWStr)]
            private string _s;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CefStringUtf16
        {
            public IntPtr Str;
            public int Length;
            public IntPtr Dtor;
        }
    }
}
