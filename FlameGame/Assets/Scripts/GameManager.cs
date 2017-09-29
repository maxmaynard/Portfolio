using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
		
		public void GameStart() 
		{
		Application.LoadLevel ("MainLevel");
		}
		public void GameEnd()
		{
			Application.Quit ();
		}
		
	}
