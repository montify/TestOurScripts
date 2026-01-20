#include <iostream>
#include "DHMock.cpp"

long VI_TA_TEXTURE_KEY = 200;
long VI_TA_GLAS_KEY = 201;



class NXAA
{
public:
    const char* Run() {
        WriteKey(VI_TA_TEXTURE_KEY, "asdadd");
        //WriteKey(VI_TA_GLAS_KEY, "2");
       
        std::string prog = DA_GetProgramm();
  
     

        return "3";
    }
};


