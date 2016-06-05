using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class CreateHuman : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	Sprite[] FaceSprite;

	[SerializeField]
	Sprite[] BodySprite;

	//定数系動的に決めたほうがいいよね.
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
	// 生成した人の座標データ格納.
	static Vector3[] PosRecord;
	// 正解顔データ.
	static int AnswerFaceData;
	// 正解体データ.
	static int AnswerBodyData;

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
			Debug.LogError("そんなに多く表示できません");
			Debug.Break();
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
		if (Count < HUMAN_MAX && this.name != "AnswerFace(Clone)")
			PosRecord[Count] = pos;

		FaceRenderer = gameObject.GetComponent<SpriteRenderer>();
		int FaceSpriteNum = Random.Range(0, FaceSprite.Length);

		if (this.name == "AnswerFace")
			AnswerFaceData = FaceSpriteNum;
		FaceRenderer.transform.localPosition = pos;
		FaceRenderer.transform.localScale = new Vector3(2, 2, 1);
		FaceRenderer.sortingOrder = 1;

		Vector3 BodyPos = FaceRenderer.transform.localPosition;
		BodyPos.y -= BODY_Y_REVISE;

		foreach (Transform child in transform)
			BodyRenderer = child.GetComponent<SpriteRenderer>();

		int BodySpriteNum = Random.Range(0, BodySprite.Length);

		if (this.name != "AnswerFace")
		{
			if (this.name == "AnswerFace(Clone)")
			{
				FaceSpriteNum = AnswerFaceData;
				BodySpriteNum = AnswerBodyData;
			}
			else
			{
				while (AnswerBodyData == BodySpriteNum && AnswerFaceData == FaceSpriteNum)
				{
					BodySpriteNum = Random.Range(0, BodySprite.Length);
					BodySpriteNum = Random.Range(0, BodySprite.Length);
				}
			}
		}
		else
		{
			AnswerBodyData = BodySpriteNum;
			//Debug.Log("正解顔データ番号 : " + AnswerFaceData);
			//Debug.Log("正解体データ番号 : " + AnswerBodyData);

			GameObject AnswerObj = Instantiate(gameObject);
			AnswerObj.name = "AnswerObj";
			AnswerObj.transform.localPosition = new Vector3(100, 0, 0);
		}

		BodyRenderer.sprite = BodySprite[BodySpriteNum];
		FaceRenderer.sprite = FaceSprite[FaceSpriteNum];
		BodyRenderer.transform.position = BodyPos;

		// 強制的に(Clone)を付けないように.
		this.name = "Face";
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

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.clickCount == 1)
		{
			if (this.transform.localPosition == PosRecord[0])
				Debug.Log("正解");
			else
				Debug.Log("不正解");
		}
	}
}
