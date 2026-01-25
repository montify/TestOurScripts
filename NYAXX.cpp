#include <iostream>
#include "DHMock.cpp"

const char *VI_MANUFACTURER = "22";

class NXAA
{
    typedef std::string string;

public:
    const char *Run()
    {
        const string manu = GetStringGlobal(VI_MANUFACTURER, "AA");
        std::cout << manu;
        return "3";
    }
};
