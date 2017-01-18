using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour
{

	[SerializeField] // Private属性のままUnityから触れる.
	Sprite stone;

	//[SerializeField]
	//Vector2 Position;

	SpriteRenderer StoneRenderer;

	// Use this for initialization
	void Start()
	{
		StoneRenderer = gameObject.GetComponent<SpriteRenderer>();
		StoneRenderer.sprite = stone;

		//gameObject.transform.position = Position;


	}

	// Update is called once per frame
	void Update()
	{ // 更新処理.

	}

	void Render()
	{
		// .

	}
}
