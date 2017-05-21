#ifndef _Gasoline_H_
#define _Gasoline_H_

#include <string>
#include <random>

using namespace std;

// 静的クラス.
// メンバがすべて静的であるクラス.
// インスタンス化する必要性がない.
// http://ppp-lab.sakura.ne.jp/ProgrammingPlacePlus/cpp/language/015.html#static_class
static class Gasoline
{
private:
	// 価格.
	int Price;
	const int MIM_PRICE = 80;
	const int MAX_PRICE = 150;

	// コンストラクタを非公開にすることでインスタンス化されることを防ぐ.
	Gasoline();
public:
	// 価格.
	int GetPrice(){ return Price; }
	// 価格変動.
	void PriceChange()
	{
		// C++11のランダム処理.
		// http://vivi.dyndns.org/tech/cpp/random.html
		uniform_int_distribution<> Price(MIM_PRICE, MAX_PRICE);
	}
};

#endif _Gasoline_H_