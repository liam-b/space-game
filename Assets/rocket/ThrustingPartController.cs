using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustingPartController : PartController {
  public new ParticleSystem particleSystem; 
  public new Rigidbody2D rigidbody2D;
  public float thrustPower;

  [HideInInspector] public bool active = false;
	private ParticleSystem.EmissionModule emission;

  new void Awake() {
    base.Awake();
		emission = particleSystem.emission;
    emission.enabled = false;
  }

  new void FixedUpdate() {
    base.FixedUpdate();
    if (active && controllable) {
      rigidbody2D.AddRelativeForce(Vector2.up * thrustPower, ForceMode2D.Force);
      // ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
      // emitParams.velocity = rigidbody2D.velocity;
      // particleSystem.Emit(emitParams, 10);
    }
    emission.enabled = active && controllable;
  }
}
