using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickEvent : MonoBehaviour
{
	[SerializeField]
	string SceneName;

	float time;

	Renderer render;
	CardAnim cardAnim;
	GameObject CardObj;

	// Use this for initialization
	void Start()
	{
		// 自分自身のレンダーを取得.
		render = GetComponent<Renderer>();
		// 隠す.
		render.sortingOrder = -1500;

		// 山札画像のオブジェクト(Card)取得.
		CardObj = GameObject.Find("Card");
		// 山札画像についているスクリプト(CardAnim)取得.
		cardAnim = CardObj.GetComponent<CardAnim>();

		time = 0;
	}

	void Update()
	{
		// 山札がクリックされたら.
		if (cardAnim.isClickOnCard)
		{
			// 描画順の変更.
			render.sortingOrder = 10;
		}
	}

	// オブジェクトがクリックされたときに呼び出される.
	void OnMouseUpAsButton()
	{
		// 画面表示されてから有効化.
		if (render.sortingOrder != 10)
			return;

		// エラーチェック.
		if (SceneName != "")
			// 外部から指定されたシーンに切り替える.
			SceneManager.LoadScene(SceneName);
	}

	// オブジェクトの上にマウスがあるときに呼び出される.
	void OnMouseOver()
	{
		// 画面表示されてから有効化.
		if (render.sortingOrder != 10)
			return;

		// カードのアニメーション速度.
		time += 0.035f;
		// Sinで-1~1の数字を作り出し、それを1/10にする.
		// 0.1fがカードの最大サイズ(1+0.1f=1.1f).
		float variation = Mathf.Sin(time) * 0.1f;
		// Absを使い0~0.1に補正する.
		variation = Mathf.Abs(variation);

		// 選択中のカードをアニメーション(1.0~1.1)する.
		transform.localScale = new Vector3(1 + variation, 1 + variation, 1);
	}

	// オブジェクトの上からマウスが離れた時に呼び出される.
	void OnMouseExit()
	{
		time = 0;
		// 画面表示されてから有効化.
		if (render.sortingOrder != 10)
			return;
		transform.localScale = new Vector3(1, 1, 1);
	}
}
