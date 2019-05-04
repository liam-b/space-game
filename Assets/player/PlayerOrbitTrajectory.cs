using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbitTrajectory : MonoBehaviour {
  public PlayerController player;
  public LineRenderer lineRenderer;
  public int lineSegments;

  void Start() {
    lineRenderer.positionCount = lineSegments;
  }

  void Update() {
    Vector2 centerOfMass = Vector2.zero;
    Vector2 averageVelocity = Vector2.zero;
		float totalMass = 0f;
		foreach (PartController part in player.ship.parts) {
			if (part.controllable) {
				Rigidbody2D rigidbody = part.GetComponent<Rigidbody2D>();
				centerOfMass += rigidbody.worldCenterOfMass * rigidbody.mass;
        averageVelocity += rigidbody.velocity * rigidbody.mass;
				totalMass += rigidbody.mass;
			}
		}
		centerOfMass /= totalMass;
    averageVelocity /= totalMass;

    float gravitationalParameter = player.ship.closestPlanet.mass * PlanetController.gravitationalConstant;
    Vector3 shipPosition = centerOfMass;
    Vector3 planetPosition = player.ship.closestPlanet.transform.position;
    Vector3 relativePosition = planetPosition - shipPosition;
    Vector3 shipVelocity = averageVelocity;

    Vector3 specificAngularMomentum = Vector3.Cross(relativePosition, shipVelocity);
    float orbitalEnergy = shipVelocity.sqrMagnitude / 2 - gravitationalParameter / relativePosition.magnitude;
    float semiLatusRectum = specificAngularMomentum.sqrMagnitude / gravitationalParameter;
    Vector3 eccentricity = Vector3.Cross(shipVelocity, specificAngularMomentum) / gravitationalParameter - relativePosition / relativePosition.magnitude;
    float trueAnomaly = Mathf.Acos(Vector3.Dot(eccentricity, relativePosition) / (eccentricity.magnitude * relativePosition.magnitude));
    if (Vector3.Dot(relativePosition, shipVelocity) > 0 ^ specificAngularMomentum.z > 0) trueAnomaly = -trueAnomaly;

    Vector3[] points = new Vector3[lineSegments];
    for (int i = 0; i < lineSegments; i++) {
      float angle = Mathf.Deg2Rad * i * (360f / (lineSegments - 1));
      float radius = semiLatusRectum / (1 + eccentricity.magnitude * Mathf.Cos(trueAnomaly + angle));

      angle += Mathf.Atan2(relativePosition.y, relativePosition.x) + Mathf.PI;
      float x = radius * Mathf.Cos(angle);
      float y = radius * Mathf.Sin(angle);

      if (float.IsInfinity(x) || float.IsNaN(x)) x = 0;
      if (float.IsInfinity(y) || float.IsNaN(y)) y = 0;

      points[i] = new Vector2(x, y);
    }
    lineRenderer.SetPositions(points);
  }
}