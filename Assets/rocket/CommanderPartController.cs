using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderPartController : GyroscopicPartController {
  [HideInInspector] public bool commanding;

  protected override bool ConnectedToCommandingPart(Joint2D brokenJoint, List<PartController> checkedParts) {
    if (commanding) return true;
    return base.ConnectedToCommandingPart(brokenJoint, checkedParts);
  }
}
