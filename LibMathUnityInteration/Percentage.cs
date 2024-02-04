namespace MathLib
{
    public static class Percentage
    {
        public static float GetPercentage(float value, float valueFrom)
        {
            if (valueFrom == 0)
            {
                return 100;
            }

            return value / valueFrom * 100;
        }
    }
}
