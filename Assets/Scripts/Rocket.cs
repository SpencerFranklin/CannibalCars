using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	public int damage = 5;
	public bool activate = false, loop = false, attack = false;
	private float moveSpeed = 25f;
	private float rotSpeed = 30f;
	private float rocketSpeed = 10f;
	// Use this for initialization
	private Vector3 startPos, endPos, influencePos, bezierCurvePos;
	private GameObject target;
	private float startTime, len, distCovered, t;
	private Vector3 prev, curr;
	public GameObject whoFired;
	public GameObject Explosion, RocketTrail;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, 1 * Time.deltaTime * rotSpeed, 0);

		if (activate) {
			float dist = (Time.time - startTime) * moveSpeed;
			this.transform.position = Vector3.Lerp (startPos, endPos, dist/len);
			if (this.gameObject.transform.position == endPos) {
				activate = false;
				Loop ();
			}
		}

		if (loop) {
			this.transform.RotateAround (endPos, new Vector3(1,0,0), 200f * Time.deltaTime);
			StartCoroutine (ShootRocket ());
		}

		if (attack) {
			endPos = target.transform.position;
			float dist = (Time.time - startTime) * (moveSpeed + 5f);
			this.transform.position = Vector3.Lerp (startPos, endPos, dist/len);

			transform.LookAt(endPos);
			this.transform.Rotate (90, 0, 0);

			if (this.gameObject.transform.position == endPos) {
				attack = false;
				MyExplosionPhysicsForce physics = Explosion.GetComponent<MyExplosionPhysicsForce>();
				physics.dealDamage = false;
				physics.whoFired = whoFired;
				physics.explosionForce = 1500;

				Instantiate (Explosion, this.transform.position, Quaternion.identity);
				Destroy (this.gameObject);
				PlayerHealth ph = target.GetComponent<PlayerHealth> ();
				ph.TakeDamage (damage);
				ph.lastAttacker = whoFired;
			}
		}
	}

	public void Launch(){
		RocketTrail.SetActive (true);
		startPos = this.transform.position;
		endPos = this.transform.position + new Vector3(0, 70f, 0);
		startTime = Time.time;
		len = Vector3.Distance (startPos, endPos);

		activate = true;
		this.transform.parent = null;
		float maxDistance = 1000000;
		foreach (GameObject o in PlayerManager.ins.playersInScene) {
			if (o.name != whoFired.name) {
				float d = Vector3.Distance (this.transform.position, o.transform.position);
				if (d < maxDistance) {
					maxDistance = d;
					target = o;
				}
			}
		}
	}

	public void Loop(){
		loop = true;

		startPos = this.transform.position;
		endPos = this.transform.position + new Vector3 (0, 0, 5);
		startTime = Time.time;
		len = Vector3.Distance (startPos, endPos);

		influencePos = new Vector3 ((endPos.x + startPos.x)/2f, endPos.y + 5f, (endPos.z + startPos.z)/2f);
		curr = this.transform.position;

	}

	IEnumerator ShootRocket(){
		yield return new WaitForSeconds(2.8f);
		startPos = this.transform.position;
		endPos = target.transform.position;
		startTime = Time.time;
		len = Vector3.Distance (startPos, endPos);
		loop = false;
		attack = true;
		if (target == null) {
			target = PlayerManager.ins.playersInScene [Random.Range (0, PlayerManager.ins.playersInScene.Count - 1)];
		}
	}
}
