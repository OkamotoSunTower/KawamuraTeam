#include "stdafx.h"
#include "Vehicle.h"

//コンストラクタ.
CVehicle::CVehicle(string name)
{
	Name = name;
	cout << Name << "の親インスタンス生成" << endl << endl;
}

CVehicle::~CVehicle()
{
	cout << Name << "の親インスタンス破棄" << endl << endl;
}

void CVehicle::Acceleration(float Add)
{
	Speed += Add;
}

void CVehicle::Vrake(float Down)
{
	Speed -= Down;
}

