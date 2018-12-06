using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {
	public GameObject surfaceChunk;

	public int chunkSize;

	public float size;
	public float amplitude;
	public float frequency;
	public int seed;

	void Start() {
		PlanetFeatures features = new PlanetFeatures(size, amplitude, frequency, seed);

		int steps = (int)(size * 3.5);
		float scale = (size / 100f);

		Transform planetTransform = transform.Find("Body");
		planetTransform.transform.localScale = new Vector2(size, size);

		for (int step = 0; step < steps; step++) {
			if (step % chunkSize == 0 && step != steps) {
				float angle = ((float)(step + chunkSize) / (float)steps) * 2 * Mathf.PI;
				float distance = size / 2 + amplitude * scale;
				distance = Mathf.Round(distance);

				float x = Mathf.Cos(angle) * distance;
				float y = Mathf.Sin(angle) * distance;

				// Debug.Log(step.ToString() + " " +  Mathf.Min(step + 200, steps).ToString());
				GameObject chunk = Instantiate(surfaceChunk, new Vector2(x, y), Quaternion.identity, transform.Find("Chunks"));
				chunk.GetComponent<ChunkController>().Initalise(step, Mathf.Min(step + chunkSize, steps), steps, features);
			}
		}
	}
}
