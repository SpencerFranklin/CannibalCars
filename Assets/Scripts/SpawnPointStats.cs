using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointStats : MonoBehaviour {

	public bool open = true;

	void Awake() {
		CheckForPlayer ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!open) {
			StartCoroutine (resetDelay ());
		}
	}

	public IEnumerator resetDelay (){
		yield return new WaitForSeconds(2f);
		open = true;
	}

	public void CheckForPlayer() {
		RaycastHit hit;
		Vector3 p1 = transform.position;

		Collider[] hitColliders = Physics.OverlapSphere (p1, 10f);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].gameObject.CompareTag ("Player")) {
				open = false;
			}
			i++;
		}
	}
}
