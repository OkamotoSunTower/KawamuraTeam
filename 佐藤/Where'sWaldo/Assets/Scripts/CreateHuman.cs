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
	const int HUMAN_MAX = 20;
	// 画面領域に収まる補正値.
	const float SCREEN_POS_REVISE = 1.5f;

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

	void Start()
	{
		// Targetタグついてるオブジェクトの数取得.
		GameObject[] Target = GameObject.FindGameObjectsWithTag("Target");
		if (Target.Length < HUMAN_MAX)
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
		Debug.Log("Right  : " + size.Right);
		Debug.Log("Left   : " + size.Left);
		Debug.Log("Bottom : " + size.Bottom);
		Debug.Log("Top    : " + size.Top);

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
//	生成時に重ならないようにする仕組み.座標指定をStart内の複製後のタイミングに変更すれば対応できそう.
//	正解の組み合わせは一体しか生成しない.
//	正解判定.
//	作らないと.
//==================================================================