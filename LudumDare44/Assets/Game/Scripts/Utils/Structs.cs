using UnityEngine;

[System.Serializable]
public class Range
{
	public float min;
	public float max;
}

[System.Serializable]
public class Constraints
{
	public Range x;
	public Range y;
	public Range z;
}

[System.Serializable]
public class SpawnPrefab
{
	public GameObject prefab;
	public Constraints rotationConstraints;
	public Constraints positionConstraints;
}