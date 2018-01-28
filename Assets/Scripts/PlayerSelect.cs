using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets.CrossPlatformInput;


public class PlayerSelect : MonoBehaviour {
	public List<Sprite> cars = new List<Sprite> ();
	private Image pic;
	private bool canSwitch = true, locked = false;
	public Text text;
	public GameObject check;
	int curr= 0; 
	public string player;
	// Use this for initialization
	void Start () {
		pic = this.transform.Find ("Picture").GetComponent<Image>(); 
		pic.sprite = cars [curr];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		text.text = cars[curr].name;
		if (!locked) {
			if (CrossPlatformInputManager.GetAxis (player + "Horizontal") > 0.1f && canSwitch) {
				if (curr < cars.Count - 1) {
					curr++;
					canSwitch = false;
				}
			}
			if (CrossPlatformInputManager.GetAxis (player + "Horizontal") < -0.1f && canSwitch) {
				if (curr > 0) {
					curr--;
					canSwitch = false;
				}
			}
			if (CrossPlatformInputManager.GetAxis (player + "Horizontal") == 0f) {
				canSwitch = true;
			}
		} else {


		}

		if(CrossPlatformInputManager.GetButtonDown(player + "Drift")) {
			if (!locked) {
				locked = true;
				check.SetActive (true);
				MenuManager.ins.playerCars [player] = cars [curr].name;
				MenuManager.ins.lockCount++;
			}
		}

		if(CrossPlatformInputManager.GetButtonDown(player + "Flip")) {
			if (locked) {
				locked = false;
				//MenuManager.ins.playerCars [player] = "";
				MenuManager.ins.lockCount--;
				check.SetActive (false);
			}
		}
			

		pic.sprite = cars [curr];
		//Debug.Log (CrossPlatformInputManager.GetAxis ("P1Horizontal"));
	}

}
