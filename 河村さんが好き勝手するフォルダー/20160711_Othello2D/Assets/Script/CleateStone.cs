using UnityEngine;
using System.Collections;

public class CleateStone : MonoBehaviour {

	[SerializeField]
	GameObject black;

	[SerializeField]
	GameObject white;

	// Use this for initialization
	void Start () {
		//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//cube.AddComponent<Rigidbody>();
		//cube.transform.position = new Vector2(1, 1);
		black.transform.position = new Vector2(-0.5f, -0.5f);
		white.transform.position = new Vector2(-0.5f, 0.5f);
		Instantiate(black);
		Instantiate(white);
		black.transform.position = new Vector2(0.5f, 0.5f);
		white.transform.position = new Vector2(0.5f, -0.5f);
		Instantiate(black);
		Instantiate(white);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Input.mousePosition;
			Debug.Log(pos);
		}
	}
}
