using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private GameObject player;
	private GameObject playerCommandPart;

	public int maxZoom;
	private Vector3 followOffset;
	private new Camera camera;
	private GameObject planet;

	private void Start() {
		camera = GetComponent<Camera>();
		player = GameObject.Find("Player");
		foreach (Transform child in player.transform) {
			if (child.GetComponent<RocketPart>().commandPart) playerCommandPart = child.gameObject;
		}
	}
	
	void Update () {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) camera.orthographicSize = newZoom;
		else camera.orthographicSize = maxZoom;

  	Vector2 centerOfMass = Vector2.zero;
		float totalMass = 0f;
		
		foreach (Transform child in player.transform) {
			if (child.GetComponent<RocketPart>().connectedToCommandPart) {
				Rigidbody2D rigidbody = child.GetComponent<Rigidbody2D>();
				centerOfMass += rigidbody.worldCenterOfMass * rigidbody.mass;
				totalMass += rigidbody.mass;
			}
		}
		centerOfMass /= totalMass;
		transform.position = new Vector3(centerOfMass.x, centerOfMass.y, -1);

		planet = player.GetComponent<GravityController>().planet;
		if (planet != null) {
			Vector2 relative = transform.position - planet.transform.position;
		  transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90);
		}
	}
}
