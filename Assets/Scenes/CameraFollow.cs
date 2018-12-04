using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject followObject;
	public new Camera camera;
	public int maxZoom;
	
	void Update () {
		float newZoom = camera.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;
		if (newZoom <= maxZoom) {
			camera.orthographicSize = newZoom;
		} else {
			camera.orthographicSize = maxZoom;
		}

  	transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -1);
	}
}
