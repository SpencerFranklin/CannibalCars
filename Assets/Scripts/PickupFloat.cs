using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloat : MonoBehaviour {
	private float rotSpeed = 25;
	private float moveSpeed = 1f;
	private Vector3 startPos, endPos;
	private float startTime, len;
	// Use this for initialization
	void Start () {
		startPos = this.transform.position + new Vector3(0,1,0);
		endPos = startPos - new Vector3 (0, 1f, 0);
		startTime = Time.time;
		len = Vector3.Distance (startPos, endPos);
	}
	
	// Update is called once per frame
	void Update () {
		float dist = (Time.time - startTime) * moveSpeed;
		this.transform.Rotate (0, 1 * Time.deltaTime * rotSpeed, 0);
		this.transform.position = Vector3.Lerp (startPos, endPos, dist/len);

		if (this.gameObject.transform.position == endPos) {
			Vector3 tmp = startPos;
			startPos = endPos;
			endPos = tmp;
			startTime = Time.time;
			len = Vector3.Distance (startPos, endPos);
		}
	}
}
