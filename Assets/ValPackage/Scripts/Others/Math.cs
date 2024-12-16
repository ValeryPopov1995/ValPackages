using UnityEngine;

namespace ValPackage.Common
{
    public struct Math
    {
        public static float Map(float value, float low1, float high1, float low2, float high2)
        {
            float x = low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            return x;
        }

        public static int Map(int value, int low1, int high1, int low2, int high2)
        {
            int x = low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            return x;
        }

        /// <returns>Vector3(x, 0, z)</returns>
        public static Vector3 HorizontalVector3(Vector3 source)
        {
            return new(source.x, 0, source.z);
        }

        public static bool InRange(float value, Vector2 range)
        {
            return value > range.x && value < range.y;
        }

        public static bool[] InRange(float[] sqrValues, Vector2 constantRange)
        {
            bool[] inRanges = new bool[sqrValues.Length];
            Vector2 sqrRange = new(constantRange.x * constantRange.x, constantRange.y * constantRange.y);

            for (int i = 0; i < sqrValues.Length; i++)
                inRanges[i] = InRange(sqrValues[i], sqrRange);

            return inRanges;
        }

        public static bool InRange(int currentJumpDuration, Vector2Int range)
        {
            return currentJumpDuration >= range.x && currentJumpDuration <= range.y;
        }
    }
}