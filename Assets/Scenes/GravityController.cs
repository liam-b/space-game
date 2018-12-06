using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {
	public GameObject planet;
	private float planetMass;
	private float planetInfluenceDistance;
	private new Rigidbody2D rigidbody2D;

	private float gravitationalConstant = 10000;

	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		planet = findClosestPlanet();
	}
	
	void Update () {
		if (planet == null) planet = findClosestPlanet();
		float distance = (planet.transform.position - transform.position).sqrMagnitude;
		if (distance <= planetInfluenceDistance * planetInfluenceDistance) {
			float force = (float)((gravitationalConstant * planetMass) / distance);
			rigidbody2D.AddForce((force * (planet.transform.position - transform.position).normalized));
		} else planet = null;
	}

	GameObject findClosestPlanet() {
		GameObject closestPlanet = null;
    float distance = Mathf.Infinity;

		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
			Vector2 difference = planet.transform.position - transform.position;
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
