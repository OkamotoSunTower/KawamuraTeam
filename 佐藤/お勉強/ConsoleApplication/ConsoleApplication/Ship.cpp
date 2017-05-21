#include "stdafx.h"
#include "Ship.h"

// 派生クラスのコンストラクタでは明示しない限り.
// 親クラスのデフォルトコンストラクタが呼び出されるが.
// それがない状況で発生するエラー.
// https://www.trivia.work/?p=17
CShip::CShip(string name) : CVehicle(name)
{
	Name = name;
	State = STATE::WAIT;
	cout << Name << "インスタンス生成" << endl << endl;
}

CShip::~CShip()
{
	cout << Name << "インスタンス破棄" << endl << endl;
}

// http://cpp-lang.sevendays-study.com/day6.html