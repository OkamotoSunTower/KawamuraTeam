// ConsoleApplication.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"

#include "Ship.h"

int _tmain(int argc, _TCHAR* argv[])
{
	CShip * ship = NULL;
	ship = new CShip("船");

	delete ship;

	return 0;
}

