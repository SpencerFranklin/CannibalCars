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
	private UnityStandardAssets.Vehicles.Car.CarController m_Car; // the car controller we want to use

	private void Awake()
	{
		timer = -flipTimeout;
		ciManager = gameObject.GetComponent<CarInteractionManager> ();
		// get the car controller
		m_Car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
		id = gameObject.name;
	}

	private void FixedUpdate()
	{
		timer += Time.deltaTime;
		if(CrossPlatformInputManager.GetButtonDown(id + "Attack")) {
			ciManager.CarAttack ();
		}
		if(CrossPlatformInputManager.GetButtonDown(id + "Flip") && timer >= flipTimeout) {
			timer = 0f;
			ciManager.upright = false;
		}

		// pass the input to the car!
		if(!id.StartsWith("P")) {
			id = "";
		}

		//v will always be positive
		//float v = Mathf.Abs(CrossPlatformInputManager.GetAxis(id + "Vertical")) + Mathf.Abs(CrossPlatformInputManager.GetAxis(id + "Horizontal"));

		float v = CrossPlatformInputManager.GetAxis(id + "Vertical");
		float h = CrossPlatformInputManager.GetAxis(id + "Horizontal");

		float footbrake = -1f * CrossPlatformInputManager.GetAxis (id + "Brake");
		if (gameObject.GetComponent<Rigidbody> ().velocity.magnitude > .1f) {
			footbrake *= footbrakeMultiplier;
		}

		float handbrake = CrossPlatformInputManager.GetAxis(id + "Drift");

		m_Car.Move(h, v, footbrake, handbrake);
	}
}