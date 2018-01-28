using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour {
	private bool blow = false;
	private float maxIntensity = 200f, minIntensity = 0f;
	private float nextIntensity = 200;
	private float speed = .5f;
	public GameObject whoFired;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (blow) {
			//Debug.Log (this.gameObject.transform.Find ("dynamite_v02").name);
			if (this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ()) {
				this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ().intensity = Mathf.Lerp (this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ().intensity, nextIntensity, speed * Time.deltaTime);
				//Debug.Log (this.gameObject.transform.Find ("dynamite_v02").gameObject.transform.Find ("light").GetComponent<Light> ().intensity);
				if (this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ().intensity >= maxIntensity - 150f) {
					nextIntensity = minIntensity;
					speed += .2f;
				}
				if (this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ().intensity <= minIntensity + 20f) {
					nextIntensity = maxIntensity;
					speed += .2f;
				}
			}
		}
	}

	void OnCollisionEnter(Collision col){
		Destroy (this.gameObject.GetComponent<Rigidbody> ());
		if (this.gameObject.GetComponent<CapsuleCollider> ()) {
			this.gameObject.GetComponent<CapsuleCollider> ().enabled = false;
			Debug.Log ("1");
		}
		if (this.gameObject.transform.Find ("dynamite_v02").GetComponent<CapsuleCollider> ()) {
			this.gameObject.transform.Find ("dynamite_v02").GetComponent<CapsuleCollider> ().enabled = false;
			Debug.Log ("2");
		}
		//this.gameObject.transform.parent = col.gameObject.transform;
		blow = true;
		StartCoroutine (Timer ());
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds (Random.Range (3f, 4f));
		this.gameObject.transform.Find ("dynamite_v02").Find ("light").GetComponent<Light> ().intensity = 0;
		blow = false;
		//do explosion
		MyExplosionPhysicsForce physics = explosion.GetComponent<MyExplosionPhysicsForce>();
		physics.dealDamage = true;
		physics.explosionForce = 3000;
		physics.whoFired = whoFired;
		Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
