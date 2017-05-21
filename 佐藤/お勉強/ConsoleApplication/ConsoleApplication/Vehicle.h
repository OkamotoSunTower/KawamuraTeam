#ifndef _Vehicle_H_
#define _Vehicle_H_

#include <string>
#include <iostream>
using namespace std;

// 乗り物クラス.
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

	// 名前.
	string Name;
	// 速度.
	float Speed;

	// 積載量.
	//int Loadage;

	// 最大燃料.
	double MaxFuel;
	// 燃料.
	double Fuel;
	// 燃費.
	double FuelExpenses;

	// 燃料消費.
	void Consumption(){ Fuel -= FuelExpenses; }
public:
	// コンストラクタ.
	CVehicle(string name);
	// デストラクタ.
	virtual ~CVehicle();

	// 名前関係.
	void SetName(string name){ Name = Name; }
	string GetName(){ return Name; }

	// 速度関係.
	void SetSpeed(float speed){ Speed = speed; }
	float GetSpeed(){ return Speed; }

	// 加速.
	void Acceleration(float Add);
	// 減速.
	void Vrake(float Down);

	// 最大燃料.
	double GetMAxFuel(){ return MaxFuel; }
	// 燃料.
	double GetFuel(){ return Fuel; }
	// 燃費.
	double GetFuelExpenses(){ return FuelExpenses; }

	// 燃料補給.
	void Refuel(double fuel);
};
#endif _Vehicle_H_