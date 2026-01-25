using System.Runtime.InteropServices;

namespace TestOurScripts
{
    internal class Bridge
    {
        public Bridge()
        {
            TEst();
        }

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr Test();

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void DA_SetGetProgramNameCallback(GetProgramNameDelegate cb);

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void DA_GetStringGlobalCallback(GetStringGlobalDelegate cb);

        //Get called from C++
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetProgramNameDelegate();

        static IntPtr ExposeProgrammname()
        {
            return Marshal.StringToHGlobalAnsi("Was ge");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringGlobalDelegate();

        static IntPtr GetStringGlobalFn()
        {
            return Marshal.StringToHGlobalAnsi("test");
        }

        GetProgramNameDelegate getProgramNameDelegate = ExposeProgrammname;
        GetStringGlobalDelegate getStringGlobalCallback = GetStringGlobalFn;

        private void TEst()
        {
            // Register callback with C++
            DA_SetGetProgramNameCallback(getProgramNameDelegate);
            DA_GetStringGlobalCallback(getStringGlobalCallback);
        }

        public void Run()
        {
            Test();
        }
    }
}
