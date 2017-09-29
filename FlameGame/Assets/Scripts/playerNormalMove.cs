using UnityEngine;
using System.Collections;


public class playerNormalMove : MonoBehaviour {
	public Animation anim;
	public Rigidbody2D felixbod;
	public bool up=false;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 0.15f;
	float maxSpeed = 20000f;

	public float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	public SplineController splines;

	enum State{Paused, Active};
	State currentState;

	// Use this for initialization
	void Start () {
		felixbod = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animation> ();
		currentState = State.Active;
		splines = gameObject.GetComponent<SplineController> ();

		//anim.AddClip (anim.GetClip("IdleAnim"), "IdleAnim");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (up == true) {
			moveSpeed = 0.1f;
		}
		if (up == false) {
			moveSpeed = 0.15f;
		}

		if (Input.GetKey (KeyCode.P)) 
		
		{
			currentState = State.Paused;
		}
		if (Input.GetKey (KeyCode.O)) 
		{
			currentState = State.Active;
		}

		if (currentState == State.Active) {

			if (Input.GetKey (KeyCode.D)) {
				gameObject.transform.Translate (moveSpeed, 0, 0);
				if (felixbod.velocity.x < maxSpeed) {
					felixbod.AddForce (transform.forward * 100f);
				}
			}

			if (Input.GetKey (KeyCode.A)) {
				gameObject.transform.Translate (-moveSpeed, 0, 0);
				if (felixbod.velocity.x > -maxSpeed) {
					felixbod.AddForce (transform.forward * -100f);
				}
			}
		}
		
		if (Input.GetKeyDown (KeyCode.R))
		{
			Application.LoadLevel("MainLevel");
		}

		if (Input.GetKeyDown (KeyCode.Space) && up == false) {
			StartCoroutine (Jump (1f));	

		}
	}
		

	IEnumerator Jump(float wait){
		felixbod.AddForce (transform.up * 10000f);
		up = true;
		yield return new WaitForSeconds (1f);
		up = false;

}
}