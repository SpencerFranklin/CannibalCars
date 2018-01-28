using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grind : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, Time.deltaTime * 75, 0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			PlayerHealth ph = other.GetComponentInParent<PlayerHealth> ();
			ph.TakeDamage(100);
		}
	}
}
