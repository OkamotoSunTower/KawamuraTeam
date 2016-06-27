using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour
{
	DrinkWater Drink;
	Judge judge;
	// Use this for initialization
	void Start()
	{
		Drink = this.GetComponent<DrinkWater>();
		judge = this.GetComponent<Judge>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (judge.Mode)
		{
			case Judge.State.Normal:
				if (Vector3.Distance(new Vector3(0, 0, 0), transform.localPosition) >= 1)
				{
					this.transform.LookAt(new Vector3(0, 0, 0));
					transform.localPosition += (transform.forward * 0.1f);
				}
				break;
			case Judge.State.Thirsty:
				Drink.Act();
				break;
			default:
				break;
		}
	}

}
