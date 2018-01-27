using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerManager : MonoBehaviour {

	public int numPlayers = 4;
	//List<CarUserControl> playerList = new List<CarUserControl> ();
	public List<GameObject> playersInScene = new List<GameObject> ();
	public List<GameObject> spawnPoints = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numPlayers; i++) {
			GameObject o = Instantiate (Resources.Load ("truck", typeof(GameObject))) as GameObject;
			string id = "P" + (i + 1);

			o.GetComponent<MyCarUserControl> ().id = id;
			o.name = id;

			int pick = Random.Range (0, numPlayers);
			while(!spawnPoints [pick].GetComponent<SpawnPointStats> ().open) {
				pick = Random.Range (0, numPlayers);
			}

			o.transform.position = spawnPoints [pick].transform.position;
			playersInScene.Add (o);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
