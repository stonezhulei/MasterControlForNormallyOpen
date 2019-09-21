// DllTool.cpp : dll 测试入口，该工程使用 C++ (非 CLR) 导出 dll，不能被 C# 通过“项目”来引用，只能引用 dll 文件
//

#include "stdafx.h"
#include <iostream>
#include "FinsPLC/fins.h"

using namespace std;
using namespace OmronPlc;

int _tmain(int argc, _TCHAR* argv[])
{
	//Fins fins = Fins::CreateExportObj(TransportType::Udp);
	//fins.Connect();
	//fins.Close();
	system("pause");
	return 0;
}

