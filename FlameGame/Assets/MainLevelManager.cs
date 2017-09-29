using UnityEngine;
using System.Collections;

public class MainLevelManager : MonoBehaviour {

	public GameObject PauseText;
	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.P)) {
			Time.timeScale = 0.0f;
			PauseText.SetActive (true);
		}
		if (Input.GetKey (KeyCode.O)) {
			Time.timeScale = 2.5f;
			PauseText.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
}
}