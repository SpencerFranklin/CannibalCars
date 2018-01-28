using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour {
	public string player;
	public Image pic;
	public Text health;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.Equals ("P1")) {
			pic.GetComponent<Image>().color = new Color (159f/255f, 11f/255f, 16f/255f);
		} else if (player.Equals ("P2")) {
			pic.GetComponent<Image>().color = new Color (19f/255f, 0f, 1f);
		} else if (player.Equals ("P3")) {
			pic.GetComponent<Image>().color = new Color (1f, 216f/255f, 0f);
		} else if (player.Equals ("P4")) {
			pic.GetComponent<Image>().color = new Color (38f/255f, 240f/255f, 0f);
		}

		foreach (GameObject o in PlayerManager.ins.playersInScene) {
			if (o.name.Equals (player)) {
				//Debug.Log (o.GetComponent<PlayerHealth> ().curHealth.ToString());

				health.GetComponent<Text>().text = o.GetComponent<PlayerHealth> ().curHealth.ToString();

			}

		}
	}
}
