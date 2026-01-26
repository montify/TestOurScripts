using System.Runtime.InteropServices;

namespace TestOurScripts
{
    internal class Bridge
    {
        public Bridge()
        {
            RegisterCallbacks();
        }

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr Test();

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void DA_SetGetProgramNameCallback(GetProgramNameDelegate cb);

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void DA_GetStringGlobalCallback(GetStringGlobalDelegate cb);

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void RegisterGetAusfKey_With_DaModellNrCallback(
            GetAusfKey_With_DaModellNrCallback cb
        );

        //Get called from C++
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetProgramNameDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringGlobalDelegate(long key, string defaultValue);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr RegisterGetAusfKey_With_DaModellNrDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long GetAusfKey_With_DaModellNrCallback(IntPtr obj, long value);

        GetProgramNameDelegate getProgramNameDelegate = () =>
        {
            return Marshal.StringToHGlobalAnsi("Was ge");
        };
        GetStringGlobalDelegate getStringGlobalCallback = (long key, string defaultValue) =>
        {
            //    Console.WriteLine($"getStringGlobal c#: {key}");

            var foundValue = "TEST_FOUND";
            return Marshal.StringToHGlobalAnsi(foundValue);
        };

        static GetAusfKey_With_DaModellNrCallback getAusfKey_With_DaModellNrCallback = (
            obj,
            value
        ) =>
        {
            var key = value;

            //Key Database
            if (key == 2611)
                return key;

            return 0;
        };

        private void RegisterCallbacks()
        {
            // Register callback with C++
            DA_SetGetProgramNameCallback(getProgramNameDelegate);
            DA_GetStringGlobalCallback(getStringGlobalCallback);
            RegisterGetAusfKey_With_DaModellNrCallback(getAusfKey_With_DaModellNrCallback);
        }

        public void Run()
        {
            Test();
        }
    }
}
