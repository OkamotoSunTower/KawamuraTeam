using UnityEngine;
using System.Collections;

public class ClickAnim : MonoBehaviour
{
	[SerializeField]
	RectTransform image;

	Vector3 initPos;
	float move;

	TitleText Title;
	GameObject TitleObj;

	AnimCut animCut;
	GameObject AnimCutObj;

	CardAnim cardAnim;
	GameObject CardObj;

	// Use this for initialization
	void Start()
	{
		// タイトル文字のオブジェクト(Title)取得.
		TitleObj = GameObject.Find("TitleText");
		// タイトル文字についているスクリプト(Title)取得.
		Title = TitleObj.GetComponent<TitleText>();
		// 初期位置は隠しておく.
		image.localPosition = new Vector3(1000, -125, 0);
		// アニメーション開始位置.
		initPos = new Vector3(109, -125, 0);
		// 移動量設定.
		move = 0.5f;

		// 背景画像のオブジェクト(BackGround)取得.
		AnimCutObj = GameObject.Find("BackGround");
		// 背景画像についているスクリプト(AnimCut)取得.
		animCut = AnimCutObj.GetComponent<AnimCut>();

		// 山札画像のオブジェクト(Card)取得.
		CardObj = GameObject.Find("Card");
		// 山札画像についているスクリプト(CardAnim)取得.
		cardAnim = CardObj.GetComponent<CardAnim>();
	}

	// Update is called once per frame
	void Update()
	{
		// タイトル文字の表示が完了したら.
		if (Title.isTitleTextFin || animCut.isAnimCut)
		{
			if (image.localPosition.y == -125)
				move = 0.5f;
			else if (image.localPosition.y == -100)
				move = -0.5f;

			initPos.y += move;
			image.localPosition = initPos;
		}

		// 山札がクリックされたら役目終わり.
		if (cardAnim.isClickOnCard)
			Destroy(gameObject);
	}
}

