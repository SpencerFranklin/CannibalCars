using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour {
	bool down;
	public float downSpeed, upSpeed, downDelay, upDelay, knockback;
	public int damage;
	Animator animCont;
	float timer;

	// Use this for initialization
	void Start () {
		animCont = GetComponent<Animator> ();
		timer = 0;
		down = true;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (down && timer > downDelay) {
			timer = 0;
			animCont.SetTrigger ("prepare");
			down = false;
		}
		if (!down && timer > upDelay) {
			timer = 0;
			animCont.SetTrigger ("return");
			down = true;
		}
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
