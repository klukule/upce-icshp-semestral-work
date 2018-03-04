using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine
{
    public struct AlignedBoxCorners
    {
        public Vector3 NearTopLeft;
        public Vector3 NearTopRight;
        public Vector3 NearBottomLeft;
        public Vector3 NearBottomRight;
        public Vector3 FarTopLeft;
        public Vector3 FarTopRight;
        public Vector3 FarBottomLeft;
        public Vector3 FarBottomRight;
    }
}