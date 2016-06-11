using UnityEngine;
using System.Collections;

public class ScoreObjMove : MonoBehaviour
{
	Vector3 InitPos;
	float time;
	// Use this for initialization
	void Start()
	{
		InitPos = transform.localPosition;
		time = 0;
	}

	// Update is called once per frame
	void Update()
	{
		time += 0.1f;
		transform.localPosition = new Vector3(InitPos.x + Mathf.Sin(time), InitPos.y, InitPos.z);
	}
}
