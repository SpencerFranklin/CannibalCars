using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (UnityStandardAssets.Vehicles.Car.CarController))]

public class MyCarUserControl : MonoBehaviour
{
	CarInteractionManager ciManager;
	public string id;
	public float footbrakeMultiplier = 5f;
	public float flipTimeout = 2f;
	private float timer;
	private float lastTurn = 0;
	private UnityStandardAssets.Vehicles.Car.CarController m_Car;

	private void Awake()
	{
		timer = -flipTimeout;
		ciManager = gameObject.GetComponent<CarInteractionManager> ();
		m_Car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
		id = gameObject.name;
	}

	private void FixedUpdate()
	{
		if (!GameManager.ins.gameOver) {
			timer += Time.deltaTime;
			if (CrossPlatformInputManager.GetButtonDown (id + "Attack")) {
				ciManager.CarAttack ();
			}
			if (CrossPlatformInputManager.GetButtonDown (id + "Flip") && timer >= flipTimeout) {
				timer = 0f;
				ciManager.upright = false;
			}

			if (!id.StartsWith ("P")) {
				id = "";
			}

			float v = CrossPlatformInputManager.GetAxis (id + "Vertical");
			float h = CrossPlatformInputManager.GetAxis (id + "Horizontal");

			float footbrake = -1f * CrossPlatformInputManager.GetAxis (id + "Brake");
			if (gameObject.GetComponent<Rigidbody> ().velocity.magnitude > .1f) {
				footbrake *= footbrakeMultiplier;
			}

			float handbrake = CrossPlatformInputManager.GetAxis (id + "Drift");

			m_Car.Move (h, v, footbrake, handbrake);
		}
	}
}