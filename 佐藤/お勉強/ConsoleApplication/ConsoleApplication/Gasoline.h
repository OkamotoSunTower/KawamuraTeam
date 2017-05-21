#ifndef _Gasoline_H_
#define _Gasoline_H_

#include <string>
#include <random>

using namespace std;

// �ÓI�N���X.
// �����o�����ׂĐÓI�ł���N���X.
// �C���X�^���X������K�v�����Ȃ�.
// http://ppp-lab.sakura.ne.jp/ProgrammingPlacePlus/cpp/language/015.html#static_class
static class Gasoline
{
private:
	// ���i.
	int Price;
	const int MIM_PRICE = 80;
	const int MAX_PRICE = 150;

	// �R���X�g���N�^�����J�ɂ��邱�ƂŃC���X�^���X������邱�Ƃ�h��.
	Gasoline();
public:
	// ���i.
	int GetPrice(){ return Price; }
	// ���i�ϓ�.
	void PriceChange()
	{
		// C++11�̃����_������.
		// http://vivi.dyndns.org/tech/cpp/random.html
		uniform_int_distribution<> Price(MIM_PRICE, MAX_PRICE);
	}
};

#endif _Gasoline_H_