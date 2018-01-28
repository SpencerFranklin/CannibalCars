using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class MenuManager : MonoBehaviour {
	public GameObject menuButtons, numPlayers, logo, p1, p2, p3, p4, levelSelect;
	public Hashtable playerCars = new Hashtable();
	public static MenuManager ins;
	public List<Sprite> levels = new List<Sprite>();
	private int players = -1, curr = 0;
	public int lockCount = 0;
	private bool pickMap= false, canSwitch = true;
	// Use this for initialization
	void Start () {
		playerCars.Add ("P1", "-");
		playerCars.Add ("P2", "-");
		playerCars.Add ("P3", "-");
		playerCars.Add ("P4", "-");

		numPlayers.SetActive (false);
		menuButtons.SetActive (true);
	}
	void Awake(){
		ins = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (lockCount == players) {

			SaveData ();
			players = -1;
			//SceneManager.LoadScene ("Level1");
			pickMap = true;
		}

		if (pickMap) {
			
			if (CrossPlatformInputManager.GetAxis ("P1Horizontal") > 0.1f && canSwitch) {
				if (curr < levels.Count - 1) {
					curr++;
					canSwitch = false;
				}
			}
			if (CrossPlatformInputManager.GetAxis ("P1Horizontal") < -0.1f && canSwitch) {
				if (curr > 0) {
					curr--;
					canSwitch = false;
				}
			}
			if (CrossPlatformInputManager.GetAxis ("P1Horizontal") == 0f) {
				canSwitch = true;
			}

			if(CrossPlatformInputManager.GetButtonDown("P1Drift")) {
				SceneManager.LoadScene ("Level1");
				pickMap = false;
			}


		}
	}

	public void SaveData() {
		GlobalControl.ins.numPlayers = players;
		GlobalControl.ins.playerCars = playerCars;
	}
	public void Quit(){
		Application.Quit ();
	}

	public void NumberOfPlayers(){
		menuButtons.SetActive (false);
		numPlayers.SetActive (true);
	}

	public void ThreePlay(){
		numPlayers.SetActive (false);
		logo.SetActive (false);
		p1.SetActive (true);
		p2.SetActive (true);
		p3.SetActive (true);
		levelSelect.SetActive (true);
		players = 3;

	}

	public void TwoPlay(){
		numPlayers.SetActive (false);
		logo.SetActive (false);
		p1.SetActive (true);
		p2.SetActive (true);
		levelSelect.SetActive (true);

		players = 2;
	}

	public void FourPlay(){
		numPlayers.SetActive (false);
		logo.SetActive (false);
		p1.SetActive (true);
		p2.SetActive (true);
		p3.SetActive (true);
		p4.SetActive (true);
		levelSelect.SetActive (true);

		players = 4;
	}
}
