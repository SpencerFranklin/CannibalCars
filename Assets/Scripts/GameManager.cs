using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float roundDuration = 210f;
	int p1Score, p2Score, p3Score, p4Score;
	private float gameTimer = -5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameTimer += Time.timeSinceLevelLoad;

		if (gameTimer >= roundDuration) {

		}
	}
}
