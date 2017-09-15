using UnityEngine;
using System.Collections;

public class RotateWithPlayer : MonoBehaviour {
    public Camera Player;
	
	// Manages turning of crosshairs
	void Update () {
        transform.LookAt(Player.transform.position);
        transform.Rotate(0, 180f, 0f);
        transform.position = Player.transform.position + Player.transform.rotation * Vector3.forward * 1f;
	}
}
