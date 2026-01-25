#include <cstring>

namespace TA7
{
    namespace Utils
    {
        std::string GetShort(std::string str)
        {
            return str;
        }
    }
}

typedef void (*WriteKeyCallback)(int, const char *);
typedef void (*ReadKeyCallback)(int);
typedef void (*SetProgrammCallback)(const char *);

static WriteKeyCallback writeKeyCallback = nullptr;
static ReadKeyCallback readKeyCallback = nullptr;
static SetProgrammCallback programmCallback = nullptr;

// Write values to c#
extern "C" __declspec(dllexport) void RegisterWriteKeyCallback(WriteKeyCallback callback)
{
    writeKeyCallback = callback;
}
extern "C" __declspec(dllexport) void RegisterReadKeyCallback(ReadKeyCallback callback)
{
    readKeyCallback = callback;
}

extern "C" __declspec(dllexport) void RegisterProgrammCallback(SetProgrammCallback callback)
{
    programmCallback = callback;
}

typedef const char *(*GetProgramNameCallback)();
static GetProgramNameCallback g_getProgramName = nullptr;
extern "C" __declspec(dllexport) void DA_SetGetProgramNameCallback(GetProgramNameCallback cb)
{
    g_getProgramName = cb;
}

typedef const char *(*GetStringGlobalCallback)();
static GetStringGlobalCallback g_GetStringGlobal = nullptr;
extern "C" __declspec(dllexport) void DA_GetStringGlobalCallback(GetStringGlobalCallback cb)
{
    g_GetStringGlobal = cb;
}

// Read values from c#
extern "C" __declspec(dllexport)
const char *
DA_GetProgramm()
{
    if (g_getProgramName)
        return g_getProgramName();

    return "No callback set";
}

const char *GetStringGlobal(const char *key, const char *defaultReturnValue)
{
    const char *result = g_GetStringGlobal();

    if (result != nullptr && strlen(result) == 0)
        return defaultReturnValue;
    else
        return result;
}
