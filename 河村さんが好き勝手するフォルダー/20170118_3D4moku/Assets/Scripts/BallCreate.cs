using UnityEngine;
using System.Collections;

public class BallCreate : MonoBehaviour
{

	// スペース押したときにボールを作って出す.
	// ボールを落とす(画面の外にいったら消す)

	[SerializeField]
	private GameObject Ball;

	GameObject obj;

	int count;
	// Use this for initialization
	void Start()
	{
		count = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			count++;
			obj = Instantiate(Ball);
			//Instantiate(Ball, new Vector3(0, 3, 0), Quaternion.identity);
			obj.name = "ball" + count;
			obj.AddComponent<DeleteObj>();
		}
	}

}