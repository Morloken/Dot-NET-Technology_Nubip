// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
extern "C" {

    __declspec(dllexport) int FindMax(int* arr, int size) {
        if (size <= 0) return 0;
        int maxVal = arr[0];
        for (int i = 1; i < size; i++) {
            if (arr[i] > maxVal) {
                maxVal = arr[i];
            }
        }
        return maxVal;
    }

    __declspec(dllexport) int HexCharToDec(char hex) {
        if (hex >= '0' && hex <= '9') return hex - '0';
        if (hex >= 'A' && hex <= 'F') return hex - 'A' + 10;
        if (hex >= 'a' && hex <= 'f') return hex - 'a' + 10;
        return -1; // якщо символ некоректний
    }
}


