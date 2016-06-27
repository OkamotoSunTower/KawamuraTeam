using UnityEngine;
using System.Collections;

public class Judge : MonoBehaviour
{
	public float DrinkGauge;

	public enum State
	{
		Normal,
		Thirsty
	}

	public State Mode;

	// Use this for initialization
	void Start()
	{
		DrinkGauge = 100;
		Mode = State.Normal;
	}

	// Update is called once per frame
	void Update()
	{
		DrinkGauge -= 0.1f;
		if (DrinkGauge < 30)
			Mode = State.Thirsty;
		else
			Mode = State.Normal;
	}
}
