using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
	bool down, up, moving;
	public float downSpeed, upSpeed, downDelay, upDelay;
	public GameObject left, right;
	public Transform pivotLeft, pivotRight;
	float timer;

	// Use this for initialization
	void Start () {
		down = true;
		up = false;
		moving = false;
		timer = 0;
	}

	// Update is called once per frame
	void Update () {
		if (!moving) {
			timer += Time.deltaTime;
			if (down && timer > downDelay) {
				timer = 0;
				moving = true;
			}
			if (!down && timer > upDelay) {
				timer = 0;
				moving = true;
			}
		}

		if (moving && down) {
			left.transform.RotateAround (pivotLeft.position, Vector3.forward, Time.deltaTime * downSpeed);
			right.transform.RotateAround (pivotRight.position, Vector3.forward, -Time.deltaTime * downSpeed);
		} else if (moving && !down) {
			left.transform.RotateAround (pivotLeft.position, Vector3.forward, -Time.deltaTime * upSpeed);
			right.transform.RotateAround (pivotRight.position, Vector3.forward, Time.deltaTime * upSpeed);
		}

		float angle = left.transform.eulerAngles.z;
		angle = (angle > 180) ? angle - 360 : angle;
		if (down && angle >= 90) {
			down = false;
			moving = false;
		}
		if (!down && angle <= 0) {
			down = true;
			moving = false;
		}
	}
}
