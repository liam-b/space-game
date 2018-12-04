using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject followObject;
	public new Camera camera;
	
	void Update () {
		camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * camera.orthographicSize * 0.1f;

  	transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -1);
	}
}
