using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name == "FelixFlame")
		{
			StartCoroutine(burnPlatform());
		}
	}
				IEnumerator burnPlatform(){
				print ("burning platform");
				
				yield return new WaitForSeconds (4f);
		gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				yield return new WaitForSeconds (10f);
		gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;

}
}


