using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace THelper
{
    public static class Cache
    {
        public static Color MainColor;
        public static Color BaseColor;
        public static Color HoverColor;
        public static Color PressColor;
        public static Color BorderColor;
        public static Color MNColor;
        public static Color MHColor;
        public static Color TextColor;

        public static ShowByOCR showByOCR;
        public static Image scrImg;
        public static Point mainPoint;
        public static Size mainSize;

        public static bool existOCR = false;
        public static bool existGetWord = false;
        public static bool existScreenshot = false;
        public static bool exist = false;

       // public static string edition = "v1.2.0";
        public static string updateTime = "2018.04.18";
        //public static string updateUrl = "http://p75ytoawg.bkt.clouddn.com/th_edition_v120.html";
        public static string edition = "v1.0.0";
        public static string updateUrl = "http://p75ytoawg.bkt.clouddn.com/th_edition_v100.html";
        public static Color ChangeColor(Color color, float correctionFactor)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            if (red < 0) red = 0;
            if (red > 255) red = 255;
            if (green < 0) green = 0;
            if (green > 255) green = 255;
            if (blue < 0) blue = 0;
            if (blue > 255) blue = 255;
            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
