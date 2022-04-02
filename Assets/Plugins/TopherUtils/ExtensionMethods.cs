using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Plugins.TopherUtils
{
    public static class ExtensionMethods
    {
#region Random

        /// <summary>Returns a random element from the array.</summary>
        public static T RandomElement<T>(this T[] array)
            => array[Random.Range(0, array.Length)];

        /// <summary>Returns a float between x and y.</summary>
        public static float RandomValue(this Vector2 minMax)
            => Random.Range(minMax.x, minMax.y);

        public static Vector2 WithPointVariation(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(original.x + Random.Range(-x ?? 0f, x ?? 0f),
                               original.y + Random.Range(-y ?? 0f, y ?? 0f));
        }

        public static float NextDeterminedRandom(this Vector2 minMax, System.Random random)
        {
            var minInt = (int)(minMax.x * 1000);
            var maxInt = (int)(minMax.y * 1000);

            return random.Next(minInt, maxInt) / 1000f;
        }

#endregion

#region Physics

        /// <summary>Returns whether or not a provided object is contained on the other side of the collision.
        /// If so, the object is provided via the out var. </summary>
        public static bool CollidedWith<T>(this Collision2D collision, out T potentialObject)
            => collision.collider.TryGetComponent(out potentialObject);

        public static bool WasHitFromBottom(this Collision2D collision)
            => collision.contacts[0].normal.y > 0.5f;

        public static bool WasHitFromTop(this Collision2D collision)
            => collision.contacts[0].normal.y < -0.5f;

        public static bool WasHitFromSide(this Collision2D collision)
            => collision.contacts[0].normal.x is < -0.5f or > 0.5f;

#endregion

#region Other

        public static void SetAllActive(this IEnumerable<Collider> colliders, bool active)
        {
            foreach(var c in colliders)
                c.enabled = active;
        }
        
        /// <summary>Returns the Vector3 with specified values replaced.</summary>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
            => new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);

        public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
            => new Vector2(x ?? original.x, y ?? original.y);

        public static Color With(this Color original, float? r = null, float? g = null, float? b = null,
                                 float?     a = null)
            => new Color(r ?? original.r, g ?? original.g, b ?? original.b, a ?? original.a);

        public static float Map(this float f, float iMin, float iMax, float oMin, float oMax)
        {
            var t = Mathf.InverseLerp(iMin, iMax, f);
            return Mathf.Lerp(oMin, oMax, t);
        }        
        
        public static Color Map(this float f, float iMin, float iMax, Color oMin, Color oMax)
        {
            var t = Mathf.InverseLerp(iMin, iMax, f);
            return Color.Lerp(oMin, oMax, t);
        }

        public static int NumDigits(this int n)
        {
            if(n >= 0)
            {
                return n switch
                {
                    < 10         => 1,
                    < 100        => 2,
                    < 1000       => 3,
                    < 10000      => 4,
                    < 100000     => 5,
                    < 1000000    => 6,
                    < 10000000   => 7,
                    < 100000000  => 8,
                    < 1000000000 => 9,
                    _            => 10
                };
            }

            return n switch
            {
                > -10         => 2,
                > -100        => 3,
                > -1000       => 4,
                > -10000      => 5,
                > -100000     => 6,
                > -1000000    => 7,
                > -10000000   => 8,
                > -100000000  => 9,
                > -1000000000 => 10,
                _             => 11
            };
        }

#endregion
    }
}