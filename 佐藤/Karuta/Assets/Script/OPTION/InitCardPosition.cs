using UnityEngine;
using System.Collections;

public class InitCardPosition : MonoBehaviour
{
	const int TERRITORY_CARD_NUM = 25;
	const int X_CARD_MAX = 12;
	const int Y_CARD_MAX = 6;

	// 取り札の初期位置(右上から設定).
	public bool[] isCardSetPos { get; private set; }
	// 初期配置をランダムにするか.
	bool isRandom;

	// Use this for initialization
	void Start()
	{
		// テスト用.
		isRandom = true;
		isCardSetPos = new bool[X_CARD_MAX * Y_CARD_MAX];
		int count = 0;
		if (isRandom)
		{
			int num;
			for (; count < TERRITORY_CARD_NUM*2; count++)
			{
				do
				{
					num = Random.Range(0, X_CARD_MAX * Y_CARD_MAX);
				} while (isCardSetPos[num]);
				isCardSetPos[num] = true;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
