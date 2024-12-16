using System.Globalization;
using System.Linq;
using UnityEngine;

namespace ValPackage.Common
{
    public static class Parser
    {
        public static Vector3 String2Vector3(string data)
        {
            var floats = ParseFloats(data);

            return new(
                floats[0],
                floats[1],
                floats[2]
                );
        }

        public static Vector3Int String2Vector3Int(string data)
        {
            var ints = ParseInts(data);

            return new(
                ints[0],
                ints[1],
                ints[2]
                );
        }

        public static Quaternion String2Quaternion(string data)
        {
            var floats = ParseFloats(data);

            return new(
                floats[0],
                floats[1],
                floats[2],
                floats[3]
                );
        }



        private static float[] ParseFloats(string data)
        {
            return data
                .Substring(1, data.Length - 2)
                .Split(",")
                .Select(x => float.Parse(x, new CultureInfo("en-US")))
                .ToArray();
        }

        private static int[] ParseInts(string data)
        {
            return data
                .Substring(1, data.Length - 2)
                .Split(",")
                .Select(x => int.Parse(x, new CultureInfo("en-US")))
                .ToArray();
        }
    }
}