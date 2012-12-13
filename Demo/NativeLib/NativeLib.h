// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the NATIVELIB_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// NATIVELIB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef NATIVELIB_EXPORTS
#define NATIVELIB_API __declspec(dllexport)
#else
#define NATIVELIB_API __declspec(dllimport)
#endif

// This class is exported from the NativeLib.dll
class NATIVELIB_API CNativeLib {
public:
	CNativeLib(void);
	// TODO: add your methods here.
};

extern NATIVELIB_API int nNativeLib;

NATIVELIB_API int fnNativeLib(void);
