using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour {
  [HideInInspector] public bool debris;
  [HideInInspector] public bool controllable;
  protected ShipController ship;

  private new Rigidbody2D rigidbody;

  protected void Awake() {
    rigidbody = GetComponent<Rigidbody2D>();
    ship = transform.parent.parent.GetComponent<ShipController>();
  }

  protected void FixedUpdate() {
    PlanetController closestPlanet = ship.closestPlanet;

    if (closestPlanet != null) {
      float distance = (closestPlanet.transform.position - transform.position).sqrMagnitude;
      float force = (float)((PlanetController.gravitationalConstant * closestPlanet.mass * rigidbody.mass) / distance);
      rigidbody.AddForce((force * (closestPlanet.transform.position - transform.position).normalized), ForceMode2D.Force);
    }
  }

  void OnJointBreak2D(Joint2D brokenJoint) {
    ship.UpdatePartsControllability(brokenJoint);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    float impulse = 0f;
    foreach (ContactPoint2D contact in collision.contacts) {
      impulse += contact.normalImpulse;
    }

    // Vector3 normal = collision.contacts[0].normal;
	  // float collisionAngle = 90 - Vector3.Angle(rigidbody.velocity, -normal);

    // Debug.Log(name + " " + collisionAngle);
  }

  public void UpdateControllability(Joint2D brokenJoint) {
    controllable = ConnectedToCommandingPart(brokenJoint, new List<PartController>());
    if (!controllable) ship.ConvertToDebris(this);
  }

  protected virtual bool ConnectedToCommandingPart(Joint2D brokenJoint, List<PartController> checkedParts) {
    checkedParts.Add(this);

    foreach (PartController part in ship.parts) {
      if (ObjectsAreConnected(gameObject, part.gameObject, brokenJoint) && !checkedParts.Contains(part)) {
        if (part.ConnectedToCommandingPart(brokenJoint, checkedParts)) return true;
      }
    }

    return false;
  }

  private static bool ObjectsAreConnected(GameObject obj1, GameObject obj2, Joint2D brokenJoint) {
    foreach (Joint2D joint in obj1.GetComponents<Joint2D>()) {
      if (joint != brokenJoint) {
        if (joint.connectedBody.gameObject == obj2) return true;
      }
    }

    foreach (Joint2D joint in obj2.GetComponents<Joint2D>()) {
      if (joint != brokenJoint) {
        if (joint.connectedBody.gameObject == obj1) return true;
      }
    }

    return false;
  }
}
