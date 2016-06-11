using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
	// ゲーム開始時の速度.
	[SerializeField]
	float Speed;

	[SerializeField]
	GameObject ScoreTextObj;

	[SerializeField]
	[Range(-1.0f, 1.0f)]
	float Vector_X;

	[SerializeField]
	[Range(-1.0f, 1.0f)]
	float Vector_Y;

	Vector2 Vector;
	Renderer Render;
	Vector3 InitPos;
	Rigidbody Rigid;
	float AlphaInterval;
	static bool isAlpha;

	ScoreCountUp ScoreClass;

	// Use this for initialization
	void Start()
	{
		InitPos = transform.localPosition;
		Vector = new Vector2(Vector_X, Vector_Y);
		Rigid = this.GetComponent<Rigidbody>();
		Rigid.AddForce(Vector * Speed, ForceMode.VelocityChange);
		Render = this.GetComponent<Renderer>();
		AlphaInterval = 0.1f;
		isAlpha = false;
		ScoreClass = ScoreTextObj.GetComponent<ScoreCountUp>();
	}

	void OnBecameInvisible()
	{
		if (!isAlpha)
		{
			isAlpha = true;
			// エラー情報.
			// http://qiita.com/naoK/items/55fb18bd348cfaa92708.
			StartCoroutine("Alpha");
			transform.localPosition = InitPos;
		}
	}

	IEnumerator Alpha()
	{
		int count = 0;
		Rigid.Sleep();
		while (count <= 10)
		{
			count++;
			Render.enabled = !Render.enabled;
			yield return new WaitForSeconds(AlphaInterval);
		}
		if (Render.enabled == false)
			Render.enabled = true;
		isAlpha = false;
		Rigid.AddForce(Vector * Speed, ForceMode.VelocityChange);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.localPosition = InitPos;
			Rigid.AddForce(Vector * Speed);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Minus")
			ScoreClass.Score--;
		else if (collision.gameObject.tag == "Plus")
			ScoreClass.Score++;
	}
}
