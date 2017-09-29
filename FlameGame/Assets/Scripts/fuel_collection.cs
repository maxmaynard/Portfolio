using UnityEngine;
using System.Collections;

public class fuel_collection : MonoBehaviour {
	public float sizeChange =.5f;
	public float maxSizeX;
	public float maxSizeY;
	public float maxSizeZ;
	public float maxSize;

	public Camera mainCamera;
	public float maxCamSize;
	float currentCamSize;

	public int numOfBurnedObjects;
	public int numOfBurnedBarrels;
	public int numOfBurnedPinecones;
	public int numOfBurnedLogs;
	public int numOfBurnedHouses;

	public Vector3 respawnPoint;
	GameObject targetBurn;
	GameObject fuelPart;
	SpriteRenderer blacken;

	GameObject targetPlatform;

	// Use this for initialization
	void Start () {

		maxSize = gameObject.transform.localScale.magnitude;
		//gameObject.transform.localScale = new Vector3 (maxSizeX, maxSizeY, maxSizeZ);
		maxCamSize = 3.020304f;
		currentCamSize = 3.020304f;
		enFuego = gameObject.transform.GetChild (1).gameObject.GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.transform.localScale.magnitude > maxSize) 
		
		{
			maxSizeX = gameObject.transform.localScale.x;
			maxSizeY = gameObject.transform.localScale.y;
			maxSizeZ = gameObject.transform.localScale.z;
			maxSize = gameObject.transform.localScale.magnitude;


		}
		//gameObject.transform.localScale -= new Vector3 (0.00005f, 0.00005f, 0f); 
		currentCamSize -= 0.0001f;
		//mainCamera.orthographicSize = currentCamSize;

		enFuego.startSize-=.0000f;
		enFuego.startLifetime-=.0000f;

		//if (currentCamSize > maxCamSize) 
		//{
			//maxCamSize = currentCamSize;
		//}


		if (gameObject.transform.localScale.x < 0.01f)
		{
			gameObject.transform.position = respawnPoint;
		}

	}

	ParticleSystem enFuego;

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "burns") {
			if (col.transform.transform.localScale.x < gameObject.transform.localScale.x) {

				targetBurn = col.gameObject.transform.GetChild (0).gameObject;
				blacken = col.gameObject.GetComponent<SpriteRenderer> ();
				fuelPart = col.gameObject;
				fuelPart.GetComponent<PolygonCollider2D> ().enabled = false;

				targetBurn.SetActive (true);
				blacken.color = Color.black;
				StartCoroutine (Ashes ());

				gameObject.transform.localScale += new Vector3 (sizeChange, sizeChange, sizeChange);
				enFuego.startSize+=.1f;
				enFuego.startLifetime+=.25f;
				currentCamSize = currentCamSize + 0.25f;
				numOfBurnedObjects++;

				if (col.gameObject.name.Contains ("pinecone")) {
					numOfBurnedPinecones++;
				}
				if (col.gameObject.name.Contains ("barrel")) {
					numOfBurnedBarrels++;
				}
				if (col.gameObject.name.Contains ("log")) {
					numOfBurnedLogs++;
				}
				if (col.gameObject.name.Contains ("house") || col.gameObject.name.Contains("House")) {
					numOfBurnedHouses++;
				}
			}
		}

		if (col.gameObject.tag == "platform") {
			targetPlatform = col.gameObject;
			print ("target platform is" + col.gameObject.name);
			//StartCoroutine (burnPlatform ());
		}
		if (col.gameObject.name.Contains("Lantern")) {
			print ("is colliding");
			gameObject.transform.localScale = new Vector3 (maxSizeX, maxSizeY, maxSizeZ);
			respawnPoint = col.gameObject.transform.position;
			respawnPoint += new Vector3 (0f, 2.5f, 0f);
			print ("respawn point is: " + respawnPoint.x);
			targetBurn = col.gameObject.transform.GetChild (0).gameObject;
			targetBurn.SetActive (true);
			mainCamera.orthographicSize = maxCamSize;
			enFuego.enableEmission = true;
		}

		if (col.gameObject.name == "Water")
		{
			StartCoroutine (Death ());
		}
	}



	IEnumerator Death()
	{
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		enFuego.enableEmission = false; 
		yield return new WaitForSeconds (4); 
		gameObject.transform.position = respawnPoint;
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;


	}



	IEnumerator Ashes(){
		yield return new WaitForSeconds(3);
		fuelPart.SetActive(false);
	}



	}



