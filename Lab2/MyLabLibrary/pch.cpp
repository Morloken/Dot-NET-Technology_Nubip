// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"

// When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
extern "C" {
	__declspec(dllexport) int FindMax(int* arr, int size);
	__declspec(dllexport) int HexCharToDec(char hex);
}