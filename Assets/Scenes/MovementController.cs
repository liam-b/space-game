using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	private new Rigidbody2D rigidbody2D;
	public new ParticleSystem particleSystem;

  public KeyCode activateEngine;
	public KeyCode rotateLeft;
	public KeyCode rotateRight;

	public float enginePower;
	public float rotationPower;

	private ParticleSystem.EmissionModule emission;

  void Start() {
		rigidbody2D = GetComponent<Rigidbody2D>();
		emission = particleSystem.emission;
  }

  void FixedUpdate() {
		if (Input.GetKey(activateEngine)) {
			rigidbody2D.AddRelativeForce(Vector2.up * enginePower * Time.deltaTime);
			emission.enabled = true;
		}
		else {
			emission.enabled = false;
		}

		if (Input.GetKey(rotateLeft)) {
			rigidbody2D.AddTorque(rotationPower * Time.deltaTime);
		}

		if (Input.GetKey(rotateRight)) {
			rigidbody2D.AddTorque(-rotationPower * Time.deltaTime);
		}
  }
}
