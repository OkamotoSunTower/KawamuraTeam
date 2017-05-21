#ifndef _Vehicle_H_
#define _Vehicle_H_

#include <string>
#include <iostream>
using namespace std;

// ��蕨�N���X.
class CVehicle
{
private:

protected:
	enum STATE
	{
		WAIT,
		MOVE,
		BACK
	};

	STATE State;

	// ���O.
	string Name;
	// ���x.
	float Speed;

	// �ύڗ�.
	//int Loadage;

	// �ő�R��.
	double MaxFuel;
	// �R��.
	double Fuel;
	// �R��.
	double FuelExpenses;

	// �R������.
	void Consumption(){ Fuel -= FuelExpenses; }
public:
	// �R���X�g���N�^.
	CVehicle(string name);
	// �f�X�g���N�^.
	virtual ~CVehicle();

	// ���O�֌W.
	void SetName(string name){ Name = Name; }
	string GetName(){ return Name; }

	// ���x�֌W.
	void SetSpeed(float speed){ Speed = speed; }
	float GetSpeed(){ return Speed; }

	// ����.
	void Acceleration(float Add);
	// ����.
	void Vrake(float Down);

	// �ő�R��.
	double GetMAxFuel(){ return MaxFuel; }
	// �R��.
	double GetFuel(){ return Fuel; }
	// �R��.
	double GetFuelExpenses(){ return FuelExpenses; }

	// �R���⋋.
	void Refuel(double fuel);
};
#endif _Vehicle_H_