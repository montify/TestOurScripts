namespace TA7{
    namespace Utils{
        std::string GetShort(std::string str)
        {
            return str;
        }
    }
}

typedef void (*WriteKeyCallback)(int, const char*);
typedef void (*ReadKeyCallback)(int);
typedef void (*SetProgrammCallback)(const char*);

static WriteKeyCallback writeKeyCallback = nullptr;
static ReadKeyCallback readKeyCallback = nullptr;
static SetProgrammCallback programmCallback = nullptr;

extern "C" __declspec(dllexport) void RegisterWriteKeyCallback(WriteKeyCallback callback) {
    writeKeyCallback = callback;
}
extern "C" __declspec(dllexport) void RegisterReadKeyCallback(ReadKeyCallback callback) {
    readKeyCallback = callback;
}

extern "C" __declspec(dllexport) void RegisterProgrammCallback(SetProgrammCallback callback) {
    programmCallback = callback;
}



void WriteKey(int key, const char* s)
{
    if (writeKeyCallback)
        writeKeyCallback(key, s);
}

void ReadKey(int key)
{
    if (readKeyCallback)
        readKeyCallback(key);
}

void LOGCL(const char* msg)
{
    //return to C#

   
}

// C-compatible function pointer type
typedef const char* (*GetProgramNameCallback)();
// Store callback
static GetProgramNameCallback g_getProgramName = nullptr;

// Called by C# to register callback
extern "C" __declspec(dllexport) void DA_SetGetProgramNameCallback(GetProgramNameCallback cb) {
    g_getProgramName = cb;
}


extern "C" __declspec(dllexport)
const char* DA_GetProgramm()
{
    if (g_getProgramName)
        return g_getProgramName(); 

    return "No callback set";
}
