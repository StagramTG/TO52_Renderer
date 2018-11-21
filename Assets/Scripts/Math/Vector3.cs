using System;
using Newtonsoft.Json;

namespace JsonMap.Math
{
    /// <summary>
    /// Simple data structure that represent 3D vector with operator override
    /// and few pre made common vectors.
    /// </summary>
    public struct Vector3
    {
        /**
         * ==========================================
         *      FIELDS
         * ==========================================
         */
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /**
         * ==========================================
         *      PROPERTIESS
         * ==========================================
         */
        // Norm of the vector
        [JsonIgnore]
        public float Norm
        {
            get
            {
                return (float)System.Math.Sqrt(System.Math.Pow(X, 2) + System.Math.Pow(Y, 2) + System.Math.Pow(Z, 2));
            }
        }

        // Commonly used vectors
        [JsonIgnore]
        public static Vector3 Forward
        {
            get
            {
                return new Vector3(0, 0, 1);
            }
        }

        [JsonIgnore]
        public static Vector3 Left
        {
            get
            {
                return new Vector3(-1, 0, 0);
            }
        }

        [JsonIgnore]
        public static Vector3 Right
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        [JsonIgnore]
        public static Vector3 Up
        {
            get
            {
                return new Vector3(0, 1, 0);
            }
        }

        [JsonIgnore]
        public Vector3 Normalized
        {
            get
            {
                return new Vector3(X, Y, Z) / Norm;
            }
        }

        /**
         * ==========================================
         *      CONSTRUCTORS
         * ==========================================
         */
        public Vector3(float px, float py, float pz)
        {
            X = px;
            Y = py;
            Z = pz;
        }

        public Vector3(float px, float py)
        {
            X = px;
            Y = py;
            Z = 0f;
        }

        public Vector3(float pxyz)
        {
            X = pxyz;
            Y = pxyz;
            Z = pxyz;
        }

        /**
         * ==========================================
         *      METHODS
         * ==========================================
         */
        public void Normalize()
        {
            X /= Norm;
            Y /= Norm;
            Z /= Norm;
        }

        /**
         * ==========================================
         *      OPERATORS
         * ==========================================
         */
        public static Vector3 operator +(Vector3 pv1, Vector3 pv2)
        {
            return new Vector3(pv1.X + pv2.X, pv1.Y + pv2.Y, pv1.Z + pv2.Z);
        }

        public static Vector3 operator -(Vector3 pv1, Vector3 pv2)
        {
            return new Vector3(pv1.X - pv2.X, pv1.Y - pv2.Y, pv1.Z - pv2.Z);
        }

        public static Vector3 operator *(Vector3 pv, float m)
        {
            return new Vector3(pv.X * m, pv.Y * m, pv.Z * m);
        }

        public static Vector3 operator /(Vector3 pv, float d)
        {
            return new Vector3(pv.X / d, pv.Y / d, pv.Z / d);
        }
    }
}
