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
		//	//std::stringתcli��string  
		//	//String^ stdToCli = marshal_as<String^>(strOld); 

		//	//cli��stringתstd::string  
		//	//std::string cliToStd = marshal_as<std::string>(strNew);  
		//}

		bool ReadDM(uint16_t address, uint16_t %value);
		bool ReadDM(uint16_t address, uint32_t %value);
		bool ReadDM(uint16_t address, array<uint8_t>^ data); 
		bool ReadDM(uint16_t address, array<uint16_t>^ data);
		bool WriteDM(uint16_t address, array<uint8_t>^ data);
		bool WriteDM(uint16_t address, const uint16_t data);

	private:
		Fins *_fins;
	};
}

