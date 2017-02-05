using UnityEngine;
using System.Collections;

public class AnimCut : MonoBehaviour
{
	// 背景画像がクリックされたとき.
	public bool isAnimCut { get; private set; }

	CardAnim cardAnim;
	GameObject CardObj;

	// Use this for initialization
	void Start()
	{
		isAnimCut = false;
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
	}

	// オブジェクトがクリックされたときに呼び出される.
	void OnMouseUpAsButton()
	{
		// タイトルの演出を省略する.
		isAnimCut = true;
	}
}
