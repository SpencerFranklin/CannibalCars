using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteractionManager : MonoBehaviour {

	//public bool rocket,plow,hammer = false;
	public bool upright = true;
	private GameObject rocketPrefab, plowPrefab, dynamitePrefab, shieldFPrefab, shieldTPrefab;
	private Hashtable weapons = new Hashtable();
	private Vector3 floorPos;
	public float TossSpeed = 10f;
	// Use this for initialization

	void Awake() {
		weapons.Add ("Rocket", false);
		weapons.Add ("Plow", false);
		weapons.Add ("Hammer", false);
		weapons.Add ("Dynamite", false);
		weapons.Add ("Shield", false);

	}

	void Start () {
		rocketPrefab = Resources.Load ("Rocket") as GameObject;
		plowPrefab = Resources.Load ("Plow") as GameObject;
		dynamitePrefab = Resources.Load ("Dynamite") as GameObject;
		shieldFPrefab = Resources.Load ("ShieldF") as GameObject;
		shieldTPrefab = Resources.Load ("ShieldT") as GameObject;



	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!upright) {
			floorPos = new Vector3 (transform.rotation.x, transform.rotation.y, 1f);
			Quaternion targetRotation = Quaternion.LookRotation (floorPos - transform.position);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, .01f);
			if (targetRotation == transform.rotation) {
				upright = true;
			}
		}
	}

	public void ResetWeapons() {
		if(transform.Find("AttachFront").childCount > 0)
			Destroy(transform.Find("AttachFront").GetChild(0).gameObject);
		if(transform.Find("AttachTop").childCount > 0)
			Destroy(transform.Find("AttachTop").GetChild(0).gameObject);
		weapons["Rocket"] = false;
		weapons["Plow"] = false;
		weapons["Hammer"] = false;
		weapons["Dynamite"] = false;
		weapons["Shield"] = false;
	}

	void OnTriggerEnter(Collider col){
		string t = col.gameObject.tag;
		if (t == "Item") {
			string name = col.gameObject.name;
			bool nopick = false;
			/*foreach (bool b in weapons) {
				if (!b) {
					nopick = true;
					Debug.Log ("true");
				}
			}*/

			foreach (string s in weapons.Keys) {
				if ((bool)weapons [s]) {
					nopick = true;
				}
			}

			//determine what to do with pickup item based on name (or tag?)
			switch (name) {
			case "PlowPickup(Clone)":
				if (nopick)
					break;
				Destroy (col.gameObject);
				PickupManager.ins.numOnMap--;
				AddPlow ();
				break;
			case "ShieldPickup(Clone)":
				if (nopick)
					break;
				Destroy (col.gameObject);
				PickupManager.ins.numOnMap--;
				AddShield ();
				break;
			case "RocketPickup(Clone)":
				if (nopick)
					break;
				Destroy (col.gameObject);
				PickupManager.ins.numOnMap--;
				AddRocket ();
				break;
			case "DynamitePickup(Clone)":
				if (nopick)
					break;
				Destroy (col.gameObject);
				PickupManager.ins.numOnMap--;
				AddDynamite ();
				break;
			case "HealthPickup(Clone)":
				if (this.GetComponent<PlayerHealth> ().curHealth < 100) {
					Destroy (col.gameObject);
					if (this.GetComponent<PlayerHealth> ().curHealth + 10 > 100) {
						this.GetComponent<PlayerHealth> ().curHealth = 100;
					} else {

						this.GetComponent<PlayerHealth> ().curHealth += 10;
					}

				}
				break;
			default:
				break;
			}
		} else if (col.name == "Transmission(Clone)") {
			Destroy (col.gameObject);
			this.GetComponent<PlayerScore> ().score += 3;
		}
	}

	void AddRocket(){
		GameObject rkt = Instantiate (rocketPrefab, this.gameObject.transform.Find ("AttachTop"));
		StartCoroutine (SmallDelay ());
	}

	void AddPlow (){
		weapons["Plow"] = true;
		GameObject plw = Instantiate (plowPrefab, this.gameObject.transform.Find ("AttachFront"));
		StartCoroutine (PlowTimer());
	}

	void AddDynamite(){
		weapons ["Dynamite"] = true;
		GameObject bmb = Instantiate (dynamitePrefab, this.gameObject.transform.Find ("AttachTop"));
	}

	void AddShield(){
		weapons ["Shield"] = true;
		GameObject shf = Instantiate (shieldFPrefab, this.gameObject.transform.Find ("AttachFront"));
		GameObject sht = Instantiate (shieldTPrefab, this.gameObject.transform.Find ("AttachTop"));
		this.GetComponent<PlayerHealth> ().armoured = true;
		StartCoroutine (ShieldTimer (shf, sht));
	}

	public IEnumerator ShieldTimer (GameObject sF, GameObject sT){
		yield return new WaitForSeconds(15f);
		Destroy (sF);
		Destroy (sT);
		this.GetComponent<PlayerHealth> ().armoured = false;
		weapons["Shield"] = false;
	}

	public IEnumerator SmallDelay (){
		yield return new WaitForSeconds(.5f);
		weapons["Rocket"] = true;
	}

	public IEnumerator PlowTimer(){
		yield return new WaitForSeconds(8f);
		GameObject obj = this.gameObject.transform.Find ("AttachFront").gameObject;
		Destroy (obj.transform.Find("Plow(Clone)").gameObject);
		weapons["Plow"] = false;
	}

	public void CarAttack(){
		if ((bool)weapons ["Rocket"]) {
			GameObject g = this.gameObject.transform.Find ("AttachTop").gameObject;
			if (g.transform.childCount > 0) {
				weapons ["Rocket"] = false;
				g = g.transform.GetChild (0).gameObject;
				Rocket r = g.GetComponent<Rocket> ();
				r.whoFired = this.gameObject;
				r.Launch ();
			}
		} else if ((bool)weapons ["Dynamite"]) {
			GameObject g = this.gameObject.transform.Find ("AttachTop").gameObject;
			if (g.transform.childCount > 0) {
				g = g.transform.GetChild (0).gameObject;
				g.transform.parent = null;
				/*Vector3 vel = (this.transform.forward * TossSpeed + this.transform.up * 15);
				g.AddComponent<Rigidbody> ().velocity = vel;
				g.GetComponent<Rigidbody> ().mass = 10000;*/
				g.AddComponent<Rigidbody> ();
				g.GetComponent<Rigidbody> ().AddForce (this.gameObject.transform.forward * 1500f);
				g.GetComponent<Rigidbody> ().AddForce (Vector3.up * 800f);
				g.GetComponent<Rigidbody> ().AddForce (Physics.gravity * 2f);
				g.GetComponent<Dynamite> ().whoFired = this.gameObject;
				weapons ["Dynamite"] = false;
			}
		}
	}
}
