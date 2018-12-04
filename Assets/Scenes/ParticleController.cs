using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	public new ParticleSystem particleSystem;
  private ParticleSystem.Particle[] particles;

	public GameObject player;

	void Start () {
		// particles = new ParticleSystem.Particle[particleSystem.maxParticles]; 
	}
	
	void FixedUpdate () {
		// int numParticlesAlive = (int)particleSystem.emission.rateOverTime.Evaluate(1f);
		// // int numParticlesAlive = particleSystem.GetParticles(particles);

		// Debug.Log(numParticlesAlive);

		// if (particleSystem.emission.enabled) {
		// 	for (int i = 0; i < numParticlesAlive; i++) {
		// 		particles[i].velocity = new Vector2(particles[i].velocity.x, particles[i].velocity.y) + player.GetComponent<Rigidbody2D>().velocity;
		// 	}
		// }

		// particleSystem.SetParticles(particles, particleSystem.GetParticles(particles));
	}
}
