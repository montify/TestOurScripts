using System.Runtime.InteropServices;

//MySys2 commands:

// C:/msys64/mingw64.exe
// cd /c/Users/Alex/source/TestOurScripts/   <-- this is the Solution Path
// g++ -m64 -shared -static -o test.dll test.cpp
// cp -f test.dll TestOurScripts/bin/Debug/net7.0/
class TestClass
{
    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr Test();

    // 1. Define the delegate signature
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int WriteKeyDelegate(int key, string message);
    public delegate void ReadKeyDelegate(int key);
    public delegate void SetProgrammDelegate(string programmName);

    // 2. Import the two functions
    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern void RegisterWriteKeyCallback(WriteKeyDelegate callback);

    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern void RegisterReadKeyCallback(ReadKeyDelegate callback);

    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern void DA_SetGetProgramNameCallback(GetProgramNameDelegate cb);

    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr DA_GetProgramm(string programName);

    static Dictionary<int, object> Dh_Keys = new Dictionary<int, object>();

    static string ProgrammName = "TestProgrammXX";

    static void AddKey(int key)
    {
        if (Dh_Keys.ContainsKey(key))
        {
            throw new Exception("Cant cant be added twice");
            return;
        }

        Dh_Keys.Add(key, null);
    }

    static WriteKeyDelegate writeKeyDelegate = (key, msg) =>
    {
        if (Dh_Keys.ContainsKey(key))
        {
            Dh_Keys[key] = msg;
            Console.WriteLine($"Write key {key} with value: {msg}");
            return key;
        }
        else
        {
            throw new Exception($"Cant find Key {key}");
        }
    };

    static ReadKeyDelegate readKeyDelegate = (key) =>
    {
        if (!Dh_Keys.TryGetValue(key, out var dhKey))
            return;

        var msg = dhKey;

        if (msg == null)
            throw new Exception("Try Read Key, but its null!");

        Console.WriteLine($"ReadKey: {key} : {msg}");
    };

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetProgramNameDelegate();

    //Get called from C++
    static IntPtr ExposeProgrammname()
    {
        return Marshal.StringToHGlobalAnsi(ProgrammName);
    }

    static void Main(string[] args)
    {
        AddKey(5015135);

        RegisterWriteKeyCallback(writeKeyDelegate);
        RegisterReadKeyCallback(readKeyDelegate);

        GetProgramNameDelegate callback = ExposeProgrammname;

        // Register callback with C++
        DA_SetGetProgramNameCallback(callback);

        // Call C++ function (which calls back into C#)
        IntPtr result = DA_GetProgramm("Test");
        string programName = Marshal.PtrToStringAnsi(result);

        IntPtr ptr = Test();

        var res = Marshal.PtrToStringAnsi(ptr);

        Console.Read();
    }
}
