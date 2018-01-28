using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager ins;
	public int numPlayers = -1;
	public int maxPlayerHealth = 100;
	public float respawnDelay = 5;
	GameObject[] spawnPoints;
	public List<GameObject> playersInScene = new List<GameObject> ();
	public GameObject explosion;
	private Hashtable playerCars = new Hashtable();

	void Awake() {
		ins = this;
		//playerCars.Add ("P1", "Dee");
		playerCars = GlobalControl.ins.playerCars;
		/*foreach (KeyCode k in GlobalControl.ins.playerCars) {
			playerCars.Add (k, GlobalControl.ins.playerCars [k]);
		}*/
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawnpoint");
		numPlayers = GlobalControl.ins.numPlayers;
		Debug.Log (numPlayers);
		for (int i = 0; i < numPlayers; i++) {
			string id = "P" + (i + 1);
			Debug.Log (id);
			GameObject o = Instantiate (Resources.Load ((string)playerCars[id], typeof(GameObject))) as GameObject;

			o.GetComponent<MyCarUserControl> ().id = id;
			o.GetComponent<PlayerHealth> ().maxHealth = maxPlayerHealth;
			o.name = id;

			SpawnPlayer (o);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playerDead(GameObject go) {
		Vector3 pos = go.transform.position;
		playersInScene.Remove (go);
		go.GetComponent<CarInteractionManager> ().ResetWeapons ();
		go.SetActive (false);
		GameObject o = Instantiate (Resources.Load ("Transmission", typeof(GameObject))) as GameObject;
		o.transform.position = pos;
		MyExplosionPhysicsForce expl = explosion.GetComponent<MyExplosionPhysicsForce>();
		expl.dealDamage = false;
		expl.explosionForce = 1500;
		Instantiate (explosion, go.transform.position, Quaternion.identity);
		StartCoroutine (RespawnTimer (go));
	}

	public IEnumerator RespawnTimer(GameObject go){
		yield return new WaitForSeconds(respawnDelay);
		SpawnPlayer (go);
	}

	void SpawnPlayer(GameObject go) {
		int pick = Random.Range (0, spawnPoints.Length);
		SpawnPointStats posStats = spawnPoints [pick].GetComponent<SpawnPointStats> ();
		posStats.CheckForPlayer ();

		while(!posStats.open) {
			pick = Random.Range (0, spawnPoints.Length);
			posStats = spawnPoints [pick].GetComponent<SpawnPointStats> ();
			posStats.CheckForPlayer ();
		}

		GameObject spawnPos = spawnPoints [pick];
		posStats.open = false;
		go.transform.position = spawnPos.transform.position;
		go.transform.rotation = spawnPos.transform.rotation;
		go.SetActive(true);
		playersInScene.Add (go);
	}


}
