using UnityEngine;
using System.Collections;

public class respawnPlatforms : MonoBehaviour {
	public GameObject platformRespawner;
	public GameObject[] respawns;
	// Use this for initialization
	void Start () {
		if (respawns == null) {
			respawns = GameObject.FindGameObjectsWithTag ("platform");
			Debug.Log (respawns.Length + "long" );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator iLiiveeee(){
		yield return new WaitForSeconds (4f);
		//gameObject.SetActive (true);
		Debug.Log ("Suck it Jesus");
	}
}
