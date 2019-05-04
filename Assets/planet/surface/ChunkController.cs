using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour {
	public GameObject surfaceBlock;

	private float loadDistance;
	private bool loaded = false;

	private int startStep;
	private int endStep;
	private int totalSteps;

	private PlanetFeatures features;
	private PartController player;

	public void Initalise(int startStep, int endStep, int totalSteps, float loadDistance, PlanetFeatures features) {
		this.startStep = startStep;
	  this.endStep = endStep;
	  this.totalSteps = totalSteps;
		this.loadDistance = loadDistance;
		this.features = features;
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>().commandingPart;
	}
	
	void Update() {
		float distance = (player.transform.position - transform.position).sqrMagnitude;
		if (distance <= loadDistance * loadDistance && !loaded) loadSurface();
		if (distance >= loadDistance * loadDistance + 100 && loaded) unloadSurface();
  }

	void loadSurface() {
		loaded = true;

		// float scale = (features.size / 100f);
		float scale = 20;
		for (int step = startStep; step < endStep; step++) {
			float angle = ((float)step / (float)totalSteps) * 2 * Mathf.PI;
			float distance = proceduralDistance(features, angle, scale);

			placeSurfaceBlock(Mathf.Round(distance), distance - (features.size / 2) + 2, angle);
		}
	}

	void unloadSurface() {
		loaded = false;
		foreach (Transform child in transform) ObjectPooler.instance.ReleaseObject(child.gameObject);
	}

	void placeSurfaceBlock(float distance, float height, float angle) {
		float x = Mathf.Cos(angle) * (distance - height / 2);
		float y = Mathf.Sin(angle) * (distance - height / 2);

		GameObject block = ObjectPooler.instance.DrawObject(new Vector2(x, y), Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg), transform);
		block.transform.localScale = new Vector2(height, 1);
	}

	float proceduralDistance(PlanetFeatures features, float angle, float scale) {
		return features.size / 2 + (Mathf.PerlinNoise(angle * features.frequency * scale * (1 - Mathf.PerlinNoise(angle * features.frequency * 0.01f, features.seed + 100) / 2), features.seed) * features.amplitude * scale);
	}
}
