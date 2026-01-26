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
            GetAusfKey_With_DaModellNrDelegate cb
        );

        [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void AddKey(AddKeyDelegate cb);

        //Get called from C++
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetProgramNameDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringGlobalDelegate(long key, string defaultValue);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr RegisterGetAusfKey_With_DaModellNrDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long GetAusfKey_With_DaModellNrDelegate(IntPtr obj, long value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AddKeyDelegate(long key);

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

        static GetAusfKey_With_DaModellNrDelegate getAusfKey_With_DaModellNrCallback = (
            obj,
            value
        ) =>
        {
            var key = value;

            long prefix = 180000;
            //Key Database
            if (key == 2611)
            {
                long result = long.Parse(prefix.ToString()[0..3] + key.ToString());
                return result;
            }

            return 0;
        };
        AddKeyDelegate addKeyCallback = (long key) =>
        {
            long prefix = 180000;
            long result = long.Parse(prefix.ToString()[0..3] + key.ToString());

            var foundValue = "TEST_FOUND";

            Console.WriteLine($"addKeyCallback c#: {result}");
        };

        private void RegisterCallbacks()
        {
            // Register callback with C++
            DA_SetGetProgramNameCallback(getProgramNameDelegate);
            DA_GetStringGlobalCallback(getStringGlobalCallback);
            RegisterGetAusfKey_With_DaModellNrCallback(getAusfKey_With_DaModellNrCallback);
            AddKey(addKeyCallback);
        }

        public void Run()
        {
            Test();
        }
    }
}
