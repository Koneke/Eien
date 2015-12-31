namespace Eien.Framework
{
	class Random
	{
		private System.Random random;

		public Random()
		{
			random = new System.Random();
		}

		public double NextDouble()
		{
			return random.NextDouble();
		}

		public void NextBytes(byte[] buffer)
		{
			random.NextBytes(buffer);
		}

		public int NextInt()
		{
			return random.Next();
		}

		public int NextInt(int maxValue)
		{
			return random.Next(maxValue);
		}

		public int NextInt(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}

		public double Variance(double magnitude)
		{
			return (random.NextDouble() - 0.5) * magnitude;
		}
	}
}