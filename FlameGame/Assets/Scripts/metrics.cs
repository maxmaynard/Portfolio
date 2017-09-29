using UnityEngine;
using System.Collections;
using System.IO;

public class metrics : MonoBehaviour {

	string createText = "";

	public fuel_collection FuelCollector;

	public int burnedThings;

	public static int numberOfRestarts, sampleMetric2;

	public static float timePlaying;

	bool playing;

	void Start () {
		FuelCollector = gameObject.GetComponent<fuel_collection> ();

	}
	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			numberOfRestarts++;
		}

		if (!playing) {
			playing = true;
			StartCoroutine (playtimePlaying ());
		}



	}

	//When the game quits we'll actually write the file.
	void OnApplicationQuit(){
		GenerateMetricsString ();
		string time = System.DateTime.UtcNow.ToString ();string dateTime = System.DateTime.Now.ToString (); //Get the time to tack on to the file name
		print("before: " + time);
		time = time.Replace ("/", "-"); // Replace slashes with dashes, because Unity thinks they are directories..
		time = time.Replace (":", "-"); // Replace colons with dashes
		time = time.Replace (" ", "__"); // Replace white space with two underscores
		string reportFile = "GameName_Metrics_" + time + ".txt"; 
		createText = createText.Replace("\n", System.Environment.NewLine); // Replace newline characters with the system's newline representation.
		File.WriteAllText (reportFile, createText);
		//In Editor, this will show up in the project folder root (with Library, Assets, etc.)
		//In Standalone, this will show up in the same directory as your executable
	}

	void GenerateMetricsString(){
		createText = 
			"Number of times player restarted: " + numberOfRestarts + "\n"
		+ "Time spent playing: " + timePlaying + "\n"
		+ "Number of things burned : " + FuelCollector.numOfBurnedObjects + "\n"
		+ "Number of pinecones burned : " + FuelCollector.numOfBurnedPinecones + "\n"
		+ "Number of logs burned : " + FuelCollector.numOfBurnedLogs + "\n"
		+ "Number of barrels burned : " + FuelCollector.numOfBurnedBarrels + "\n"
		+ "Number of houses burned : " + FuelCollector.numOfBurnedHouses;


	}

	//Add to your set metrics from other classes whenever you want
	public void AddToSample1(int amtToAdd){
		numberOfRestarts += amtToAdd;


	}

	IEnumerator playtimePlaying(){
		timePlaying=timePlaying + .2f;
		yield return new WaitForSeconds (1f);
		playing = false;
	}
}

