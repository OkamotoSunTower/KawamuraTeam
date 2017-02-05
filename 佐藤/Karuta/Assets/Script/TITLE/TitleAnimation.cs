using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour
{
	public enum Season
	{
		SPRING,
		SUMMER,
		FALL,
		WINTER
	};

	// 一つの季節に対して表示するパーティクルの数.
	const int SeasonParticleCount = 2;
	// 出現するパーティクルをランダムにする変数.
	public int RenderParticleSeason { get; private set; }
	// 表示するパーティクル番号.
	int RandomParticleValue;
	// 移動量.
	Vector3 move;
	// 目的地.
	Vector3 target;
	// 移動対象オブジェクト座標.
	Vector3 Position;
	// 移動時間.
	float second;
	// 経過時間.
	float time;
	// 桜の移動完了フラグ.
	public bool isMoveFin { get; private set; }

	[SerializeField]
	ParticleSystem[] Particle;

	[SerializeField]
	GameObject[] SpriteObj;

	AnimCut animCut;
	GameObject AnimCutObj;

	CardAnim cardAnim;
	GameObject CardObj;

	// Use this for initialization
	void Start()
	{
		time = 0;
		isMoveFin = false;

		int ParticleCountBuff = Particle.Length / SeasonParticleCount;
		RenderParticleSeason = Random.Range(0, ParticleCountBuff);

		// 表示するスプライトデータの決定.
		switch (RenderParticleSeason)
		{
			case (int)Season.SPRING:
				SpringStart();
				break;
			case (int)Season.SUMMER:
				SummerStart();
				break;
			case (int)Season.FALL:
				FallStart();
				break;
			case (int)Season.WINTER:
				WinterStart();
				break;
			default:
				Debug.Log("表示するスプライトデータの決定がおかしい");
				break;
		}

		SpriteObj[RenderParticleSeason].transform.position = Position;

		for (int i = 0; i < SpriteObj.Length; i++)
		{
			if (i != RenderParticleSeason)
				Destroy(SpriteObj[i]);
		}

		// 表示するパーティクルの決定.
		RandomParticleValue = RenderParticleSeason * SeasonParticleCount;

		for (int num = 0; num < SeasonParticleCount; num++)
		{
			Instantiate(Particle[num + RandomParticleValue], Particle[num + RandomParticleValue].transform.position, Quaternion.identity);
			Particle[num + RandomParticleValue].Play();
		}

		// 背景画像のオブジェクト(BackGround)取得.
		AnimCutObj = GameObject.Find("BackGround");
		// 背景画像についているスクリプト(AnimCut)取得.
		animCut = AnimCutObj.GetComponent<AnimCut>();

		// 山札画像のオブジェクト(Card)取得.
		CardObj = GameObject.Find("Card");
		// 山札画像についているスクリプト(CardAnim)取得.
		cardAnim = CardObj.GetComponent<CardAnim>();
	}

	void SpringStart()
	{
		// 初期位置.
		Position = new Vector3(-10, 13.13f, 0);
		target = new Vector3(-0.87f, 4, 0);
		second = 2.0f;
		// 1秒ごとの移動距離算出.
		move.x = (target.x - Position.x) / second;
		move.y = (target.y - Position.y) / second;
		move.z = 0;
	}

	void SummerStart()
	{
		// 初期位置.
		Position = new Vector3(-0.1f, -15, 0);
		target = new Vector3(-0.1f, 0.1f, 0);
		second = 2.0f;
		// 1秒ごとの移動距離算出.
		move.x = (target.x - Position.x) / second;
		move.y = (target.y - Position.y) / second;
		move.z = 0;
	}

	void FallStart()
	{
		// 初期位置.
		Position = new Vector3(0, 15, 0);
		target = new Vector3(0, 0.3f, 0);
		second = 2.0f;
		// 1秒ごとの移動距離算出.
		move.x = (target.x - Position.x) / second;
		move.y = (target.y - Position.y) / second;
		move.z = 0;
	}

	void WinterStart()
	{
		// 初期位置.
		Position = new Vector3(4, -12, 0);
		target = new Vector3(4, -3.3f, 0);
		second = 2.0f;
		// 1秒ごとの移動距離算出.
		move.x = (target.x - Position.x) / second;
		move.y = (target.y - Position.y) / second;
		move.z = 0;
	}

	// Update is called once per frame
	void Update()
	{
		// 山札がクリックされたら役目終わり.
		if (cardAnim.isClickOnCard)
		{
			for (int num = 0; num < SeasonParticleCount; num++)
			{
				// Destroy(Particle[num])では元のプレハブが削除されてしまうため.
				// 下記の様にする事で、Instantiateで生成されたプレハブだけを消す.
				Destroy(GameObject.Find(Particle[num + RandomParticleValue].name + "(Clone)"));
			}
			Destroy(gameObject);
		}
		else if (animCut.isAnimCut)
		{
			if(!isMoveFin)
				time = 0.0f;
			isMoveFin = true;
			SpriteObj[RenderParticleSeason].transform.position = target;
		}
		else if (!isMoveFin)
		{
			if (second >= time)
			{
				SpriteObj[RenderParticleSeason].transform.position += move * Time.deltaTime; // 環境に依存せず秒ごとに移動できる.
			}
			else
			{
				if (3 <= time)
				{
					isMoveFin = true;
					time = 0.0f;
				}
			}
		}
		if (RenderParticleSeason != (int)Season.SPRING && isMoveFin)
		{
			if (time > 1.0f)
			{
				time = 0.0f;
				Vector3 FlipAngle = SpriteObj[RenderParticleSeason].transform.localScale;
				FlipAngle.x = -FlipAngle.x;
				SpriteObj[RenderParticleSeason].transform.localScale = FlipAngle;
			}
		}
		time += Time.deltaTime;
	}
}
