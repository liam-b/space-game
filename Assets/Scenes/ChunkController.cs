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
	private GameObject player;

	public void Initalise(int startStep, int endStep, int totalSteps, float loadDistance, PlanetFeatures features) {
		this.startStep = startStep;
	  this.endStep = endStep;
	  this.totalSteps = totalSteps;
		this.loadDistance = loadDistance;
		this.features = features;
	}

	void Start() {
		player = GameObject.Find("Player");
	}
	
	void Update() {
		float distance = (player.transform.position - transform.position).sqrMagnitude;
		if (distance <= loadDistance && !loaded) loadSurface();
		if (distance >= loadDistance + 100 && loaded) unloadSurface();
  }

	void loadSurface() {
		loaded = true;

		float scale = (features.size / 100f);
		for (int step = startStep; step < endStep; step++) {
			float angle = ((float)step / (float)totalSteps) * 2 * Mathf.PI;
			float distance = features.size / 2 + Mathf.PerlinNoise(angle * features.frequency * scale, features.seed) * features.amplitude * scale;

			placeSurfaceBlock(Mathf.Round(distance), distance - (features.size / 2) + 1, angle);
		}
	}

	void unloadSurface() {
		loaded = false;
		foreach (Transform child in transform) child.gameObject.SetActive(false);
	}

	void placeSurfaceBlock(float distance, float length, float angle) {
		float x = Mathf.Cos(angle) * (distance - length / 2);
		float y = Mathf.Sin(angle) * (distance - length / 2);

		GameObject block = ObjectPooler.SharedInstance.GetPooledObject();
		if (block != null) {
			block.transform.position = new Vector2(x, y);
			block.transform.localScale = new Vector2(length, 1);
			block.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
			block.transform.parent = transform;
			block.SetActive(true);
		}
	}
}
