using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

	public int damage = 15;
	public float knockback;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Rigidbody rb = other.attachedRigidbody;
			rb.velocity = Vector3.zero;
			Vector3 pos = other.ClosestPointOnBounds (transform.position);
			Vector3 dir = other.transform.position - this.transform.position;
			rb.AddForceAtPosition ((dir * 1000 + transform.up * 250) * knockback, pos);
			PlayerHealth ph = rb.GetComponent<PlayerHealth> ();
			ph.TakeDamage(damage);
		}
	}
}
