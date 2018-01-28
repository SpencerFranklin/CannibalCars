using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager ins;
	public float roundDuration = 210f;
	int P1score, P2score, p3Score, p4Score;
	private float gameTimer = -5f;
	public bool gameOver;

	// Use this for initialization
	void Awake () {
		ins = this;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		gameTimer += Time.timeSinceLevelLoad;

		if (gameTimer >= roundDuration) {

		}
	}
}
