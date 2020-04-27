using UnityEngine;

public static class RandomVec
{
	public static Vector3 getRandomPosition(Constraints c, Vector3 avoidVector, float avoidRadius)
	{
		Vector3 vect = getRandomVector(c);
		int count = 0;
		while (Vector3.Distance(vect, avoidVector) < avoidRadius)
		{
			vect = getRandomVector(c);
			if (++count > 100)
			{
				Debug.Log("[GenerateScene] broke out of infinite loop (>100 loops)");
				break;
			}
		}
		return vect;
	}

	public static Vector3 getRandomVector(Constraints c)
	{
		return new Vector3(randomRange(c.x), randomRange(c.y), randomRange(c.z));

	}

	public static float randomRange(Range range)
	{
		return Random.Range(range.min, range.max);
	}
}