public static class Math
{
    public static int max(params int[] numbers)
    {
        int max = numbers[0];
        foreach(int number in numbers)
        {
            if (number > max) max = number;
        }
        return max;
    }

    public static int min(params int[] numbers)
    {
        int min = numbers[0];
        foreach (int number in numbers)
        {
            if (number < min) min = number;
        }
        return min;
    }

    public static int clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

	public static float maxf(params float[] numbers)
	{
		float max = numbers[0];
		foreach (float number in numbers)
		{
			if (number > max) max = number;
		}
		return max;
	}

	public static float minf(params float[] numbers)
	{
		float min = numbers[0];
		foreach (int number in numbers)
		{
			if (number < min) min = number;
		}
		return min;
	}

	public static float clampf(float value, float min, float max)
	{
		if (value < min) return min;
		if (value > max) return max;
		return value;
	}
}
