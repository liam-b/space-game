using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public PlayerController player;
	public int maxZoom;

	private new Camera camera;

	private void Start() {
		camera = GetComponent<Camera>();
	}
	
	void LateUpdate() {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) camera.orthographicSize = newZoom;
		else camera.orthographicSize = maxZoom;

  	Vector2 centerOfMass = Vector2.zero;
		float totalMass = 0f;
		foreach (PartController part in player.ship.parts) {
			if (part.controllable) {
				Rigidbody2D rigidbody = part.GetComponent<Rigidbody2D>();
				centerOfMass += rigidbody.worldCenterOfMass * rigidbody.mass;
				totalMass += rigidbody.mass;
			}
		}
		centerOfMass /= totalMass;
		transform.position = new Vector3(centerOfMass.x, centerOfMass.y, -1);

		if (player.ship.closestPlanet != null) {
			Vector2 relative = transform.position - player.ship.closestPlanet.transform.position;
		  transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90);
		}
	}
}
