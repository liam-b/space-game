using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {
	public GameObject surfaceBlock;

	public float size;
	public float amplitude;
	public float frequency;
	public int seed;

	void Start () {
		int steps = (int)size * 4;
		float scale = (size / 100f);

		Transform planetTransform = transform.parent.Find("Body");
		planetTransform.transform.localScale = new Vector2(size, size);
		planetTransform.gameObject.GetComponent<Renderer>().material.color = new Color(0.65f, 0.43f, 0.46f, 1);

		for (int step = 0; step < steps; step++) {
			float angle = ((float)step / (float)steps) * 2 * Mathf.PI;
			float distance = size / 2 + Mathf.PerlinNoise(angle * frequency * scale, seed) * amplitude * scale;
			distance = Mathf.Round(distance);

			for (int i = 0; i < distance - (size / 2) + 1; i++) {
				placeSurfaceBlock(distance - i, angle);
			}
		}
	}

	void placeSurfaceBlock(float distance, float angle) {
		float x = Mathf.Cos(angle) * distance;
		float y = Mathf.Sin(angle) * distance;

		Instantiate(surfaceBlock, new Vector2(x, y), Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg), transform)
			.GetComponent<Renderer>().material.color = new Color(0.65f, 0.43f, 0.46f, 1);
	}
	
	void Update () {
	}
}
