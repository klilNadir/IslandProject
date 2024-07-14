using IslandProject.Models.Enums;

namespace IslandProject.Models.Setup
{
    public static class RunDetails
    {
        public static bool IsMax { get; set; }
        public static int CallCount { get; set; }
        public static int Dimension { get; set; }
        public static double LowerBound { get; set; }
        public static double UpperBound { get; set; }
        public static FunctionType FunctionType { get; set; }
    }
}
