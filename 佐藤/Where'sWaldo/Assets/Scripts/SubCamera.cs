using UnityEngine;
using System.Collections;

public class SubCamera : MonoBehaviour
{
	[SerializeField]
	Camera CameraObj;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			CameraObj.enabled = !CameraObj.enabled;
	}
}
