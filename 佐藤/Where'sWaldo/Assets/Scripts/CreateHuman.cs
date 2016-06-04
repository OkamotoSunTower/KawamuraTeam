using UnityEngine;
using System.Collections;

public class CreateHuman : MonoBehaviour
{
	[SerializeField]
	Sprite[] FaceSprite;

	[SerializeField]
	Sprite[] BodySprite;

	// 顔に対しての体の座標補正値(Y座標のみ).
	const int BODY_Y_REVISE = 1;
	// 生成する人の最大数.
	const int HUMAN_MAX = 100;
	// 画面領域に収まる補正値(人同士の最低X距離).
	const float SCREEN_POS_REVISE = 1.5f;
	// 人同士の最低Y距離.
	const float HUMAN_POS_Y_REVISE = 2.3f;

	// 生成した人の数.
	static int Count;
	// 生成した人の座標データ格納().
	static Vector3[] PosRecord;

	SpriteRenderer FaceRenderer;
	SpriteRenderer BodyRenderer;
	Camera CameraObj;

	struct ScreenSize
	{
		public float Left;
		public float Right;
		public float Top;
		public float Bottom;
	}

	ScreenSize Size;

	static CreateHuman()
	{
		if (HUMAN_MAX > 100)
		{
			Debug.LogError("その数は表示できない");
			return;
		}
		Count = 0;
		PosRecord = new Vector3[HUMAN_MAX];
		for (int i = 0; i < HUMAN_MAX; i++)
			PosRecord[i] = new Vector3(float.MaxValue, float.MaxValue, 0);
	}

	void Start()
	{
		Count++;
		if (Count < HUMAN_MAX)
		{
			// オブジェクト複製.
			GameObject obj = Instantiate(gameObject);
			// 強制的に(Clone)を付けないように.
			obj.name = gameObject.name;
		}
		else
			return;
	}

	// Use this for initialization
	void Awake()
	{
		// カメラオブジェクト取得.
		GameObject obj = GameObject.Find("Main Camera");
		CameraObj = obj.GetComponent<Camera>();

		SizeDecide(ref Size);
		Vector3 pos;
		pos.x = Random.Range(Size.Left + SCREEN_POS_REVISE, Size.Right - SCREEN_POS_REVISE);
		pos.y = Random.Range(Size.Bottom + SCREEN_POS_REVISE + BODY_Y_REVISE, Size.Top - SCREEN_POS_REVISE);
		pos.z = 0;

		bool isPosOK;
		// 処理重い.
		do
		{
			isPosOK = true;
			for (int i = 0; i < Count; i++)
			{
				float Y = System.Math.Abs(pos.y - PosRecord[i].y);
				float X = System.Math.Abs(pos.x - PosRecord[i].x);

				if (Y < HUMAN_POS_Y_REVISE && X < SCREEN_POS_REVISE)
				{
					pos.y = Random.Range(Size.Bottom + SCREEN_POS_REVISE + BODY_Y_REVISE, Size.Top - SCREEN_POS_REVISE);
					pos.x = Random.Range(Size.Left + SCREEN_POS_REVISE, Size.Right - SCREEN_POS_REVISE);
					isPosOK = false;
					break;
				}
			}

		} while (!isPosOK);
		
		// 苦肉の策.
		if (Count < HUMAN_MAX)
			PosRecord[Count] = pos;

		FaceRenderer = gameObject.GetComponent<SpriteRenderer>();

		FaceRenderer.sprite = FaceSprite[Random.Range(0, FaceSprite.Length)];
		FaceRenderer.transform.localPosition = pos;
		FaceRenderer.sortingOrder = 1;

		Vector3 BodyPos = FaceRenderer.transform.localPosition;
		BodyPos.y -= BODY_Y_REVISE;
		foreach (Transform child in transform)
		{
			BodyRenderer = child.GetComponent<SpriteRenderer>();
			BodyRenderer.sprite = BodySprite[Random.Range(0, BodySprite.Length)];
			BodyRenderer.transform.position = BodyPos;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SizeDecide(ref ScreenSize size)
	{
		Vector3 TopLeft = GetScreenTopAndLeft();
		Vector3 BottomRight = GetScreenBottomAndRight();
		size.Right = BottomRight.x;
		size.Left = TopLeft.x;
		size.Bottom = BottomRight.y;
		size.Top = TopLeft.y;
		//Debug.Log("Right  : " + size.Right);
		//Debug.Log("Left   : " + size.Left);
		//Debug.Log("Bottom : " + size.Bottom);
		//Debug.Log("Top    : " + size.Top);
	}

	Vector3 GetScreenTopAndLeft()
	{
		// 画面の左上を取得.
		Vector3 topLeft = CameraObj.ScreenToWorldPoint(Vector3.zero);
		// 上下反転させる.
		topLeft.Scale(new Vector3(1f, -1f, 1f));
		return topLeft;
	}

	Vector3 GetScreenBottomAndRight()
	{
		// 画面の右下を取得.
		Vector3 bottomRight = CameraObj.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
		// 上下反転させる.
		bottomRight.Scale(new Vector3(1f, -1f, 1f));
		return bottomRight;
	}
}

//==================================================================
//=================================TODO=============================
//
//	正解の組み合わせは一体しか生成しない.
//	正解判定.
//	作らないと.
//==================================================================