// NativeLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "NativeLib.h"
#include <stdio.h>


// This is an example of an exported variable
NATIVELIB_API int nNativeLib=0;

// This is an example of an exported function.
NATIVELIB_API int fnNativeLib(void)
{
	printf("fnNativeLib called\n");
	return 42;
}

// This is the constructor of a class that has been exported.
// see NativeLib.h for the class definition
CNativeLib::CNativeLib()
{
	return;
}
