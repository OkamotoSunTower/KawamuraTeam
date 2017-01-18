using UnityEngine;
using System.Collections;

public class CylinderCopy : MonoBehaviour
{
	// https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

	// カメラはどのシーンにもあるからカメラにつけた.

	[SerializeField]
	private GameObject Cylinder;

	const int CYLINDER_X_MAX = 4;
	const int CYLINDER_Z_MAX = 4;
	const int POS_SPACE_X = 1;
	const int POS_SPACE_Z = -1;

	// Use this for initialization
	void Start()
	{
		if (Cylinder == null)
		{
			// エラー出力.
			Debug.LogError("Cylinderが見つからない");
			return;
		}

		Vector3 InitPos = Cylinder.transform.position;

		int XSpace = 0;
		int ZSpace = 0;


		// オブジェクトの複製.
		for (int x = 0; x < CYLINDER_X_MAX; x++)
		{
			XSpace = x * POS_SPACE_X;
			for (int z = 0; z < CYLINDER_Z_MAX; z++)
			{
				ZSpace = z * POS_SPACE_Z;

				Instantiate(Cylinder, new Vector3(InitPos.x + XSpace, InitPos.y, InitPos.z + ZSpace), Quaternion.identity);
			}
		}
	}

}
