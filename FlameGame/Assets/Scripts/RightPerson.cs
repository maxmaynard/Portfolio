using UnityEngine;
using System.Collections;

public class RightPerson : MonoBehaviour {
	float maxTime;
	// Use this for initialization
	void Start () {
		maxTime = Time.time + 4f;
	}
	
	// Update is called once per frame
	void Update () {
		

		gameObject.transform.Translate (-0.25f, 0f, 0f);
		if (Time.time > maxTime)
		{
			Destroy (gameObject);
	}
}
}
