using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// 追従したいオブジェクト.
	[SerializeField]
	GameObject target;

	// Use this for initialization
	void Start()
	{
		if (target == null)
		{
			target = GameObject.FindWithTag("Player");
			Debug.Log("CameraControlに対してターゲットとなるゲームオブジェクトアタッチしてないから自動でPlayerタグついてるオブジェクト参照する");
		}
	}

	// Update is called once per frame
	void Update()
	{
		// 追従.
		transform.LookAt(target.transform.localPosition);
	}
}
