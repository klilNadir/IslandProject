namespace IslandProject.Models.Random
{
    public interface IRandom
    {
        public Boolean[] NextBooleans();
        public int NextInt(int lowerBound, int upperBound);
        public int[] NextInts(int lowerBound, int upperBound);

        public double NextDouble (double lowerBound, double upperBound);
        public double[] NextDoubles(double lowerBound, double upperBound);

    }
}
