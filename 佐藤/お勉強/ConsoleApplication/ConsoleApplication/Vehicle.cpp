#include "stdafx.h"
#include "Vehicle.h"

//�R���X�g���N�^.
CVehicle::CVehicle(string name)
{
	Name = name;
	cout << Name << "�̐e�C���X�^���X����" << endl << endl;
}

CVehicle::~CVehicle()
{
	cout << Name << "�̐e�C���X�^���X�j��" << endl << endl;
}

void CVehicle::Acceleration(float Add)
{
	Speed += Add;
}

void CVehicle::Vrake(float Down)
{
	Speed -= Down;
}

