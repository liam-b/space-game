using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopicPartController : PartController {
  public new Rigidbody2D rigidbody2D;
  public float rotationPower;
  
  [HideInInspector] public int direction;

  new void FixedUpdate() {
    base.FixedUpdate();
    if (direction != 0 && controllable) rigidbody2D.AddTorque(rotationPower * direction);
  }
}
