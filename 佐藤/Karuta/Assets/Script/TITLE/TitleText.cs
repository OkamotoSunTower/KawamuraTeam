using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour
{
	Vector3 initPos;
	TitleAnimation titleAnimation;
	GameObject titleObj;

	float time;

	// タイトル文字表示完了フラグ.
	public bool isTitleTextFin { get; private set; }

	AnimCut animCut;
	GameObject AnimCutObj;

	CardAnim cardAnim;
	GameObject CardObj;

	// Use this for initialization
	void Start()
	{
		// タイトル用アニメーションゲームオブジェクト取得.
		titleObj = GameObject.Find("TitleAnimation");
		titleAnimation = titleObj.GetComponent<TitleAnimation>();

		// 取得したオブジェクトついているスクリプト取得.
		titleAnimation = titleObj.GetComponent<TitleAnimation>();

		// 初期位置は隠しておく.
		transform.localPosition = new Vector3(100, 2, 0);
		isTitleTextFin = false;
		time = 0;

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
		// 山札がクリックされたら役目終わり.
		if (cardAnim.isClickOnCard)
			Destroy(gameObject);
		else if (animCut.isAnimCut)
		{
			isTitleTextFin = true;
			transform.localPosition = new Vector3(0, 2, 0);
		}
		else if (!isTitleTextFin)
		{
			// タイトルのアニメーションが完了したら.
			if (titleAnimation.isMoveFin)
			{
				time += Time.deltaTime;
				if (time >= 1)
					isTitleTextFin = true;
				transform.localPosition = new Vector3(0, 2, 0);
			}
		}
	}
}

