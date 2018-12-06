using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFeatures {
  public float size;
	public float amplitude;
	public float frequency;
	public int seed;

	public PlanetFeatures(float size, float amplitude, float frequency, int seed) {
		this.size = size;
		this.amplitude = amplitude;
		this.frequency = frequency;
		this.seed = seed;
	}
}
