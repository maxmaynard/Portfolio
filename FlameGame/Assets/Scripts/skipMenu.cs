using UnityEngine;
using System.Collections;

public class skipMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			Application.LoadLevel ("TutorialLevel");
		}
	}
}
