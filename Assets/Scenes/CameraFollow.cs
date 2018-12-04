using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject followObject;
	private Rigidbody2D followRigidbody;
	public new Camera camera;
	public int maxZoom;

	public GameObject planet;

	private void Start() {
		followRigidbody = followObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) {
			camera.orthographicSize = newZoom;
		} else {
			camera.orthographicSize = maxZoom;
		}

		// Debug.Log(followRigidbody.centerOfMass);

  	transform.position = new Vector3(followRigidbody.worldCenterOfMass.x, followRigidbody.worldCenterOfMass.y, -1);

		Vector2 relative = transform.position - planet.transform.position;
		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90);
		
  	// transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -1);
	}
}
