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
	private GameObject rocketPickup, plowPickup, dynamitePickup, shieldPickup, healthPickup, spikePickup;
	private GameObject[] pickups = new GameObject[6];

	// Use this for initialization
	void Awake () {
		ins = this;

		rocketPickup = Resources.Load ("RocketPickup") as GameObject;
		plowPickup = Resources.Load ("PlowPickup") as GameObject;
		dynamitePickup = Resources.Load ("DynamitePickup") as GameObject;
		shieldPickup = Resources.Load ("ShieldPickup") as GameObject;
		healthPickup = Resources.Load ("HealthPickup") as GameObject;
		spikePickup = Resources.Load ("SpikePickup") as GameObject;
		pickups[0] = rocketPickup;
		pickups [1] = plowPickup;
		pickups [2] = dynamitePickup;
		pickups [3] = shieldPickup;
		pickups [4] = healthPickup;
		pickups [5] = spikePickup;
		floorPieces = GameObject.Find ("Environment").transform.Find ("Floor").GetComponentsInChildren<Transform> ();
		int i = 1;
		while (numOnMap < maxDrops) {
			if (!CheckForPlayer (floorPieces [i])) {
				float f = Random.Range (0f, 1f);
				ChooseItem (f, i);
			}
			i++;
			if (i == floorPieces.Length)
				i = 1;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!GameManager.ins.gameOver && numOnMap < maxDrops) {
			timer += Time.deltaTime;
			if (timer >= spawnDelay) {
				for (int i = 1; i < floorPieces.Length; i++) {
					if (!CheckForPlayer (floorPieces [i])) {
						float f = Random.Range (0f, 1f);
						ChooseItem (f, i);
						break;
					}
				}
				timer = 0f;
			}

		}
	}

	//False if it finds a player near
	public bool CheckForPlayer(Transform t) {
		RaycastHit hit;
		Vector3 p1 = new Vector3(t.position.x, t.position.y + 1f, t.position.z);

		Collider[] hitColliders = Physics.OverlapSphere (p1, 7f);
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
		if (c <= .075f) {
				Instantiate (rocketPickup, floorPieces [i].position, Quaternion.identity);
		} else if (c <= .15f) {
				Instantiate (shieldPickup, floorPieces [i].position, Quaternion.identity);
		} else if (c <= .3f) {
				Instantiate (plowPickup, floorPieces [i].position, Quaternion.identity);
		} else if (c <= .48f) {
				Instantiate (healthPickup, floorPieces [i].position, Quaternion.identity);
		} else if (c <= .74f) {
			Instantiate (spikePickup, floorPieces [i].position, Quaternion.identity);
		} else if (c <= 1f) {
			Instantiate (dynamitePickup, floorPieces [i].position, Quaternion.identity);
		}
		numOnMap++;
	}
}
