#include <iostream>
#include "DHMock.cpp"

class NXAA
{
public:
    const char* Run() {
        WriteKey(5015135, "10001");
        ReadKey(5015135);


        std::string prog = DA_GetProgramm();


        std::cout << "Programaaam ist: " << prog << "\n";
     

       // LOGCL(prog);
        return "3";
    }
};


