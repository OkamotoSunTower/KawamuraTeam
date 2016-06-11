using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * 0.1f, 0, 0);
	}
}
