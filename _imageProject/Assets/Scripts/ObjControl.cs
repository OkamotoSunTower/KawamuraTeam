using UnityEngine;
using System.Collections;

public class ObjControl : MonoBehaviour
{
	// MoveSpeed設定ミスがあった場合に設定される値.
	const float AUTO_SPEED = 0.1f;

	// オブジェクトの移動量.
	[SerializeField]
	float MoveSpeed;

	// 実際のY座標移動量.
	float MoveY;
	// 初期座標.
	Vector3 InitPosition;

	void Start()
	{
		InitPosition = transform.localPosition;
		MoveY = 0.0f;
		if (MoveSpeed <= 0)
		{
			MoveSpeed = AUTO_SPEED;
			Debug.Log("ObjControlでのMoveSpeedおかしいから自動で値を設定");
		}
	}

	void Update()
	{
		// キー入力優先順位.
		// H > L > Space > 上下左右.

		if (Input.GetKey(KeyCode.H))
			MoveY = MoveSpeed;
		else if (Input.GetKey(KeyCode.L))
			MoveY = -MoveSpeed;
		else
		{
			MoveY = 0.0f;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				transform.localPosition = InitPosition;
				// このフレームでは再度座標更新する必要なし.
				return;
			}
		}
		transform.localPosition += new Vector3(Input.GetAxis("Vertical") * -MoveSpeed, MoveY, Input.GetAxis("Horizontal") * MoveSpeed);
	}


}