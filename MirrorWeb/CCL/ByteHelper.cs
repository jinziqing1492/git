using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAME.CCL
{
    public class ByteHelper
    {
        public static string HumanReadableFilesize(double size)
        {
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];

        }
    }
}
