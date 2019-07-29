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
	
bool FinsProxy::ReadDM(uint16_t address, uint32_t %value)
{
	uint32_t data = 0;
	//bool ret = _fins->ReadDM(address, value);
	data = _fins->ReadDM(address);
	value = data;
	return true;
}

bool FinsProxy::ReadDM(uint16_t address, array<uint8_t>^ data)
{
	uint16_t count = data->Length;
	uint8_t *temp = new uint8_t[count];
	bool ret = _fins->ReadDM(address, temp, count);
	if (ret) {
		for (int i = 0; i < count; i++) {
			data[i] = temp[i];
		}
	}

	delete []temp;
	return ret;
}

bool FinsProxy::ReadDM(uint16_t address, array<uint16_t>^ data)
{
	uint16_t count = data->Length;
	uint16_t *temp = new uint16_t[count];
	bool ret = _fins->ReadDM(address, temp, count);
	if (ret) {
		for (int i = 0; i < count; i++) {
			data[i] = temp[i];
		}
	}

	delete []temp;
	return ret;
}

bool FinsProxy::WriteDM(uint16_t address, array<uint8_t>^ data)
{
	uint16_t count = data->Length;
	uint8_t *temp = new uint8_t[count];
	for (int i = 0; i < count; i++) {
		temp[i] = data[i];
	}

	bool ret = _fins->WriteDM(address, temp, count, true);

	delete []temp;
	return ret;
}

bool FinsProxy::WriteDM(uint16_t address, const uint16_t data)
{
	bool ret = _fins->WriteDM(address, data);
	return ret;
}


