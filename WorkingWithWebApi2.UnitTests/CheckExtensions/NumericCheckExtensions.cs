namespace WorkingWithWebApi2.UnitTests.CheckExtensions
{
    public static class NumericCheckExtensions
    {
        public static bool InRange(this int num, int lowerRange, int higherRange)
        {
            return (num >= lowerRange) && (num <= higherRange);
        }

        public static bool InRange(this long num, long lowerRange, long higherRange)
        {
            return (num >= lowerRange) && (num <= higherRange);
        }

    }
}
