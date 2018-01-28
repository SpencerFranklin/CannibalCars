using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl ins;
	public Hashtable playerCars = new Hashtable();
	public int numPlayers;
	// Use this for initialization
	void Awake () {
		if (ins == null) {
			DontDestroyOnLoad (gameObject);
			ins = this;
		} 
	}

	void Start() {
		

	}
	// Update is called once per frame
	void Update () {
	}
}
