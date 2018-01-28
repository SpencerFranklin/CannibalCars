using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

	public static PickupManager ins;
	public int maxDrops = 10;
	//public float spawnProbability = .3f;
	public float spawnDelay = 3f;
	public Transform[] floorPieces;
	public int numOnMap = 0;
	private float timer = -5f;
	private GameObject rocketPickup, plowPickup, dynamitePickup, shieldPickup, healthPickup;
	private GameObject[] pickups = new GameObject[5];

	// Use this for initialization
	void Awake () {
		ins = this;

		rocketPickup = Resources.Load ("RocketPickup") as GameObject;
		plowPickup = Resources.Load ("PlowPickup") as GameObject;
		dynamitePickup = Resources.Load ("DynamitePickup") as GameObject;
		shieldPickup = Resources.Load ("ShieldPickup") as GameObject;
		healthPickup = Resources.Load ("HealthPickup") as GameObject;
		pickups[0] = rocketPickup;
		pickups [1] = plowPickup;
		pickups [2] = dynamitePickup;
		pickups [3] = shieldPickup;
		pickups [4] = healthPickup;
		floorPieces = GameObject.Find ("Environment").transform.Find ("Floor").GetComponentsInChildren<Transform> ();
		int i = 1;
		while (numOnMap < maxDrops) {
			float f = Random.Range (0f, 1f);
			ChooseItem (f, i);
			i++;
			if (i == floorPieces.Length)
				i = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (numOnMap < maxDrops) {
			timer += Time.deltaTime;
			if (timer >= spawnDelay) {
				for (int i = 1; i < floorPieces.Length; i++) {
					float f = Random.Range (0f, 1f);
					ChooseItem (f, i);
				}
				timer = 0f;
			}
		}
	}

	//False if it finds a player near
	public bool CheckForPlayer(Transform t) {
		RaycastHit hit;
		Vector3 p1 = t.position;

		Collider[] hitColliders = Physics.OverlapSphere (p1, 5f);
		int i = 1;
		while (i < hitColliders.Length) {
			if (hitColliders [i].gameObject.CompareTag ("Player") || hitColliders [i].gameObject.CompareTag ("Item")) {
				return true;
			}
			i++;
		}
		return false;
	}

	private void ChooseItem(float c,  int i) {
		if (c <= .1f) {
			//c = Random.Range (0, pickups.Length);
			if (!CheckForPlayer (floorPieces [i])) {
				Instantiate (rocketPickup, floorPieces [i].position, Quaternion.identity);
				numOnMap++;
			}
		} else if (c <= .25f) {
			//c = Random.Range (0, pickups.Length);
			if (!CheckForPlayer (floorPieces [i])) {
				Instantiate (shieldPickup, floorPieces [i].position, Quaternion.identity);
				numOnMap++;
			}
		} else if (c <= .45f) {
			//c = Random.Range (0, pickups.Length);
			if (!CheckForPlayer (floorPieces [i])) {
				Instantiate (plowPickup, floorPieces [i].position, Quaternion.identity);
				numOnMap++;
			}
		} else if (c <= .7f) {
			//c = Random.Range (0, pickups.Length);
			if (!CheckForPlayer (floorPieces [i])) {
				Instantiate (healthPickup, floorPieces [i].position, Quaternion.identity);
				numOnMap++;
			}
		} else if (c <= 1f) {
			//c = Random.Range (0, pickups.Length);
			if (!CheckForPlayer (floorPieces [i])) {
				Instantiate (dynamitePickup, floorPieces [i].position, Quaternion.identity);
				numOnMap++;
			}
		}
	}
}
