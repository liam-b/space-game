using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {
	[HideInInspector]
	public GameObject commandPart;

	[HideInInspector]
	public GameObject planet;
	private float planetMass;
	private float planetInfluenceDistance;

	private float gravitationalConstant = 10000;

	public float radius;
	public GameObject fing;

	void Start () {
		foreach (Transform child in transform) {
			if (child.GetComponent<RocketPart>().commandPart) commandPart = child.gameObject;
		}

		planet = findClosestPlanet();

		foreach (Transform child in transform) {
			child.gameObject.GetComponent<Rigidbody2D>().velocity = Mathf.Sqrt((gravitationalConstant * planetMass) / radius) * Vector2.left;
			// child.position = new Vector3(0, radius, 0) - (commandPart.transform.position - child.position);
		}

		// fing.GetComponent<Rigidbody2D>().velocity = Mathf.Sqrt((gravitationalConstant * planetMass) / radius) * Vector2.left;
	}
	
	void FixedUpdate() {
		if (planet == null) planet = findClosestPlanet();
		float distance = (planet.transform.position - commandPart.transform.position).sqrMagnitude;
		if (distance <= planetInfluenceDistance * planetInfluenceDistance) {
			foreach (Transform child in transform) {
				Rigidbody2D rigidbody = child.GetComponent<Rigidbody2D>();
				float childDistance = (planet.transform.position - child.transform.position).sqrMagnitude;
				float force = (float)((gravitationalConstant * planetMass * rigidbody.mass) / childDistance);
				rigidbody.AddForce((force * (planet.transform.position - child.transform.position).normalized), ForceMode2D.Force);
				// Debug.Log(child.gameObject.name);
			}
		} else planet = null;
	}

	GameObject findClosestPlanet() {
		GameObject closestPlanet = null;
    float distance = Mathf.Infinity;

		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
			Vector2 difference = planet.transform.position - commandPart.transform.position;
			float currentDistance = difference.sqrMagnitude;

			if (currentDistance < distance) {
        closestPlanet = planet;
        distance = currentDistance;
      }
		}

		planetInfluenceDistance = closestPlanet.gameObject.GetComponent<PlanetController>().influenceDistance;
		planetMass = closestPlanet.gameObject.GetComponent<PlanetController>().mass;

		return closestPlanet;
	}
}
