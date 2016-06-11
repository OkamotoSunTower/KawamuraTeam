using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCountUp : MonoBehaviour
{
	Text TextObj;
	const string BASE_TEXT = "SCORE : ";
	public int Score;

	// Use this for initialization
	void Start()
	{
		Score = 0;
		TextObj = this.GetComponent<Text>();
		TextObj.text = BASE_TEXT + Score.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		TextObj.text = BASE_TEXT + Score.ToString();
	}
}
