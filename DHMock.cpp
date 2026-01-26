#include <cstring>

typedef void (*WriteKeyCallback)(int, const char *);
typedef void (*ReadKeyCallback)(int);
typedef void (*SetProgrammCallback)(const char *);

static WriteKeyCallback writeKeyCallback = nullptr;
static ReadKeyCallback readKeyCallback = nullptr;
static SetProgrammCallback programmCallback = nullptr;

// Write values to c to c#
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

// Read values from c# to c
typedef const char *(*GetProgramNameCallback)();
static GetProgramNameCallback g_getProgramName = nullptr;
extern "C" __declspec(dllexport) void DA_SetGetProgramNameCallback(GetProgramNameCallback cb)
{
    g_getProgramName = cb;
}

typedef const char *(*GetStringGlobalCallback)(long key, const char *defaultReturnValue);
static GetStringGlobalCallback g_GetStringGlobal = nullptr;
extern "C" __declspec(dllexport) void DA_GetStringGlobalCallback(GetStringGlobalCallback cb)
{
    g_GetStringGlobal = cb;
}

typedef long (*GetAusfKey_With_DaModellNrCallback)(void *obj, long value);
static GetAusfKey_With_DaModellNrCallback g_GetAusfKey = nullptr;

extern "C" __declspec(dllexport) void RegisterGetAusfKey_With_DaModellNrCallback(GetAusfKey_With_DaModellNrCallback cb)
{
    g_GetAusfKey = cb;
}

typedef long (*AddKeyCallback)(long key);
static AddKeyCallback g_AddKey = nullptr;

extern "C" __declspec(dllexport) void AddKey(AddKeyCallback cb)
{
    g_AddKey = cb;
}

typedef long (*AddStringCallback)(long key, const char *value);
static AddStringCallback g_AddString = nullptr;

extern "C" __declspec(dllexport) void AddString(AddStringCallback cb)
{
    g_AddString = cb;
}

const long GetAusfKey_With_DaModellNr(void *obj, long value)
{
    if (g_GetAusfKey)
        return g_GetAusfKey(obj, value);

    return 0;
}

extern "C" __declspec(dllexport)
const char *
DA_GetProgramm()
{
    if (g_getProgramName)
        return g_getProgramName();

    return "No callback set";
}

const char *GetStringGlobal(long key, const char *defaultReturnValue)
{
    if (g_getProgramName)
        return g_GetStringGlobal(key, defaultReturnValue);

    return "No callback set";
}

void AddKey(long key)
{
    if (g_AddKey)
        g_AddKey(key);
}

void AddString(long key, const char *value)
{
    if (g_AddString)
        g_AddString(key, value);
}
