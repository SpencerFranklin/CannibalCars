using UnityStandardAssets.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyExplosionPhysicsForce : MonoBehaviour
{
	public int damage = 15;
	public float explosionForce = 4;
	public GameObject whoFired = null;
	public bool dealDamage = false;

	private IEnumerator Start()
	{
		// wait one frame because some explosions instantiate debris which should then
		// be pushed by physics force
		yield return null;
			float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;
			float r = 10*multiplier;
		var cols = Physics.OverlapSphere(transform.position, r);
		var rigidbodies = new List<Rigidbody>();
		foreach (var col in cols)
		{
			if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
			{
				rigidbodies.Add(col.attachedRigidbody);
			}
		}
		foreach (var rb in rigidbodies)
		{
			rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, 1*multiplier, ForceMode.Impulse);
			if(dealDamage && rb.CompareTag("Player")) {	//if(attacker) means dynamite attached
				PlayerHealth ph = rb.GetComponent<PlayerHealth> ();
				ph.TakeDamage(damage);
				ph.lastAttacker = whoFired;
			}
		}
		StartCoroutine (WaitDelay ());
	}

	public IEnumerator WaitDelay (){
		yield return new WaitForSeconds(4f);
		Destroy (gameObject);
	}
}
