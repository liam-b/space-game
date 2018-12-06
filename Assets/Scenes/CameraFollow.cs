using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject followObject;
	public int maxZoom;

	private Vector3 followOffset;
	private new Camera camera;
	private GameObject planet;

	private void Start() {
		camera = GetComponent<Camera>();

		Vector2 centerOfMass = Vector2.zero;
		float totalMass = 0f;
		
		foreach (Transform child in followObject.transform) {
			Rigidbody2D rigidbody = child.GetComponent<Rigidbody2D>();
			centerOfMass += rigidbody.worldCenterOfMass * rigidbody.mass;
			totalMass += rigidbody.mass;
		}
		centerOfMass /= totalMass;
		
		followOffset = new Vector3(followObject.transform.GetChild(0).transform.position.x - centerOfMass.x, followObject.transform.GetChild(0).transform.position.y - centerOfMass.y, -1);
	}
	
	void Update () {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) camera.orthographicSize = newZoom;
		else camera.orthographicSize = maxZoom;

  	transform.position = (followObject.transform.GetChild(0).transform.position + followOffset);

		planet = followObject.GetComponent<GravityController>().planet;
		if (planet != null) {
			Vector2 relative = transform.position - planet.transform.position;
		  transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90);
		}
	}
}
