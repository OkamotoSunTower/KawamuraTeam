using UnityEngine;
using System.Collections;
using System.IO;

public class CardSet : MonoBehaviour
{
	[SerializeField]
	Sprite frame;

	const int CARD_MAX = 100;
	const int USE_CARD = 50;

	// 重複チャック用.
	bool[] isCheck;

	// カード設置箇所表示用フレーム.
	Renderer[] FrameRender;
	// フレーム上に取り札があるかどうか.
	bool[] isOnCard;

	// 表示するカード番号(読み札と共通).
	public int[] CARD_NUM { get; private set; }
	// 表示するカードオブジェクト.
	GameObject[] CardObj;

	// 取り札の初期位置管理クラス(仮).
	InitCardPosition InitCardInfo;
	int CardCount;


	//--------------------------------------------
	//			描画時カード情報.
	//--------------------------------------------
	const float INIT_X = 6.08f;
	const float X_SPACE = 1.10f;
	const float X_CENTER_SPACE = 0.06f;
	const int X_CARD_MAX = 12;

	const float INIT_Y = 4.2f;
	const float Y_SPACE = 1.5f;
	const float Y_CENTER_SPACE = 0.9f;
	const int Y_CARD_MAX = 6;

	const float CARD_SCALE = 0.3454983f;
	//--------------------------------------------

	void Init()
	{
		isCheck = new bool[CARD_MAX];
		FrameRender = new Renderer[X_CARD_MAX * Y_CARD_MAX];
		isOnCard = new bool[X_CARD_MAX * Y_CARD_MAX];
		CARD_NUM = new int[USE_CARD];
		CardObj = new GameObject[USE_CARD];
		InitCardInfo = GetComponent<InitCardPosition>();
		CardCount = 0;
	}

	void Start()
	{
		Init();
		// 選ばれた取り札表示.
		CreateCards();
		// 選ばれなかった取り札削除.
		DeleteCards();
		// 設置位置にフレーム表示.
		CreateFrames();
	}

	void CreateFrames()
	{
		int count = 0;
		for (int x = 0; x < X_CARD_MAX; x++)
		{
			for (int y = 0; y < Y_CARD_MAX; y++)
			{
				new GameObject("frame" + count).AddComponent<SpriteRenderer>().sprite = frame;
				FrameRender[count] = GameObject.Find("frame" + count).GetComponent<Renderer>();
				FrameRender[count].sortingOrder = 10;
				FrameRender[count].transform.localScale = new Vector3(CARD_SCALE, CARD_SCALE, 1);
				FrameRender[count].transform.localPosition = new Vector3(INIT_X - (X_SPACE * x), INIT_Y - (Y_SPACE * y), 1);
				if (x >= X_CARD_MAX * 0.5)
				{
					if (y >= Y_CARD_MAX * 0.5)
					{
						FrameRender[count].transform.localPosition = new Vector3(INIT_X - (X_SPACE * x) - X_CENTER_SPACE, INIT_Y - (Y_SPACE * y) - Y_CENTER_SPACE, 1);
						CardDataSet(ref count);
						continue;
					}
					FrameRender[count].transform.localPosition = new Vector3(INIT_X - (X_SPACE * x) - X_CENTER_SPACE, INIT_Y - (Y_SPACE * y), 1);
				}
				else if (y >= Y_CARD_MAX * 0.5)
				{
					if (x >= X_CARD_MAX * 0.5)
					{
						FrameRender[count].transform.localPosition = new Vector3(INIT_X - (X_SPACE * x) - X_CENTER_SPACE, INIT_Y - (Y_SPACE * y) - Y_CENTER_SPACE, 1);
						CardDataSet(ref count);
						continue;
					}
					FrameRender[count].transform.localPosition = new Vector3(INIT_X - (X_SPACE * x), INIT_Y - (Y_SPACE * y) - Y_CENTER_SPACE, 1);
				}
				CardDataSet(ref count);
			}
		}
	}

	// ref : 参照渡し.
	void CardDataSet(ref int count)
	{
		// 生成したフレームにコリジョン適応.
		GameObject.Find("frame" + count).AddComponent<BoxCollider2D>();
		// 取り札の設置.
		if (InitCardInfo.isCardSetPos[count])
			SettingCards(count);
		count++;
	}

	void CreateCards()
	{
		int num;

		for (int i = 0; i < CARD_MAX; i++)
			isCheck[i] = false;

		for (int i = 0; i < USE_CARD; i++)
		{
			do
			{
				num = Random.Range(0, CARD_MAX);
			} while (isCheck[num]);

			isCheck[num] = true;
			CARD_NUM[i] = num + 1;
			string CardString = "100";
			if (CARD_NUM[i] >= 10)
			{
				if (CARD_NUM[i] != 100)
					CardString = "0" + CARD_NUM[i];
			}
			else
				CardString = "00" + CARD_NUM[i];

			CardObj[i] = GameObject.Find("t_" + CardString);
			CardObj[i].transform.localScale = new Vector3(CARD_SCALE, CARD_SCALE, 1);
			// 取り札にコリジョン適応.
			CardObj[i].AddComponent<BoxCollider2D>();
			// 取り札に取得判定スクリプト適応.
			CardObj[i].AddComponent<CardJudge>();
		}
	}

	void DeleteCards()
	{
		for (int s = 0; s < CARD_MAX; s++)
		{
			if (!isCheck[s])
			{
				int i = s + 1;
				string CardString = "100";
				if (i >= 10)
				{
					if (i != 100)
						CardString = "0" + i;
				}
				else
					CardString = "00" + i;

				GameObject DeleteCardObj = GameObject.Find("t_" + CardString);
				Destroy(DeleteCardObj);
			}
		}
	}

	void SettingCards(int count)
	{
		// trueで相手側にある取り札.
		if (((count / 3) + 1) % 2 != 0)
		{
			// Scale x:-1,y:-1で反転
			CardObj[CardCount].transform.localScale = new Vector3(-CARD_SCALE, -CARD_SCALE, 1);
		}
		CardObj[CardCount].transform.localPosition = FrameRender[count].transform.localPosition;
		CardCount++;
	}

	// Update is called once per frame
	void Update()
	{

	}
}

//----------------------------------------------------
//どの位置に取り札を置くかは外部で設定する.
//----------------------------------------------------