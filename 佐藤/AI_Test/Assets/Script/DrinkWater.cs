using UnityEngine;
using System.Collections;

public class DrinkWater : MonoBehaviour
{
	GameObject Water;
	Vector3 WaterPos;
	Judge judge;
	// Use this for initialization
	void Start()
	{
		Water = GameObject.Find("Water");
		WaterPos = Water.transform.localPosition;
		judge = this.GetComponent<Judge>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void Act()
	{
		if (Vector3.Distance(WaterPos, transform.localPosition) < 3)
			judge.DrinkGauge += 80;
		else
		{
			transform.LookAt(WaterPos);
			transform.localPosition += (transform.forward * 0.1f);
		}
	}
}
