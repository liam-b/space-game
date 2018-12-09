using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour {
  public bool commandPart;
  public bool connectedToCommandPart;

  void OnJointBreak2D(Joint2D brokenJoint) {
    updateConnectedness(brokenJoint);
    brokenJoint.connectedBody.GetComponent<RocketPart>().updateConnectedness(brokenJoint);
  }

  void updateConnectedness(Joint2D brokenJoint) {
    if (!commandPart) connectedToCommandPart = connectedToCommand(gameObject, transform.parent.GetComponent<GravityController>().commandPart, brokenJoint, new List<GameObject>());
    if (!connectedToCommandPart) {
      foreach (Transform child in transform.parent) {
        if (child.GetComponent<RocketPart>().connectedToCommandPart && connectedToObject(gameObject, child.gameObject, null)) {
          child.GetComponent<RocketPart>().updateConnectedness(brokenJoint);
        }
      }
    }
  }

  bool connectedToCommand(GameObject obj, GameObject commandPart, Joint2D brokenJoint, List<GameObject> checkedObjects) {
    checkedObjects.Add(obj);
    foreach (Transform child in transform.parent) {
      if (!checkedObjects.Contains(child.gameObject) && child.gameObject != obj && connectedToObject(obj, child.gameObject, brokenJoint)) {
        if (child.GetComponent<RocketPart>().commandPart) return true;
        else return connectedToCommand(child.gameObject, commandPart, brokenJoint, checkedObjects);
      }
    }

    return false;
  }

  bool connectedToObject(GameObject obj1, GameObject obj2, Joint2D brokenJoint) {
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
