// ConsoleApplication.cpp : �R���\�[�� �A�v���P�[�V�����̃G���g�� �|�C���g���`���܂��B
//

#include "stdafx.h"

#include "Ship.h"

int _tmain(int argc, _TCHAR* argv[])
{
	CShip * ship = NULL;
	ship = new CShip("�D");

	delete ship;

	return 0;
}

