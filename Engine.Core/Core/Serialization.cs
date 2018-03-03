using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Engine.Core
{
    public static class Serialization
    {
        public static string StringFromXml(this XmlNode n, string def = "")
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return n.Attributes[0].Value;
            }
            return def;
        }

        public static bool BoolFromXml(this XmlNode n, bool def = false)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return bool.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static char CharFromXml(this XmlNode n, char def = '\0')
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return char.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static byte ByteFromXml(this XmlNode n, byte def = 0)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return byte.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static int IntFromXml(this XmlNode n, int def = 0)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return int.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static uint UIntFromXml(this XmlNode n, uint def = 0u)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return uint.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static float FloatFromXml(this XmlNode n, float def = 0f)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return float.Parse(n.Attributes[0].Value);
            }
            return def;
        }
        public static double DoubleFromXml(this XmlNode n, double def = 0.0)
        {
            if (n != null && n.Attributes.Count > 0)
            {
                return double.Parse(n.Attributes[0].Value);
            }
            return def;
        }
    }
}
