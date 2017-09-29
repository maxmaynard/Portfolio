using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {
	public GameObject LeftPerson;
	public GameObject RightPerson;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name == "FelixFlame")
		{
			if (col.transform.localScale.x > gameObject.transform.localScale.x)
			{
				gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
				Instantiate(LeftPerson, gameObject.transform.position, Quaternion.identity);
				Instantiate(RightPerson, gameObject.transform.position, Quaternion.Euler(0, 180, 0));
			}
		}


			}
}
