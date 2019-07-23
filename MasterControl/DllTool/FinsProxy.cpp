#include "StdAfx.h"
#include "FinsProxy.h"

using namespace OmronPlc;

FinsProxy::FinsProxy(String^ ipaddr)
{
	_fins = new Fins(Udp);
	_fins->SetRemote(marshal_as<std::string>(ipaddr));
}

FinsProxy::~FinsProxy(void)
{
	delete _fins;
	_fins = NULL;
}

bool FinsProxy::Connect()
{
	return _fins->Connect();
}

void FinsProxy::Close()
{
	_fins->Close();
}

bool FinsProxy::ReadDM(uint16_t address, uint16_t %value)
{
	uint16_t data = 0;
	bool ret = _fins->ReadDM(address, data);
	value = data;
	return ret;
}
	
bool FinsProxy::ReadDM(uint16_t address, uint8_t data[], uint16_t count)
{
	return _fins->ReadDM(address, data, count);
}

bool FinsProxy::ReadDM(uint16_t address, uint16_t data[], uint16_t count)
{
	return _fins->ReadDM(address, data, count);
}

bool FinsProxy:: WriteDM(uint16_t address, uint8_t data[], uint16_t count)
{
	bool ret = _fins->WriteDM(address, data, count, true);
	return ret;
}

bool FinsProxy::WriteDM(uint16_t address, const uint16_t data)
{
	bool ret = _fins->WriteDM(address, data);
	return ret;
}


