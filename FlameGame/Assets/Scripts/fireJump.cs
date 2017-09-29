using UnityEngine;
using System.Collections;

public class fireJump : MonoBehaviour {
	public bool up=false;
	public Rigidbody2D felixbod;


	// Use this for initialization
	void Start () {
		felixbod = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name.Contains("fireplace")) {
			if (Input.GetKeyDown (KeyCode.Space) && up == false) {
				StartCoroutine (FlameJump (1f));	
			}
		}
	}
		void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Contains("tree"))
		{
			StartCoroutine(FlameJump(1f));
		}

			}
	IEnumerator FlameJump(float wait){
		felixbod.AddForce (transform.up * 10000f);
		up = true;
		yield return new WaitForSeconds (1f);
		up = false;
	}
}

