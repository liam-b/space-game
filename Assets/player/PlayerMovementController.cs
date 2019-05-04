using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public ShipController ship;
	[HideInInspector] public List<ThrustingPartController> thrusters;
	[HideInInspector] public List<GyroscopicPartController> gyroscopes;

  public KeyCode activateEngine;
	public KeyCode rotateLeft;
	public KeyCode rotateRight;

	void Start() {
		thrusters = ship.GetParts<ThrustingPartController>();
		gyroscopes = ship.GetParts<GyroscopicPartController>();
	}

  void Update() {
		if (Input.GetKeyDown(activateEngine)) {
			foreach (ThrustingPartController thruster in thrusters) {
				thruster.active = true;
			}
		} 
		
		if (Input.GetKeyUp(activateEngine)) {
			foreach (ThrustingPartController thruster in thrusters) {
				thruster.active = false;
			}
		}

		if (Input.GetKey(rotateLeft)) {
			foreach (GyroscopicPartController gyroscope in gyroscopes) {
				gyroscope.direction = 1;
			}
		} else if (Input.GetKey(rotateRight)) {
			foreach (GyroscopicPartController gyroscope in gyroscopes) {
				gyroscope.direction = -1;
			}
		} else {
			foreach (GyroscopicPartController gyroscope in gyroscopes) {
				gyroscope.direction = 0;
			}
		}
  }
}
