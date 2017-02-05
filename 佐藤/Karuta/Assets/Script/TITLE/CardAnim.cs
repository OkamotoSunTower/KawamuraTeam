using UnityEngine;
using System.Collections;

public class CardAnim : MonoBehaviour
{
	Vector3 initPos;
	TitleText Title;
	GameObject TitleObj;

	AnimCut animCut;
	GameObject AnimCutObj;

	public bool isClickOnCard { get; private set; }

	// Use this for initialization
	void Start()
	{
		// タイトル文字のオブジェクト(Title)取得.
		TitleObj = GameObject.Find("TitleText");
		// タイトル文字についているスクリプト(Title)取得.
		Title = TitleObj.GetComponent<TitleText>();
		// 初期位置は隠しておく.
		transform.localPosition = new Vector3(500, -133, 0);
		// アニメーション開始位置.
		initPos = new Vector3(0, -3.28f, 0);

		// 背景画像のオブジェクト(BackGround)取得.
		AnimCutObj = GameObject.Find("BackGround");
		// 背景画像についているスクリプト(AnimCut)取得.
		animCut = AnimCutObj.GetComponent<AnimCut>();

		isClickOnCard = false;
	}

	// Update is called once per frame
	void Update()
	{
		// タイトル文字の表示が完了したら又は演出省略フラグOnなら.
		if (Title.isTitleTextFin || animCut.isAnimCut)
		{
			transform.localPosition = initPos;
		}
	}

	// オブジェクトがクリックされたときに呼び出される.
	void OnMouseUpAsButton()
	{
		if (!Title.isTitleTextFin && !animCut.isAnimCut)
			return;

		// クリックされたことを外部に通知.
		isClickOnCard = true;
		// 役目終わり.
		Destroy(gameObject);
	}
}
