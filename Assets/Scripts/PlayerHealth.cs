using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	//maxHealth get overriden by PlayerManager
	public int maxHealth = 100;
	public float minY = -10f;
	public int curHealth;
	public bool dead = false;
	public float lastAttackerTimer = 5f;
	public bool armoured = false;
	private float timer = 0;
	public GameObject lastAttacker = null;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(lastAttacker) {
			timer += Time.deltaTime;
			if (timer >= lastAttackerTimer) {
				lastAttacker = null;
				timer = 0f;
			}
		}

		if (transform.position.y < minY) {
			gameObject.GetComponent<PlayerScore> ().score -= 1;
			dead = true;
		}
		if (dead) {
			if (lastAttacker) {
				lastAttacker.GetComponent<PlayerScore> ().score += 1;
			}
			dead = false;
			curHealth = maxHealth;
			PlayerManager.ins.playerDead (this.gameObject);
		}
	}

	public void TakeDamage(int d) {
		if (armoured) {
			curHealth -= (d/2);
		} else {
			curHealth -= d;
		}

		if (curHealth <= 0) {
			dead = true;
		}
	}
}
