#pragma once

#include "FinsPLC/fins.h"
#include <vcclr.h>
#include <msclr/marshal_cppstd.h>

using namespace msclr::interop;  
using namespace System;

namespace OmronPlc
{
	public ref class FinsProxy
	{
	public:
		FinsProxy(String^ ipaddr);
		~FinsProxy(void);

		bool Connect();

		void Close();
		//void SetRemote(String^ ipaddr, uint16_t port)
		//{
		//	//std::string转cli的string  
		//	//String^ stdToCli = marshal_as<String^>(strOld); 

		//	//cli的string转std::string  
		//	//std::string cliToStd = marshal_as<std::string>(strNew);  
		//}

		bool ReadDM(uint16_t address, uint16_t %value);
		bool ReadDM(uint16_t address, uint8_t data[], uint16_t count);
		bool ReadDM(uint16_t address, uint16_t data[], uint16_t count);
		bool WriteDM(uint16_t address, uint8_t data[], uint16_t count);
		bool WriteDM(uint16_t address, const uint16_t data);

	private:
		Fins *_fins;
	};
}

