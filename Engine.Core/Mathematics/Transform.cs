using Engine.Level;
using System;
using System.Numerics;
using static Engine.Core.MyMath;

namespace Engine
{
    [Serializable]
    public struct Transform
    {
        public Vector3 Translation;
        public Quaternion Orientation;
        public Vector3 Scale;
        private static readonly Transform _identity;
        public static Transform Identity => _identity;

        public Matrix4x4 Rotation
        {
            get => Matrix4x4.CreateFromQuaternion(Orientation);
            set => Orientation = Quaternion.Normalize(Quaternion.CreateFromRotationMatrix(value));
        }

        public Matrix4x4 World
        {
            get => Matrix4x4.CreateScale(Scale) *
                Matrix4x4.CreateFromQuaternion(Orientation) *
                Matrix4x4.CreateTranslation(Translation);
        }

        static Transform()
        {
            _identity.Translation = Vector3.Zero;
            _identity.Orientation = Quaternion.Identity;
            _identity.Scale = Vector3.One;
        }

        public Transform(Vector3 translation)
        {
            Translation = translation;
            Orientation = Quaternion.Identity;
            Scale = Vector3.One;
        }

        public Transform(Vector3 translation, Quaternion orientation)
        {
            Translation = translation;
            Orientation = orientation;
            Scale = Vector3.One;
        }

        public Transform(Vector3 translation, Quaternion orientation, Vector3 scale)
        {
            Translation = translation;
            Orientation = orientation;
            Scale = scale;
        }

        public Transform(Actor a)
        {
            Translation = a.Position;
            Orientation = a.Orientation;
            Scale = a.Scale;
        }

        public Transform(ActorElement ae)
        {
            Translation = ae.Position;
            Orientation = ae.Orientation;
            Scale = ae.Scale;
        }

        public void MakeValid()
        {
            Scale = Scale.Clamp(0.00001f, 100000f);
            Orientation = Quaternion.Normalize(Orientation);
        }

        public bool Equals(Transform other)
        {
            return Translation == other.Translation && Orientation == other.Orientation && Scale == other.Scale;
        }

        public override bool Equals(object obj)
        {
            if (obj is Transform t)
            {
                return Equals(t);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashHelper.Combine(Translation.GetHashCode(), Orientation.GetHashCode(), Scale.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format("Translation:{0} Orientation:{1} Scale:{2}", Translation, Orientation, Scale);
        }

        public static Transform Parse(string value)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Transform m1, Transform m2)
        {
            return m1.Translation == m2.Translation && m1.Orientation == m2.Orientation && m1.Scale == m2.Scale;
        }

        public static bool operator !=(Transform m1, Transform m2)
        {
            return m1.Translation != m2.Translation || m1.Orientation != m2.Orientation || m1.Scale != m2.Scale;
        }

        public static Transform operator +(Transform left, Transform right)
        {
            return new Transform(left.Translation + right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }

        public static Transform operator -(Transform left, Transform right)
        {
            return new Transform(left.Translation - right.Translation, left.Orientation * Quaternion.Inverse(right.Orientation), left.Scale / right.Scale);
        }

        public static Transform operator -(Transform value)
        {
            return new Transform(-value.Translation, Quaternion.Inverse(value.Orientation), new Vector3(1 / value.Scale.X, 1 / value.Scale.Y, 1 / value.Scale.Z));
        }

        public static Transform operator *(Transform left, Transform right)
        {
            return new Transform(left.Translation * right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }

        public static Transform operator /(Transform left, Transform right)
        {
            return new Transform(left.Translation / right.Translation, left.Orientation * Quaternion.Inverse(right.Orientation), left.Scale / right.Scale);
        }

        public static Transform Lerp(Transform from, Transform to, float amount)
        {
            return new Transform(Vector3.Lerp(from.Translation, to.Translation, amount), Quaternion.Lerp(from.Orientation, to.Orientation, amount), Vector3.Lerp(from.Scale, to.Scale, amount));
        }

        public static void Add(ref Transform left, ref Transform right, out Transform result)
        {
            result = new Transform(left.Translation + right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }

        public static Transform Add(Transform left, Transform right)
        {
            return new Transform(left.Translation + right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }

        public static void Subtract(ref Transform left, ref Transform right, out Transform result)
        {
            result = new Transform(left.Translation - right.Translation, left.Orientation * Quaternion.Inverse(right.Orientation), left.Scale / right.Scale);
        }

        public static Transform Subtract(Transform left, Transform right)
        {
            return new Transform(left.Translation - right.Translation, left.Orientation * Quaternion.Inverse(right.Orientation), left.Scale / right.Scale);
        }

        public static void Multiply(ref Transform left, ref Transform right, out Transform result)
        {
            result = new Transform(left.Translation * right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }

        public static Transform Multiply(Transform left, Transform right)
        {
            return new Transform(left.Translation * right.Translation, left.Orientation * right.Orientation, left.Scale * right.Scale);
        }
    }
}