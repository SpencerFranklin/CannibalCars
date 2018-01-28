using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUI : MonoBehaviour {

	Hashtable table = new Hashtable();
	public Sprite baby, dee, max, yolk;
	// Use this for initialization
	void Start () {
		table.Add ("Baby", baby);
		table.Add ("Dee", dee);
		table.Add ("Max", max);
		table.Add ("Yolk", yolk);

		foreach (string s in GlobalControl.ins.playerCars.Keys) {
			if (s.Equals("P1") && !GlobalControl.ins.playerCars["P1"].Equals("")) {
				GameObject.Find ("PlayerInfo1").SetActive (true);
				GameObject.Find ("PlayerInfo1").GetComponent<PlayerUI> ().pic.sprite = (Sprite) table [(string) GlobalControl.ins.playerCars ["P1"]];
			}
			if (s.Equals("P2") && !GlobalControl.ins.playerCars["P2"].Equals("")) {
				GameObject.Find ("PlayerInfo2").SetActive (true);
				GameObject.Find ("PlayerInfo2").GetComponent<PlayerUI> ().pic.sprite = (Sprite) table [(string) GlobalControl.ins.playerCars ["P2"]];
			}
			if (s.Equals("P3") && !GlobalControl.ins.playerCars["P3"].Equals("")) {
				GameObject.Find ("PlayerInfo3").SetActive (true);
				GameObject.Find ("PlayerInfo3").GetComponent<PlayerUI> ().pic.sprite = (Sprite) table [(string) GlobalControl.ins.playerCars ["P3"]];
			}
			if (s.Equals("P4") && !GlobalControl.ins.playerCars["P4"].Equals("")) {
				GameObject.Find ("PlayerInfo4").SetActive (true);
				GameObject.Find ("PlayerInfo4").GetComponent<PlayerUI> ().pic.sprite = (Sprite) table [(string) GlobalControl.ins.playerCars ["P4"]];
			}



		}
		if (!GlobalControl.ins.playerCars ["P4"].Equals(null) && GlobalControl.ins.playerCars ["P4"].Equals ("")) {
			GameObject.Find ("PlayerInfo4").SetActive (false);
		}
		if (!GlobalControl.ins.playerCars ["P3"].Equals(null) && GlobalControl.ins.playerCars ["P3"].Equals ("")) {
			GameObject.Find ("PlayerInfo3").SetActive (false);
		}
		if (!GlobalControl.ins.playerCars ["P2"].Equals(null) && GlobalControl.ins.playerCars ["P2"].Equals ("")) {
			GameObject.Find ("PlayerInfo2").SetActive (false);
		}
		if (!GlobalControl.ins.playerCars ["P1"].Equals(null) && GlobalControl.ins.playerCars ["P1"].Equals ("")) {
			GameObject.Find ("PlayerInfo1").SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
