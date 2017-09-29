using UnityEngine;
using System.Collections;

public class navmeshtest : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
	}
}
