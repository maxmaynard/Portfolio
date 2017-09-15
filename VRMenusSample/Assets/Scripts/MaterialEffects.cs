using UnityEngine;
using System.Collections;

public class MaterialEffects : MonoBehaviour {
    public Material[] materials = new Material[3];
	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(GetComponent<MeshRenderer>().material.name.Contains("Green"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        if (!GetComponent<MeshRenderer>().material.name.Contains("Green"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        if (GetComponent<MeshRenderer>().material.name.Contains("Yellow"))
        {
            Physics.IgnoreLayerCollision(8, 9);
        }
        if (!GetComponent<MeshRenderer>().material.name.Contains("Yellow "))
        {
            Physics.IgnoreLayerCollision(8, 9, false);
        }
        if (GetComponent<MeshRenderer>().material.name.Contains("Blue"))
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
        if (!GetComponent<MeshRenderer>().material.name.Contains("Blue"))
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
