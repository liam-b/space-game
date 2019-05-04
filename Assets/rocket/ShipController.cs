using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
	public CommanderPartController commandingPart;
	[HideInInspector] public PlanetController closestPlanet;

	public List<PartController> parts;

	void Start() {
		commandingPart.commanding = true;
		commandingPart.controllable = true;

		parts = new List<PartController>();
		foreach (Transform child in transform.Find("Parts")) {
			PartController part = child.GetComponent<PartController>();
			parts.Add(part);
		}
		
		UpdatePartsControllability(null);

		if (gameObject.name == "Station") {
		// if (true) {
			foreach (PartController part in parts) {
        part.GetComponent<Rigidbody2D>().velocity = new Vector3(-390, 0, 0);
        // part.GetComponent<Rigidbody2D>().velocity = new Vector3(-1000, 0, 0);
      }
		}
	}
 	
	void FixedUpdate() {
		if (closestPlanet == null) closestPlanet = FindClosestPlanet();
		float distance = (closestPlanet.transform.position - commandingPart.transform.position).sqrMagnitude;
		if (distance >= Mathf.Pow(closestPlanet.influenceDistance, 2)) closestPlanet = null;
	}

	public List<T> GetParts<T>() where T : PartController {
		List<T> foundParts = new List<T>();
		foreach (PartController part in parts) {
			if (part is T) foundParts.Add(part as T);
		}

		return foundParts;
	}

	public void UpdatePartsControllability(Joint2D brokenJoint) {
		foreach (PartController part in parts) {
			part.UpdateControllability(brokenJoint);
		}
	}

	public void ConvertToDebris(PartController part) {
		part.transform.parent = transform.Find("Debris");
		part.debris = true;
	}

	private PlanetController FindClosestPlanet() {
		PlanetController closestPlanet = null;
    float distance = Mathf.Infinity;

		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
			Vector2 difference = planet.transform.position - commandingPart.transform.position;
			float currentDistance = difference.sqrMagnitude;

			if (currentDistance < distance) {
        closestPlanet = planet.GetComponent<PlanetController>();
        distance = currentDistance;
      }
		}

		return closestPlanet;
	}
}
