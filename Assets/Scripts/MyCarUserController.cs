using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof (CarController))]

public class MyCarUserControl : MonoBehaviour
{
	
	public string id;
	private CarController m_Car; // the car controller we want to use

	public MyCarUserControl(string id_){
		id = id_;
	}

	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
	}

	private void FixedUpdate()
	{
		// pass the input to the car!
		float h = CrossPlatformInputManager.GetAxis(id + "Horizontal");
		float v = CrossPlatformInputManager.GetAxis(id + "Vertical");
		#if !MOBILE_INPUT
		float handbrake = CrossPlatformInputManager.GetAxis(id + "Jump");
		m_Car.Move(h, v, v, handbrake);
		#else
		m_Car.Move(h, v, v, 0f);
		#endif
	}
}