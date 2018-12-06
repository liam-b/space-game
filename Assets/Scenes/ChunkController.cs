using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour {
	public GameObject surfaceBlock;

	public float loadDistance;
	private bool loaded = false;

	private int startStep;
	private int endStep;
	private int totalSteps;

	private PlanetFeatures features;
	private GameObject player;

	public void Initalise(int startStep, int endStep, int totalSteps, PlanetFeatures features) {
		this.startStep = startStep;
	  this.endStep = endStep;
	  this.totalSteps = totalSteps;
		this.features = features;
	}

	void Start() {
		player = GameObject.Find("Player");
	}
	
	void Update() {
		float distance = (player.transform.position - transform.position).sqrMagnitude;
		if (distance <= loadDistance && !loaded) loadSurface();
		if (distance > loadDistance && loaded) unloadSurface();
  }

	void loadSurface() {
		loaded = true;

		float scale = (features.size / 100f);
		for (int step = startStep; step < endStep; step++) {
			float angle = ((float)step / (float)totalSteps) * 2 * Mathf.PI;
			float distance = features.size / 2 + Mathf.PerlinNoise(angle * features.frequency * scale, features.seed) * features.amplitude * scale;
			distance = Mathf.Round(distance);

			for (int i = 0; i < distance - (features.size / 2) + 1; i++) {
				placeSurfaceBlock(distance - i, angle);
			}
		}
	}

	void unloadSurface() {
		loaded = false;

		foreach (Transform child in transform) {
      GameObject.Destroy(child.gameObject);
 		}
	}

	void placeSurfaceBlock(float distance, float angle) {
		float x = Mathf.Cos(angle) * distance;
		float y = Mathf.Sin(angle) * distance;

		Instantiate(surfaceBlock, new Vector2(x, y), Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg), transform);
	}
}
