using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// MoveSpeed設定ミスがあった場合に設定される値.
	const float AUTO_SPEED = 0.1f;

	// オブジェクトの移動量.
	[SerializeField]
	float MoveSpeed;

	// 追従したいオブジェクト.
	[SerializeField]
	GameObject target;

	// 実際のY座標移動量.
	float MoveY;
	// 初期座標.
	Vector3 InitPosition;

	// Use this for initialization
	void Start()
	{
		InitPosition = transform.localPosition;

		if (target == null)
		{
			target = GameObject.FindWithTag("Player");
			Debug.Log("CameraControlに対してターゲットとなるゲームオブジェクトアタッチしてないから自動でPlayerタグついてるオブジェクト参照する");
		}
		if (MoveSpeed <= 0)
		{
			MoveSpeed = AUTO_SPEED;
			Debug.Log("CameraControlでのMoveSpeedおかしいから自動で値を設定");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.N))
			MoveY = MoveSpeed;
		else if (Input.GetKey(KeyCode.M))
			MoveY = -MoveSpeed;
		else
		{
			if (Input.GetKeyDown(KeyCode.Return))
				transform.localPosition = InitPosition;
			MoveY = 0.0f;
		}
		transform.localPosition += new Vector3(0, MoveY, 0);

		// 追従.
		transform.LookAt(target.transform.localPosition);
	}
}
