#include <iostream>
#include "NYAXX.cpp"

extern "C" __declspec(dllexport) const char *Test()
{
    NXAA myObject;
    return myObject.Run();
};
