using UnityEngine;
using System.Collections;

public class burningSpline : MonoBehaviour {
	SplineController splines;
	Rigidbody2D felixbod;
	// Use this for initialization
	void Start () {
		felixbod = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
	if (col.gameObject.tag == "start") {
		
		gameObject.AddComponent<SplineController>();
		gameObject.AddComponent<SplineInterpolator>();
		splines = gameObject.GetComponent<SplineController>();
		
		splines.SplineRoot = col.gameObject;
		splines.AutoClose = false;
		splines.HideOnExecute = false;
		splines.TimeBetweenAdjacentNodes = .2f;

		Debug.Log ("working");

	}
	if (col.gameObject.tag == "end") {
		Destroy (splines);

			Destroy(GetComponent<SplineInterpolator>());

		
		felixbod.AddForce(transform.right*-1f*300f);
		felixbod.AddForce(transform.up*300f);
		

		Destroy (col.gameObject.transform.parent.gameObject);
		
	}
	}
}
