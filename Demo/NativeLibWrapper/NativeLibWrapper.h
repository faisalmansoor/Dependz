// NativeLibWrapper.h

#pragma once

using namespace System;

#include "NativeLib.h"

namespace NativeLibWrapper {

	public ref class NativeLibWrapper
	{
	public:
		void UseNativeLib()
		{
			fnNativeLib();
		}
	};
}
