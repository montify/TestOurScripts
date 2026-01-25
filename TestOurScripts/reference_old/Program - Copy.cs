//using System.Reflection;
//using System.Runtime.InteropServices;
//using ExcelReader;
//using TestOurScripts;

////MySys2 commands:

//// C:/msys64/mingw64.exe
//// cd /c/Users/Alex/source/TestOurScripts/   <-- this is the Solution Path
//// g++ -m64 -shared -static -o test.dll test.cpp && cp -f test.dll TestOurScripts/bin/Debug/net7.0/

//class TestClass
//{
//    static Dictionary<string, long> Dh_Keys = new Dictionary<string, long>();
//    static Dictionary<long, object> Dh_Keys_Storage = new Dictionary<long, object>();

//    static void ReflectKeys()
//    {
//        var fields = typeof(KeyList)
//            .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
//            .Where(f => f.IsStatic && f.IsInitOnly);

//        foreach (var field in fields)
//        {
//            var value = field.GetValue(null); // null for static fields
//            Console.WriteLine($"{field.Name} = {value}");

//            if (!long.TryParse(value.ToString(), out var longValue))
//            {
//                throw new Exception("Cant parse Value");
//            }

//            Dh_Keys.Add(field.Name, longValue);
//            Dh_Keys_Storage.Add(longValue, null);
//        }

//        Console.WriteLine();
//    }

//    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
//    static extern IntPtr Test();

//    // 1. Define the delegate signature
//    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
//    public delegate int WriteKeyDelegate(int key, [MarshalAs(UnmanagedType.LPStr)] string message);
//    public delegate void ReadKeyDelegate(int key);
//    public delegate void SetProgrammDelegate(string programmName);

//    // 2. Import the two functions
//    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
//    static extern void RegisterWriteKeyCallback(WriteKeyDelegate callback);

//    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
//    static extern void RegisterReadKeyCallback(ReadKeyDelegate callback);

//    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
//    static extern void DA_SetGetProgramNameCallback(GetProgramNameDelegate cb);

//    [DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
//    static extern IntPtr DA_GetProgramm(string programName);

//    static string ProgrammName = "TestProgrammXX";

//    static void AddKey(int key)
//    {
//        //if (Dh_Keys_Storage.ContainsKey(key))
//        //{
//        //    throw new Exception("Cant cant be added twice");
//        //}

//        //Dh_Keys_Storage.Add(key, null);
//    }

//    static WriteKeyDelegate writeKeyDelegate = (key, msg) =>
//    {
//        if (!Dh_Keys_Storage.ContainsKey(key))
//        {
//            Console.WriteLine($"Unknown key {key}");
//            return -1;
//        }

//        Dh_Keys_Storage[key] = msg;
//        Console.WriteLine($"Write key {key} with value: {msg}");
//        return 0;
//    };

//    static ReadKeyDelegate readKeyDelegate = (key) =>
//    {
//        if (!Dh_Keys_Storage.TryGetValue(key, out var dhKey))
//            return;

//        var msg = dhKey;

//        if (msg == null)
//            throw new Exception("Try Read Key, but its null!");

//        Console.WriteLine($"ReadKey: {key} : {msg}");
//    };

//    //Get called from C++
//    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
//    public delegate IntPtr GetProgramNameDelegate();

//    static IntPtr ExposeProgrammname()
//    {
//        return Marshal.StringToHGlobalAnsi(ProgrammName);
//    }

//    //We need more
//    // ExposeGetDimX(),...

//    static void Main(string[] args)
//    {
//        //ReflectKeys();
//        //AddKey(5015135);

//        //RegisterWriteKeyCallback(writeKeyDelegate);
//        //RegisterReadKeyCallback(readKeyDelegate);

//        //GetProgramNameDelegate callback = ExposeProgrammname;

//        //// Register callback with C++
//        //DA_SetGetProgramNameCallback(callback);

//        //// Call C++ function (which calls back into C#)
//        //IntPtr result = DA_GetProgramm("Test");
//        //string programName = Marshal.PtrToStringAnsi(result);

//        //IntPtr ptr = Test();

//        //var res = Marshal.PtrToStringAnsi(ptr);

//        //Excel
//        var excelReader = new ExcelReader.ExcelReader();
//        excelReader.GetWorkSheedContent("D:/FurnDebug/BLABLA.xlsx");
//        Console.Read();
//    }
//}
