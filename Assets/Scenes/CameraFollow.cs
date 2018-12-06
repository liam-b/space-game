using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject followObject;
	public int maxZoom;

	private Rigidbody2D followRigidbody;
	private new Camera camera;
	private GameObject planet;

	private void Start() {
		followRigidbody = followObject.GetComponent<Rigidbody2D>();
		camera = GetComponent<Camera>();
	}
	
	void Update () {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) camera.orthographicSize = newZoom;
		else camera.orthographicSize = maxZoom;

  	transform.position = new Vector3(followRigidbody.worldCenterOfMass.x, followRigidbody.worldCenterOfMass.y, -1);

		planet = followObject.GetComponent<GravityController>().planet;
		if (planet != null) {
			Vector2 relative = transform.position - planet.transform.position;
		  transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90);
		}
	}
}
