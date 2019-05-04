namespace SmartAss.Navigation
{
    public static class CompassPoints
    {
        public static readonly CompassPoint[] All = new[] { CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W };

        public static char ToChar(this CompassPoint compassPoint)
        {
            return "nesw?"[(int)compassPoint];
        }
    }
}
