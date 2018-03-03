using System;
using System.Text;

namespace Engine.Utilities
{
    public static class Tools
    {
        public static string BytesToText(long bytes)
        {
            string[] units = new string[]
            {
                "B",
                "KB",
                "MB",
                "GB",
                "TB"
            };
            int index = 0;
            float number = (float)bytes;
            while ((int)((float)bytes / 1024f) > 0)
            {
                number = (float)bytes / 1024f;
                index++;
                bytes /= 1024L;
            }
            return string.Format("{0:0.00} {1}", number, units[index]);
        }
        public static string ToGoodString(this DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.ToShortDateString().Replace(' ', '_').Replace('-', '_').Replace('/', '_').Replace('\\', '_'));
            sb.Append('_');
            sb.Append(dt.ToLongTimeString().Replace(':', '_').Replace(' ', '_'));
            return sb.ToString();
        }
    }
}