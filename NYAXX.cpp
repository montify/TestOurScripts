#include <iostream>
#include "DHMock.cpp"

const char *VI_MANUFACTURER = "22";
long VI_XX_ECKRADIUS_S = 2611;
long VI_XX_Tisch_S = 600;
class NXAA
{
    typedef std::string string;

public:
    const char *Run()
    {
        // const string manu = GetStringGlobal(VI_MANUFACTURER, "AA");

        long TischplatteKey = GetAusfKey_With_DaModellNr(this, VI_XX_ECKRADIUS_S); // 2611
        std::cout << "TischplatteKey: " << TischplatteKey << "\n";

        string Tischplatte = GetStringGlobal(TischplatteKey, "tt");
        std::cout << "Tischplatte: " + Tischplatte << "\n";

        AddKey(TischplatteKey);
        AddKey(VI_XX_Tisch_S);
        return "3";
    }
};
